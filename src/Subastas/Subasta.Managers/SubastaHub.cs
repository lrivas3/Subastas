using Microsoft.AspNetCore.SignalR;

namespace Subasta.Managers
{
    public class SubastaHub : Hub
    {
        private static List<Participant> participants = [];

        public async Task SendBid(string user, decimal amount)
        {
            await Clients.All.SendAsync("ReceiveBid", user, amount);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task UpdateParticipants(string user, bool joined)
        {
            if (joined)
            {
                if (!participants.Any(p => p.NombreUsuario == user))
                {
                    participants.Add(new Participant { NombreUsuario = user, CorreoUsuario = $"{user}@example.com", FechaSubasta = DateTime.Now });
                }
            }
            else
            {
                participants.RemoveAll(p => p.NombreUsuario == user);
            }

            await Clients.All.SendAsync("UpdateParticipants", participants);
        }

        public override async Task OnConnectedAsync()
        {
            var user = Context.User.Identity.Name;
            await UpdateParticipants(user, true);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = Context.User.Identity.Name;
            await UpdateParticipants(user, false);
            await base.OnDisconnectedAsync(exception);
        }

        public class Participant
        {
            public string NombreUsuario { get; set; }
            public string CorreoUsuario { get; set; }
            public DateTime FechaSubasta { get; set; }
        }
    }


}
