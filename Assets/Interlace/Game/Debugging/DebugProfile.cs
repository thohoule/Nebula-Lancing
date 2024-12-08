using UnityEngine;
using TeaSteep;

namespace Interlace.Debugging
{
    public class DebugProfile : MonoBehaviour
    {
        private static DebugProfile instance;

        [SerializeField, ReadOnly]
        private string profileName;

        public static string ProfileName { get => instance.profileName; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("ClientID: Only one client ID may be assigned per runtime.");
                return;
            }

            instance = this;
            profileName = System.Guid.NewGuid().ToString();
        }
    }
}
