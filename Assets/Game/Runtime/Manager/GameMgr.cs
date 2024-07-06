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
            this.Reborn = reborn;
        }

    }
}