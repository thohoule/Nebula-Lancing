//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//namespace Assets.Editor
//{
//    [ExecuteInEditMode]
//    public class TraversalTest : MonoBehaviour
//    {
//        [SerializeField]
//        private Vector2 target;
//        [SerializeField]
//        private Vector2 velocity;
//        [SerializeField]
//        private float impulse;

//        public void OnGUI()
//        {
//            var position = transform.position;
//            var targetPos = new Vector2(position.x + target.x, position.z + target.y);

//            Gizmos.DrawSphere(new Vector3(targetPos.x, position.y, targetPos.y), 1);
//        }

//        public Vector2 Plugboard(Vector2 target, Vector2 position, Vector2 velocity,
//            float impulse)
//        {
//            var moveDirection = (target - position).normalized;
//            var difference = Mathf.Max(0, velocity.magnitude - impulse);

//            var velDot = Vector2.Dot(moveDirection, velocity);

//            var velocityInverse = velocity * (velDot + -difference);
//            var impulseVector = moveDirection + velocityInverse;

//            return impulseVector;
//        }
//    }
//}
