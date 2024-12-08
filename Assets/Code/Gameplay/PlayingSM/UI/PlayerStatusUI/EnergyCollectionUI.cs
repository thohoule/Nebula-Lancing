using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Gameplay.PlayingSM.UI
{
    public class EnergyCollectionUI : MonoBehaviour
    {
        private int maxAmount;
        private int fillAmount;

        [SerializeField]
        private List<EnergyColumnUI> energyColumns;

        public int ColumnTotalSlots { get => energyColumns.Count * EnergyColumnUI.Max_Slots; }
        public int MaxSlots { get => maxAmount; set => setMax(value); }
        public int CurrentFill { get => fillAmount; set => setFill(value); }

        private void setMax(int value)
        {
            maxAmount = Math.Clamp(value, 0, ColumnTotalSlots);

            int remaining = maxAmount;

            for (int columnIndex = 0; columnIndex < energyColumns.Count; columnIndex++)
            {
                var column = energyColumns[columnIndex];
                column.MaxSlots = Math.Min(remaining, 5);

                remaining = Math.Max(remaining - 5, 0);
            }
        }

        private void setFill(int value)
        {
            fillAmount = Math.Clamp(value, 0, maxAmount);

            int remaining = fillAmount;

            for (int columnIndex = 0; columnIndex < energyColumns.Count; columnIndex++)
            {
                var column = energyColumns[columnIndex];
                column.CurrentFill = Math.Min(remaining, 5);

                remaining = Math.Max(remaining - 5, 0);
            }
        }
    }
}
