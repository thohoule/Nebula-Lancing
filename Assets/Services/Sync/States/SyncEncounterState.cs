using Assets.Game.Gameplay;
using Assets.Game.Gameplay.PlayerControlSM;
using Assets.Services.Player;
using TeaSteep;

namespace Assets.Services.Sync
{
    public class SyncEncounterState : IStartContent, IEndContent
    {
        public void OnStateStart(TransitionOperation operation)
        {
            //enable UI
            StatusService.GetUIObject().SetActive(true);
            foreach (var player in LobbyService2.GetActivePlayers())
            {
                player.ActorHandler.StatusUI.gameObject.SetActive(true);

                player.ActorHandler.RefreshStatus();
            }
            //Set Camera Target
            //FocusCameraController.
            PlayerControl.EnableControl();
        }

        public void OnStateEnd(TransitionOperation operation)
        {
        }
    }
}
