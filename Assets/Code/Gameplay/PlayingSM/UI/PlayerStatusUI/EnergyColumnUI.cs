using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Gameplay.PlayingSM.UI
{
    public class EnergyColumnUI : MonoBehaviour
    {
        public const int Max_Slots = 5;

        private int maxSlots = Max_Slots;
        private int currentFill;

        [SerializeField]
        private GameObject slotObject1;
        [SerializeField]
        private GameObject slotObject2;
        [SerializeField]
        private GameObject slotObject3;
        [SerializeField]
        private GameObject slotObject4;
        [SerializeField]
        private GameObject slotObject5;

        [SerializeField]
        private GameObject barObject1;
        [SerializeField]
        private GameObject barObject2;
        [SerializeField]
        private GameObject barObject3;
        [SerializeField]
        private GameObject barObject4;
        [SerializeField]
        private GameObject barObject5;

        public int MaxSlots { get => maxSlots; set => setMax(value); }
        public int CurrentFill { get => currentFill; set => setFill(value); }

        private void setMax(int maxValue)
        {
            maxSlots = Math.Clamp(maxValue, 0, Max_Slots);

            slotObject1.SetActive(maxSlots > 0);
            slotObject2.SetActive(maxSlots > 1);
            slotObject3.SetActive(maxSlots > 2);
            slotObject4.SetActive(maxSlots > 3);
            slotObject5.SetActive(maxSlots > 4);

            setFill(currentFill);
        }

        private void setFill(int fillValue)
        {
            currentFill = Math.Clamp(fillValue, 0, MaxSlots);

            barObject1.SetActive(currentFill > 0);
            barObject2.SetActive(currentFill > 1);
            barObject3.SetActive(currentFill > 2);
            barObject4.SetActive(currentFill > 3);
            barObject5.SetActive(currentFill > 4);
        }
    }
}
