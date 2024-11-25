using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoProject.Physics
{
    public class RagdollEnabler : MonoBehaviour
    {
        public Transform RootTransform;
        [SerializeField] private Collider[] m_Colliders;
        [SerializeField] private Rigidbody[] m_Rigidbodies;
        
        private RagdollTransformData[] m_OriginalTransforms;

        private void Awake()
        {
            SaveInitialTransformData();
        }

        private void SaveInitialTransformData()
        {
            m_OriginalTransforms = new RagdollTransformData[m_Colliders.Length];
            for (var i = 0; i < m_Colliders.Length; i++)
            {
                m_OriginalTransforms[i] = new RagdollTransformData(m_Colliders[i].transform);
            }
        }

        public void Enable()
        {
            for (var i = 0; i < m_Colliders.Length; i++)
            {
                var col = m_Colliders[i];
                var rb = m_Rigidbodies[i];
                col.enabled = true;
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }

        public void Disable()
        {
            for (var i = 0; i < m_Colliders.Length; i++)
            {
                var col = m_Colliders[i];
                var rb = m_Rigidbodies[i];
                col.enabled = false;
                rb.useGravity = false;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.Sleep();
            }
        }

        public void BackToOriginalPosition()
        {
            for (var i = 0; i < m_Colliders.Length; i++)
            {
                var col = m_Colliders[i];
                m_OriginalTransforms[i].Apply(col.transform);
            }
        }

#if UNITY_EDITOR
        [Button]
        private void GetRigidbodies()
        {
            Array.Resize(ref m_Rigidbodies, m_Colliders.Length);
            for (var i = 0; i < m_Colliders.Length; i++)
            {
                if (m_Colliders[i].TryGetComponent<Rigidbody>(out var rb))
                {
                    m_Rigidbodies[i] = rb;
                }
            }
        }
#endif
        private struct RagdollTransformData
        {
            public Vector3 LocalPosition;
            public Quaternion LocalRotation;

            public RagdollTransformData(Transform transform)
            {
                LocalPosition = transform.localPosition;
                LocalRotation = transform.localRotation;
            }

            public void Apply(Transform transform)
            {
                transform.localPosition = LocalPosition;
                transform.localRotation = LocalRotation;
            }
        }
    }
}