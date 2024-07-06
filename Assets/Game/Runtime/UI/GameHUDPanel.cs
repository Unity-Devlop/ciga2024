using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityToolkit;

namespace Game
{
    public class GameHUDPanel : UIPanel
    {
        // [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private ProgressBar energyBar;
        private PlayerData _data;

        public void Bind(PlayerData data)
        {
            _data = data;
            data.Bind.Listen(OnData);
            OnData(data);
        }

        private void OnData(PlayerData data)
        {
            // healthText.text = $"{data.Health}";
            energyBar.SetWithoutNotify(data.CurrentEnergy, 0, data.MaxEnergy);
        }

        public void UnBind()
        {
            _data.Bind.UnListen(OnData);
        }
    }
}