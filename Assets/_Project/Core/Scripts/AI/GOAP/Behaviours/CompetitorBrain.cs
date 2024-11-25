using System;
using CrashKonijn.Goap.Behaviours;
using DemoProject.AI.Goals;
using UnityEngine;

namespace DemoProject.AI.Behaviours
{
    public class CompetitorBrain : MonoBehaviour
    {
        public AgentBehaviour AgentBehaviour;

        private void Start()
        {
            SetInitialGoal();
        }

        public void SetInitialGoal()
        {
            AgentBehaviour.ClearGoal();
            AgentBehaviour.SetGoal<ReachPlatformEndGoal>(endAction: false);
        }
    }
}