using System;
using Unity.Mathematics;

namespace DemoProject.Player
{
    public struct MovementModifier : IEquatable<MovementModifier>
    {
        public static MovementModifier Default => new(id: -1);
        
        public int Id;
        public float3 Movement;
        public ModificationType Type;

        public MovementModifier(int id = 0, float3 movement = default, ModificationType type = ModificationType.Add)
        {
            Id = id;
            Movement = movement;
            Type = type;
        }

        public bool IsValid => Id > -1;

        public bool Equals(MovementModifier other) => Id == other.Id;

        public override bool Equals(object obj) => obj is MovementModifier other && Equals(other);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(MovementModifier lhs, MovementModifier rhs) => lhs.Id == rhs.Id;

        public static bool operator !=(MovementModifier lhs, MovementModifier rhs) => lhs.Id != rhs.Id;
    }
}