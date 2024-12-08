using TeaSteep;

namespace Interlace
{
    public class SyncEncounterState : IStartContent, IEndContent
    {
        public void OnStateStart(TransitionOperation operation)
        {
        }

        public void OnStateEnd(TransitionOperation operation)
        {
        }


        //public void OnStateStart()
        //{
        //    //AvatarService.EnableControls();
        //    //Enable agents //should be enabled server side
        //    //AgentService.EnableAgents();

        //    //Set Camera Target
        //}

        //public void OnStateEnd()
        //{
        //}
    }
}
