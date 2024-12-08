using UnityEngine;
using TeaSteep.Character;
using FishNet.Object;

namespace Assets.Code.Characters
{
    [RequireComponent(typeof(NetActorControl))]
    public class AvatarStats : NetworkBehaviour
    {
        private NetActorControl actor;

        public const string Health = "Health";
        public const string Energy = "Energy";

        [SerializeField, Min(1)]
        private float health = 1;
        [SerializeField, Min(1)]
        private float energy = 1;

        private void Awake()
        {
            actor = GetComponent<NetActorControl>();

            actor.Status.SetOrAddStatValue(Health, health);
            actor.Status.SetOrAddStatValue(Energy, energy);
        }

        //public override void OnStartClient()
        //{
        //    actor = GetComponent<NetActor>();

        //    actor.Status.SetOrAddStatValue(Health, health);
        //    actor.Status.SetOrAddStatValue(Energy, energy);
        //}

        private void FixedUpdate() //temp
        {
            if (!isActiveAndEnabled)
                return;

            health = actor.Status.GetStatValue(Health);
        }
    }
}
