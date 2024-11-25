using UnityEngine;

namespace DemoProject.LevelManagement
{
    [CreateAssetMenu(fileName = "New Platform Type", menuName = "Level Management/Platform Type")]
    public class PlatformType : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public PlatformActionType ActionType { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }

    public enum PlatformActionType
    {
        Flat,
        Balance,
        Slide
    }
}