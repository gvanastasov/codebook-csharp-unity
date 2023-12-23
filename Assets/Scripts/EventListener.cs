using System;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public List<EventEmitter> eventEmitters = new List<EventEmitter>();

    void Start()
    {
        foreach (var eventEmitter in eventEmitters)
        {
            eventEmitter.OnMouseDown_EventTriggered += OnMouseDownEventHandler;
        }
    }

    private void OnMouseDownEventHandler(EventEmitter emitter)
    {
        this.gameObject.BroadcastMessage("OnMouseDown_Broadcast", emitter, SendMessageOptions.DontRequireReceiver);
    }
}
