using System;
using System.Collections.Generic;
using UnityEngine;
using TeaSteep;
using Assets.Code.Characters;
using Assets.Code.Characters.Weapons.Primary;
using Assets.Code.Gameplay.MainMenuSM;
using Assets.Code.Gameplay.Network;

namespace Assets.Code.Gameplay
{
    public class ArenaTestStartup : MonoBehaviour
    {
        [SerializeField]
        private FocusCameraController cameraController;

        //private void Awake()
        //{
        //    var prefab = PrefabAsset.GetPrefab<NetActor>();
        //    var avatar = Instantiate(prefab);
        //    var target = avatar.GetComponent<CameraTarget>();

        //    ActorManager.Avatar = avatar;
        //    cameraController.Target = target;
        //    avatar.PrimaryWeapon = new TestShooterWeapon(avatar);
        //}

        //private void Start()
        //{
            
        //}

        private void Update()
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
            //else if (Input.GetKeyUp(KeyCode.PageDown))
            //{
            //    skipPrepStartAsClient();
            //}
        }

        private void enterPrepAsHost()
        {
            ConnectionTransaction.HostLobby(onConnection);
        }

        private void enterPrepAsClient()
        {
            ConnectionTransaction.Connect(onConnection);
        }

        private void skipPrepStartAsHost()
        {
            //loadAvatar();
            ConnectionTransaction.HostLobby(onConnection);
        }

        //private void skipPrepStartAsClient()
        //{
        //    loadAvatar();
        //}

        //private void loadAvatar()
        //{
        //    var prefab = PrefabAsset.GetPrefab<NetActor>();
        //    var avatar = Instantiate(prefab);
        //    var target = avatar.GetComponent<CameraTarget>();

        //    ActorManager.Avatar = avatar;
        //    cameraController.Target = target;
        //    avatar.PrimaryWeapon = new TestShooterWeapon(avatar);
        //}

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
