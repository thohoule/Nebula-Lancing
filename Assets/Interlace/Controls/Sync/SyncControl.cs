using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using TeaSteep;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Interlace.Sync;

namespace Interlace
{
    public class SyncControl : MonoBehaviour, IBranchContent, 
        IDefineContent
    {
        private int currentState;
        private ConcurrentDictionary<int, int> syncLock;
        private BranchMachine machine;
        //private MachineInstance<SyncControl> machine;
        //private SyncState currentState;

        private SyncHandlerService syncHandler { get => SyncHandlerService.instance; }

        private static SyncControl instance;

        #region States

        #region Lobby
        public TeaState LobbyState { get; private set; }
        #endregion

        #endregion

        //public UltraState<SyncLobbyState> LobbyState { get; private set; }

        //public static PrepSyncState PrepSyncState { get; private set; }
        public static int CurrentPhase { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            //PrepSyncState = new PrepSyncState();
            currentState = 1;
            syncLock = new ConcurrentDictionary<int, int>();
        }

        public void OnDefine(DefineOperation operation)
        {
            LobbyState = operation.AddState(new SyncLobbyState());
        }

        //public void Define(DefineMachine<SyncControl> machine)
        //{
        //    LobbyState = machine.AddState(new SyncLobbyState());
        //}

        public static void SetState(int syncStateValue)
        {
            //send a batch of syncs to all clients
            instance.currentState = syncStateValue;
            instance.syncHandler.OnStateSync(null, instance.currentState);
        }

        public static void SyncPlayer(NetworkConnection connection)
        {
            instance.sendSync(connection);
        }

        private void sendSync(NetworkConnection connection)
        {
            syncHandler.OnStateSync(connection, currentState);

            //if (syncLock.TryAdd(connection.ClientId, currentState))
            //    syncHandler.OnStateSync(connection, currentState);
            //else
            //    Debug.LogError("A sync state update was sent to a client already awaiting a sync.");
        }

        internal static void ProcessSyncConfirmation(NetworkConnection connection,
            int syncState)
        {
            instance.confirmSync(connection, syncState);
        }

        private void confirmSync(NetworkConnection connection,
            int syncState)
        {
            throw new NotImplementedException("Not planned for Lance.");

            if (syncLock.TryRemove(connection.ClientId, out int expectedValue))
            {
                if (syncState != expectedValue)
                    Debug.LogError("Sync state confirmation was different form expected.");
            }
            else
                Debug.LogError("Sync state confirmation from an unexpected client.");
        }
    }

    [Serializable]
    public class SyncMessage
    {
        public int SyncState;

        public SyncMessage()
        { }

        public SyncMessage(int syncState)
        {
            SyncState = syncState;
        }

        public SyncState GetSyncState(ISyncControl control)
        {
            switch (SyncState)
            {
                case 0:
                    Debug.Log("Error State");
                    return null;
                case 1:
                    return control.PrepSyncState;
                default:
                    Debug.Log("Error State");
                    return null;
            }
        }
    }

    public interface ISyncControl
    {
        PrepSyncState PrepSyncState { get; }
    }

    public abstract class SyncState
    {
        public abstract void OnSyncPlayer(PlayerHandler handler);
    }

    public class PrepSyncState : SyncState
    {
        public override void OnSyncPlayer(PlayerHandler handler)
        {
            
        }
    }
}
