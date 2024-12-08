using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using Assets.Code.Gameplay;

namespace Assets.Code.Characters
{
    public class NetAvatarHandler : NetActorHandler
    {
        /*Can have another handler for the UI updating, this would reduce
         the need to have an "Owner" handler for now.*/
        protected IAvatarUIHandler avatarUIHandler;

        #region Downstream
        protected internal override void OnHealthChange(NetworkConnection connection, int value)
        {
            Actor.Health = value;
            //update UI
            avatarUIHandler?.SetHealth(value);
        }

        protected internal override void OnMaxHealthChange(NetworkConnection connection, int value)
        {
            Actor.MaxHealth = value;
            avatarUIHandler?.SetMaxHealth(value);
        }

        protected internal override void OnShieldChange(NetworkConnection connection, int value)
        {
            Actor.Shield = value;
            avatarUIHandler?.SetShield(value);
        }

        protected internal override void OnMaxShieldChange(NetworkConnection connection, int value)
        {
            Actor.MaxShield = value;
            avatarUIHandler?.SetMaxShield(value);
        }

        internal void SetUIHandler(int handlerType)
        {
            switch (handlerType)
            {
                case 0:
                    avatarUIHandler = PlayingUIService.MainStatusHandler;
                    break;
            }
            /*Very temmp method, should let the player service handle the
             heavy lifting on whos UI is to be updated.*/
        }

        //or
        internal void OnAssignOwner(PlayerClient client)
        {

        }
        #endregion
    }
}
