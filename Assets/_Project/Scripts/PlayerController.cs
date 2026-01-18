using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 25f;
    [SerializeField] private float _jumpForce = 5f;

    private float _horizontalMovement;
    private float _verticalMovement;
    private Vector3 _direction;

    private bool _isSprinting;
    private bool _isJumping;
    private bool _canDoubleJump;

    private Rigidbody _rigidbody;
    private GroundCheck _groundCheck;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
    }

    private void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        _direction = new Vector3(_horizontalMovement, 0f, _verticalMovement).normalized;

        _isSprinting = Input.GetKey(KeyCode.LeftShift);
        _isJumping = Input.GetButtonDown("Jump");

        if (_isJumping)
        {
            if (_groundCheck.IsGrounded)
            {
                Debug.Log("Salto");
                Jump();
                _canDoubleJump = true;
            }
            else if (_canDoubleJump)
            {
                Debug.Log("Doppio salto");
                Jump();
                _canDoubleJump = false;
            }
        }
        Debug.Log(_groundCheck.IsGrounded);
    }

    private void FixedUpdate()
    {
        if (_direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.Lerp(transform.forward, _direction, _rotationSpeed * Time.fixedDeltaTime));

            if (_isSprinting)
                _rigidbody.MovePosition(transform.position + _direction * (2 * _speed * Time.fixedDeltaTime));
            else
                _rigidbody.MovePosition(_rigidbody.position + _direction * (_speed * Time.deltaTime));
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _isJumping = false;
    }
}
