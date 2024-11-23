using UnityEngine;

namespace DemoProject.Collectibles
{
    [CreateAssetMenu(fileName = "New Collectible Type", menuName = "Collectibles/Type")]
    public class CollectibleType : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}