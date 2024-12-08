using UnityEngine;
using TeaSteep;
using Assets.Code.Gameplay.Network;
using Assets.Code.Characters;
using Assets.Code.Characters.Weapons.Primary;

namespace Assets.Code.Gameplay.GameSM.States
{
    /// <summary>
    /// Game Loop is Server only, normally the client would not interact with this,
    /// but for degbugging the client uses this state to connect (for now).
    /// </summary>
    public class DebugStartingState : MonoBehaviour, IUpdateContent<GameLoopControl>
    {
        [SerializeField]
        private FocusCameraController cameraController;

        public void OnStateStart(GameLoopControl control)
        {
        }

        public void OnStateEnd()
        {
        }

        public void OnStateUpdate(BranchOperation operation, GameLoopControl control)
        {
            if (Input.GetKeyUp(KeyCode.Home))
            {
                enterPrepAsHost();
            }
            else if (Input.GetKeyUp(KeyCode.End))
            {
                enterPrepAsClient();
            }
            else if (Input.GetKeyUp(KeyCode.PageUp))
            {
                skipPrepStartAsHost();
            }
            else if (Input.GetKeyUp(KeyCode.PageDown))
            {
                skipPrepStartAsClient();
            }
        }

        private void enterPrepAsHost()
        {

        }

        private void enterPrepAsClient()
        {

        }

        private void skipPrepStartAsHost()
        {
            loadAvatar();
        }

        private void skipPrepStartAsClient()
        {
            loadAvatar();
        }

        private void loadAvatar()
        {
            //var prefab = PrefabAsset.GetPrefab<NetActor>();
            //var avatar = Instantiate(prefab);
            //var target = avatar.GetComponent<CameraTarget>();

            //ActorManager.Avatar = avatar;
            //cameraController.Target = target;
            //avatar.PrimaryWeapon = new TestShooterWeapon(avatar);
        }

        private void onConnection(TransactionResult result)
        {
            if (result.State == TransactionState.Successful)
            {
                Debug.Log(string.Format("Joinded Server as {0}", ClientID.ID));
            }
            else
            {
                Debug.LogError(string.Format("Failed to join: {0}", result.ErrorMessage));
            }
        }
    }
}
