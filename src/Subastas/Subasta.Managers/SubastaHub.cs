using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Subasta.Managers
{
    public class SubastaHub : Hub
    {
        private static List<Participant> participants = new List<Participant>();

        public async Task SendBid(string user, decimal amount, string subastaId)
        {
            await Clients.Group(subastaId).SendAsync("ReceiveBid", user, amount);
        }

        public async Task SendMessage(string user, string message, string subastaId)
        {
            await Clients.Group(subastaId).SendAsync("ReceiveMessage", user, message);
        }

        public async Task UpdateParticipants(string user, bool joined, string subastaId)
        {
            // Si el usuario no está asociado a ninguna subasta
            if (string.IsNullOrEmpty(subastaId) && joined && !participants.Exists(p => p.NombreUsuario == user))
            {
                participants.Add(new Participant
                {
                    NombreUsuario = user,
                    CorreoUsuario = $"{user}@example.com",
                    FechaSubasta = DateTime.Now
                });

                return;
            }

            // Si el usuario se une a una subasta
            if (joined)
            {
                var participant = participants.Find(p => p.NombreUsuario == user);
                if (participant == null)
                {
                    participants.Add(new Participant
                    {
                        NombreUsuario = user,
                        CorreoUsuario = $"{user}@example.com",
                        Group = subastaId,
                        FechaSubasta = DateTime.Now
                    });
                }
                else
                {
                    // Actualizamos el grupo del usuario
                    participant.Group = subastaId;
                }

                // Agregamos al usuario al grupo en SignalR
                await Groups.AddToGroupAsync(Context.ConnectionId, subastaId);

                // Notificamos solo al grupo correspondiente
                await Clients.Group(subastaId).SendAsync("UpdateParticipants", participants.Where(p => p.Group == subastaId).ToList());
            }
            else
            {
                // Si el usuario se va de la subasta
                participants.RemoveAll(p => p.NombreUsuario == user && p.Group == subastaId);

                // Quitamos al usuario del grupo en SignalR
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, subastaId);

                // Notificamos solo al grupo correspondiente
                await Clients.Group(subastaId).SendAsync("UpdateParticipants", participants.Where(p => p.Group == subastaId).ToList());
            }
        }

        public override async Task OnConnectedAsync()
        {
            var user = Context.User.Identity.Name;
            await UpdateParticipants(user, true, null);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = Context.User.Identity.Name;
            await UpdateParticipants(user, false, null);
            await base.OnDisconnectedAsync(exception);
        }

        public class Participant
        {
            public string NombreUsuario { get; set; }
            public string CorreoUsuario { get; set; }
            public string Group { get; set; }
            public DateTime FechaSubasta { get; set; }
        }
    }
}
