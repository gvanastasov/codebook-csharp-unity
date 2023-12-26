using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLogger : MonoBehaviour
{
    public bool LogAwake = true;
    public bool LogStart = true;
    public bool LogEnable = true;
    public bool LogDisable = true;

    private void Awake()
    {
        if (LogAwake)
        {
            Debug.Log($"{this.name} Awake");
        }
    }

    private void Start()
    {
        if (LogStart)
        {
            Debug.Log($"{this.name} Start");
        }
    }

    private void OnEnable()
    {
        if (LogEnable)
        {
            Debug.Log($"{this.name} Enable");
        }
    }

    private void OnDisable()
    {
        if (LogDisable)
        {
            Debug.Log($"{this.name} Disable");
        }
    }
}
