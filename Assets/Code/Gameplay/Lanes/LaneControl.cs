using UnityEngine;
using Assets.Code.Gameplay.Network;
using Assets.Code.Gameplay.GameSM;
using Assets.Code.Gameplay.PlayingSM;

namespace Assets.Code.Gameplay
{
    public class LaneControl : MonoBehaviour
    {
        [SerializeField]
        private int laneNumber;
        [SerializeField]
        private PlayerPrepLane prepLane;
        [SerializeField]
        private PlayerStatusLane statLane;

        public int Number { get => laneNumber; }

        private void Awake()
        {
            //prepLane.control = this;
            //playLane.control = this;
        }

        public void AssignSeat(PlayerSeat seat, bool grantControl)
        {

        }
    }
}
