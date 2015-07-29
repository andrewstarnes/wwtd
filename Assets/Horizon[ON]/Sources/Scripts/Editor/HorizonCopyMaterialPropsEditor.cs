using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Horizon {
	[CanEditMultipleObjects]
	[CustomEditor (typeof(HorizonCopyMaterialProps))]
	public class HorizonCopyMaterialPropsEditor : Editor {

		#if UNITY_EDITOR

		string installPath;
		string inspectorGUIPath;

		void OnEnable(){
			string scriptLocation = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
			installPath = scriptLocation.Replace ("/Sources/Scripts/Editor/HorizonCopyMaterialPropsEditor.cs", "");
			inspectorGUIPath = installPath + "/Sources/Scripts/Editor/InspectorGUI";
		}

		public override void OnInspectorGUI () {
			HorizonCopyMaterialProps _target=(HorizonCopyMaterialProps)target;

			Rect bgRect = EditorGUILayout.GetControlRect ();
			bgRect = new Rect (bgRect.x+1, bgRect.y-18, Screen.width-40, bgRect.height+1);
			
			Texture2D bgTex;
			Texture2D logoTex = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/Horizon[ON]Inspector_Logo.png", typeof (Texture2D))as Texture2D;

			if (EditorGUIUtility.isProSkin) {
				bgTex = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/Horizon[ON]Inspector_bgTex_DarkSkin.jpg", typeof(Texture2D))as Texture2D;
			} else {
				bgTex = AssetDatabase.LoadAssetAtPath (inspectorGUIPath + "/Horizon[ON]Inspector_bgTex_LightSkin.jpg", typeof(Texture2D))as Texture2D;
			}
			EditorGUI.DrawPreviewTexture (bgRect, bgTex);
			GUI.DrawTexture (new Rect ((Screen.width/2)-110,bgRect.y+7, 210,36), logoTex);
			
			EditorGUILayout.Space ();
			//EditorGUILayout.Space ();

			EditorGUILayout.LabelField ("Copy Material Properties from...", EditorStyles.boldLabel);


			GameObject prevObject = _target.source;
			_target.source = EditorGUILayout.ObjectField ("", _target.source, typeof(GameObject), true) as GameObject;
			Color backCol = GUI.backgroundColor;
			GUI.backgroundColor = new Color (0.4f, 1, 0.4f, 0.4f);
			if (_target.source!=null) {
				if (prevObject!=_target.source) {
					Undo.RecordObjects(targets, "Horizon[ON] Copy material props");
					for(int i=0; i<targets.Length; i++) {
						HorizonCopyMaterialProps _atarget=(HorizonCopyMaterialProps)targets[i];
						_atarget.source=_target.source;
					}
				}
				if (GUILayout.Button("All suitable material props")) {
					Undo.RecordObjects(targets, "Horizon[ON] Copy material props");
					for(int i=0; i<targets.Length; i++) {
						HorizonCopyMaterialProps _atarget=(HorizonCopyMaterialProps)targets[i];
						_atarget.Sync(false);
					}
				}
				if (GUILayout.Button("Suitable material props (enabled only)")) {
					Undo.RecordObjects(targets, "Horizon[ON] Copy material props");
					for(int i=0; i<targets.Length; i++) {
						HorizonCopyMaterialProps _atarget=(HorizonCopyMaterialProps)targets[i];
						_atarget.Sync(true);
					}
				}
			}
			GUI.backgroundColor = backCol;
		}
		#endif
	}
}