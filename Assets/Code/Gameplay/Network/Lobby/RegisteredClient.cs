using System;
using System.Collections.Generic;
using FishNet.Connection;

namespace Assets.Code.Gameplay.Network
{
    public class RegisteredClient
    {
        public NetworkConnection Connection { get; internal set; }
        public PlayerClient Player { get; internal set; }
        //public ClientProfile Profile { get; internal set; }
        //public bool IsReady { get; set; }

        //public OwnershipMessage CreateOwnershipMessage()
        //{
        //    if (Profile == null)
        //        return default;

        //    return new OwnershipMessage(Profile.OwnershipData,
        //        Profile.AllProfiles);
        //}
    }
}
