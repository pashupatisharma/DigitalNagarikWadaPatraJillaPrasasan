using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpDesk
{

    using Microsoft.AspNet.SignalR;
    using System.Collections.Concurrent;

    public class LiveHub : Hub
    {
        // Track broadcaster per room
        private static ConcurrentDictionary<string, string> RoomBroadcasters = new ConcurrentDictionary<string, string>();

        // Track viewers per room
        private static ConcurrentDictionary<string, ConcurrentBag<string>> RoomViewers = new ConcurrentDictionary<string, ConcurrentBag<string>>();

        public void JoinAsBroadcaster(string roomId)
        {
            RoomBroadcasters[roomId] = Context.ConnectionId;
            if (!RoomViewers.ContainsKey(roomId))
                RoomViewers[roomId] = new ConcurrentBag<string>();

            Clients.Caller.SystemMessage("You joined as broadcaster in room: " + roomId);
        }

        public void JoinAsViewer(string roomId)
        {
            var viewerId = Context.ConnectionId;
            if (!RoomViewers.ContainsKey(roomId))
                RoomViewers[roomId] = new ConcurrentBag<string>();

            RoomViewers[roomId].Add(viewerId);

            if (RoomBroadcasters.TryGetValue(roomId, out string broadcasterId))
            {
                Clients.Client(broadcasterId).ViewerJoined(viewerId);
            }

            Clients.Caller.SystemMessage("You joined as viewer in room: " + roomId);
        }

        public void SendOffer(string toId, object offer) => Clients.Client(toId).ReceiveOffer(Context.ConnectionId, offer);
        public void SendAnswer(string toId, object answer) => Clients.Client(toId).ReceiveAnswer(Context.ConnectionId, answer);
        public void SendIceCandidate(string toId, object candidate) => Clients.Client(toId).ReceiveIceCandidate(Context.ConnectionId, candidate);

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var connectionId = Context.ConnectionId;

            // Check if broadcaster disconnected
            foreach (var kv in RoomBroadcasters)
            {
                if (kv.Value == connectionId)
                {
                    var roomId = kv.Key;
                    RoomBroadcasters.TryRemove(roomId, out _);
                    // Notify all viewers
                    if (RoomViewers.TryGetValue(roomId, out var viewers))
                        foreach (var viewer in viewers) Clients.Client(viewer).BroadcasterLeft();
                }
            }

            // Remove viewer from room
            foreach (var kv in RoomViewers)
            {
                var viewers = kv.Value;
                if (viewers.Contains(connectionId))
                {
                    viewers.TryTake(out _);
                    if (RoomBroadcasters.TryGetValue(kv.Key, out var broadcasterId))
                        Clients.Client(broadcasterId).ViewerLeft(connectionId);
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }


}