using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public abstract class BaseControl : IControl
    {
        protected Player _player;
        protected Rigidbody2D _rb2D => _player.rb2D;
        protected CustomInputActions.PlayerActions Input => _player.Input;

        protected PlayerData data => _player.data;
        protected PhysicsChecker _checker => _player.checker;

        public void Set(Player player)
        {
            _player = player;
        }

        public virtual void Update()
        {
            Fire();
            Move();
            Accelerate();
            Jump();
            Dash();
        }

        protected virtual void Fire()
        {
        }

        protected virtual void Move()
        {
            if (!data.canMove) return;

            Vector2 move = Input.Move.ReadValue<Vector2>();
            _rb2D.velocity = new Vector2(move.x * data.moveSpeed, _rb2D.velocity.y);
        }

        protected virtual void Jump()
        {
            if (!data.canJump) return;
            if (Mathf.Approximately(Input.Jump.ReadValue<float>(), 1) && _checker.isGrounded)
            {
                Vector2 velocity = _rb2D.velocity;
                velocity.y = data.jumpSpeed;
                _rb2D.velocity = velocity;
            }
        }

        protected virtual void Dash()
        {
        }


        protected virtual void Accelerate()
        {
            if (!data.canAccelerate) return;
            if (Mathf.Approximately(Input.Accelerate.ReadValue<float>(), 1) && data.HasEnergy)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x * data.accelerateMultiplier, _rb2D.velocity.y);
                data.ChangeEnergy(Time.deltaTime * -1 * data.accelerateCost);
            }
        }

        protected async void DashLock(float time)
        {
            data.canMove = false;
            data.canJump = false;
            data.canAccelerate = false;
            await UniTask.Delay(TimeSpan.FromSeconds(time));
            data.canMove = true;
            data.canJump = true;
            data.canAccelerate = true;
        }

        protected async void DashCoolDown(float time)
        {
            data.canDash = false;
            await UniTask.Delay(TimeSpan.FromSeconds(time));
            data.canDash = true;
        }
    }
}