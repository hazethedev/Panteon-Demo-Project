using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DemoProject.Utils
{
    public static class ScriptableObjectUtilities
    {
        public static List<T> GetScriptableObjectsAtPath<T>(string path) where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets($"t: {typeof(T)}", new[] { path });
            return guids
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToList();
        }
    }
}