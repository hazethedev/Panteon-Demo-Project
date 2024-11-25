using UnityEngine;

namespace DemoProject.LevelManagement
{
    public class ForceApplier : MonoBehaviour
    {
        public float ForceStrength;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<CompetitorBase>(out var levelPlayerBase))
            {
                var stickPosition = transform.position;
                var playerPosition = levelPlayerBase.transform.position;
                
                var forceDirection = Vector3.zero;

                var xDifference = playerPosition.x - stickPosition.x;
                var zDifference = playerPosition.z - stickPosition.z;

                if (Mathf.Abs(xDifference) > Mathf.Abs(zDifference))
                {
                    forceDirection = xDifference > 0 ? Vector3.right : Vector3.left;
                }
                else
                {
                    forceDirection = zDifference > 0 ? Vector3.forward : Vector3.back;
                }

                forceDirection *= ForceStrength;
                forceDirection.y = 5f;
                levelPlayerBase.Push(forceDirection);
            }
        }
    }
}