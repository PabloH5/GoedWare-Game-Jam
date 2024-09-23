using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
        private PlayerControllers _playerControllers;
        
        public Vector2 direction;
    
        private Rigidbody2D rb;
        [SerializeField] private PlayerInput _playerInput;
        
        
        [SerializeField] private float velMove;

        private float _verticalMovement;
        private float _horizontalMovement;

        private void Awake()
        {
            _playerControllers = new ();
            rb = GetComponent<Rigidbody2D>();
        }
        private void OnEnable()
        {
            _playerControllers.Enable();
            _playerControllers.PlayerMovement.MovementHorizontal.started += StartMoveHorizontal;
            _playerControllers.PlayerMovement.MovementHorizontal.canceled += CancelMoveHorizontal;
            
            _playerControllers.PlayerMovement.MovementVertical.started += StartMoveVertical;
            _playerControllers.PlayerMovement.MovementVertical.canceled += CancelMoveVertical;
            
        }
        
        private void OnDisable()
        {
            _playerControllers.Disable();
            _playerControllers.PlayerMovement.MovementHorizontal.started -= StartMoveHorizontal;
            _playerControllers.PlayerMovement.MovementHorizontal.canceled -= CancelMoveHorizontal;
            
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
        private void UpdateDirection()
        {
            direction = new Vector2(_horizontalMovement, _verticalMovement);
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

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(direction.x * velMove, direction.y * velMove);
        }
}