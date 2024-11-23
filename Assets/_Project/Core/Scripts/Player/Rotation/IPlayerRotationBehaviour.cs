using UnityEngine;

namespace DemoProject.Player
{
    public interface IPlayerRotationBehaviour
    {
        void Rotate(in Vector3 movementInput, float deltaTime);
    }
}