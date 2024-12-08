using FishNet.Connection;
using FishNet.Object;

namespace Interlace.Sync
{
    public class SyncOrator : NetworkBehaviour
    {
        [ServerRpc(RequireOwnership = false)]
        internal void ConfirmSync(int stateValue, NetworkConnection connection = null)
        {
            SyncControl.ProcessSyncConfirmation(connection, stateValue);
        }
    }
}
