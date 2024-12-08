using Assets.Entities;
using Assets.Game.Gameplay;
using Assets.Services;

namespace Interlace
{
    public partial class PrepHandler
    {
        public class ClientLocalMethods
        {
            private PrepHandler handler;

            private PlayerHandler2 playerHandler { get => handler.playerHandler; }
            private int seatNumber { get => playerHandler.ProfileHandler.SeatNumber; }
            private PrepEntity prep { get => playerHandler.ProfileHandler.Entity.Prep; }

            internal ClientLocalMethods(PrepHandler handler)
            {
                this.handler = handler;
            }

            public void RefreshMockShip()
            {
                var mockShip = PrepService.GetMockShip(seatNumber);
                mockShip.SetShip(prep);
            }

            public void RefreshShipTypeName()
            {
                var ui = PrepService.GetPrepUI(seatNumber);

                switch (prep.SelectedShip)
                {
                    case 0:
                        ui.SetSelectShipText(Small_Ship_Text);
                        break;
                    case 1:
                        ui.SetSelectShipText(Medium_Ship_Text);
                        break;
                    case 2:
                        ui.SetSelectShipText(Large_Ship_Text);
                        break;
                }
            }

            public void RefreshReadyState()
            {
                var ui = PrepService.GetPrepUI(seatNumber);
                ui.SetReadyState(prep.IsReady);
            }

            public void StartPrepState()
            {
                var mockShip = PrepService.GetMockShip(seatNumber);
                mockShip.gameObject.SetActive(true);
                mockShip.PlayEnterAnimation();

                var ui = PrepService.GetPrepUI(seatNumber);
                ui.gameObject.SetActive(true);
            }

            public void EndPrepState()
            {
                var ui = PrepService.GetPrepUI(seatNumber);
                ui.gameObject.SetActive(false);
            }

            public void OnTransitionStart()
            {
                var mockShip = PrepService.GetMockShip(seatNumber);
                mockShip.PlayTransitionAnimation();
            }

            public void OnTransitionEnd()
            {
                var mockShip = PrepService.GetMockShip(seatNumber);
                mockShip.gameObject.SetActive(false);
            }

            public void OnEncounterStart()
            {
                //Enable Playing state
                //PlayerControl.EnableControl();
            }

            public void OnEncounterEnd()
            {

            }
        }
    }
}
