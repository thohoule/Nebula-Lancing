using System;
using System.Collections.Generic;
using System.Linq;
using FishNet;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Transporting;
using UnityEngine;
using Assets.Code.Gameplay.Network;

namespace Assets.Code.Gameplay.MainMenuSM
{
    public class ConnectionTransaction : NetworkBehaviour
    {
        private static ConnectionTransaction instance;
        private static TransactionThread current;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            current = new TransactionThread();
        }

        #region Connect
        public static void HostLobby(TransactionHandler handler)
        {
            if (current.IsLocked)
                return;

            current.Begin(handler);

            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ServerManager.OnServerConnectionState += onServerConnectionState;
        }

        //public static void HostWorld(string worldName, TransactionHandler handler)
        //{
        //    if (current.IsLocked)
        //        return;

        //    current.Begin(handler);

        //    if (WorldIO.LoadFromName(worldName, out WorldIO worldIO))
        //    {
        //        LobbyIO.LoadWorld(worldIO);

        //        InstanceFinder.ServerManager.StartConnection();
        //        InstanceFinder.ServerManager.OnServerConnectionState += onServerConnectionState;
        //    }
        //}

        private static void onServerConnectionState(ServerConnectionStateArgs obj)
        {
            if (obj.ConnectionState == LocalConnectionState.Started)
            {
                InstanceFinder.ServerManager.OnServerConnectionState -= onServerConnectionState;
                instance.tryConnect();
            }
            else if (obj.ConnectionState == LocalConnectionState.Stopped)
            {
                InstanceFinder.ServerManager.OnServerConnectionState -= onServerConnectionState;
            }
        }

        public static void Connect(TransactionHandler handler)
        {
            if (current.IsLocked)
                return;

            current.Begin(handler);
            instance.tryConnect();
        }

        private void tryConnect()
        {
            InstanceFinder.ClientManager.OnClientConnectionState += onClientStateChange;
            InstanceFinder.ClientManager.StartConnection("10.0.1.88");
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (IsClient)
            {
                InstanceFinder.ClientManager.OnClientConnectionState -= onClientStateChange;
                confirmConnection();
            }
        }

        private void onClientStateChange(ClientConnectionStateArgs obj)
        {
            switch (obj.ConnectionState)
            {
                case LocalConnectionState.Started:
                    break;
                case LocalConnectionState.Stopped:
                    InstanceFinder.ClientManager.OnClientConnectionState -= onClientStateChange;
                    current.End(TransactionResult.Error("Unable to connect to server."));
                    break;
            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void confirmConnection(NetworkConnection connection = null)
        {
            Lobby.NewConnection(connection);
            lobbyHandshake(connection);
        }

        /// <summary>
        /// Insures that the player can't make a login request until they are registered.
        /// </summary>
        /// <param name="connection"></param>
        [TargetRpc]
        private void lobbyHandshake(NetworkConnection connection)
        {
            //current.End(TransactionResult.Successful());
            applyClientRegistration(ClientID.ID);
        }

        [ServerRpc(RequireOwnership = false)]
        private void applyClientRegistration(string clientID, NetworkConnection connection = null)
        {
            var results = Lobby.RegisterConnection(connection, clientID);

            if (results.State == TransactionState.Successful)
                registrationConfirmed(connection);
            else
            {
                registrationRejected(connection, results.ErrorMessage);
                Lobby.RejectUnregisteredConnection(connection);
            }
        }

        [TargetRpc]
        private void registrationConfirmed(NetworkConnection connection)
        {
            current.End(TransactionResult.Successful());
        }

        [TargetRpc]
        private void registrationRejected(NetworkConnection connection, string message)
        {
            current.End(TransactionResult.Error(message));
        }
        #endregion
    }
}
