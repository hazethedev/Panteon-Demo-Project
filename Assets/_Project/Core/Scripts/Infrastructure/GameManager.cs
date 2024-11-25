using DemoProject.Misc;
using UnityEngine.SceneManagement;

namespace DemoProject.Infrastructure
{
    public class GameManager : Singleton<GameManager>, ISceneService
    {
        public void ChangeScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
}