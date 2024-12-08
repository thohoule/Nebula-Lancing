using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

namespace Assets.Entities
{
    /// <summary>
    /// Virtural Player Object; is a runtime only object and is used to sync player 
    /// variables between clients.
    /// </summary>
    public class PlayerEntity
    {
        public string ProfileName { get; internal set; }
        public NetworkConnection Connection { get; internal set; }
        public PrepEntity Prep { get; internal set; }
        public int SeatNumber { get; internal set; }
    }
}
