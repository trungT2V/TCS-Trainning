using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bill : BaseInteractiveObject
{
    [SerializeField] Rigidbody _rigidbody;

    public override void Interactive(Transform atackTransform)
    {
        _rigidbody.isKinematic = true;
        transform.SetParent(atackTransform, false);
        transform.localPosition = Vector3.zero;
    }
}
