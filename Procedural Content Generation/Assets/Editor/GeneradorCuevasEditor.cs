using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GeneradorCuevas))]
public class GeneradorCuevasEditor : Editor {
	GeneradorCuevas script;
	int ultimoAnchoMapa;
	int ultimaAlturaMapa;
	int ultimoPorcentajeMuro;
	int ultimoMinimoVecinos;
	int ultimoNumeroIteraciones;
	int ultimaSemilla;

	public void OnEnable(){
		script = (GeneradorCuevas)target;
	}

	public override void OnInspectorGUI()
	{
		script.anchoCueva = EditorGUILayout.IntSlider("Ancho Cueva", script.anchoCueva, 5, 250);
		script.alturaCueva = EditorGUILayout.IntSlider("Altura Cueva", script.alturaCueva, 5, 250);
		script.porcentajeMuro = EditorGUILayout.IntSlider("Porcentaje Muro", script.porcentajeMuro, 0, 100);
		script.iteracionesSuavizado = EditorGUILayout.IntSlider("Iteraciones de Suavizado", script.iteracionesSuavizado, 0, 5);
		script.semilla = EditorGUILayout.IntField("Semilla", script.semilla);

		if(script.anchoCueva < 0){
			script.anchoCueva = 0;
		}
		if(script.alturaCueva < 0){
			script.alturaCueva = 0;
		}

		if(GUILayout.Button("Regenerar Cueva"))
		{
			script.CrearCueva();
		}

		if(ultimoAnchoMapa != script.anchoCueva || ultimaAlturaMapa != script.alturaCueva || ultimoPorcentajeMuro != script.porcentajeMuro || ultimaSemilla != script.semilla || ultimoNumeroIteraciones != script.iteracionesSuavizado){
			script.CrearCueva();
		}

		ultimoAnchoMapa = script.anchoCueva;
		ultimaAlturaMapa = script.alturaCueva;
		ultimoPorcentajeMuro = script.porcentajeMuro;
		ultimoNumeroIteraciones = script.iteracionesSuavizado;
		ultimaSemilla = script.semilla;

		SceneView.RepaintAll();
	}
}
