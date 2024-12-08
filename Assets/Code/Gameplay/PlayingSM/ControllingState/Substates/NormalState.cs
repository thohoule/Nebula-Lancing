using TeaSteep;

namespace Assets.Code.Gameplay.PlayingSM.ControllingState.Substates
{
    public class NormalState : IUpdateContent<ControllingActorState>
    {
        public void OnStateStart(ControllingActorState control)
        {
        }

        public void OnStateEnd()
        {
        }

        public void OnStateUpdate(BranchOperation operation, ControllingActorState branch)
        {
            var input = CurrentInput.Get();

            branch.UseAim();
            branch.UseMovementInputs(input);
            branch.UseActionInputs(input);
        }
    }
}
