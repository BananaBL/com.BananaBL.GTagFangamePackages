using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using easyInputs;
using Photon.Pun;

public class FlashlightV2 : MonoBehaviour
{
    public enum button
    {
        Grip, Trigger, Primary,Secondary
    }
    [Header("By HuhMonke")]


    [Header("Hand To Toggle With")]
    public EasyHand Hand;

    [Header("Button U Wanna Toggle With")]
    public button ButtonToToggle = button.Trigger;

    [Header("Light Gameobject")]
    public GameObject Light;

    [Header("if its on or off")]
    public bool on = false;

    [Header("Cosmetic Or Not if cosmetic make sure u put photon view")]
    public bool Cosmetic = false;
    public PhotonView PV;

    private bool Pressed = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Light.SetActive(on);
    }

    // Update is called once per frame
    void Update()
    {
        Light.SetActive(on);

        if(PV.IsMine && Cosmetic)
        {
            switch (ButtonToToggle)
            {
                case button.Grip:
                    if (!Pressed && EasyInputs.GetGripButtonDown(Hand))
                    {
                        Toggle();
                        Pressed = true;
                    }
                    if (Pressed && !EasyInputs.GetGripButtonDown(Hand))
                    {
                        Pressed = false;
                    }
                    break;
                case button.Trigger:
                    if (!Pressed && EasyInputs.GetTriggerButtonDown(Hand))
                    {
                        Toggle();
                        Pressed = true;
                    }
                    if (Pressed && !EasyInputs.GetTriggerButtonDown(Hand))
                    {
                        Pressed = false;
                    }
                    break;
                case button.Primary:
                    if (!Pressed && EasyInputs.GetPrimaryButtonDown(Hand))
                    {
                        Toggle();
                        Pressed = true;
                    }
                    if (Pressed && !EasyInputs.GetPrimaryButtonDown(Hand))
                    {
                        Pressed = false;
                    }
                    break;
                case button.Secondary:
                    if (!Pressed && EasyInputs.GetSecondaryButtonDown(Hand))
                    {
                        Toggle();
                        Pressed = true;
                    }
                    if (Pressed && !EasyInputs.GetSecondaryButtonDown(Hand))
                    {
                        Pressed = false;
                    }
                    break;
            }
        }
        else if(!Cosmetic)
        {
            switch (ButtonToToggle)
            {
                case button.Grip:
                    if (!Pressed && EasyInputs.GetGripButtonDown(Hand))
                    {
                        Toggle();
                        Pressed = true;
                    }
                    if (Pressed && !EasyInputs.GetGripButtonDown(Hand))
                    {
                        Pressed = false;
                    }
                    break;
                case button.Trigger:
                    if (!Pressed && EasyInputs.GetTriggerButtonDown(Hand))
                    {
                        Toggle();
                        Pressed = true;
                    }
                    if (Pressed && !EasyInputs.GetTriggerButtonDown(Hand))
                    {
                        Pressed = false;
                    }
                    break;
                case button.Primary:
                    if (!Pressed && EasyInputs.GetPrimaryButtonDown(Hand))
                    {
                        Toggle();
                        Pressed = true;
                    }
                    if (Pressed && !EasyInputs.GetPrimaryButtonDown(Hand))
                    {
                        Pressed = false;
                    }
                    break;
                case button.Secondary:
                    if (!Pressed && EasyInputs.GetSecondaryButtonDown(Hand))
                    {
                        Toggle();
                        Pressed = true;
                    }
                    if (Pressed && !EasyInputs.GetSecondaryButtonDown(Hand))
                    {
                        Pressed = false;
                    }
                    break;
            }
        }
    }

    void Toggle()
    {
        on = !on;
    }
}
