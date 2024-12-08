using UnityEngine;

namespace Assets.Code.Gameplay.PlayingSM.UI
{
    public class PlayerStatUI : MonoBehaviour
    {
        [SerializeField]
        private SliderControl healthSlider;
        [SerializeField]
        private SliderControl shieldSlider;
        [SerializeField]
        private CooldownSlider shieldCooldown;
        [SerializeField]
        private CooldownSlider primaryCooldown;
        [SerializeField]
        private CooldownSlider secondaryCooldown;
        [SerializeField]
        private EnergyCollectionUI shieldCollection;
        [SerializeField]
        private EnergyColumnUI primaryColumn;
        [SerializeField]
        private EnergyColumnUI secondaryColumn;
        [SerializeField]
        private EnergyColumnUI thrusterColumn;

        public SliderControl HealthSlider { get => healthSlider; }
        public SliderControl ShieldSlider { get => shieldSlider;}
        public CooldownSlider ShieldCooldown { get => shieldCooldown; }
        public CooldownSlider PrimaryCooldown { get => primaryCooldown; }
        public CooldownSlider SecondaryCooldown { get => secondaryCooldown; }
        public EnergyCollectionUI ShieldCollection { get => shieldCollection; }
        public EnergyColumnUI PrimaryColumn { get => primaryColumn; }
        public EnergyColumnUI SecondaryColumn { get => secondaryColumn; }
        public EnergyColumnUI ThrusterColumn { get => thrusterColumn; }
    }
}
