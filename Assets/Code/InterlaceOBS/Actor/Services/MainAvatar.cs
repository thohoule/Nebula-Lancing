using UnityEngine;
using Assets.Code.Gameplay;

namespace Assets.Code.Characters
{
    public class MainAvatar
    {
        public static void Move(Vector2 direction)
        {
            PlayerService.MainAvatarHandler.Move(direction);
        }

        public static void FirePrimary()
        {
            PlayerService.MainAvatarHandler.FirePrimary();
        }

        public static void FireSecondary()
        {
            PlayerService.MainAvatarHandler.FireSecondary();
        }

        public static void InflictDamageTo(int amount)
        {
            PlayerService.MainAvatarHandler.Control.InflictDamage(amount);
        }
    }
}
