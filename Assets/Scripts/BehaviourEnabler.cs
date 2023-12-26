using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BehaviourEnabler : MonoBehaviour
{
    public Behaviour target;

    public UnityEvent onEnable;

    public UnityEvent onDisable;

    public float delay = 2f;

    void Start()
    {
        StartCoroutine(EnableToggle());    
    }

    IEnumerator EnableToggle()
    {
        yield return new WaitForSeconds(delay);
        target.enabled = !target.enabled;

        if (target.enabled)
        {
            onEnable.Invoke();
        }
        else
        {
            onDisable.Invoke();
        }

        StartCoroutine(EnableToggle());
    }
}
