using TeaSteep;
using UnityEngine;
using Assets.Services.Sync;
using FishNet.Connection;
using FishNet.Object;
using Interlace;

namespace Assets.Services
{
    public class SyncService : NetworkBehaviour, IBranchContent,
        IDefineContent
    {
        private BranchMachine machine;
        //private MachineInstance<SyncService> machine;

        #region States

        #region SyncLobby
        private SyncLobbyState lobbyContent;
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

        internal static SyncService Instance;
        public static int SyncValue { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            machine = BranchMachine.CreateMachine(this);
            //machine = new MachineInstance<SyncService>(this);
        }

        public void OnDefine(DefineOperation operation)
        {
            lobbyContent = new SyncLobbyState();

            Lobby = operation.AddState(lobbyContent);
            Transition = operation.AddState(new SyncTransitionState());
            Encounter = operation.AddState(new Sync.SyncEncounterState());
        }

        //public void Define(DefineMachine<SyncService> machine)
        //{
        //    Lobby = machine.AddState(new SyncLobbyState());
        //    Transition = machine.AddState(new SyncTransitionState());
        //    Encounter = machine.AddState(new SyncEncounterState());
        //}

        #region Downstream
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
        #endregion

        #region Local
        internal void syncHandler(PlayerHandler2 handler)
        {
            if (machine.Current == Lobby)
                lobbyContent.StartPrep(handler);
        }
        #endregion
    }
}
