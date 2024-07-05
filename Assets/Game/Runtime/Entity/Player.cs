using System;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour, IFish
    {
        private CustomInputActions _customInput;
        private CustomInputActions.PlayerActions Input => _customInput.Player;

        public Vector2 moveSpeed = new Vector2(10, 10);

        [field: SerializeField] public float curAngel { get; private set; }
        [field: SerializeField] public float eyeAngel { get; private set; }
        [field: SerializeField] public float radius { get; private set; }


        private void Awake()
        {
            _customInput = new CustomInputActions();
        }

        private void Update()
        {
            var move = Input.Move.ReadValue<Vector2>();
            if (move == Vector2.zero) return;
            var moveDelta = move * moveSpeed * Time.deltaTime;
            transform.position += new Vector3(moveDelta.x, moveDelta.y, 0);

            // lerp to target angle
            var targetAngle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            curAngel = Mathf.LerpAngle(curAngel, targetAngle, 0.1f);
            transform.rotation = Quaternion.Euler(0, 0, curAngel);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IFish fish))
            {
                fish.OnBeCaught();
                CaughtOther(fish);
            }
        }

        private void OnEnable()
        {
            _customInput.Enable();
        }

        private void OnDisable()
        {
            _customInput.Disable();
        }


        public void CaughtOther(IFish other)
        {
            Debug.Log("I caught a fish!");
            transform.localScale *= 1.1f;
        }

        public void OnBeCaught()
        {
        }
    }
}