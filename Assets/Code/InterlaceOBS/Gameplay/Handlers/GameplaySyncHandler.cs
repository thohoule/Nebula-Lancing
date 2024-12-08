using UnityEngine;
using TeaSteep;
using FishNet.Object;
using FishNet.Connection;
using Assets.Code.Gameplay.PlayingSM;
using Assets.Code.Gameplay;

namespace Assets.Code.Interlace.Gameplay
{
    public class GameplaySyncHandler : NetworkBehaviour
    {
        [SerializeField]
        private GameObject playingUIObject;

        #region Downstream
        //[ObserversRpc]
        //internal void OnShowPrepUI()
        //{
        //    PrepService.ShowUI();

        //    if (PlayerService.Player1.IsAssigned)
        //        PlayerService.Player1.OnAssginPlayer
        //}

        [TargetRpc]
        internal void OnPrepSync(NetworkConnection connection)
        {
            //set all OnPlayer assign for all players that where before this
            //player, this needs to be done server side

            //if (PlayerService.Player1.IsAssigned)
                
        }

        [ObserversRpc]
        internal void OnTransition()
        {
            //hide Prep UI
            PrepService.HideUI();
            //PrepService.
            //enable Playing state
            PlayingControl.EnableControlling();
            //Show Playing UI
            playingUIObject.SetActive(true);

            var handler = PlayerService.MainAvatarHandler;
            var avatar = handler.Actor;

            handler.OnMaxHealthChange(null, avatar.MaxHealth);
            handler.OnHealthChange(null, avatar.Health);
            handler.OnMaxShieldChange(null, avatar.MaxShield);
            handler.OnShieldChange(null, avatar.Shield);
        }
        #endregion

        #region Upstream
        #endregion
    }
}
