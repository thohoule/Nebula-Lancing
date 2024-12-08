using Assets.Game;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using Assets.Code.Gameplay;
using TeaSteep;
using Assets.Code;

namespace Interlace
{
    [RequireComponent(typeof(IAvatarControl))]
    public class AvatarHandler : ActorHandler
    {
        private IAvatarControl avatarControl;

        [SerializeField]
        private FocusCameraController cameraController;

        //public ParticleSystem DamagedParticles { get; set; }
        //public MultiParticles DeathEffect { get; set; }

        //private void Awake()
        //{
        //    //avatarControl = GetComponent<IAvatarControl>();
        //}

        [TargetRpc]
        internal void OnAssign(NetworkConnection connection, GameObject gameObject)
        {
            transform.position = gameObject.transform.position;
            gameObject.transform.SetParent(transform, true);

            cameraController.Target = GetComponent<CameraTarget>();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();

            //if (IsOwner)
            //{
            //    avatarControl = AvatarService.CreateControl();
            //}
        }

        protected override void onIsDeadChange(bool value)
        {
            if (value)
                avatarControl.OnDeath();
        }

        protected override void onHealthChange(int oldValue, int currentValue)
        {
            //update health UI
        }

        //public static void TestBuild(NetworkConnection owner)
        //{
        //    GameObject gameObject = new GameObject("Avatar Handler");
        //    var handler = gameObject.AddComponent<AvatarHandler>();
        //    var orator = gameObject.AddComponent<AvatarOrator>();
        //    gameObject.AddComponent<ActorEntity>();

        //    handler.Spawn(gameObject, owner);
        //}
    }
}
