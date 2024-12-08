using UnityEngine;
using Assets.Code;
using TeaSteep.Character.Controller;
using Interlace;
using Assets.Entities;

namespace Assets.Game.Character
{
    public class AvatarControl : MonoBehaviour, IAvatarControl
    {
        //protected IActorEntity entitiy { get => Handler.Entity; }
        protected IActorEntity entity { get; private set; }

        public ActorController Controller { get; private set; }
        //public ActorHandler Handler { get; set; }
        //public int Health { get => entity.Health; }
        //public bool IsDead { get; set; }

        private void Awake()
        {
            //AvatarService.SetAvatar(this);
        }

        private void Update()
        {
            if (!entity.IsDead)
                Controller.ControllerUpdate();

            //if (transform.position.y != 0)
            //    transform.position = new Vector3(transform.position.x,
            //        0, transform.position.z);
        }

        public void OnDeath()
        {

        }

        #region Movement
        public void Move(Vector2 movement)
        {
            if (entity.IsDead)
                return;

            Debug.Log(string.Format("I {0} got movement for {1}", gameObject.name,
                movement));

            var impulse = new Vector3(movement.x, 0, movement.y);
            //transmorm value to the rotation of the controller
            impulse = Controller.TransformVector(impulse);

            Controller.Impulse(impulse);
        }

        public void AimTowards(Vector3 target)
        {
            if (entity.IsDead)
                return;

            var screenPos = new Vector3(target.x, target.y,
                Camera.main.transform.position.y);
            var position = Camera.main.ScreenToWorldPoint(screenPos);
            //position = new Vector3(position.x, 0, position.y);
            var direction = (position - transform.position).normalized;
            var degrees = Vector2.SignedAngle(Vector2.up, new Vector2(direction.x, direction.z));

            AimTowards(-degrees);
        }

        public void AimTowards(float degrees)
        {
            if (entity.IsDead)
                return;

            Controller.Local.TargetOrientation = Quaternion.Euler(new Vector3(0, degrees, 0));
        }
        #endregion

        //public static AvatarControl CreateControl(IActorEntity entity)
        //{
        //    var control = handler.gameObject.AddComponent<AvatarControl>();
        //    control.entity = entity;
        //    //control.Handler = handler;

        //    return control;
        //}
    }
}
