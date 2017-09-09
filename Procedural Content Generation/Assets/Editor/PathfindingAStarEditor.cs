using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PathfindingAStar))]
public class PathfindingAStarEditor : Editor {
	PathfindingAStar script;

	public void OnEnable(){
		script = (PathfindingAStar)target;
	}

	public override void OnInspectorGUI(){
		DrawDefaultInspector();

		if(GUILayout.Button("Actualizar Mapa")){
			script.ActualizarMapa();
		}
	}
}
