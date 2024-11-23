using DemoProject.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DemoProject.Infrastructure
{
    public class GameManager : MonoBehaviour, ISceneService
    {
        public void ChangeScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }

        public void TryRespawnPlayer(Transform playerTransform)
        {
            if (playerTransform.TryGetComponent<IResetHelper>(out var resetHelper))
            {
                CoroutineRunner.Instance.Run(GetInstanceID(), 1.5f, () => resetHelper.ResetAll());
            }
        }
    }
}