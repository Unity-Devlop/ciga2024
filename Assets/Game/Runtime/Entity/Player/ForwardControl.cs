using System;
using Cysharp.Threading.Tasks;
using NodeCanvas.Tasks.Actions;
using UnityEngine;

namespace Game
{
    public class ForwardControl : BaseControl
    {
        protected override void Move()
        {
            if (!data.canMove) return;
            _rb2D.velocity = new Vector2(data.fixedMoveSpeed, _rb2D.velocity.y);
        }

        protected override void Dash()
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
                _player.dashEffect.Play();
                _player.dashSfx.Play();
                _rb2D.AddForce(faceDir * data.dashSpeed, ForceMode2D.Impulse);
                data.ChangeEnergy(-data.dashEnergyCost);

                // 禁止移动一会
                DashLock(data.dashLock);
                DashCoolDown(data.dashCoolDown);
            }
        }

        protected override void UpdateAnim()
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

            if (!data.isDashing && _player.checker.isGrounded)
            {
                Debug.Log("ToMove");
                ToMove();
                return;
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
        }

        protected override void ChangeFacingDirection()
        {
            _player.direction = FacingDirection.Right;
        }
    }
}