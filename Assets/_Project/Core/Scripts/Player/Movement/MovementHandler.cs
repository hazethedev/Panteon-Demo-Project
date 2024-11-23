using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace DemoProject.Player
{
    public class MovementHandler : MonoBehaviour
    {
        private NativeArray<MovementModifier> m_Modifiers;
        private Vector3 m_CurrentVelocity;

        private bool m_IsDirty = false;
        
        private void Awake()
        {
            InitializeModifiers(length: 8);
        }

        public void ClearModifiers()
        {
            var length = m_Modifiers.Length;
            for (var i = 0; i < length; i++)
            {
                m_Modifiers[i] = MovementModifier.Default;
            }
        }

        public Vector3 GetVelocity()
        {
            if (m_IsDirty) CalculateVelocity();
            return m_CurrentVelocity;
        }

        public void CalculateVelocity()
        {
            m_CurrentVelocity = Vector3.zero;
            
            var velocityRef = new NativeReference<float3>(m_CurrentVelocity, Allocator.TempJob);
            var movementCalculationJob = new MovementCalculationJob
            {
                Modifiers = m_Modifiers,
                VelocityRef = velocityRef,
            };
            
            movementCalculationJob.Run(m_Modifiers.Length);
            
            m_CurrentVelocity = velocityRef.Value;
            
            velocityRef.Dispose();
            
            m_IsDirty = false;
        }

        private void OnDestroy()
        {
            if (m_Modifiers.IsCreated)
                m_Modifiers.Dispose();
        }

        public int AddModifier(Vector3 movement, ModificationType modificationType = ModificationType.Add)
        {
            var availableIndex = GetAvailableSlotIndex();
            if (availableIndex == -1)
            {
                // no slot available
                // TODO: allocate a new NativeArray and sync with currentDependency OR use NativeList instead of NativeArray
                Debug.Log("No available slot found. Unable to add a modifier.");
                return -1;
            }

            m_Modifiers[availableIndex] = new MovementModifier(availableIndex, movement, modificationType);
            m_IsDirty = true;
            return availableIndex;
        }
        
        public bool RemoveModifier(int id)
        {
            if (id < 0 || id >= m_Modifiers.Length) return false;
            m_Modifiers[id] = MovementModifier.Default;
            m_IsDirty = true;
            return true;
        }

        public bool ChangeModifier(int id, Vector3 movement, ModificationType modificationType = ModificationType.Add)
        {
            if (id < 0 || id >= m_Modifiers.Length) return false;
            m_Modifiers[id] = new MovementModifier(id, movement, modificationType);
            m_IsDirty = true;
            return true;
        }
        
        private int GetAvailableSlotIndex()
        {
            // create a "not valid" modifier as a seeker (id: -1)
            var seeker = MovementModifier.Default;
            var index = m_Modifiers.IndexOf(seeker);
            return index;
        }
        
        private void InitializeModifiers(int length)
        {
            m_Modifiers = new NativeArray<MovementModifier>(length, Allocator.Persistent);
            for (var i = 0; i < length; i++)
            {
                m_Modifiers[i] = MovementModifier.Default;
            }
        }
        
        [BurstCompile]
        private struct MovementCalculationJob : IJobFor
        {
            public NativeArray<MovementModifier> Modifiers;
            public NativeReference<float3> VelocityRef;
            
            public void Execute(int index)
            {
                var modifier = Modifiers[index];
                if (!modifier.IsValid) return;

                var velocity = VelocityRef.Value;
                VelocityRef.Value = modifier.Type switch
                {
                    ModificationType.Add => velocity + modifier.Movement,
                    ModificationType.Multiply => velocity * modifier.Movement,
                    _ => VelocityRef.Value
                };
            }
        }
    }
}