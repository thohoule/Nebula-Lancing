using TeaSteep;
using Assets.Code.Gameplay;

namespace Assets.Game.Gameplay.PlayerControlSM
{
    public abstract class ControllingState : ITeaContent, IUpdateContent
    {
        public void OnStateUpdate(LeafOperation operation)
        {
            var input = CurrentInput.Get();

            updateAim();
            updateMovementInputs(input);
            updateActionInputs(input);
        }

        //public virtual void OnStateUpdate(ControlMachine controlMachine)
        //{
        //    var input = CurrentInput.Get();

        //    updateAim();
        //    updateMovementInputs(input);
        //    updateActionInputs(input);
        //}

        protected virtual void updateAim()
        { }

        protected virtual void updateMovementInputs(CurrentInput input)
        { }

        protected virtual void updateActionInputs(CurrentInput input)
        { }
    }
}
