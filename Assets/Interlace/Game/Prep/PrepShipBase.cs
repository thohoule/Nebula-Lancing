using UnityEngine;
using Interlace;
using Assets.Entities;

namespace Assets.Game
{
    public abstract class PrepShipBase : MonoBehaviour
    {
        public virtual void SetShip(PrepEntity prep)
        { }

        public virtual void SetPrimary(PrepEntity prep)
        { }

        public virtual void SetSecondary(PrepEntity prep)
        { }

        public virtual void PlayEnterAnimation()
        { }

        public virtual void PlayTransitionAnimation()
        { }
    }
}
