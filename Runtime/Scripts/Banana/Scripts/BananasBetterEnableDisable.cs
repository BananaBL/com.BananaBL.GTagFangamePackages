using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum scripttype
{
    Disable,
    Enable
}
public enum startortrigger
{
    Start,
    Trigger
}
public class BananasBetterEnableDisable : MonoBehaviour
{
    [Header("Start is when the game opens and trigger is for buttons ig")]
    public startortrigger TriggerOrStart;

    public scripttype ScriptType;
    public List<GameObject> ObjectsToEnableOrDisable = new List<GameObject>();
    public bool IsTesting;
    public bool TestEnable;
    public bool TestDisable;
    public UnityEvent OnEnable;
    public UnityEvent OnDisable;
    void Start()
    {
        if (TriggerOrStart == startortrigger.Start)
        {
            if (ScriptType == scripttype.Enable)
            {
                foreach (GameObject obj in ObjectsToEnableOrDisable)
                {
                    obj.SetActive(true);
                }
                OnEnable.Invoke();
            }
            if (ScriptType == scripttype.Disable)
            {
                foreach (GameObject obj in ObjectsToEnableOrDisable)
                {
                    obj.SetActive(false);
                }
                OnDisable.Invoke();
            }
        }
    }
    private void Update()
    {
        if (IsTesting)
        {
            if (ScriptType == scripttype.Enable)
            {
                if (TestEnable)
                {
                    foreach (GameObject obj in ObjectsToEnableOrDisable)
                    {
                        obj.SetActive(true);
                    }
                    OnEnable.Invoke();
                }
            }
            if (ScriptType == scripttype.Disable)
            {
                if (TestDisable)
                {
                    foreach (GameObject obj in ObjectsToEnableOrDisable)
                    {
                        obj.SetActive(false);
                    }
                    OnDisable.Invoke();
                }
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerOrStart == startortrigger.Trigger)
        {
            if (ScriptType == scripttype.Enable)
            {
                foreach (GameObject obj in ObjectsToEnableOrDisable)
                {
                    obj.SetActive(true);
                }
                OnEnable.Invoke();

            }
            if (ScriptType == scripttype.Disable)
            {
                foreach (GameObject obj in ObjectsToEnableOrDisable)
                {
                    obj.SetActive(false);
                }
                OnDisable.Invoke();
            }
        }
    }
}
