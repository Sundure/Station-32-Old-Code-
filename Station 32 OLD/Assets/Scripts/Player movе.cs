using UnityEngine;

public class Playermovment : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _playerHeight;
    private float _groundDrag;
    [SerializeField] private LayerMask _whatIsGround;
    private bool grounded;
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _scale;
    private float _horizontalInput;
    private float _verticalInput;
    [SerializeField] private Rigidbody _rb;
    Vector3 _moveDirection;
    public bool _crouch;
    private float _decarseSpeed = 1f;
    [SerializeField] private bool _canStay;
    public bool Run;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }
    private void Update()
    {
        SpeedControl();
        grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);

        MyInput();

        if (grounded)
        {
            _rb.linearDamping = _groundDrag;
        }
        else
        {
            _rb.linearDamping = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            RaycastHit hit;
            float raycastDistance = 1.5f;
            Vector3 raycastDirection = Vector3.up;

            if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastDistance) && _crouch == true)
            {
                _canStay = false;
            }
            else
            {
                _canStay = true;
            }

            if (_canStay == true)
            {
                _crouch = !_crouch;
            }
        }
        if (_crouch == true)
        {
            _moveSpeed = 1.5f;
            Vector3 _currentScale = transform.localScale;

            _currentScale.y -= _decarseSpeed * Time.deltaTime;

            _currentScale.y = Mathf.Max(_currentScale.y, 0.5f);
            transform.localScale = _currentScale;
        }
        else if (_canStay == true)
        {
            _moveSpeed = 3;
            Vector3 _currentScale = transform.localScale;

            _currentScale.y += _decarseSpeed * Time.deltaTime;

            _currentScale.y = Mathf.Clamp(_currentScale.y, 0.5f, 1f);
            transform.localScale = _currentScale;
        }

        if (Input.GetKey(KeyCode.LeftShift) && _crouch == false)
        {
            _moveSpeed = 5f;
            Run = true;
        }
        else
        {
            Run = false;
        }
        PlayerMove();
    }
    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void PlayerMove()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
        _rb.AddForce(10f * _moveSpeed * _moveDirection.normalized, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        if (flatVel.magnitude > _moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
        }
    }
}
