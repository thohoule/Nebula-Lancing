using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Gameplay;
using Assets.Code.Gameplay.PlayingSM.UI;

namespace Assets.Code.Characters
{
    public class StatusControl : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatUI statUI;

        public void Setup()
        {
            statUI.HealthSlider.Setup(ActorManager.Avatar.Health,
                ActorManager.Avatar.MaxHealth);
            statUI.ShieldSlider.Setup(ActorManager.Avatar.Shield,
                ActorManager.Avatar.MaxShield);
        }

        private void Update()
        {
            if (ActorManager.Avatar != null)
            {
                statUI.HealthSlider.Value = ActorManager.Avatar.Health;
                statUI.ShieldSlider.Value = ActorManager.Avatar.Shield;
            }
        }
    }
}
