using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Assets.Code.Gameplay.PlayingSM;
using Assets.Code.Characters;

namespace Assets.Code.Gameplay
{
    public class PlayingInterlace : NetworkBehaviour
    {
        //[SyncVar(OnChange = )]
        //private int state;

        [SerializeField]
        private PlayingControl control;
        [SerializeField]
        private StatusControl statusUIControl;
        [SerializeField]
        private GameObject playerUIObject;

        [ObserversRpc]
        internal void StartControlState()
        {
            control.SetState(control.Controlling);
            statusUIControl.Setup();
            statusUIControl.gameObject.SetActive(true);
            playerUIObject.SetActive(true);
        }
    }
}
