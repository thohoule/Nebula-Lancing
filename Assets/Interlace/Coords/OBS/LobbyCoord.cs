using FishNet.Connection;
using UnityEngine;
using TeaSteep;
using Assets.Services;

namespace Interlace.OBS
{
    public class LobbyCoord : MonoBehaviour
    {
        private const string Player1_Object_Name = "Player 1 Coord Object";

        public PlayerCoord Player1 { get; private set; }

        private void Awake()
        {
            var playerObject = new GameObject(name);

            Player1 = createPlayerCoord(Player1_Object_Name);
            Player1.PlayerHandler = LobbyService.GetPlayerBySeat(1);
        }

        private PlayerCoord createPlayerCoord(string objectName)
        {
            var playerObject = new GameObject(objectName);
            playerObject.SetParentAndLocals(gameObject);

            return playerObject.AddComponent<PlayerCoord>();
        }

        #region Global Server

        #endregion
    }
}
