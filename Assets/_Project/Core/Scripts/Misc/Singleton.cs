using UnityEngine;

namespace DemoProject.Misc
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindObjectOfType<T>();
                    if (m_Instance != null) return m_Instance;
                    
                    var singletonObj = new GameObject(name: typeof(T).ToString());
                    m_Instance = singletonObj.AddComponent<T>();
                }
                return m_Instance;
            }
        }

        protected virtual void Awake()
        {
            if (m_Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            m_Instance = GetComponent<T>();

            if (m_Instance == null)
                return;
        }
    }
}