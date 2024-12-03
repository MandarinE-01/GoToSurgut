using Fusion;
using Fusion.Addons.Physics;
using System.Collections;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    public Camera CameraPlayer;

    [Header("Movement")]
    [SerializeField] private float _playerSpeed = 6f;
    [SerializeField] private float _jumpForce = 5f;

    [SerializeField] private float _delayMove = 3f;
    [SerializeField] private float _runSpeed = 1.3f;

    [Header("Player")]
    [SerializeField] private GameObject _head;

    private Rigidbody _rigidbody;
    private bool _canJump = true;
    private bool _isJumping = false;
    private bool _delayMoveBool;
    Vector3 move;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            CameraPlayer = Camera.main;
            CameraPlayer.GetComponent<MoveCamera>().PlayerTarget = transform;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump == true)
        {
            _isJumping = false;
        }
    }
    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        Quaternion cameraRotationY = Quaternion.Euler(0, CameraPlayer.transform.rotation.eulerAngles.y, 0);
        transform.rotation = cameraRotationY;
        float _verticalInput = Input.GetAxis("Vertical");
        float _horizontalInput = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(_horizontalInput, 0, _verticalInput) * _playerSpeed * Runner.DeltaTime;
        if (!_isJumping)
        {
            _rigidbody.AddForce(new Vector3(0, _jumpForce, 0));
            _isJumping = true; 
        }
        transform.Translate(move);
    }

    private void OnTriggerEnter(Collider other)
    {
        _canJump = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _canJump = false;
    }
}
