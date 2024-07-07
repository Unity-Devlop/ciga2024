using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public Rigidbody2D rb2D { get; private set; }
        public PhysicsChecker checker { get; private set; }
        [field: SerializeField] public PlayerData data;

        private CustomInputActions _customInput;
        public CustomInputActions.PlayerActions Input => _customInput.Player;
        [field: SerializeField] public PlayerState State { get; private set; } = PlayerState.NormalControl;

        public FacingDirection direction;
        private NormalControl _normalControl;
        private ForwardControl _forwardControl;

        public ParticleSystem dashEffect;
        public ParticleSystem jumpEffect;

        public AudioSource rainSfx;

        public AudioSource dashSfx;

        public AudioSource jumpSfx;

        public AudioSource normalBgm;

        public AudioSource forwardBgm;
        
        public AudioSource deadSfx;

        private Animator _animator;

        public IControl Control
        {
            get
            {
                switch (State)
                {
                    case PlayerState.NormalControl:
                        return _normalControl;
                    case PlayerState.ForwardControl:
                        return _forwardControl;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        private void Awake()
        {
            checker = GetComponent<PhysicsChecker>();
            rb2D = GetComponent<Rigidbody2D>();
            _customInput = new CustomInputActions();

            _animator = GetComponent<Animator>();

            _normalControl = new NormalControl();
            _forwardControl = new ForwardControl();

            _normalControl.Set(this);
            _forwardControl.Set(this);
        }


        private void Update()
        {
            data.isGounded = checker.isGrounded;
            if (State == PlayerState.NormalControl)
            {
                if (forwardBgm.isPlaying)
                {
                    forwardBgm.Stop();
                }

                if (!normalBgm.isPlaying)
                {
                    normalBgm.Play();
                }

                _normalControl.Update();
                return;
            }

            if (State == PlayerState.ForwardControl)
            {
                if (!forwardBgm.isPlaying)
                {
                    forwardBgm.Play();
                }

                if (normalBgm.isPlaying)
                {
                    normalBgm.Stop();
                }

                _forwardControl.Update();
            }
        }

        public void OnEnable()
        {
            rainSfx.Play();
            _customInput.Enable();
        }

        private void OnDisable()
        {
            rainSfx.Stop();
            _customInput.Disable();
        }

        public void SetState(PlayerState state)
        {
            State = state;
        }

        public void SetVelocity(Vector2 velocity, float angularVelocity = 0)
        {
            rb2D.velocity = velocity;
            rb2D.angularVelocity = angularVelocity;
        }

        public void DisableInput()
        {
            Input.Disable();
        }

        public void EnableInput()
        {
            Input.Enable();
        }

        public void OnDeadReborn()
        {
            deadSfx.Play();
        }
    }
}