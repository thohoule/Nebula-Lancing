using Assets.Game;
using FishNet.Connection;
using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Interlace
{
    public class PlayerService : MonoBehaviour
    {
        [SerializeField]
        private OwnerOrator orator;

        private static PlayerService instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        public static OwnerOrator GetOrator()
        {
            return instance.orator;
        }

        public static void PlayerResyncRequest()
        {

        }
    }
}
