using Fusion;
using System.Collections;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    public Camera CameraPlayer;

    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _playerSpeed = 6f;
    [SerializeField] private float _junpForce = 5f;


    private CharacterController _cController;
    private bool _isJumping = false;
    private Vector3 _velocity;
    private bool _canMove;

    private void Awake()
    {
        _cController = GetComponent<CharacterController>();
    }
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            CameraPlayer = Camera.main;
            CameraPlayer.GetComponent<MoveCamera>().PlayerTarget = transform;
            StartCoroutine(WaitForStart());
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false && _cController.isGrounded)
        {
            _isJumping = true;
        }
    }
    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (!_canMove) return;
        if (_cController.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }
        Quaternion cameraRotationY = Quaternion.Euler(0, CameraPlayer.transform.rotation.eulerAngles.y, 0);
        Vector3 move = cameraRotationY * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * _playerSpeed;

        _velocity.y += -_gravity * Runner.DeltaTime;
        if (_isJumping && _cController.isGrounded)
        {
            _velocity.y += _junpForce;
            _isJumping = false;
        }
        _cController.Move(move + _velocity * Runner.DeltaTime);
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForFixedUpdate();
        _canMove = true;
    }
}
