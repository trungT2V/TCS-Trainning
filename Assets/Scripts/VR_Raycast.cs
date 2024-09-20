using HurricaneVR.Framework.ControllerInput;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Raycast : MonoBehaviour
{
    [SerializeField] LayerMask boxLayer;
    [SerializeField] float rayDistance = 100f;
    [SerializeField] HVRPlayerInputs input;
    [SerializeField] Transform labelAnchor;
    [SerializeField] VR_Label label;

    private Transform currentLabel;

    public void SetLabel(Transform label)
    {
        currentLabel = label;
        currentLabel.SetParent(labelAnchor, false);
    }

    private void Update()
    {
        if (currentLabel)
            DoRaycast();
    }

    private void DoRaycast()
    {
        Ray ForwardRaycast = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ForwardRaycast, out hit, rayDistance, boxLayer))
        {
            if (currentLabel == null)
                return;

            currentLabel.position = hit.point + (.01f * hit.normal);
            currentLabel.forward = hit.normal;

            if (input.RightTriggerGrabState.Active)
            {
                currentLabel.SetParent(hit.transform);
                currentLabel = null;
                label.StickLabel();
            }
        }
        else
        {
            currentLabel.localPosition = Vector3.zero;
            currentLabel.forward = -transform.forward;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);
    }

    public void ClearLabel()
    {
        currentLabel.SetParent(null);
        currentLabel = null;
    }
}
