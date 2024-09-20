using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Box : BaseInteractiveObject
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] TMP_Text sender;
    [SerializeField] TMP_Text receiver;
    [SerializeField] TMP_Text note;

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

    public void SetSender(string sender)
    {
        this.sender.text = sender;
    }

    public void SetReceiver(string receiver)
    {
        this.receiver.text = receiver;
    }

    public void SetNote(string note)
    {
        this.note.text = note;
    }
}
