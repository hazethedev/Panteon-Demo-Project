using UnityEngine;

namespace DemoProject.LevelManagement
{
    public class Platform : MonoBehaviour
    {
        [field: SerializeField] public PlatformType Type;
        public Transform EndPoint;
    }
}