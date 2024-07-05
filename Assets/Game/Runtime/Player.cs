using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        private CustomInputActions _customInput;
        private CustomInputActions.PlayerActions _input => _customInput.Player;

        private bool _canJump = true;
        
        public float forwardSpeed = 10f;
        private void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _customInput = new CustomInputActions();
        }

        private void Update()
        {
            Vector2 velocity = _rb2D.velocity;
            velocity.x = forwardSpeed;
            _rb2D.velocity = velocity;
            
            if(_input.Jump.triggered && _canJump)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _rb2D.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            _canJump = false;
        }

        public void OnEnable()
        {
            _customInput.Enable();
        }

        private void OnDisable()
        {
            _customInput.Disable();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _canJump = true;
        }
    }
}