using CrashKonijn.Goap.Behaviours;
using UnityEngine;
using VContainer;

namespace DemoProject.AI.Behaviours
{
    public class GoapSetBinder : MonoBehaviour
    {
        public AgentBehaviour AgentBehaviour;
        #region Dependency Injection

        [Inject]
        private void Construct(GoapRunnerBehaviour goapRunner)
        {
            AgentBehaviour.GoapSet = goapRunner.GetGoapSet("CompetitorSet");
        }

        #endregion
    }
}