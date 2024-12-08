using FishNet.Object;

namespace Assets.Code.Gameplay.Network
{
    public delegate void TransactionHandler(TransactionResult result);
    public delegate void TransactionHandler<T>(TransactionResult<T> result);

    public abstract class NetworkTransaction : NetworkBehaviour
    {
        /// <summary>
        /// Client side lock, client can only start one Transaction type at a time.
        /// </summary>
        public bool IsLocked { get; protected set; }
    }
}
