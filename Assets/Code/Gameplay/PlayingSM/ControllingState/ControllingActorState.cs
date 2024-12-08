using System;
using UnityEngine;
using TeaSteep;
using Assets.Code.Gameplay.PlayingSM.ControllingState.Substates;
using Assets.Code.Characters;
using Interlace;

namespace Assets.Code.Gameplay.PlayingSM.ControllingState
{
    public class ControllingActorState : MonoBehaviour, IBranchContent
    {
        //private StateChangeLock<StateInstance<ControllingActorState>> stateLock;

        //public StateInstance<ControllingActorState> Normal { get; private set; }

        //public void OnInitialize(StateInstance instance)
        //{
        //    //stateLock = new StateChangeLock<StateInstance<ControllingActorState>>();

        //    //Normal = new StateInstance<ControllingActorState>(new NormalState(), this);
        //}

        public void OnStateStart(PlayingControl control)
        {
            //stateLock.CurrentState = Normal;
        }

        public void OnStateEnd()
        {

        }

        public void OnStateUpdate(PlayingControl control)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                //switch to Menu State
            }
            else
            {
                //stateLock.CurrentState.OnStateUpdate();
            }
        }

        public void UseAim()
        {


            //var mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            ActorManager.Avatar.AimTowards(Input.mousePosition);
        }

        public void UseMovementInputs(CurrentInput input)
        {
            if (input.ForwardPressed)
                //MainAvatar.Move(Vector2.up);
            ActorManager.Avatar.Move(Vector2.up);
            else if (input.BackwardsPressed)
                MainAvatar.Move(Vector2.down);
            //ActorManager.Avatar.Move(Vector2.down);

            if (input.LeftPressed)
                MainAvatar.Move(Vector2.left);
            //ActorManager.Avatar.Move(Vector2.left);
            else if (input.RightPressed)
                MainAvatar.Move(Vector2.right);
            //ActorManager.Avatar.Move(Vector2.right);
        }

        public void UseActionInputs(CurrentInput input)
        {
            var primaryHeld = Input.GetMouseButton(0);
            var secondaryHeld = Input.GetMouseButton(1);
            var primaryReleased = Input.GetMouseButtonUp(0);
            var secondaryReleased = Input.GetMouseButtonUp(1);

            //if (primaryHeld && secondaryHeld &&
            //    ActorManager.Avatar.DualChargeEnabled)
            //    ActorManager.Avatar.ChargeDual();
            //else if (primaryHeld)
            //    ActorManager.Avatar.ChargePrimary();
            //else if (secondaryHeld)
            //    ActorManager.Avatar.ChargeSecondary();

            //if (ActorManager.Avatar.DualChargeValue == 1 &&
            //    !primaryHeld && !secondaryHeld)
            //    ActorManager.Avatar.FireDual();
            if (primaryReleased)
            {
                //ActorManager.Avatar.FirePrimary();
                MainAvatar.FirePrimary();
                PlayingControl.PlayingUI.PrimaryCooldown.StartCooldown(0.5f);
            }
            else if (secondaryReleased)
            {
                //ActorManager.Avatar.FireSecondary();
                MainAvatar.FireSecondary();
                PlayingControl.PlayingUI.SecondaryCooldown.StartCooldown(0.5f);
            }

            if (Input.GetKeyUp(KeyCode.Y))
            {
                MainAvatar.InflictDamageTo(20);
                //ActorManager.Avatar.InflictDamage(20);
            }
        }
    }
}
