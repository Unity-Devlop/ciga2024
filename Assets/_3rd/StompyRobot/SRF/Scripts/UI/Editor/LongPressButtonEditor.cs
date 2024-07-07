using UnityEditor;
using UnityEditor.UI;

namespace SRF.UI.Editor
{
    [CustomEditor(typeof (LongPressButton), true)]
    [CanEditMultipleObjects]
    public class LongPressButtonEditor : ButtonEditor
    {
        LongPressButton button;
        private SerializedProperty _onLongPressProperty;
        private SerializedProperty Repeat;
        private SerializedProperty StartTime;
        private SerializedProperty Internal;

        protected override void OnEnable()
        {
            base.OnEnable();
            _onLongPressProperty = serializedObject.FindProperty("_onLongPress");
            Repeat = serializedObject.FindProperty("Repeat");
            StartTime = serializedObject.FindProperty("StartTime");
            Internal = serializedObject.FindProperty("Internal");
        }

        public override void OnInspectorGUI()
        {
            button = target as LongPressButton;
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            serializedObject.Update();
            EditorGUILayout.PropertyField(_onLongPressProperty);
            EditorGUILayout.PropertyField(Repeat);
            EditorGUILayout.PropertyField(StartTime);
            EditorGUILayout.PropertyField(Internal);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
