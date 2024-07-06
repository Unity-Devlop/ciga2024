using System;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    public class PlayerAnimCtrl : MonoBehaviour
    {
        public enum AnimState
        {
            Move,
            Idle
        }

        private SpriteRenderer _spriteRenderer;

        private Animator _animator;
        private Player _player;
        
        [field: SerializeField] public AnimState State { get; private set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _player = GetComponent<Player>();

        }

        private void Update()
        {
            switch (_player.direction)
            {
                case FacingDirection.Left:
                    _spriteRenderer.flipX = true;
                    break;
                case FacingDirection.Right:
                    _spriteRenderer.flipX = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Vector2 move = _player.Input.Move.ReadValue<Vector2>();
            if (move.sqrMagnitude > 0)
            {
                Debug.Log("Move");
                ToMove();
                return;
            }

            if (move == Vector2.zero && !_player.data.isDashing && _player.checker.isGrounded &&
                _player.rb2D.velocity.y == 0)
            {
                ToIdle();
                return;
            }

            // new Camera().GetOrthographicCameraRect();
        }

        private void ToIdle()
        {
            if (State == AnimState.Idle) return;
            _animator.Play("idle");
            State = AnimState.Idle;
        }

        private void ToMove()
        {
            if (State == AnimState.Move) return;
            _animator.Play("move");
            State = AnimState.Move;
        }
    }
}