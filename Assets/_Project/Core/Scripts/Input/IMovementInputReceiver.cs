using UnityEngine;

namespace DemoProject.Input
{
    public interface IMovementInputReceiver
    {
        void SetInput(Vector3 input, InputEvent @event);
    }
    
    public enum InputEvent
    {
        Start,
        Perform,
        Cancel
    }
}