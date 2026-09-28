using Microsoft.AspNet.SignalR;

namespace HelpDesk
{
    public class VideoHub : Hub
    {

        public void SendOffer(string viewerId, string offerJson)
        {
            Clients.Client(viewerId).receiveOffer(Context.ConnectionId, offerJson);
        }

        public void SendAnswer(string adminId, string answerJson)
        {
            Clients.Client(adminId).receiveAnswer(Context.ConnectionId, answerJson);
        }

        public void SendCandidate(string targetId, string candidateJson)
        {
            Clients.Client(targetId).receiveCandidate(Context.ConnectionId, candidateJson);
        }

        public void RequestOffer(string viewerId)
        {
            Clients.All.requestOffer(viewerId);
        }
    }
}
