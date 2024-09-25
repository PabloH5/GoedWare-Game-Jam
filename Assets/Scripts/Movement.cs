using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
        private PlayerControllers _playerControllers;
        
        private Vector2 _direction;
        private float _verticalMovement;
        private float _horizontalMovement;
    
        private Rigidbody2D rb;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] Animator _animator;
        
        private float _velMove;
        private Vector2 _speed;

        [SerializeField] private PlayerStats _playerStats;

        private void Awake()
        {
            _playerControllers = new ();
            rb = GetComponent<Rigidbody2D>();
            _velMove = _playerStats.velMove;
        }
        //Subscribe to the movements methods
        private void OnEnable()
        {
            _playerControllers.Enable();
            //This for horizontal
            _playerControllers.PlayerMovement.MovementHorizontal.started += StartMoveHorizontal;
            _playerControllers.PlayerMovement.MovementHorizontal.canceled += CancelMoveHorizontal;
            //This for vertical
            _playerControllers.PlayerMovement.MovementVertical.started += StartMoveVertical;
            _playerControllers.PlayerMovement.MovementVertical.canceled += CancelMoveVertical;
            
        }
        //Unsubscribe to the movements methods
        private void OnDisable()
        {
            _playerControllers.Disable();
            //This for horizontal
            _playerControllers.PlayerMovement.MovementHorizontal.started -= StartMoveHorizontal;
            _playerControllers.PlayerMovement.MovementHorizontal.canceled -= CancelMoveHorizontal;
            //This for vertical
            _playerControllers.PlayerMovement.MovementVertical.started -= StartMoveVertical;
            _playerControllers.PlayerMovement.MovementVertical.canceled -= CancelMoveVertical;
            
        }
        
        private void StartMoveHorizontal(InputAction.CallbackContext context)
        {
            _horizontalMovement = context.ReadValue<float>();
            UpdateDirection();
        }
        private void StartMoveVertical(InputAction.CallbackContext context)
        {
            _verticalMovement = context.ReadValue<float>();
            UpdateDirection();
        }
        private void CancelMoveVertical(InputAction.CallbackContext context)
        {
            _verticalMovement = 0f;
            UpdateDirection();
        }
        private void CancelMoveHorizontal(InputAction.CallbackContext context)
        {
            _horizontalMovement = 0f;
            UpdateDirection();
        }
        private void UpdateDirection()
        {
            _direction = new Vector2(_horizontalMovement, _verticalMovement);
        }
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(_direction.x * _velMove, _direction.y * _velMove);
            _speed = new Vector2(_direction.x, _direction.y).normalized;
            _animator.SetFloat("Horizontal", _direction.x);
            _animator.SetFloat("Vertical", _direction.y);
            _animator.SetFloat("Speed", _speed.sqrMagnitude);
            
            
        }
}