using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum HandType
{
    Left,
    Right
}

public class Fingers : MonoBehaviour
{
    public HandType handType;
    private float thumb = 0.4f;

    private Animator animator;
    private InputDevice inputDevice;

    private float Index;
    private float Middle;
    private float Thumb;

    public PhotonView view;

    void Start()
    {
        animator = GetComponent<Animator>();
        inputDevice = GetInputDevice();
    }

    void Update()
    {
        if (view.IsMine)
        {
            try
            {
                AnimateHand();
            }
            catch
            {
                //catch
            }
        }
    }

    InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics controllerCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        if (handType == HandType.Left)
        {
            controllerCharacteristic |= InputDeviceCharacteristics.Left;
        }
        else
        {
            controllerCharacteristic |= InputDeviceCharacteristics.Right;
        }

        List<InputDevice> inputDevices = new();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, inputDevices);

        return inputDevices[0];
    }

    void AnimateHand()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out Index);
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out Middle);

        inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryTouched);

        if (primaryTouched)
        {
            Thumb += thumb;
        }
        else
        {
            Thumb -= thumb;
        }

        Thumb = Mathf.Clamp(Thumb, 0, 1);

        if (handType == HandType.Right)
        {
            animator.SetFloat("pose1", Index);
            animator.SetFloat("pose2", Middle);
            animator.SetFloat("pose3", Thumb);
        }
        else
        {
            animator.SetFloat("pose1", Index);
            animator.SetFloat("pose2", Middle);
            animator.SetFloat("pose3", Thumb);
        }
    }
}