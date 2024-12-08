using UnityEngine;
using TeaSteep;

namespace Assets.Game
{
    public class GameplayControl : MonoBehaviour, IBranchContent, 
        IDefineContent
    {
        private BranchMachine machine;
        //private MachineInstance<GameplayControl> machine;

        #region States

        #region Lobby
        public static LobbyState LobbyContent { get; private set; }
        public TeaState LobbyPhase { get; private set; }
        #endregion

        #region Transition
        public TeaState TransitionPhase { get; private set; }
        #endregion

        #region Encounter
        public TeaState Encounter { get; private set; }
        #endregion

        #endregion

        //public static UltraState<LobbyState> LobbyPhase { get; private set; }
        //public static UltraState<TransitionState> TransitionPhase { get; private set; }
        //public static UltraState<EncounterState> Encounter { get; private set; }

        public void OnDefine(DefineOperation operation)
        {
            LobbyContent = new LobbyState();
            LobbyPhase = operation.AddState(LobbyContent);
            TransitionPhase = operation.AddState(new TransitionState());
            Encounter = operation.AddState(new EncounterState());
        }

        //public void Define(DefineMachine<GameplayControl> machine)
        //{
        //    LobbyPhase = machine.AddState(new LobbyState());
        //    TransitionPhase = machine.AddState(new TransitionState());
        //    Encounter = machine.AddState(new EncounterState());
        //}

        private void Awake()
        {
            machine = BranchMachine.CreateMachine(this);
            //machine = new MachineInstance<GameplayControl>(this);

            machine.SetState(LobbyPhase);
        }

        private void FixedUpdate()
        {
            machine.Update();
        }
    }
}
