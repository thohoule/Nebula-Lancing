/*Instead of seperate abilities, The weapon instead acts as an ability.*/

//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using TeaSteep.Ability;

//namespace Assets.Code.Ability
//{
//    public abstract class WeaponFireAbility : IAbility<AbilityContext>
//    {
//        public AbilityConditionHandler<AbilityContext> ConditionHandler => canFire;
//        public AbilityUseHandler<AbilityContext> UseHandler => onFire;

//        protected virtual bool canFire(ActionInstance<AbilityContext> instance) { return true; }
//        protected abstract void onFire(ActionInstance<AbilityContext> instance);
//    }
//}
