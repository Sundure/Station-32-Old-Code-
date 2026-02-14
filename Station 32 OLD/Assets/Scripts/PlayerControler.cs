using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;

    [SerializeField] private Transform _bodyTransform;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _crouchHeight;
    private float _originalHeight;
    private float _currentHeight;
    private float _scaleToHeightRatio;

    [SerializeField] private float _crouchingSpeed = 3f;

    private bool _inCrouchingProcess;
    private bool _isCrouching;

    private void Awake()
    {
        _originalHeight = _bodyTransform.localScale.y;
        _currentHeight = _bodyTransform.localScale.y;
        _scaleToHeightRatio = _characterController.bounds.size.y / _bodyTransform.localScale.y;
    }

    private void Update()
    {
        float xSpeed = Input.GetAxis("Horizontal");
        float ySpeed = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * xSpeed + transform.forward * ySpeed;

        _characterController.Move(_moveSpeed * Time.deltaTime * moveDirection);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isCrouching = !_isCrouching;
            _inCrouchingProcess = true;
        }

        if (_inCrouchingProcess)
        {
            if (_isCrouching) // Crouching
            {
                print(_currentHeight);
                print(_crouchHeight);
                if (_currentHeight <= _crouchHeight)
                {
                    _currentHeight = _crouchHeight;
                    _inCrouchingProcess = false;
                    _bodyTransform.localScale = new Vector3(_bodyTransform.localScale.x, Mathf.Clamp(_currentHeight, _crouchHeight, _originalHeight), _bodyTransform.localScale.z);
                    return;
                }
                float changedDistance = 0;
                changedDistance -= _crouchingSpeed * Time.deltaTime;

                _currentHeight += changedDistance;

                _bodyTransform.localScale = new Vector3(_bodyTransform.localScale.x, Mathf.Clamp(_currentHeight, _crouchHeight, _originalHeight), _bodyTransform.localScale.z);
                _characterController.Move(new Vector3(0, changedDistance * _scaleToHeightRatio / 2, 0));
            }
            else // Rising
            {
                if (_currentHeight >= _originalHeight)
                {
                    _currentHeight = _originalHeight;
                    _inCrouchingProcess = false;
                    _bodyTransform.localScale = new Vector3(_bodyTransform.localScale.x, Mathf.Clamp(_currentHeight, _crouchHeight, _originalHeight), _bodyTransform.localScale.z);
                    return;
                }
                float changedDistance = 0;
                changedDistance += _crouchingSpeed * Time.deltaTime;

                _currentHeight += changedDistance;

                _bodyTransform.localScale = new Vector3(_bodyTransform.localScale.x, Mathf.Clamp(_currentHeight, _crouchHeight, _originalHeight), _bodyTransform.localScale.z);
                _characterController.Move(new Vector3(0, changedDistance * _scaleToHeightRatio / 2, 0));
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {

        }

    }
}
