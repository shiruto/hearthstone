using UnityEngine;
using UnityEditor;
using System.Linq;

public class SceneNameAttribute: PropertyAttribute { }

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer: PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		// Get the list of scenes in Build Settings
		var scenes = EditorBuildSettings.scenes
			.Where(s => !string.IsNullOrEmpty(s.path))
			.Select(s => System.IO.Path.GetFileNameWithoutExtension(s.path))
			.ToArray();

		// Get the index of the currently selected scene
		var index = Mathf.Max(0, System.Array.IndexOf(scenes, property.stringValue));

		// Draw a popup to select the scene
		EditorGUI.BeginChangeCheck();
		index = EditorGUI.Popup(position, label.text, index, scenes);
		if(EditorGUI.EndChangeCheck()) {
			property.stringValue = scenes[index];
		}
	}
}
#endif