using UnityEngine;

namespace DemoProject.Misc
{
    public interface ITargetLookAt
    {
        void LookAt(Transform target);
        void ReleaseTarget(Transform target);
    }
}