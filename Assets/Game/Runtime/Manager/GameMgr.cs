using System.IO;
using NodeCanvas.DialogueTrees;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityToolkit;

namespace Game
{
    public class GameMgr : MonoSingleton<GameMgr>
    {
        [field: SerializeField] public Player Local { get; private set; }

        public IReborn Reborn { get; private set; }


        public static SystemLocator Systems => Singleton._systemLocator;
        private SystemLocator _systemLocator;

        protected override void OnInit()
        {
            _systemLocator = new SystemLocator();
            _systemLocator.Register<ConditionalVisualSystem>();


         
            string jsonStr = "";
            if (File.Exists(Const.LocalPlayerDataPath))
            {
                jsonStr = File.ReadAllText(Const.LocalPlayerDataPath);
                var data = JsonConvert.DeserializeObject<PlayerData>(jsonStr);
                Local.data = data;
            }
            else
            {
                // TODO 默认存档
            }
            
            Local.gameObject.SetActive(true);

            
            Reborn = GameObject.FindGameObjectWithTag("DefaultReborn").GetComponent<IReborn>();
            Reborn.Active();
            
            GameHUDPanel gameHUDPanel = UIRoot.Singleton.OpenPanel<GameHUDPanel>();
            gameHUDPanel.Bind(Local.data);
        }

        protected override void OnDispose()
        {
            if (UIRoot.SingletonNullable == null) return;
            if (UIRoot.Singleton.GetOpenedPanel(out GameHUDPanel hudPanel))
            {
                hudPanel.UnBind();
            }

            UIRoot.Singleton.ClosePanel<GameHUDPanel>();
        }

        public void OnObstacleEnter(Player player, IObstacle obstacle)
        {
            if (!player.data.infiniteHealth)
            {
                player.data.ChangeHealth(-1);
                if (player.data.Health <= 0)
                {
                    player.SetVelocity(Vector2.zero, 0);
                    // 重生
                    player.data.ChangeHealth(player.data.MaxHealth);

                    player.transform.position = Reborn.transform.position;
                }
            }
            else
            {
            }
        }

        public void OnObstacleExit(Player component, IObstacle obstacle)
        {
        }

        public void OnRebornHit(Player player, IReborn reborn)
        {
            Reborn.UnActive();
            Reborn = reborn;
            Reborn.Active();
        }

        public void OnEnergyObjectEnter(Player component, EnergyObject energyObject)
        {
            component.data.ChangeEnergy(energyObject.energyPoint);
            Destroy(energyObject.gameObject);
        }

        [SerializeField] private DialoguePanel _dialoguePanel;

        public void StartDialog(DialogueTreeController dialogueTreeController)
        {
            _dialoguePanel.gameObject.SetActive(true);
            Local.DisableInput();
            dialogueTreeController.StartDialogue((v) =>
            {
                Local.EnableInput();
                _dialoguePanel.gameObject.SetActive(false);
            });
        }

        public void OnRecoverDash(Player component, RecoverDashObject recoverDashObject)
        {
            component.Control.RecoverDash();
            recoverDashObject.CollDown();
        }

        public void OnFloatText(Player component, FloatTextTrigger floatTextTrigger)
        {
        }
        
        
        // 重置玩家存档
        public void ResetPlayerData()
        {
            if(File.Exists(Const.LocalPlayerDataPath))
                File.Delete(Const.LocalPlayerDataPath);
        }
        
        public void SavePlayerData()
        {
            string jsonStr = JsonConvert.SerializeObject(Local.data);
            File.WriteAllText(Const.LocalPlayerDataPath, jsonStr);
        }
    }
}