using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using Interlace;
using Assets.Code.Gameplay;
using TeaSteep;

namespace Assets.Services
{
    public class CameraService : NetworkBehaviour
    {
        [SerializeField]
        private FocusCameraController controller;

        private static CameraService instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        #region Downstream
        [TargetRpc]
        private void setTarget(NetworkConnection connection, ActorHandler handler)
        {
            controller.Target = handler.CameraTarget;
        }

        internal static void SetCamera(NetworkConnection connection, ActorHandler handler)
        {
            instance.setTarget(connection, handler);
        }

        //internal static void SetTarget(CameraTarget target)
        //{
        //    instance.controller.Target = target;
        //}
        #endregion
    }
}
