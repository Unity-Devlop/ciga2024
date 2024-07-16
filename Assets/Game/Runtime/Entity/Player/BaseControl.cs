using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    [Serializable]
    public abstract class BaseControl : IControl
    {
        protected Player _player;
        protected Rigidbody2D _rb2D => _player.rb2D;
        protected CustomInputActions.PlayerActions Input => _player.Input;

        protected PlayerData data => _player.data;
        protected PhysicsChecker _checker => _player.checker;

        private CancellationTokenSource _dashCoolDownCts;

        protected SpriteRenderer SpriteRenderer;

        protected Animator _animator;

        private string _idleAnim;
        private string _moveAnim;
        private string _jumpAnim;
        private string _dashAnim;

        public void Set(Player player)
        {
            SpriteRenderer = player.GetComponent<SpriteRenderer>();
            _animator = player.GetComponent<Animator>();
            _player = player.GetComponent<Player>();
            _player = player;

            var stateStage = player.data.stateStage;
            _idleAnim = $"idle{stateStage}";
            _moveAnim = $"move{stateStage}";
            _jumpAnim = $"jump{stateStage}";
            _dashAnim = $"dash{stateStage}";
        }

        public virtual void Update()
        {
            UpdateAnim();
            ReDashCheck();
            ChangeFacingDirection();
            Fire();
            Move();
            Accelerate();
            Jump();
            Dash();

            if (!data.isDashing)
            {
                _rb2D.gravityScale = 3;
            }
        }

        protected virtual void UpdateAnim()
        {
            if (_animator == null) return;
            switch (_player.direction)
            {
                case FacingDirection.Left:
                    SpriteRenderer.flipX = true;
                    break;
                case FacingDirection.Right:
                    SpriteRenderer.flipX = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            if (_player.data.isDashing)
            {
                ToDash();
                return;
            }

            if (!_player.checker.isGrounded)
            {
                ToJump();
                return;
            }

            Vector2 move = _player.Input.Move.ReadValue<Vector2>();
            if (move.sqrMagnitude > 0)
            {
                ToMove();
                return;
            }

            if (move == Vector2.zero && !_player.data.isDashing && _player.checker.isGrounded &&
                _player.rb2D.velocity.y <= 0)
            {
                ToIdle();
            }
        }

        protected void ToIdle()
        {
            _animator.Play(_idleAnim);
        }

        protected void ToMove()
        {
            _animator.Play(_moveAnim);
        }

        protected void ToJump()
        {
            _animator.Play(_jumpAnim);
        }

        protected void ToDash()
        {
            _animator.Play(_dashAnim);
        }

        protected void ReDashCheck()
        {
            if (_checker.isGrounded)
            {
                RecoverDash();
            }
        }

        protected virtual void ChangeFacingDirection()
        {
            Vector2 move = Input.Move.ReadValue<Vector2>();
            if (Mathf.Approximately(move.x, 0)) return;
            _player.direction = move.x > 0 ? FacingDirection.Right : FacingDirection.Left;
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
            _player.jumpEffect.Play();
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
            if (!data.HasEnergy && !data.infiniteEnergy) return;
            if (Mathf.Approximately(Input.Accelerate.ReadValue<float>(), 1) && data.HasEnergy)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x * data.accelerateMultiplier, _rb2D.velocity.y);
                data.ChangeEnergy(Time.deltaTime * -1 * data.accelerateCost);
            }
        }

        protected async void DashLock(float time)
        {
            data.isDashing = true;
            data.canMove = false;
            data.canJump = false;
            data.canAccelerate = false;
            _rb2D.gravityScale = 0;
            await UniTask.Delay(TimeSpan.FromSeconds(time));
            _rb2D.gravityScale = 3f;
            data.canMove = true;
            data.canJump = true;
            data.canAccelerate = true;
            data.isDashing = false;
        }

        protected async void DashCoolDown(float time)
        {
            _dashCoolDownCts?.Cancel();
            _dashCoolDownCts = new CancellationTokenSource();
            data.canDash = false;
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _dashCoolDownCts.Token);
            }
            catch (OperationCanceledException)
            {
            }

            data.canDash = true;
        }

        public void RecoverDash()
        {
            _dashCoolDownCts?.Cancel();
            data.canDash = true;
        }
    }
}