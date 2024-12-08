using System.Collections.Generic;
using UnityEngine;
using TeaSteep;
using TeaSteep.Ability;

namespace Assets.Code.Characters
{
    public class AbilityContext : IAbilityContext
    {
        public float ChargeValue { get; private set; }
        public float EffectValue { get; set; }
        public Game.IGameEntity Target { get; set; }
        //public IReadOnlyList<NetActor> AllTargets { get; }

        public AbilityContext(float charge)
        {
            ChargeValue = charge;
        }
    }
}
