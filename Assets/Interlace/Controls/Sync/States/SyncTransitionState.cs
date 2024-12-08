using TeaSteep;
using Assets.Services;

namespace Interlace.Sync
{
    public class SyncTransitionState : IStartContent, IEndContent
    {
        public void OnStateStart(TransitionOperation operation)
        {
            foreach (var handler in LobbyService.GetActivePlayers())
                handler.OnTransitionStart();

            LoadAvatarCoord();
        }

        //public void OnStateStart()
        //{
        //    foreach (var handler in LobbyService.GetActivePlayers())
        //        handler.OnTransitionStart();

        //    LoadAvatarCoord();
        //}

        public void LoadAvatarCoord()
        {
            
        }

        public void OnStateEnd(TransitionOperation operation)
        {
            foreach (var handler in LobbyService.GetActivePlayers())
                handler.OnTransitionEnd();
        }

        //public void OnStateEnd()
        //{
        //    foreach (var handler in LobbyService.GetActivePlayers())
        //        handler.OnTransitionEnd();
        //}
    }
}
