using UnityEngine;
using Assets.Code.Gameplay.PlayingSM;

namespace Assets.Code.Characters
{
    public class PlayingUIService : MonoBehaviour
    {
        private static PlayingUIService instance;

        [SerializeField]
        private MainStatusHandler mainUIHandler;

        public static MainStatusHandler MainStatusHandler { get => instance.mainUIHandler; }

        private void Awake()
        {
            instance = this;
        }
    }
}
