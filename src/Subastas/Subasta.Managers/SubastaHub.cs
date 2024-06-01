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
            var connectionId = Context.ConnectionId;

            if (joined)
            {
                var participant = participants.Find(p => p.NombreUsuario == user && p.Group == subastaId);
                if (participant == null)
                {
                    participants.Add(new Participant
                    {
                        NombreUsuario = user,
                        CorreoUsuario = $"{user}@example.com",
                        Group = subastaId,
                        FechaSubasta = DateTime.Now,
                        ConnectionId = connectionId
                    });
                }

                // Agregar el usuario al grupo de SignalR
                await Groups.AddToGroupAsync(connectionId, subastaId);

                // Notificar solo al grupo correspondiente
                await Clients.Group(subastaId).SendAsync("UpdateParticipants", participants.Where(p => p.Group == subastaId).ToList());
            }
            else
            {
                // Remover el usuario del grupo y de la lista de participantes para esa subasta
                participants.RemoveAll(p => p.NombreUsuario == user && p.Group == subastaId && p.ConnectionId == connectionId);

                // Quitar el usuario del grupo de SignalR
                await Groups.RemoveFromGroupAsync(connectionId, subastaId);

                // Notificar solo al grupo correspondiente
                await Clients.Group(subastaId).SendAsync("UpdateParticipants", participants.Where(p => p.Group == subastaId).ToList());
            }
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            var user = Context.User.Identity.Name;

            // Obtener todos los grupos en los que estaba el usuario
            var userGroups = participants.Where(p => p.ConnectionId == connectionId).Select(p => p.Group).ToList();

            foreach (var group in userGroups)
            {
                await UpdateParticipants(user, false, group);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public class Participant
        {
            public string NombreUsuario { get; set; }
            public string CorreoUsuario { get; set; }
            public string Group { get; set; }
            public DateTime FechaSubasta { get; set; }
            public string ConnectionId { get; set; }
        }
    }
}
