using UnityEngine;

// Required component will automatically be added to Game Object
[RequireComponent(typeof(PlayerMovement))]
public class PlayerMechanism : MonoBehaviour
{
    // Allows private variable to be accessed from Unity Inespector
    [SerializeField]
    private float playerSpeed = 5f;

    [SerializeField]
    private float mouseSensitivity = 1f;

    [SerializeField]
    private float jumpPower = 100f;

    private PlayerMovement playerMovement;
    private ConfigurableJoint confJoint;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
        if (Input.GetButton("Jump"))
        {
            _jumpPower = Vector3.up * jumpPower;
        }

        playerMovement.SetJumpPower(_jumpPower);

    }
}