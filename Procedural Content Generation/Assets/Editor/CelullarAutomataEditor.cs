using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CelullarAutomata))]
public class CelullarAutomataEditor : Editor {
	CelullarAutomata script;
	int ultimoAnchoMapa;
	int ultimaAlturaMapa;
	int ultimoPorcentajeMuro;
	int ultimoMinimoVecinos;
	int ultimoNumeroIteraciones;
	int ultimaSemilla;

	public void OnEnable(){
		script = (CelullarAutomata)target;
	}

	public override void OnInspectorGUI()
	{
		script.anchoCueva = EditorGUILayout.IntField("Ancho Cueva", script.anchoCueva);
		script.alturaCueva = EditorGUILayout.IntField("Altura Cueva", script.alturaCueva);
		script.porcentajeMuro = EditorGUILayout.IntSlider("Porcentaje Muro", script.porcentajeMuro, 0, 100);
		script.iteracionesSuavizado = EditorGUILayout.IntSlider("Iteraciones de Suavizado", script.iteracionesSuavizado, 0, 5);
		script.semilla = EditorGUILayout.IntField("Semilla", script.semilla);
		script.actualizacionAutomaticaMapa = EditorGUILayout.Toggle("Actualizar Mapa Automaticamente", script.actualizacionAutomaticaMapa);

		if(script.anchoCueva < 0){
			script.anchoCueva = 0;
		}
		if(script.alturaCueva < 0){
			script.alturaCueva = 0;
		}
		if(script.frecuenciaActualizacion < 1){
			script.frecuenciaActualizacion = 1;
		}

		if(ultimoAnchoMapa != script.anchoCueva || ultimaAlturaMapa != script.alturaCueva || ultimoPorcentajeMuro != script.porcentajeMuro || ultimaSemilla != script.semilla || ultimoNumeroIteraciones != script.iteracionesSuavizado){
			script.RegeneraMapa();
		}

		if(!script.actualizacionAutomaticaMapa){
			script.DesinvocarActualizacionMapaAutomaticamente();	
		}else{
			script.InvocarActualizacionMapaAutomaticamente();
			script.frecuenciaActualizacion = EditorGUILayout.IntField("Frecuencia Actualizacion", script.frecuenciaActualizacion);	
		}

		ultimoAnchoMapa = script.anchoCueva;
		ultimaAlturaMapa = script.alturaCueva;
		ultimoPorcentajeMuro = script.porcentajeMuro;
		ultimoNumeroIteraciones = script.iteracionesSuavizado;
		ultimaSemilla = script.semilla;
	}
}
