using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;

    public float moveSpeed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    public float acceleration = 12f;
    public float deceleration = 16f;

    public float mouseSensitivity = 120f;
    public float cameraSmoothTime = 0.03f;

    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 currentMoveVelocity;

    private float xRotation = 0f;

    private Vector2 smoothedMouseDelta;
    private Vector2 mouseDeltaVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraTransform == null)
        {
            Camera cam = GetComponentInChildren<Camera>();

            if (cam != null)
                cameraTransform = cam.transform;
        }
    }

    void Update()
    {
        LookAround();
        MovePlayer();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject t = hit.gameObject;

        if (t != null &&
            t.name.Contains("Platform") &&
            t.GetComponent<PlatformParams>().IsNext)
        {
            PlatformGrid.instance.NextPlatform();
        }
    }

    void LookAround()
    {
        if (Mouse.current == null || cameraTransform == null)
            return;

        Vector2 rawMouseDelta = Mouse.current.delta.ReadValue();

        smoothedMouseDelta = Vector2.SmoothDamp(
            smoothedMouseDelta,
            rawMouseDelta,
            ref mouseDeltaVelocity,
            cameraSmoothTime);

        float mouseX =
            smoothedMouseDelta.x *
            mouseSensitivity *
            Time.deltaTime;

        float mouseY =
            smoothedMouseDelta.y *
            mouseSensitivity *
            Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

        cameraTransform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);
    }

    void MovePlayer()
    {
        if (Keyboard.current == null)
            return;

        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = 0f;
        float z = 0f;

        if (Keyboard.current.aKey.isPressed)
            x -= 1f;

        if (Keyboard.current.dKey.isPressed)
            x += 1f;

        if (Keyboard.current.wKey.isPressed)
            z += 1f;

        if (Keyboard.current.sKey.isPressed)
            z -= 1f;

        Vector3 desiredMove =
            transform.right * x +
            transform.forward * z;

        if (desiredMove.magnitude > 1f)
            desiredMove.Normalize();

        desiredMove *= moveSpeed;

        float smoothSpeed =
            desiredMove.magnitude > 0.01f
                ? acceleration
                : deceleration;

        currentMoveVelocity = Vector3.Lerp(
            currentMoveVelocity,
            desiredMove,
            smoothSpeed * Time.deltaTime);

        controller.Move(
            currentMoveVelocity * Time.deltaTime);

        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            isGrounded)
        {
            velocity.y = Mathf.Sqrt(
                jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(
            velocity * Time.deltaTime);
    }
}