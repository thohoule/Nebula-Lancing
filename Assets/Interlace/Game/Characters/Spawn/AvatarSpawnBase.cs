using UnityEngine;
using Interlace;
using FishNet.Object;
using FishNet.Connection;
using Assets.Entities;

namespace Assets.Game.Character
{
    public abstract class AvatarSpawnBase : NetworkBehaviour
    {
        public AvatarHandler UseSpawn(PlayerEntity entity)
        {
            var avatarObject = onUsed(entity);

            if (avatarObject == null)
            {
                Debug.LogError("AvatarSpawnBase: Spawn was unable to build an avatar.");
                return null;
            }
            else
            {
                sendAssignCommand(entity.Connection, avatarObject.gameObject);
                return avatarObject;
            }
        }

        protected abstract AvatarHandler onUsed(PlayerEntity entity);

        private void sendAssignCommand(NetworkConnection connection, 
            GameObject avatarObject)
        {
            AvatarService.Handler.OnAssign(connection, avatarObject);
        }
    }
}
