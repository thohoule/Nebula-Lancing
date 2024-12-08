using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep;
using Interlace.Transactions;

namespace Interlace.Debugging
{
    public class DebugStartup : MonoBehaviour
    {
        private bool isLocked;

        private void Update()
        {
            if (isLocked)
                return;

            if (Input.GetKeyUp(KeyCode.Home))
            {
                isLocked = true;
                enterPrepAsHost();
            }
            else if (Input.GetKeyUp(KeyCode.End))
            {
                isLocked = true;
                enterPrepAsClient();
            }
            //else if (Input.GetKeyUp(KeyCode.PageUp))
            //{
            //    skipPrepStartAsHost();
            //}
        }

        private void enterPrepAsHost()
        {
            ConnectionTransaction.HostLobby(onConnection);
        }

        private void enterPrepAsClient()
        {
            ConnectionTransaction.Join(onConnection);
        }

        private void onConnection(TransactionResult result)
        {
            if (result.State == TransactionState.Successful)
            {
                gameObject.SetActive(false);
                Debug.Log(string.Format("Joinded Server as {0}", DebugProfile.ProfileName));
            }
            else
            {
                Debug.LogError(string.Format("Failed to join: {0}", result.ErrorMessage));
            }

            isLocked = false;
        }
    }
}
