using UnityEngine;

namespace DemoProject.Misc
{
    public class TargetLockProvider : MonoBehaviour
    {
        public Transform TargetTransform;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ITargetLookAt>(out var targetLookAt))
            {
                targetLookAt.LookAt(TargetTransform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<ITargetLookAt>(out var targetLookAt))
            {
                targetLookAt.ReleaseTarget(TargetTransform);
            }
        }
    }
}