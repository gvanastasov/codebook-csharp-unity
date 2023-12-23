using UnityEngine;

public class EventEmitter : MonoBehaviour
{
    public delegate void OnMouseDown_Event(EventEmitter emitter);
    public event OnMouseDown_Event OnMouseDown_EventTriggered;
    public string OnMouseDown_EventData = string.Empty;

    public void OnMouseDown()
    {
        if (OnMouseDown_EventTriggered != null)
        {
            OnMouseDown_EventTriggered(emitter: this);
        }
    }
}
