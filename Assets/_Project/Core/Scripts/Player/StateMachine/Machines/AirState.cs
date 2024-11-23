using UnityEngine;
using UnityHFSM;

namespace DemoProject.Player
{
    public class AirState : StateMachine<PlayerStateId, PlayerStateTriggerEvent>
    {
        public PlayerContext Context;
        
        public AirState(PlayerContext ctx)
        {
            Context = ctx;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Enter Air State");
        }
    }
}