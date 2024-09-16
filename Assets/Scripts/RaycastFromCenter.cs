using System;
using Unity.VisualScripting;
using UnityEngine;

public enum E_Carry
{
    EMPTY,
    BOX,
    LABEL
}

public class RaycastFromCenter : MonoBehaviour
{
    public Camera playerCamera;
    public float rayDistance = 100f;
    public LayerMask boxLayer;
    public LayerMask tableLayer;
    public Transform afterlableParent;
    public Transform atackTransform;
    public MouseLook mouseLook;

    public BaseInteractiveObject lastInteractive;
    private E_Carry E_Carry;

    [Header("Rotate Box")]
    public float rotationSpeed = 100f;
    public bool invertY = false;
    private float mouseX;
    private float mouseY;

    [SerializeField] GameObject labelPanel;
    public LabelGroup labelGroup;
    [SerializeField] GameObject labelAxitPrefab;
    [SerializeField] GameObject labelFirePrefab;
    [SerializeField] GameObject labelArowPrefab;
    [SerializeField] GameObject labelBillPrefab;
    private GameObject currentLabel;
    private E_LABEL currentLabelType;

    void Update()
    {
        switch (E_Carry)
        {
            case E_Carry.EMPTY:
                DoRaycastEmpty();
                break;
            case E_Carry.BOX:
                DoRaycastBox();
                break;
            case E_Carry.LABEL:
                DoRaycastLabel();
                break;
        }
    }

    private void DoRaycastLabel()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, tableLayer))
        {
            afterlableParent.GetChild(0).position = hit.point + (0.128f * hit.normal);
            afterlableParent.GetChild(0).up = hit.normal;

            if (Input.GetMouseButtonDown(0))
            {
                afterlableParent.GetChild(0).SetParent(null);
                E_Carry = E_Carry.EMPTY;
            }
        }
        else
        {
            afterlableParent.GetChild(0).localPosition = Vector3.zero;
        }
    }

    private void DoRaycastBox()
    {
        E_LABEL newLabel = labelGroup.GetCurrentSelectedLabel();
        if (newLabel != E_LABEL.NONE)
        {
            if (currentLabel == null)
            {
                currentLabel = GetLabel(labelGroup.GetCurrentSelectedLabel());
            }
            else
            {
                if (currentLabelType != newLabel)
                {
                    Destroy(currentLabel);
                    currentLabel = null;
                    currentLabelType = newLabel;
                    return;
                }
            }

            Vector3 mousePosition = Input.mousePosition;
            Ray ray = playerCamera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50, boxLayer))
            {
                currentLabel.transform.position = hit.point + (0.01f * hit.normal);
                currentLabel.transform.forward = hit.normal;

                if (Input.GetMouseButtonDown(0))
                {
                    currentLabel.transform.SetParent(hit.transform, true);
                    currentLabel = null;
                }
            }
            else
            {
                currentLabel.transform.position = ray.origin + (2 * ray.direction);
            }
        }
        else
        {
            if (currentLabel != null)
            {
                Destroy(currentLabel);
                currentLabel = null;
            }

            if (Input.GetMouseButton(0))
            {
                // Lấy giá trị di chuyển của chuột theo trục X và Y
                mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

                // Đảo ngược trục Y nếu cần
                if (invertY)
                {
                    mouseY = -mouseY;
                }

                // Xoay đối tượng theo trục Y (ngang) và trục X (dọc)
                atackTransform.Rotate(Vector3.up, -mouseX, Space.World);  // Xoay quanh trục Y (ngang)
                atackTransform.Rotate(transform.right, mouseY, Space.World); // Xoay quanh trục X (dọc)
            }
        }
    }

    private void DoRaycastEmpty()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, boxLayer))
        {
            BaseInteractiveObject baseInteractive = hit.transform.GetComponent<BaseInteractiveObject>();

            if (baseInteractive != null && lastInteractive != baseInteractive)
            {
                if (lastInteractive != null)
                    lastInteractive.SetActiveOutlight(false);

                lastInteractive = baseInteractive;
                lastInteractive.SetActiveOutlight(true);
            }
        }
        else
        {
            if (lastInteractive != null)
                lastInteractive.SetActiveOutlight(false);
            lastInteractive = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (lastInteractive == null)
                return;

            lastInteractive.Interactive(atackTransform);
            mouseLook.Active(false);
            E_Carry = E_Carry.BOX;
            labelPanel.SetActive(true);
        }
    }

    private GameObject GetLabel(E_LABEL label)
    {
        GameObject result = null;

        switch (label)
        {
            case E_LABEL.FIRE:
                result = Instantiate(labelFirePrefab);
                break;
            case E_LABEL.AXIT:
                result = Instantiate(labelAxitPrefab);
                break;
            case E_LABEL.ARROW:
                result = Instantiate(labelArowPrefab);
                break;
            case E_LABEL.BILL:
                result = Instantiate(labelBillPrefab);
                break;
        }

        return result;
    }

    public void BTN_LabelConfirm()
    {
        labelPanel.SetActive(false);
        atackTransform.GetChild(0).SetParent(afterlableParent, false);
        mouseLook.Active(true);
        E_Carry = E_Carry.LABEL;
    }
}
