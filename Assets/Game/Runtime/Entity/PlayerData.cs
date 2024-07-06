using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityToolkit;

namespace Game
{
    // [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
    [Serializable]
    public class PlayerData //: ScriptableObject
    {
        public BindData<PlayerData> Bind { get; private set; }
        
        public float moveSpeed = 10f;
        public float fixedMoveSpeed = 10;
        
        public float jumpSpeed = 10f;
        public float accelerateMultiplier = 2f;


        public float dashEnergyCost = 10f;
        public float accelerateCost = 12f;
        public Vector2 dashSpeed = new Vector2(10, 5);

        [field: SerializeField] public int Health { get; private set; } = 1;
        [field: SerializeField] public int MaxHealth { get; private set; } = 1;
        [field: SerializeField] public float MaxEnergy { get; private set; } = 100;
        [field: SerializeField] public float CurrentEnergy { get; private set; } = 100;


        public bool HasEnergy => CurrentEnergy > 0;


        // Runtime State Property
        public bool canMove = true;
        public bool canDash = true;
        public bool canJump = true;
        public bool canAccelerate = true;
        public float dashTime = 2f;

        public PlayerData()
        {
            Bind = new BindData<PlayerData>(this);
        }

        public void ChangeEnergy(float value)
        {
            CurrentEnergy += value;
            CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
            Bind.SetDirty();
        }
    }
}