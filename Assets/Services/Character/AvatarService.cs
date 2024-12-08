using UnityEngine;
using Assets.Game;
using Assets.Services;
using FishNet.Component.Transforming;

namespace Interlace
{
    public class AvatarService : MonoBehaviour
    {
        //private IAvatarControl avatar;

        [SerializeField]
        private AvatarHandler handler;
        [SerializeField]
        private AvatarOrator orator;
        [SerializeField]
        private AvatarCoord coord;

        private static AvatarService instance;

        public static AvatarHandler Handler { get => instance.handler; }
        public static AvatarOrator Orator { get => instance.orator; }

        public static AvatarCoord Coord { get => instance.coord; }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        public static void FirePrimary(int charge)
        {
            AvatarOrator.FirePrimary(charge);
        }

        public static void FireSecondary(int charge)
        {
            AvatarOrator.FireSecondary(charge);
        }

        public static IAvatarControl CreateControl()
        {
            return null;
        }

        public static void Move(Vector2 movement)
        {
            //instance.avatar.Move(movement);
        }

        public static void AimTowards(Vector3 point)
        {
            //instance.avatar.AimTowards(point);
        }

        #region Spawn
        public static void SpawnAll()
        {
            foreach (var player in LobbyService2.GetActivePlayers())
            {
                player.transform.position = PrepService.GetSpawnPosition(
                player.ProfileHandler.Entity.SeatNumber);

                player.GetComponent<NetworkTransform>().ForceSend();
            }
        }

        private void spawnPuppet()
        {

        }
        #endregion

        //public static void EnableAll()
        //{
        //    foreach(var seat in LobbyService.GetActivePlayers())
        //    {
        //        seat.AvatarHandler.
        //    }
        //}

        public static void EnableControls() //shouldn't be in avatar
        {
            Handler.gameObject.SetActive(true);
            Coord.gameObject.SetActive(true);
            PlayerControlBase.EnableControl();
        }

        //public static void SetHealth(int value)
        //{
        //    Orator.SetHealth(value);
        //}

        //public static void SetShield(int value)
        //{

        //}
    }
}
