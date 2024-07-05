using System;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D _rb2D;
        private CustomInputActions _customInput;
        private CustomInputActions.PlayerActions _input => _customInput.Player;


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