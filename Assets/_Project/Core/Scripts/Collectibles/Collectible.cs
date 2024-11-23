using UnityEngine;

namespace DemoProject.Collectibles
{
    public class Collectible : MonoBehaviour
    {
        public CollectibleType Type;
        public int Amount;

        public void OnCollected(Collector collector)
        {
            gameObject.SetActive(false);
        }
    }
}