using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuhMonke.Oxygen;
using System.Linq;

namespace HuhMonke.Oxygen.Trigger
{
    public class UnderwaterTrigger : MonoBehaviour
    {
        [Header("Put this on the thing thats gonna trigger ur thingy")]
        [Space]

        public string[] TagsToTrigger = { "Finger", "Toe", "HandTag", "Balls", "Drippy Cheese Bruh", "Sigma", "Sigma", "Boy", "Skibidi", "Toilet" } ;

        private bool IsTrigger = true;

        private void Start()
        {
            Collider col = GetComponent<Collider>();
            if(col != null)
            {
                if(col.isTrigger)
                {
                    IsTrigger = true;
                }
                else
                {
                    IsTrigger = false;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (TagsToTrigger.Contains(collision.gameObject.tag) && !IsTrigger)
            {
                HuhMonke.Oxygen.OxygenManager.instance.OxygenDecreasing = true;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (TagsToTrigger.Contains(collision.gameObject.tag) && !IsTrigger)
            {
                HuhMonke.Oxygen.OxygenManager.instance.OxygenDecreasing = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (TagsToTrigger.Contains(collision.gameObject.tag) && !IsTrigger)
            {
                HuhMonke.Oxygen.OxygenManager.instance.OxygenDecreasing = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(TagsToTrigger.Contains(other.tag) && IsTrigger)
            {
                HuhMonke.Oxygen.OxygenManager.instance.OxygenDecreasing = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (TagsToTrigger.Contains(other.tag) && IsTrigger)
            {
                HuhMonke.Oxygen.OxygenManager.instance.OxygenDecreasing = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (TagsToTrigger.Contains(other.tag) && IsTrigger)
            {
                HuhMonke.Oxygen.OxygenManager.instance.OxygenDecreasing = false;
            }
        }
    }
}
