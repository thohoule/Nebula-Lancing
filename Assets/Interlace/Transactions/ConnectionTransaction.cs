using System;
using System.Collections.Generic;
using System.Linq;
using FishNet;
using FishNet.Object;
using FishNet.Connection;
using FishNet.Transporting;
using UnityEngine;
using Interlace.Debugging;
using TeaSteep;
using Assets.Game;
using Interlace.Authorities;

namespace Interlace.Transactions
{
    public class ConnectionTransaction : NetworkBehaviour
    {
        private const string Test_Address = "10.0.1.88";
        //private const string Test_Address = "192.168.0.120";

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

        #region Host
        public static void HostLobby(TransactionHandler handler)
        {
            if (current.IsLocked)
                return;

            current.Begin(handler);

            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ServerManager.OnServerConnectionState +=
                instance.onServerConnectionState;
        }

        private void onServerConnectionState(ServerConnectionStateArgs obj)
        {
            if (obj.ConnectionState == LocalConnectionState.Started)
            {
                InstanceFinder.ServerManager.OnServerConnectionState -= onServerConnectionState;
                createServerControls();
                tryJoin();
            }
            else if (obj.ConnectionState == LocalConnectionState.Stopped)
            {
                InstanceFinder.ServerManager.OnServerConnectionState -= onServerConnectionState;
            }
        }

        private void createServerControls()
        {
            gameObject.AddComponent<LobbyControl>();
            gameObject.AddComponent<SyncControl>();
            gameObject.AddComponent<GameplayControl>();
            gameObject.AddComponent<AgentAuthority>();

            gameObject.AddComponent<SyncAuthority>();
            gameObject.AddComponent<LobbyAuthority>();
        }
        #endregion

        #region Join
        public static void Join(TransactionHandler handler)
        {
            if (current.IsLocked)
                return;

            current.Begin(handler);
            instance.tryJoin();
        }

        private void tryJoin()
        {
            InstanceFinder.ClientManager.OnClientConnectionState += onClientStateChange;
            InstanceFinder.ClientManager.StartConnection(Test_Address);
        }

        #region Faulure Event
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
        #endregion

        #region Client Start
        public override void OnStartClient()
        {
            base.OnStartClient();

            if (IsClient)
            {
                InstanceFinder.ClientManager.OnClientConnectionState -= onClientStateChange;
                confirmStart(DebugProfile.ProfileName);
            }
        }
        #endregion

        #region Assign
        [ServerRpc(RequireOwnership = false)]
        private void confirmStart(string profileName, NetworkConnection connection = null)
        {
            //var result = LobbyControl.CanBeSeated(connection, profileName,
            //    out PlayerHandler handler);

            var result = LobbyAuthority.CanbeSeated(connection, profileName,
                out PlayerHandler2 handler);

            //handler is already assigned by LobbyControl

            if (result.State == TransactionState.Successful)
                joinConfirmed(connection);
            else
                joinRejected(connection, result.ErrorMessage);
        }

        [TargetRpc]
        private void joinConfirmed(NetworkConnection connection)
        {
            current.End(TransactionResult.Successful());
        }

        [TargetRpc]
        private void joinRejected(NetworkConnection connection, string message)
        {
            current.End(TransactionResult.Error(message));
        }
        #endregion
        #endregion
    }
}
