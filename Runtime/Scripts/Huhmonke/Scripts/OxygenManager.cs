using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuhMonke.Oxygen.Trigger;

namespace HuhMonke.Oxygen
{
    public class OxygenManager : MonoBehaviour
    {
        public static OxygenManager instance;

        [Header("Amount Of Oxygen")]
        public int Oxygen = 100;

        [Header("Decrease 1 Oxygen Every Single")]
        public float DecreaseEvery = 3;

        [Header("Display Oxygen: Optional")]
        public UnityEngine.UI.Slider OxygenSlider;
        public TMPro.TMP_Text OxygenText;

        [Header("Respawning")]
        public GorillaLocomotion.Player Player;
        public Transform RespawnPoint;


        [Header("Dont Edit")]
        public bool OxygenDecreasing = false;

        private LayerMask Gorillas, Nothing;

        private void Awake()
        {
            if(instance == null)
                instance = this;
        }



        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if(Player != null)
            {
                Gorillas = Player.locomotionEnabledLayers;
            }
            StartCoroutine(OxygenManaging());
            if(OxygenSlider != null)
            {
                OxygenSlider.maxValue = 100;
                OxygenSlider.minValue = 0;
            }
        }

        IEnumerator Respawn()
        {
            if (OxygenSlider != null)
            {
                OxygenSlider.value = Oxygen;
            }
            if (OxygenText != null)
            {
                OxygenText.text = Oxygen.ToString();
            }

            Oxygen = 100;

            if(Player != null)
            {
                Player.locomotionEnabledLayers = Nothing;
                Player.transform.position = RespawnPoint.position;
                yield return new WaitForSeconds(0.1f);
                Player.locomotionEnabledLayers = Gorillas;
            }
        }

        IEnumerator OxygenManaging()
        {
            while(true)
            {
                if(OxygenDecreasing)
                {
                    Oxygen -= 1;
                }
                else
                {
                    Oxygen += 1;
                }

                Oxygen = Mathf.Clamp(Oxygen, 0, 100);

                if(Oxygen <= 0)
                {
                    StartCoroutine(Respawn());
                }

                if (OxygenSlider != null)
                {
                    OxygenSlider.value = Oxygen;
                }
                if (OxygenText != null)
                {
                    OxygenText.text = Oxygen.ToString();
                }

                yield return new WaitForSeconds(DecreaseEvery);  
            }
        }
    }
}
