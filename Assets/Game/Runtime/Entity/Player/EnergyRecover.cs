using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Player))]
    public class EnergyRecover : MonoBehaviour
    {
        private Player _player;
        private PlayerData _data => _player.data;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Update()
        {
            if (!_data.canRecoverEnergy) return;
            if(_data.CurrentEnergy >= _data.MaxEnergy) return;
            _data.ChangeEnergy(Time.deltaTime * _data.energyRecoverSpeed);
        }
    }
}