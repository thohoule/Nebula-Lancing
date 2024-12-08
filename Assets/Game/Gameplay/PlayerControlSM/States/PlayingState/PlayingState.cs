using UnityEngine;
using Assets.Code.Gameplay;
using TeaSteep;
using Interlace;
using Assets.Services;
using Assets.Services.Character;

namespace Assets.Game.Gameplay.PlayerControlSM
{
    public class PlayingState : ControllingState
    {
        protected override void updateAim()
        {
            AvatarService2.ClientLocal.AimTowards(Input.mousePosition);
        }

        protected override void updateMovementInputs(CurrentInput input)
        {
            //AvatarService2.ClientLocal.UpdateDriver();

            if (input.ForwardPressed)
                AvatarService2.ClientLocal.Move(Vector2.up);
            //AvatarService.Move(Vector2.up);
            else if (input.BackwardsPressed)
                AvatarService2.ClientLocal.Move(Vector2.down);

            if (input.LeftPressed)
                AvatarService2.ClientLocal.Move(Vector2.left);
            else if (input.RightPressed)
                AvatarService2.ClientLocal.Move(Vector2.right);

            if (Input.GetKeyUp(KeyCode.U))
            {
                AvatarOrator.EchoTest();
                AvatarService2.SanityCheck();
            }

            //AvatarService2.ClientLocal.UpdateDriver();
        }

        protected override void updateActionInputs(CurrentInput input)
        {
            var primaryHeld = Input.GetMouseButton(0);
            var secondaryHeld = Input.GetMouseButton(1);
            var primaryReleased = Input.GetMouseButtonUp(0);
            var secondaryReleased = Input.GetMouseButtonUp(1);

            if (primaryReleased)
                AvatarOrator.FirePrimary(1);
            //AvatarService.FirePrimary(1);
            else if (secondaryReleased)
                AvatarOrator.FireSecondary(1);
                //AvatarService.FireSecondary(1);
        }
    }
}
