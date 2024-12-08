using UnityEngine;
using Interlace;
using TeaSteep;
using Interlace.Authorities;

namespace Assets.Game
{
    public class TransitionState : IStartContent, IUpdateContent<GameplayControl>
    {
        public void OnStateStart(TransitionOperation operation)
        {
            //Spawn Players, but keep controls disabled
            AvatarService.SpawnAll();

            //Spawn Enemies, but keep agents disabled
            AgentAuthority.SpawnAll();
            //AgentService.SpawnAll();

            //Start sync handshake
            SyncControl.SetState(2);
            SyncAuthority.SetSync(2);
        }

        //public void OnStateStart()
        //{
        //    //Spawn Players, but keep controls disabled
        //    AvatarService.SpawnAll();

        //    //Spawn Enemies, but keep agents disabled
        //    AgentAuthority.SpawnAll();
        //    //AgentService.SpawnAll();

        //    //Start sync handshake
        //    SyncControl.SetState(2);
        //    SyncAuthority.SetSync(2);
        //}

        public void OnStateUpdate(BranchOperation operation, GameplayControl branch)
        {
            operation.StepTo(branch.Encounter);
        }

        //public void OnStateUpdate(ControlMachine<GameplayControl> controlMachine)
        //{
        //    //await sync checks //Skipped for now

        //    //transition to playing phase
        //    controlMachine.SetState(GameplayControl.Encounter);
        //}
    }
}
