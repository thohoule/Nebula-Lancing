using System;
using UnityEngine;
using TeaSteep;
using FishNet.Object;
using Assets.Code.Gameplay.GameSM.States;
using Assets.Code.Interlace.Gameplay;

namespace Assets.Code.Gameplay.GameSM
{
    public class GameLoopControl : MonoBehaviour, IBranchContent, IDefineContent
    {
        private static GameLoopControl instance;

        [SerializeField]
        private PreparingState prepareState;
        [SerializeField]
        private TransitionState transitionState;
        [SerializeField]
        private PreparingUI preparingUI;

        private BranchMachine machine;
        //private StateChangeLock<StateInstance<GameLoopControl>> stateLock;

        #region States

        #region Preparing
        public TeaState Preparing { get; private set; }
        #endregion

        #region Transition
        public TeaState Transition { get; private set; }
        #endregion

        #region Encounter
        public TeaState Encounter { get; private set; }
        #endregion

        #region Won
        public TeaState Won { get; private set; }
        #endregion

        #region Lost
        public TeaState Lost { get; private set; }
        #endregion

        #endregion

        //public StateInstance<GameLoopControl> Preparing { get; private set; }
        //public StateInstance<GameLoopControl> Transition { get; private set; }
        //public StateInstance<GameLoopControl> Encounter { get; private set; }
        //public StateInstance<GameLoopControl> Won { get; private set; }
        //public StateInstance<GameLoopControl> Lost { get; private set; }

        public static PreparingUI PreparingUI { get => instance.preparingUI; }

        private void Awake()
        {
            instance = this;
            machine = BranchMachine.CreateMachine(this);
            //stateLock = new StateChangeLock<StateInstance<GameLoopControl>>();

            //Preparing = new StateInstance<GameLoopControl>(prepareState, this);
            //Transition = new StateInstance<GameLoopControl>(transitionState, this);

            //stateLock.CurrentState = Preparing;
        }

        public void OnDefine(DefineOperation operation)
        {
            Preparing = operation.AddState(prepareState);
            Transition = operation.AddState(transitionState);

        }

        private void Update()
        {
            machine.Update();
            //stateLock.CurrentState?.OnStateUpdate();
        }

        public void OnHostStart()
        {
            machine.SetState(Preparing);
            //stateLock.CurrentState = Preparing;
            //GameplaySyncControl.ShowPrepUI();
            Debug.Log("Loop Started");
        }

        public void OnClientStart()
        {

        }

        public void SetState(TeaState state)
        {
            machine.SetState(state);
        }
    }
}
