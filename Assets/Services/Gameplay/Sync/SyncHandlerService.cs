using System;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using Assets.Game;
using Interlace.Sync;
using TeaSteep;

namespace Interlace
{
    public class SyncHandlerService : NetworkBehaviour, IBranchContent,
        IDefineContent
    {
        private BranchMachine machine;
        //private MachineInstance<SyncHandlerService> machine;

        #region States

        #region SynchLobby
        public TeaState Lobby { get; private set; }
        #endregion

        #region SyncTransition
        public TeaState Transition { get; private set; }
        #endregion

        #region SyncEncounter
        public TeaState Encounter { get; private set; }
        #endregion

        #endregion

        //public UltraState<SyncLobbyState> Lobby { get; private set; }
        //public UltraState<SyncTransitionState> Transition { get; private set; }
        //public UltraState<SyncEncounterState> Encounter { get; private set; }

        internal static SyncHandlerService instance;
        public static int SyncValue { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            machine = BranchMachine.CreateMachine(this);
            //machine = new MachineInstance<SyncHandlerService>(this);
        }

        public void OnDefine(DefineOperation operation)
        {
            Lobby = operation.AddState(new SyncLobbyState());
            Transition = operation.AddState(new SyncTransitionState());
            Encounter = operation.AddState(new SyncEncounterState());
        }

        //public void Define(DefineMachine<SyncHandlerService> machine)
        //{
        //    Lobby = machine.AddState(new SyncLobbyState());
        //    Transition = machine.AddState(new SyncTransitionState());
        //    Encounter = machine.AddState(new SyncEncounterState());
        //}

        [TargetRpc]
        [ObserversRpc]
        internal void OnStateSync(NetworkConnection connection, int state)
        {
            SyncValue = state;

            switch (state)
            {
                case 1:
                    machine.SetState(Lobby);
                    break;
                case 2:
                    machine.SetState(Transition);
                    break;
                case 3:
                    machine.SetState(Encounter);
                    break;
            }
        }

        [TargetRpc]
        internal void OnPrepSync(NetworkConnection connection)
        {

        }
    }
}
