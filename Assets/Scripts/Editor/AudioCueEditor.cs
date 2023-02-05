using UnityEngine;
using UnityEditor;

/// <summary>
/// inspired by Richard Fines talk about scriptableObjects https://www.youtube.com/watch?v=6vmRwLYWNRo
/// </summary>
[CustomEditor(typeof(AudioCue), true)]
public class AudioCueEditor : Editor
{
    [SerializeField] private AudioSource _previewer;

    private void OnEnable()
    {
        _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }
    private void OnDisable()
    {
        DestroyImmediate(_previewer.gameObject);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Preview"))
        {
            ((AudioCue) target).Play(_previewer);
        }
    }
}
