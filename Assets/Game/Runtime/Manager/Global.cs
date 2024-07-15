using cfg;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolkit;

namespace Game
{
    public class Global : MonoSingleton<Global>
    {
        // Fast Access
        public static SystemLocator Systems => Singleton._systemLocator;
        public static TypeEventSystem Event => Singleton._event;

        // public static Tables Tables => Singleton._tables;


        protected override bool DontDestroyOnLoad() => true;
        private SystemLocator _systemLocator;
        private TypeEventSystem _event;
        private Tables _tables;

        protected override void OnInit()
        {
            UIRoot.Singleton.UIDatabase.Loader = new ResourcesUILoader();

            _systemLocator = new SystemLocator();
            _systemLocator.Register<GameFlow>();

            // _tables = new Tables(Loader);
            _event = new TypeEventSystem();
        }

        protected override void OnDispose()
        {
            _systemLocator.Dispose();
            _event = null;
        }

        // private static JSONNode Loader(string name)
        // {
        //     var path = $"Tables/{name}";
        //     TextAsset asset = Resources.Load<TextAsset>(path);
        //     return JSON.Parse(asset.text);
        // }

        [Sirenix.OdinInspector.Button]
        private void ToGame()
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }
}