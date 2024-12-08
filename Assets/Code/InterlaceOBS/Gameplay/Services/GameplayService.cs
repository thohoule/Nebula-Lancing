using System;
using UnityEngine;
using Assets.Code.Gameplay.PlayingSM.UI;

namespace Assets.Code.Interlace.Gameplay
{
    public class GameplayService : MonoBehaviour
    {
        [SerializeField]
        private GameplaySyncHandler handler;
        [SerializeField]
        private PlayerStatUI playingUI;

        internal static GameplaySyncHandler Handler { get { return instance.handler; } }
        private static GameplayService instance;

        private void Awake()
        {
            instance = this;
        }

        public static void ShowPlayingUI()
        {
            //instance.playingUI.
        }
    }
}
