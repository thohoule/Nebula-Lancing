using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep;
using Assets.Code.Gameplay.PlayingSM.ControllingState;
using Assets.Code.Gameplay.PlayingSM.UI;

namespace Assets.Code.Gameplay.PlayingSM
{
    public class PlayingControl : MonoBehaviour, IBranchContent,
        IDefineContent
    {
        private BranchMachine machine;
        //private StateChangeLock<StateInstance<PlayingControl>> stateLock;

        [SerializeField]
        private ControllingActorState controllingState;
        [SerializeField]
        private PlayerStatUI playingUI;

        #region States
        public TeaState EventWaiting { get; private set; }
        public TeaState Controlling { get; private set; }
        public TeaState DeadWaiting { get; private set; }
        public TeaState GameMenu { get; private set; }
        #endregion

        //public StateInstance<PlayingControl> EventWaiting { get; private set; }
        //public StateInstance<PlayingControl> Controlling { get; private set; }
        //public StateInstance<PlayingControl> DeadWaiting { get; private set; }
        //public StateInstance<PlayingControl> GameMenu { get; private set; }

        private static PlayingControl instance;
        public static PlayerStatUI PlayingUI { get => instance.playingUI; }

        private void Awake()
        {
            instance = this;
            PlayerKeyMap keyMap = new PlayerKeyMap();
            machine = BranchMachine.CreateMachine(this);
            //stateLock = new StateChangeLock<StateInstance<PlayingControl>>();

            //EventWaiting = new StateInstance<PlayingControl>(new WaitingState(), this);
            //Controlling = new StateInstance<PlayingControl>(controllingState, this);
            //DeadWaiting = new StateInstance<PlayingControl>(new WaitingState(), this);
        }

        public void OnDefine(DefineOperation operation)
        {
            EventWaiting = operation.AddState(new WaitingState());
            Controlling = operation.AddState(controllingState);
            DeadWaiting = operation.AddState(new WaitingState());
        }

        private void Start()
        {
            machine.SetState(EventWaiting);
            //stateLock.CurrentState = EventWaiting;
        }

        private void Update()
        {
            //if (ActorManager.Avatar != null &&
            //    ActorManager.Avatar.IsDead)
            //    stateLock.CurrentState = DeadWaiting;

            //if (stateLock.CurrentState == null ||
            //    stateLock.CurrentState == EventWaiting)
            //    return;

            //InputControl.Clear();
            //CameraControl.UpdateControl();

            //stateLock.CurrentState?.OnCallback();
            //stateLock.CurrentState?.OnStateUpdate();
        }

        public void SetState(TeaState state)
        {
            machine.SetState(state);
        }

        public static void EnableControlling()
        {
            if (instance != null)
                instance.machine.SetState(instance.Controlling);
        }
    }
}
