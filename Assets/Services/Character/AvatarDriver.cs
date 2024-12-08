using TeaSteep.Character.Controller;
using UnityEngine;

namespace Assets.Services
{
    public class AvatarDriver : MonoBehaviour
    {
        [SerializeField]
        internal ActorController controller;

        public ActorController Controller { get => controller; }

        private void Update()
        {
            UpdateDriver();
        }

        public void UpdateDriver()
        {
            Controller.ControllerUpdate();

            if (transform.position.y != 0)
                transform.position = new Vector3(transform.position.x,
                    0, transform.position.z);
        }

        #region Movement
        public void Move(Vector2 movement)
        {
            var impulse = new Vector3(movement.x, 0, movement.y);
            //transmorm value to the rotation of the controller
            impulse = Controller.TransformVector(impulse);

            Controller.Impulse(impulse);
        }

        public void Teleport(Vector3 position)
        {
            Controller.transform.position = position;
        }

        public void AimTowards(Vector3 target)
        {
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
            Controller.Local.TargetOrientation = Quaternion.Euler(new Vector3(0, degrees, 0));
        }
        #endregion
    }
}
