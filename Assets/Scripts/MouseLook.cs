using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;  

    float xRotation = 0f;
    float yRotation = 0f;

    private bool active = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(active)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation -= mouseX;
            //yRotation = Mathf.Clamp(Rotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void Active(bool active)
    {
        this.active = active;

        if(active)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
