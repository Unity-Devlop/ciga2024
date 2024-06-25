using UnityEngine.SceneManagement;
using UnityToolkit;

namespace Game
{
    public class GameFlow : ISystem
    {

        public void OnInit()
        {
            
        }
        public void Dispose()
        {
        }

        public void EnterGame()
        {
            SceneManager.LoadScene("Game");
        }
        
        public void EnterMain()
        {
            SceneManager.LoadScene("Main");
        }
    }
}