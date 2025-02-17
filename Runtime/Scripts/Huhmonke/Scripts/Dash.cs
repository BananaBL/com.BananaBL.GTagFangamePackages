using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Dash : MonoBehaviour
{
    [Header("By HuhMonke")]
    [Header("Controller to use buttons with")]
    public XRController Controller;
    [Header("Buttons")]
    public Buttons Button;
    [Header("Players rigidbody")]
    public Rigidbody PlayerRigidbody;
    [Header("Cooldown")]
    public float Cooldown = 0.5f;
    [Header("amount of force to add")]
    public float ForceToAdd = 5;

    private bool Pressing = false, Cooldownn = false;

    // Update is called once per frame
    void Update()
    {
        switch(Button)
        {
            case Buttons.Primary:
                Pressing = Controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out Pressing);
                break;
            case Buttons.Secondary:
                Pressing = Controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out Pressing);
                break;
            case Buttons.Trigger:
                Pressing = Controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float valuee) && valuee > 0.1f;
                break;
            case Buttons.Grip:
                Pressing = Controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float value) && value > 0.1f;
                break;
        }

        if(Pressing && !Cooldownn)
        {
            PlayerRigidbody.AddForce(Camera.main.transform.forward * ForceToAdd);
            StartCooldown();
        }
    }

    IEnumerator StartCooldown()
    {
        Cooldownn = true;
        yield return new WaitForSeconds(Cooldown);
        Cooldownn = false;
    }


    public enum Buttons
    {
        Primary,
        Secondary,
        Trigger,
        Grip
    }
}
