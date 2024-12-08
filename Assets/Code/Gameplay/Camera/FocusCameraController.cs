using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep;

namespace Assets.Code.Gameplay
{
    public class FocusCameraController : MonoBehaviour, ICameraController
    {
        [SerializeField]
        private CameraTarget target;
        [SerializeField]
        private TrackingInfo trackingInfo;
        [SerializeField]
        private float altitude = 5;

        public CameraTarget Target { get => target; set => target = value; }
        public TrackingInfo Tracking => trackingInfo;
        public Vector3 HitDirection { get; private set; }
        public Vector3 LookDirection { get; private set; }

        //public static Line LookLine { get; private set; }

        private void Awake()
        {
            CameraControl.CurrentController = this;
        }

        public void UpdateCamera(CameraControl control)
        {
            control.GoalTransform = new PointTransform(target.transform.position + new Vector3(0, altitude, 0),
                Quaternion.LookRotation(Vector3.down, Vector3.forward));

            control.AimTransform = new PointTransform(target.transform.position,
                Quaternion.identity);

            updateLookDirections(control);

            //var mouseRay = Camera.current.GetMouseOnScreenRay();
            //LookLine = new Line(mouseRay, AltitudeDifference);
        }

        public void UpdateInputs(CameraControl control)
        {
        }

        private void updateLookDirections(CameraControl control)
        {
            var cameraForward = control.transform.forward;

            var point = control.AimTransform.Position + (cameraForward * Tracking.LookPointDistance);
            var forwardPoint = transform.position + cameraForward;

            HitDirection = (point - forwardPoint).normalized;
            LookDirection = cameraForward;
        }
    }
}
