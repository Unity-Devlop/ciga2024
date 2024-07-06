using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class ForwardControl : IControl
    {
        private Player _player;
        private Rigidbody2D _rb2D => _player.rb2D;
        private CustomInputActions.PlayerActions Input => _player.Input;

        private PlayerData data => _player.data;
        private PhysicsChecker _checker => _player.checker;

        public void Set(Player player)
        {
            _player = player;
        }

        public void Update()
        {
            Move();
            Accelerate();
            Jump();
            Dash();
        }

        private void Move()
        {
            if (!data.canMove) return;
            _rb2D.velocity = new Vector2(data.fixedMoveSpeed, _rb2D.velocity.y);
        }

        private void Jump()
        {
            if (!data.canJump) return;
            if (Input.Jump.triggered && _checker.isGrounded)
            {
                _rb2D.AddForce(Vector2.up * data.jumpSpeed, ForceMode2D.Impulse);
            }
        }

        private void Dash()
        {
            if (!data.canDash) return;
            Vector2 faceDir = Input.Move.ReadValue<Vector2>();
            if (faceDir == Vector2.zero)
            {
                faceDir = Vector2.right; // TODO 朝向
            }

            if (faceDir.y > 0)
            {
                faceDir = new Vector2(1, 1).normalized;
            }

            if (Input.Dash.triggered && data.HasEnergy && data.canDash)
            {
                _rb2D.AddForce(faceDir * data.dashSpeed, ForceMode2D.Impulse);
                data.ChangeEnergy(-data.dashEnergyCost);

                // 禁止移动一会
                DashWait(data.dashTime);
            }
        }

        private async void DashWait(float time)
        {
            data.canMove = false;
            data.canJump = false;
            data.canAccelerate = false;
            data.canDash = false;
            await UniTask.Delay(TimeSpan.FromSeconds(time));
            data.canMove = true;
            data.canJump = true;
            data.canAccelerate = true;
            data.canDash = true;
        }

        private void Accelerate()
        {
            if (!data.canAccelerate) return;
            if (Mathf.Approximately(Input.Accelerate.ReadValue<float>(), 1) && data.HasEnergy)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x * data.accelerateMultiplier, _rb2D.velocity.y);
                data.ChangeEnergy(Time.deltaTime * -1 * data.accelerateCost);
            }
        }
    }
}