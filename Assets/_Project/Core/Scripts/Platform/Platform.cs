using System;
using DemoProject.Player;
using UnityEngine;

namespace DemoProject.Platform
{
    public class Platform : MonoBehaviour
    {
        [field: SerializeField] public PlatformType PlatformType;
    }

    public enum PlatformType
    {
        Flat,
        Rotator
    }
}