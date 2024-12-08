using UnityEngine;
using TeaSteep;

namespace Assets.Game
{
    public static class CommonCharacter
    {
        private const string Health_Key = "Character_Health";
        private const string Health_Max_Key = "Character_HealthMax";

        public static KeyName Health { get; private set; } = new KeyName(Health_Key);
        public static KeyName HealthMax { get; private set; } = new KeyName(Health_Max_Key);

        public static float GetHealthPercent(Blackboard<KeyName> blackboard)
        {
            var maxHealth = blackboard.GetIntValue(HealthMax);
            var health = Mathf.Clamp(blackboard.GetIntValue(Health), 0, maxHealth);

            return health / maxHealth;
        }
    }
}
