using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        private CustomInputActions _customInput;
        public CustomInputActions.PlayerActions Input => _customInput.Player;

        private PhysicsChecker _checker;

        [field: SerializeField] public PlayerData data { get; private set; }


        private void Awake()
        {
            _checker = GetComponent<PhysicsChecker>();
            _rb2D = GetComponent<Rigidbody2D>();
            _customInput = new CustomInputActions();
        }


        private void Update()
        {
            Move();
            Accelerate();
            Jump();
        }

        private void Accelerate()
        {
            if (Mathf.Approximately(Input.Accelerate.ReadValue<float>(), 1))
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x * data.accelerateMultiplier, _rb2D.velocity.y);
            }
        }

        private void Move()
        {
            Vector2 move = Input.Move.ReadValue<Vector2>();
            _rb2D.velocity = new Vector2(move.x * GetMoveSpeed(), _rb2D.velocity.y);
        }

        private void Jump()
        {
            if (Input.Jump.triggered && CanJump())
            {
                _rb2D.AddForce(Vector2.up * data.jumpSpeed, ForceMode2D.Impulse);
            }
        }

        private bool CanJump()
        {
            return _checker.isGrounded;
        }

        public float GetMoveSpeed()
        {
            return data.moveSpeed;
        }

        public void OnEnable()
        {
            _customInput.Enable();
        }

        private void OnDisable()
        {
            _customInput.Disable();
        }
    }
}