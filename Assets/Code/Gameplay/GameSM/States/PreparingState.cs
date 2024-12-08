using UnityEngine;
using TeaSteep;
using Assets.Code.Gameplay.Network;
using Assets.Code.Interlace.V3.Player;

namespace Assets.Code.Gameplay.GameSM.States
{
    public class PreparingState : MonoBehaviour, ITeaContent
    {
        private const float Countdown_Time = 1;

        private float readyTime;
        private bool commenceCountdown;
        private bool isTransitioning;
        private bool transitioningSuccessful;

        //[SerializeField]
        //private PreparingUI ui;

        public void OnStateStart(GameLoopControl control)
        {
            readyTime = 0;
            commenceCountdown = false;
            //GameLoopControl.PreparingUI.Show();
            //ui.Show();

            //Show fake player agent and run passive animation
        }

        public void OnStateEnd()
        {
            //GameLoopControl.PreparingUI.Hide();
            //GameLoopControl.PreparingUI.StartLaneGameplay();            
        }

        public void OnStateUpdate(GameLoopControl control)
        {
            if (commenceCountdown)
            {
                readyTime += Time.deltaTime;
                var remaining = Mathf.Max(Countdown_Time - readyTime, 0);
                GameLoopControl.PreparingUI.SetReadyTime(remaining);

                if (remaining == 0)
                {
                    control.SetState(control.Transition);
                }
            }
            else if (readyCheck())
            {
                commenceCountdown = true;
                GameLoopControl.PreparingUI.ShowCoundown();
                readyTime = 0;
            }
        }

        private bool readyCheck()
        {
            if (Lobby.Registered.Count == 0)
                return false;

            //foreach (var client in Lobby.Registered)
            //{
            //    var player = client.Player;

            //    if (!client.Player.IsAssignedLane)
            //    {
            //        if (!GameLoopControl.PreparingUI.Lane1.IsAssinged)
            //            player.SetupPrep(GameLoopControl.PreparingUI.Lane1);
            //        else if (!GameLoopControl.PreparingUI.Lane2.IsAssinged)
            //            player.SetupPrep(GameLoopControl.PreparingUI.Lane2);
            //        else if (!GameLoopControl.PreparingUI.Lane3.IsAssinged)
            //            player.SetupPrep(GameLoopControl.PreparingUI.Lane3);
            //        else if (!GameLoopControl.PreparingUI.Lane4.IsAssinged)
            //            player.SetupPrep(GameLoopControl.PreparingUI.Lane4);
            //    }

            //    if (!player.IsAssignedLane || !player.Prep.IsReady)
            //        return false;
            //}

            return PlayerClientControl.ReadyCheck();
        }

        //private void triggerTransition()
        //{
        //    if (!isTransitioning)
        //    {
        //        isTransitioning = true;
        //        PreparingTransaction.StartTransition(transitionResult);
        //    }
        //}

        //private void transitionResult(TransactionResult result)
        //{
        //    if (result.State == TransactionState.Successful)
        //    {
        //        transitioningSuccessful = true;
        //    }

        //    isTransitioning = false;
        //}
    }
}
