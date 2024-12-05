using Fusion;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    public Camera CameraPlayer;


    [SerializeField] private float _gravity = 9.81f;
    [SerializeField] private float _playerSpeed = 6f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _runSpeed = 1.5f;
    [SerializeField] private GameObject _head;

    [Header("Stamina")]
    [SerializeField] private float _stamin = 10.0f;
    [SerializeField] private float _delayToRecovery = 1.5f;
    [SerializeField] private float _spendStaminToRun = 1.2f;
    [SerializeField] private float _spendStaminToJump = 2.3f;
    bool canSpend = true;

    private CharacterController _cController;
    private bool _isJumping = false;
    private Vector3 _velocity;
    private bool _canMove;
    private float _staminMax;

    private void Awake()
    {
        _cController = GetComponent<CharacterController>();
        _staminMax = _stamin;
    }
    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            CameraPlayer = Camera.main;
            CameraPlayer.GetComponent<MoveCamera>().PlayerTarget = _head.transform;
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
        if (Input.GetKey(KeyCode.LeftShift) && canSpend)
        {
            move *= _runSpeed;
        }
        transform.rotation = cameraRotationY;
        _velocity.y += -_gravity * Runner.DeltaTime;
        if (_isJumping && _cController.isGrounded && _stamin >= _spendStaminToJump)
        {
            _velocity.y += _jumpForce;
            SpendStamin(true);
            _isJumping = false;
        }
        SpendStamin(false);
        _cController.Move(move + _velocity * Runner.DeltaTime);
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForFixedUpdate();
        _canMove = true;
    }
    IEnumerator BlockStamin()
    {
        yield return new WaitForSeconds(_delayToRecovery);
        while (_stamin >= _staminMax / 4)
        {
            _stamin += Runner.DeltaTime;
        }
        yield return null;
    }
    private bool SpendStamin(bool isJumping)
    {
        if (_stamin <= 0)
        {
            canSpend = false;
            _stamin = 0.001f;
            StartCoroutine(BlockStamin());
        }
        if (canSpend)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _stamin -= _spendStaminToRun * Runner.DeltaTime;
                return true;
            }
            else if (isJumping)
            {
                _stamin -= _spendStaminToJump;
                return true;
            }
            else
            {
                _stamin += Runner.DeltaTime;
                if (_stamin >= _staminMax)
                {
                    _stamin = _staminMax;
                }
            }
        }
        return false;
    }
}


