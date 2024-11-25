using UnityEngine;

namespace DemoProject.EventSystem
{
    public class PlayerDeadEventListener : GameEventListenerBase<Transform, PlayerDeadEvent, UnityTransformEvent> { }
}