using Assets.Services.Character;
using TeaSteep;

namespace Assets.Services.Sync
{
    public class SyncTransitionState : IStartContent, IEndContent
    {
        public void OnStateStart(TransitionOperation operation)
        {
            foreach (var player in LobbyService2.GetActivePlayers())
                player.PrepHandler.ClientLocal.OnTransitionStart();
        }

        public void OnStateEnd(TransitionOperation operation)
        {
            foreach (var player in LobbyService2.GetActivePlayers())
                player.PrepHandler.ClientLocal.OnTransitionEnd();
        }
    }
}
