using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep;
using FishNet.Connection;
using Assets.Code.Interlace.V3.Player;
using Assets.Code.Interlace.Gameplay;

namespace Assets.Code.Gameplay.Network
{
    public partial class Lobby
    {
        public static IReadOnlyList<RegisteredClient> Registered { get => instance.manager.Registered; }

        #region Register
        public static TransactionResult<RegisteredClient> RegisterConnection(
            NetworkConnection connection, string clientKey)
        {
            return instance.manager.RegisterConnection(connection, clientKey);
        }
        #endregion

        #region Get
        public static RegisteredClient GetClientByConnection(NetworkConnection connection)
        {
            return instance.manager.GetClientByConnection(connection);
        }

        public static RegisteredClient GetClientByID(int clientID)
        {
            return instance.manager.GetClientByID(clientID);
        }

        public static RegisteredClient GetClientByKey(string clientKey)
        {
            return instance.manager.GetClientByKey(clientKey);
        }
        #endregion

        [Serializable]
        internal class ClientManager
        {
            [SerializeField, ReadOnly]
            private List<RegisteredClient> registered;

            public List<RegisteredClient> Registered { get => registered; }
            public HashSet<string> UnassignedClientKeys { get; private set; }
            public HashSet<int> UnregisteredConnections { get; private set; }
            public HashSet<string> DisconnectedClients { get; private set; }

            public Dictionary<string, int> ClientKeyToIndex { get; private set; }
            public Dictionary<int, int> ConnectionIdToIndex { get; private set; }

            public ClientManager()
            {
                registered = new List<RegisteredClient>();
                UnassignedClientKeys = new HashSet<string>();
                UnregisteredConnections = new HashSet<int>();
                DisconnectedClients = new HashSet<string>();

                ClientKeyToIndex = new Dictionary<string, int>();
                ConnectionIdToIndex = new Dictionary<int, int>();
            }

            #region Register
            public TransactionResult<RegisteredClient> RegisterConnection(NetworkConnection connection,
                string clientKey)
            {
                if (UnregisteredConnections.Remove(connection.ClientId))
                {
                    if (DisconnectedClients.Remove(clientKey))
                    {
                        return new TransactionResult<RegisteredClient>(
                            reconnectClient(connection, clientKey));
                    }
                    else if (!PlayerClientControl.TryGetEmptySeat(out PlayerClientHandler handler))
                    {
                        return TransactionResult<RegisteredClient>.Error(
                            "Lobby is full.");
                    }
                    else if (ClientKeyToIndex.ContainsKey(clientKey))
                    {
                        return TransactionResult<RegisteredClient>.Error(
                            string.Format("Lobby: Client by key: {0}, is already connected.", clientKey));
                    }
                    else if (UnassignedClientKeys.Remove(clientKey))
                    {
                        //connect known player
                        return new TransactionResult<RegisteredClient>(
                            connectKnownPlayer(connection, clientKey, handler));
                    }
                    else
                    {
                        return new TransactionResult<RegisteredClient>(
                            connectNewPlayer(connection, clientKey, handler));
                    }
                }

                return TransactionResult<RegisteredClient>.Error(
                            string.Format("Lobby: Unexpected connection, key: {0}, clientID: {1}", clientKey, connection.ClientId));
            }

            private RegisteredClient reconnectClient(NetworkConnection connection, string clientKey)
            {
                var client = GetClientByKey(clientKey, out int index);
                ConnectionIdToIndex.Remove(client.Connection.ClientId);
                client.Connection = connection;
                ConnectionIdToIndex.Add(connection.ClientId, index);

                //client.Profile = new ClientProfile(clientKey);

                return client;
            }

            private RegisteredClient connectKnownPlayer(NetworkConnection connection, 
                string clientKey, PlayerClientHandler handler)
            {
                var client = new RegisteredClient();
                var index = Registered.Count;

                client.Connection = connection;
                client.Player = spawnPlayer(connection);
                registered.Add(client);
                ClientKeyToIndex.Add(clientKey, index);
                ConnectionIdToIndex.Add(connection.ClientId, index);
                client.Player.SeatNumber = handler.Seat;

                handler.OnAssginPlayer(null, client.Player);
                handler.OnAssignOwner(connection);
                GameplaySyncControl.SyncNewPlayer(client);

                //client.Profile = new ClientProfile(clientKey);

                return client;
            }

            private RegisteredClient connectNewPlayer(NetworkConnection connection, 
                string clientKey, PlayerClientHandler handler)
            {
                //temp
                return connectKnownPlayer(connection, clientKey, handler);
            }

            private PlayerClient spawnPlayer(NetworkConnection connection)
            {
                var player = PrefabAsset.GetPrefab<PlayerClient>();
                var instance = GameObject.Instantiate(player);

                FishNet.InstanceFinder.ServerManager.Spawn(instance.gameObject, connection);
                return instance;
            }
            #endregion

            #region Get
            public RegisteredClient GetClientByConnection(NetworkConnection connection)
            {
                return GetClientByID(connection.ClientId);
            }

            public RegisteredClient GetClientByID(int clientID)
            {
                if (ConnectionIdToIndex.TryGetValue(clientID, out int index))
                    return Registered[index];

                return null;
            }

            public RegisteredClient GetClientByKey(string clientKey)
            {
                if (ClientKeyToIndex.TryGetValue(clientKey, out int index))
                    return Registered[index];

                return null;
            }

            public RegisteredClient GetClientByKey(string clientKey, out int index)
            {
                if (ClientKeyToIndex.TryGetValue(clientKey, out index))
                    return Registered[index];

                return null;
            }
            #endregion
        }
    }
}
