using System;
using UnityEngine;
using FishNet.Object;

namespace Assets.Code.Gameplay
{
    public class PlayingInterlaceControl : NetworkBehaviour
    {
        private PlayingInterlace clientInterlace;

        private static PlayingInterlaceControl instance;

        //private void Awake()
        //{
        //    instance = this;
        //}

        public void Initialize(PlayingInterlace playingInterlace)
        {
            if (instance == null)
            {
                instance = this;
                clientInterlace = playingInterlace;
            }
        }

        public static void StartControlState()
        {
            instance?.clientInterlace.StartControlState();
        }
    }
}
