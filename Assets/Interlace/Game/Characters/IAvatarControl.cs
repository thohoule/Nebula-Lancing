using UnityEngine;
using Interlace;

namespace Assets.Game
{
    public interface IAvatarControl
    {
        //ActorHandler Handler { get; set; }
        //bool IsDead { get; set; }
        void OnDeath();
        void Move(Vector2 movement);
        void AimTowards(Vector3 target);
    }
}
