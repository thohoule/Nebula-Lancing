using Interlace;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using TeaSteep;
using Unity.VisualScripting;
using TeaSteep.Character.Controller;
using Assets.Services.Player;

namespace Assets.Services.Character
{
    public class AvatarService2 : NetworkBehaviour
    {
        private AvatarDriver driverInstance;

        [SerializeField]
        private AvatarDriver driverPrefab;
        [SerializeField, ReadOnly]
        private ActorHandler avatarHandler;

        private static AvatarService2 instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        private void updateDriver()
        {
            if (!avatarHandler.Entity.IsDead)
                driverInstance.UpdateDriver();
        }

        public static void SanityCheck()
        {
            Debug.Log(string.Format("Sanity Pos: {0}",
                instance.avatarHandler.transform.position));
        }

        #region Downstream
        [TargetRpc]
        private void onSetAvatar(NetworkConnection connection, ActorHandler handler)
        {
            if (driverInstance == null)
            {
                avatarHandler = handler;
                addDriver(handler);
                handler.StatusUI = StatusService.GetPrimary();

                //avatarHandler.gameObject.AddComponent<AvatarDriver>(driverPrefab);

                //driverInstance = Instantiate(driverPrefab);

                //driverInstance.gameObject.SetParentAndLocals(avatarHandler.transform.parent.gameObject);

                //driverInstance.transform.position = avatarHandler.transform.position;

                //avatarHandler.gameObject.SetParentAndLocals(driverInstance.gameObject, Vector3.zero);

                //avatarHandler.transform.localPosition = Vector3.zero;
            }
        }

        private void addDriver(ActorHandler handler)
        {
            driverInstance = avatarHandler.AddComponent<AvatarDriver>();
            driverInstance.controller = avatarHandler.GetComponent<ActorController>();

            //var controller = avatarHandler.AddComponent<ActorController>();

            //driverInstance.controller = controller;
        }
        #endregion

        #region Client
        public static class ClientLocal
        {
            public static void UpdateDriver()
            {
                instance.updateDriver();
            }

            #region Movement
            public static void Move(Vector2 movement)
            {
                instance.driverInstance.Move(movement);
            }

            public static void Teleport(Vector3 position)
            {
                instance.driverInstance.Teleport(position);
            }

            public static void AimTowards(Vector3 point)
            {
                instance.driverInstance.AimTowards(point);
            }
            #endregion
        }
        #endregion

        #region Server
        public static class ServerLocal
        {
            internal static void SetAvatar(NetworkConnection connection, ActorHandler handler)
            {
                instance.onSetAvatar(connection, handler);
                //handler.transform.localPosition = Vector3.zero;
            }
        }
        #endregion
    }
}
