using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using FishNet.Connection;
using Assets.Services;

namespace Interlace
{
    public class SyncAuthority : MonoBehaviour
    {
        public static int SyncValue { get; private set; }

        private void Awake()
        {
            SyncValue = 1;
        }

        public static void SetSync(int syncValue)
        {
            SyncValue = syncValue;
            SyncService.Instance.OnStateSync(null, syncValue);
        }

        public static void SyncPlayer(NetworkConnection connection)
        {
            SyncService.Instance.OnStateSync(connection, SyncValue);
        }
    }
}
