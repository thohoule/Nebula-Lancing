using System.Collections.Generic;
using Assets.Code.Gameplay;
using Assets.Code.Gameplay.Network;

namespace Assets.Code.Interlace.V3.Player
{
    public class PlayerClientControl
    {
        private static PlayerClientControl instance;

        private Queue<int> seats;

        public static int RemainingSeats { get => instance.seats.Count; }

        public PlayerClientControl()
        {
            if (instance != null)
                return;

            instance = this;

            seats = new Queue<int>();
            seats.Enqueue(1);
            seats.Enqueue(2);
            seats.Enqueue(3);
            seats.Enqueue(4);
        }

        public static bool TryGetEmptySeat(out PlayerClientHandler playerHandler)
        {
            if (!PlayerService.Player1.IsAssigned)
            {
                playerHandler = PlayerService.Player1;
                return true;
            }
            if (!PlayerService.Player2.IsAssigned)
            {
                playerHandler = PlayerService.Player2;
                return true;
            }
            if (!PlayerService.Player3.IsAssigned)
            {
                playerHandler = PlayerService.Player3;
                return true;
            }
            if (!PlayerService.Player4.IsAssigned)
            {
                playerHandler = PlayerService.Player4;
                return true;
            }

            playerHandler = null;
            return false;
        }

        //public static bool AssignEmptySeat(PlayerClientHandler playerHandler)
        //{
        //    if (PlayerService.Player1)

        //    //if (instance.seats.Count == 0)
        //    //    return false;

        //    //var seatNumber = instance.seats.Dequeue();
        //    //playerHandler.AssignSeat(seatNumber);
        //    //return true;
        //}

        public static bool ReadyCheck()
        {
            if (PlayerService.ActiveCount == 0)
                return false;

            foreach (var playerHandler in PlayerService.GetActivePlayers())
            {
                if (!playerHandler.Player.Prep.IsReady)
                    return false;
            }

            return true;
        }

        public static void SpawnAvatars()
        {
            foreach (var client in Lobby.Registered)
            {
                switch (client.Player.SeatNumber)
                {
                    case 1:
                        PlayerService.Player1.SpawnPoint.UseSpawn(client);
                        //PlayerService.useSpawn1(client);
                        break;
                    case 2:
                        PlayerService.Player2.SpawnPoint.UseSpawn(client);
                        break;
                }
            }
        }
    }
}
