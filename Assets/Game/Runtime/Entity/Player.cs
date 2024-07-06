using System;
using Cysharp.Threading.Tasks;
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
            Dash();
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

        private void Move()
        {
            if (!data.canMove) return;

            Vector2 move = Input.Move.ReadValue<Vector2>();
            _rb2D.velocity = new Vector2(move.x * data.moveSpeed, _rb2D.velocity.y);
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
            Vector2 faceDir = Input.Move.ReadValue<Vector2>();
            if (faceDir == Vector2.zero)
            {
                faceDir = Vector2.right; // TODO 朝向
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