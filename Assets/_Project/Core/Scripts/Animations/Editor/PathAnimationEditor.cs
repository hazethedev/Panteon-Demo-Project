using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;

namespace DemoProject.Animations.Editor
{
    [CustomEditor(typeof(PathAnimation))]
    public class PathAnimationEditor : OdinEditor
    {
        private SerializedObject m_SerializedPathAnimation;
        private SerializedProperty m_WaypointsProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_SerializedPathAnimation = new SerializedObject(target);
            m_WaypointsProperty = m_SerializedPathAnimation.FindProperty("m_Waypoints");
        }

        private void OnSceneGUI()
        {
            m_SerializedPathAnimation.Update();

            if (m_WaypointsProperty == null || m_WaypointsProperty.arraySize == 0)
                return;

            // Renk ve handle stilini ayarla
            Handles.color = Color.cyan;

            // Waypoint'leri düzenlemek için Handles
            for (var i = 0; i < m_WaypointsProperty.arraySize; i++)
            {
                var pointProperty = m_WaypointsProperty.GetArrayElementAtIndex(i);
                var worldPosition = ((PathAnimation)target).transform.TransformPoint(pointProperty.vector3Value);
                var newPosition = Handles.PositionHandle(worldPosition, Quaternion.identity);

                // Eğer pozisyon değiştiyse, waypoint'i güncelle
                if (worldPosition != newPosition)
                {
                    Undo.RecordObject(target, "Move Waypoint");
                    pointProperty.vector3Value = ((PathAnimation)target).transform.InverseTransformPoint(newPosition);
                    m_SerializedPathAnimation.ApplyModifiedProperties();
                }
            }

            // Waypoint'leri bağlayan çizgiler
            for (var i = 0; i < m_WaypointsProperty.arraySize - 1; i++)
            {
                var point1 = ((PathAnimation)target).transform.TransformPoint(
                    m_WaypointsProperty.GetArrayElementAtIndex(i).vector3Value
                );
                var point2 = ((PathAnimation)target).transform.TransformPoint(
                    m_WaypointsProperty.GetArrayElementAtIndex(i + 1).vector3Value
                );
                Handles.DrawLine(point1, point2);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            m_SerializedPathAnimation.Update();

            if (GUILayout.Button("Add Waypoint"))
            {
                m_WaypointsProperty.arraySize++;
                var newPoint = m_WaypointsProperty.GetArrayElementAtIndex(m_WaypointsProperty.arraySize - 1);
                newPoint.vector3Value = Vector3.zero;
            }

            if (GUILayout.Button("Remove Last Waypoint") && m_WaypointsProperty.arraySize > 0)
            {
                m_WaypointsProperty.arraySize--;
            }

            m_SerializedPathAnimation.ApplyModifiedProperties();
        }
    }
}
