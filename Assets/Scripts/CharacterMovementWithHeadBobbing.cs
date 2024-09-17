using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementWithHeadBobbing : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private CharacterController controller;
    private Vector3 velocity;

    // Head Bobbing variables
    public bool enableHeadBobbing = true;
    public float bobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    private float defaultYPos = 0;
    private float timer = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultYPos = transform.localPosition.y;
    }

    void Update()
    {
        // Di chuyển nhân vật
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Áp dụng trọng lực
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Head Bobbing
        if (enableHeadBobbing)
        {
            if (controller.velocity.magnitude > 0.1f && controller.isGrounded)
            {
                // Tính toán hiệu ứng bobbing
                timer += Time.deltaTime * bobbingSpeed;
                float newYPos = defaultYPos + Mathf.Sin(timer) * bobbingAmount;
                transform.localPosition = new Vector3(transform.localPosition.x, newYPos, transform.localPosition.z);
            }
            else
            {
                // Đặt vị trí đầu về lại khi đứng yên
                timer = 0;
                transform.localPosition = new Vector3(transform.localPosition.x, defaultYPos, transform.localPosition.z);
            }
        }
    }
}
