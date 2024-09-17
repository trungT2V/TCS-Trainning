using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractiveObject : MonoBehaviour
{
    Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void SetActiveOutlight(bool active)
    {
        outline.enabled = active;
    }

    public virtual void Interactive(Transform atackTransform)
    {

    }

    public virtual void StopInteractive()
    {

    }
}
