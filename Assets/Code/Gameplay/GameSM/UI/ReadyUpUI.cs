using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Code.Gameplay.GameSM.UI
{
    public class ReadyUpUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject notReadyObject;
        [SerializeField]
        private GameObject readyObject;

        public GameObject NotReadyObject { get => notReadyObject; set => notReadyObject = value; }
        public GameObject ReadyObject { get => readyObject; set => readyObject = value; }

        public void SetReadyState(bool isReady)
        {
            notReadyObject.SetActive(!isReady);
            readyObject.SetActive(isReady);
        }
    }
}
