using UnityEngine;
using TeaSteep;

namespace Assets.Code.Gameplay.Network
{
    /// <summary>
    /// Temporary unique Client ID for testing.
    /// </summary>
    public class ClientID : MonoBehaviour
    {
        private static ClientID instance;

        [SerializeField, ReadOnly]
        private string clientID;

        public static string ID { get => instance.clientID; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("ClientID: Only one client ID may be assigned per runtime.");
                return;
            }

            instance = this;
            clientID = System.Guid.NewGuid().ToString();
        }
    }
}
