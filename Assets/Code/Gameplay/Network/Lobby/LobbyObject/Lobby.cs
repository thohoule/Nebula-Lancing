using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;

namespace Assets.Code.Gameplay.Network
{
    public partial class Lobby
    {
        [SerializeField]
        private ClientManager manager = new ClientManager();

        private static Lobby _instance;
        private static Lobby instance { get => _instance ?? (_instance = new Lobby()); }

        public static void NewConnection(NetworkConnection connection)
        {
            instance.manager.UnregisteredConnections.Add(connection.ClientId);
        }

        public static void RejectUnregisteredConnection(NetworkConnection connection)
        {
            connection.Disconnect(true);
            instance.manager.UnregisteredConnections.Remove(connection.ClientId);
        }
    }
}
