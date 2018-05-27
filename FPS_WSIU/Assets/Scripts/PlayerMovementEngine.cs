using UnityEngine;

// Required component will automatically be added to Game Object
[RequireComponent(typeof(PlayerMovement))]
public class PlayerMovementEngine : MonoBehaviour
{
    // Allows private variable to be accessed from Unity Inespector
    [SerializeField]
    private float playerSpeed = 5f;

    [SerializeField]
    private float mouseSensitivity = 1f;

    [SerializeField]
    private float jumpForce = 100f;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private CapsuleCollider playerColider;

    private PlayerMovement playerMovement;
    private ConfigurableJoint confJoint;
    
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerColider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        // Calculate movement velocity
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * playerSpeed;
        playerMovement.SetVelocity(_velocity);

        // Calculate rotation as a 3D vector for turning only
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotationY = new Vector3(0f, _yRot, 0f) * mouseSensitivity;
        playerMovement.SetRotation(_rotationY);

        float _xRot = Input.GetAxisRaw("Mouse Y");
        float _rotationX =  _xRot * mouseSensitivity;
        playerMovement.SetCameraRotation(_rotationX);

        // Calculate jump power
        Vector3 _jumpPower = Vector3.zero;
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _jumpPower = Vector3.up * jumpForce;
        }
        playerMovement.SetJumpPower(_jumpPower);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(playerColider.center, new Vector3(playerColider.bounds.center.x,
                                                               playerColider.bounds.min.y,
                                                               playerColider.bounds.center.z),
                                                               playerColider.radius * .9f, 
                                                               groundLayer);
        
    }

}