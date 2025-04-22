using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private InputManagerSO inputManager;
    [SerializeField] private Transform cameraTransform;

    [Header("Detección del suelo")]
    [SerializeField] private Transform foot;
    [SerializeField] private float radiusDetection;
    [SerializeField] private LayerMask whichIsGround;

    [Header("SFX")]
    [SerializeField] private AudioClip footstepSFX;

    private CharacterController controller;
    private AudioSource audioSource;
    private Vector3 direction;
    private Vector3 inputDirection;
    private Vector3 verticalVelocity;
    private readonly float gravityFactor = -9.81f;

    private void OnEnable()
    {
        inputManager.OnMovement += SetInputDirection;
        //inputManager.OnJump += Jump;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ApplyGravity();
    }

    private void SetInputDirection(Vector2 ctx)
    {
        inputDirection = new Vector3(ctx.x, 0, ctx.y);
    }

    private void Jump()
    {
        if (OnTheGround())
        {
            verticalVelocity.y = Mathf.Sqrt(-2 * gravityFactor * jumpHeight);
        }
    }

    private void Movement()
    {
        direction = cameraTransform.forward * inputDirection.z + cameraTransform.right * inputDirection.x;
        direction.y = 0;
        controller.Move(direction * movementSpeed * Time.deltaTime);
        if (direction.sqrMagnitude > 0)
        {
            RotateToDestiny();
        }
    }

    private void RotateToDestiny()
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    private void ApplyGravity()
    {
        if (OnTheGround() && verticalVelocity.y < 0)
        {
            verticalVelocity.y = 0;
        }
        verticalVelocity.y += gravityFactor * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        if (transform.position.y < -5)
        {
            inputManager.DisableControls();
            SceneManager.LoadScene("GameOver");
        }
    }

    private bool OnTheGround()
    {
        return Physics.CheckSphere(foot.position, radiusDetection, whichIsGround);
    }
}
