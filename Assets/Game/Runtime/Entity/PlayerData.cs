using System;
using UnityEngine;
using UnityToolkit;

namespace Game
{
    // [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
    [Serializable]
    public class PlayerData //: ScriptableObject
    {
        public BindData<PlayerData> Bind { get; private set; }


        public float moveSpeed = 10f;
        public float jumpSpeed = 10f;
        public float accelerateMultiplier = 2f;


        [field: SerializeField] public int Health { get; private set; } = 1;
        [field: SerializeField] public int MaxHealth { get; private set; } = 1;
        [field: SerializeField] public float MaxEnergy { get; private set; } = 100;
        [field: SerializeField] public float CurrentEnergy { get; private set; } = 100;
        

        public bool HasEnergy => CurrentEnergy > 0;

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