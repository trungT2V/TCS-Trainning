using DG.Tweening;
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

    public LabelGroup labelGroup;

    [SerializeField] GameObject labelPanel;

    private Box currentBox;
    private GameObject currentLabel;
    private bool lockRotate;

    private void Start()
    {
        labelGroup.OnSelectedLabel += OnSelectedLabelHandler;
        labelGroup.OnDeleteLabel += OnDeleteLabelHandler;
        labelGroup.OnSender += OnSenderHanlder;
        labelGroup.OnReceiver += OnReceiverHanlder;
        labelGroup.OnNote += OnNoteHandler;
        labelGroup.OnTakeNote += OnTakeNoteHandler;
    }

    private void OnTakeNoteHandler(bool status)
    {
        if (status)
        {
            lockRotate = true;
            atackTransform.localRotation = Quaternion.Euler(0f, -90f, 90f);
        }
        else
        {
            lockRotate = false;
        }
        
    }

    private void OnReceiverHanlder(string obj)
    {
        currentBox?.SetReceiver(obj);
    }

    private void OnNoteHandler(string obj)
    {
        currentBox?.SetNote(obj);
    }

    private void OnSenderHanlder(string obj)
    {
        currentBox?.SetSender(obj);
    }

    private void OnDeleteLabelHandler()
    {
        currentLabel = null;
    }

    private void OnSelectedLabelHandler(GameObject labelGO)
    {
        currentLabel = labelGO;
    }

    void Update()
    {
        switch (E_Carry)
        {
            case E_Carry.EMPTY:
                HandEmpty();
                break;
            case E_Carry.BOX:
                HandHoldingBoxToLabel();
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
            if(currentBox != null)
            {
                afterlableParent.GetChild(0).position = hit.point + (0.27f * hit.normal);
                afterlableParent.GetChild(0).up = hit.normal;
            }
            else
            {
                afterlableParent.GetChild(0).position = hit.point + (0.01f * hit.normal);
                afterlableParent.GetChild(0).up = hit.normal;
            }

           

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

    private void HandHoldingBoxToLabel()
    {
        if (currentLabel != null)
        {
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
                    labelGroup.StickLabel();
                    currentLabel = null;
                }
            }
            else
            {
                currentLabel.transform.position = ray.origin + (2 * ray.direction);
                currentLabel.transform.forward = -playerCamera.transform.forward;
            }
        }
        else
        {
            if (lockRotate)
                return;

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
            else if (Input.GetMouseButtonUp(0))
            {
                float lerpToAngleY = 0, minAngleY = 500;
                float angleY = atackTransform.localEulerAngles.y;

                float lerpToAngleX = 0, minAngleX = 500;
                float angleX = atackTransform.localEulerAngles.x;

                float lerpToAngleZ = 0, minAngleZ = 500;
                float angleZ = atackTransform.eulerAngles.z;

                if (MathF.Abs(angleY - 0) < minAngleY)
                {
                    lerpToAngleY = 0;
                    minAngleY = MathF.Abs(angleY - 0);
                }

                if (MathF.Abs(angleY - 90) < minAngleY)
                {
                    lerpToAngleY = 90;
                    minAngleY = MathF.Abs(angleY - 90);
                }

                if (MathF.Abs(angleY - 180) < minAngleY)
                {
                    lerpToAngleY = 180;
                    minAngleY = MathF.Abs(angleY - 180);
                }

                if (MathF.Abs(angleY - 270) < minAngleY)
                {
                    lerpToAngleY = 270;
                    minAngleY = MathF.Abs(angleY - 270);
                }

                if (MathF.Abs(angleY - 360) < minAngleY)
                {
                    lerpToAngleY = 0;
                }

                if (MathF.Abs(angleX - 0) < minAngleX)
                {
                    lerpToAngleX = 0;
                    minAngleX = MathF.Abs(angleX - 0);
                }

                if (MathF.Abs(angleX - 90) < minAngleX)
                {
                    lerpToAngleX = 90;
                    minAngleX = MathF.Abs(angleX - 90);
                }

                if (MathF.Abs(angleX - 180) < minAngleX)
                {
                    lerpToAngleX = 180;
                    minAngleX = MathF.Abs(angleX - 180);
                }

                if (MathF.Abs(angleX - 270) < minAngleX)
                {
                    lerpToAngleX = 270;
                    minAngleX = MathF.Abs(angleX - 270);
                }

                if (MathF.Abs(angleX - 360) < minAngleX)
                {
                    lerpToAngleX = 0;
                }

                if (MathF.Abs(angleZ - 0) < minAngleZ)
                {
                    lerpToAngleZ = 0;
                    minAngleZ = MathF.Abs(angleZ - 0);
                }

                if (MathF.Abs(angleZ - 90) < minAngleZ)
                {
                    lerpToAngleZ = 90;
                    minAngleZ = MathF.Abs(angleZ - 90);
                }

                if (MathF.Abs(angleZ - 180) < minAngleZ)
                {
                    lerpToAngleZ = 180;
                    minAngleZ = MathF.Abs(angleZ - 180);
                }

                if (MathF.Abs(angleZ - 270) < minAngleZ)
                {
                    lerpToAngleZ = 270;
                    minAngleZ = MathF.Abs(angleZ - 270);
                }

                if (MathF.Abs(angleZ - 360) < minAngleZ)
                {
                    lerpToAngleZ = 0;
                }

                atackTransform.DOLocalRotate(new Vector3(lerpToAngleX, lerpToAngleY, lerpToAngleZ), .2f);
            }
        }
    }

    private void HandEmpty()
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

            try
            {
                currentBox = (Box)lastInteractive;
                labelPanel.SetActive(true);
            }
            catch
            {
                CharacterMovementWithHeadBobbing.isActive = false;
                labelGroup.EnableBillFill();
            }

            E_Carry = E_Carry.BOX;
            mouseLook.Active(false);
            lastInteractive.Interactive(atackTransform);
        }
    }

    public void BTN_LabelConfirm()
    {
        labelPanel.SetActive(false);
        atackTransform.GetChild(0).SetParent(afterlableParent, false);
        mouseLook.Active(true);
        E_Carry = E_Carry.LABEL;
        atackTransform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
