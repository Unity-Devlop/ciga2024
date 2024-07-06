using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public enum PlayerState
    {
        NormalControl,
        ForwardControl,
    }

    public class Player : MonoBehaviour
    {
        public Rigidbody2D rb2D { get; private set; }
        public PhysicsChecker checker { get; private set; }
        [field: SerializeField] public PlayerData data { get; private set; }

        private CustomInputActions _customInput;
        public CustomInputActions.PlayerActions Input => _customInput.Player;
        [field: SerializeField] public PlayerState State { get; private set; } = PlayerState.NormalControl;
        public NormalControl NormalControl;
        public ForwardControl ForwardControl;

        private void Awake()
        {
            checker = GetComponent<PhysicsChecker>();
            rb2D = GetComponent<Rigidbody2D>();
            _customInput = new CustomInputActions();

            NormalControl = new NormalControl();
            ForwardControl = new ForwardControl();

            NormalControl.Set(this);
            ForwardControl.Set(this);
        }


        private void Update()
        {
            if (State == PlayerState.NormalControl)
            {
                NormalControl.Update();
                return;
            }

            if (State == PlayerState.ForwardControl)
            {
                ForwardControl.Update();
            }
        }

        public void OnEnable()
        {
            _customInput.Enable();
        }

        private void OnDisable()
        {
            _customInput.Disable();
        }

        public void SetVelocity(Vector2 velocity,float angularVelocity = 0)
        {
            rb2D.velocity = velocity;
            rb2D.angularVelocity = angularVelocity;
        }
    }
}