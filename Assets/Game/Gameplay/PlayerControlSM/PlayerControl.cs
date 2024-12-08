using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Game.Gameplay.PlayerControlSM;
using TeaSteep;

namespace Assets.Game.Gameplay
{
    public class PlayerControl : PlayerControlBase, IBranchContent, 
        IDefineContent
    {
        private BranchMachine machine;
        //private MachineInstance<PlayerControl> machine;

        //public UltraState<WaitingState> EventWaiting { get; private set; }
        //public UltraState<PlayingState> Playing { get; private set; }

        #region States

        #region Waiting
        public TeaState Waiting { get; private set; }
        #endregion

        #region Playing
        public TeaState Playing { get; private set; }
        #endregion

        #endregion

        private void Awake()
        {
            instance = this;
            Assets.Code.PlayerKeyMap keyMap = new Code.PlayerKeyMap();

            machine = BranchMachine.CreateMachine(this);
            machine.SetState(Waiting);

            //machine = new MachineInstance<PlayerControl>(this);
            //machine.SetState(EventWaiting);
        }

        private void Start()
        {
            //machine.SetState(EventWaiting);
        }

        public void OnDefine(DefineOperation operation)
        {
            Waiting = operation.AddState(new WaitingState());
            Playing = operation.AddState(new PlayingState());
        }

        //public void Define(DefineMachine<PlayerControl> machine)
        //{
        //    EventWaiting = machine.AddState(new WaitingState());
        //    Playing = machine.AddState(new PlayingState());
        //}

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                /*Check current state and switch to other expected*/
            }

            TeaSteep.InputControl.Clear();
            TeaSteep.CameraControl.UpdateControl();

            machine.Update();
        }

        protected override void onEnableControl()
        {
            if (machine.Current == Waiting)
                machine.SetState(Playing);
        }

        //public static void EnablePlayerControls()
        //{
        //    if (instance.machine.Current == instance.EventWaiting)
        //        instance.machine.SetState(instance.Playing);
        //}
    }
}
