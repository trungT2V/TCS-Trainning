using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : BaseInteractiveObject
{
    [SerializeField] Rigidbody _rigidbody;

    public override void Interactive(Transform atackTransform)
    {
        _rigidbody.isKinematic = true;
        transform.SetParent(atackTransform, false);
        transform.localPosition = Vector3.zero;
    }

    public override void StopInteractive()
    {
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
    }
}
