using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TeaSteep;

namespace Assets.Code
{
    [ExecuteInEditMode]
    public class TraversalTest : MonoBehaviour
    {
        [SerializeField]
        private Vector3 targetOff;
        [SerializeField]
        private Vector3 velocity;
        [SerializeField]
        private float impulse;

        public void OnDrawGizmos()
        {
            var position = transform.position;
            var target = position + targetOff;
            var velMark = position + velocity;

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(target, 0.25f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(velMark, 0.25f);
            Gizmos.DrawLine(position, velMark);

            var direction = Plugboard(target.FlatY(), position.FlatY(),
                velocity.FlatY(), impulse);

            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(position, new Vector3(direction.x, 0, direction.y));
        }

        public Vector2 Plugboard(Vector2 target, Vector2 position, Vector2 velocity,
            float impulse)
        {
            var moveDirection = (target - position).normalized;
            var difference = Mathf.Max(0, velocity.magnitude - impulse);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(new Vector3(position.x, 5, position.y),
                new Vector3(moveDirection.x, 0, moveDirection.y));

            var velDot = Vector2.Dot(moveDirection, velocity.normalized);

            //var velocityInverse = velocity * (velDot + -difference);
            var velocityInverse = velocity * (velDot + -difference);
            var impulseVector = (moveDirection + velocityInverse.normalized).normalized;

            return impulseVector;
        }
    }
}
