using UnityEngine;
using Interlace;
using TeaSteep;
using Assets.Services;

namespace Assets.Game
{
    public class LobbyState : IStartContent, IUpdateContent<GameplayControl>
    {
        private const float Countdown_Client_Time = 1;
        private const float Countdown_Time = 1.2f;

        public float ReadyTimer { get; private set; }
        public bool IsEnding { get; private set; }
        public bool HasEnded { get; private set; }

        public void OnStateStart(TransitionOperation operation)
        { }

        //public void OnStateStart()
        //{
        //    //SyncAuthority.SetSync(1);
        //}

        public void OnReadyStateChange()
        {
            if (checkReadyState(LobbyService.Player1) &&
                checkReadyState(LobbyService.Player2) &&
                checkReadyState(LobbyService.Player3) &&
                checkReadyState(LobbyService.Player4))
            {
                IsEnding = true;
                ReadyTimer = 0;
                PrepServiceHandler.StartCountdown(Countdown_Client_Time);
            }
            else
            {
                if (IsEnding)
                    PrepServiceHandler.HideCountdownTimer();

                IsEnding = false;
            }
        }

        public void OnStateUpdate(BranchOperation operation, GameplayControl branch)
        {
            if (!IsEnding || HasEnded)
                return;

            if (ReadyTimer >= Countdown_Time)
            {
                HasEnded = true;
                PrepServiceHandler.HideCountdownTimer();
                operation.StepTo(branch.TransitionPhase);
            }
            else
                ReadyTimer += Time.deltaTime;
        }

        //public void OnStateUpdate(ControlMachine<GameplayControl> controlMachine)
        //{
        //    if (!IsEnding || HasEnded)
        //        return;

        //    if (ReadyTimer >= Countdown_Time)
        //    {
        //        HasEnded = true;
        //        PrepServiceHandler.HideCountdownTimer();
        //        controlMachine.SetState(GameplayControl.TransitionPhase);
        //    }
        //    else
        //        ReadyTimer += Time.deltaTime;

        //    //update UI
        //    //PrepServiceHandler.SetReadyTimerUI(ReadyTimer); //instead just send start
        //}

        private bool checkReadyState(PlayerHandler handler)
        {
            return !handler.IsAssignedPlayer ||
                handler.Entity.Prep.IsReady;
        }
    }
}
