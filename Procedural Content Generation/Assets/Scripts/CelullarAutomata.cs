using UnityEngine;
using System.Collections;

public class CelullarAutomata : MonoBehaviour {
	public int anchoCueva = 5;
	public int alturaCueva = 5;
	public int porcentajeMuro = 50;
	public int iteracionesSuavizado = 1;
	public int semilla = 26;
	public bool actualizacionAutomaticaMapa = false;
	public int frecuenciaActualizacion = 3;

	private int[,] cueva;

	void Awake(){
		cueva = new int[anchoCueva, alturaCueva];
		LlenarMapa(); 
	}

	public void InvocarActualizacionMapaAutomaticamente(){
		if(!IsInvoking("RegeneraMapaAleatorio")){
			InvokeRepeating("RegeneraMapaAleatorio", 0.1f, frecuenciaActualizacion);
		}
	}

	public void DesinvocarActualizacionMapaAutomaticamente(){
		CancelInvoke("RegeneraMapaAleatorio");
	}

	public void RegeneraMapa(){
		cueva = new int[anchoCueva, alturaCueva];
		LlenarMapa();
	}

	public void RegeneraMapaAleatorio(){
		semilla = Random.Range(int.MinValue, int.MaxValue);
		cueva = new int[anchoCueva, alturaCueva];
		LlenarMapa();
	}

	void LlenarMapa(){
		Random.seed = semilla;
		for (int i = 0; i < anchoCueva; i++) {//Filas
			for (int j = 0; j < alturaCueva; j++) {//Columnas
				if(i == 0 || i == (anchoCueva-1) || j == 0 || j == (alturaCueva-1)){
					cueva[i,j] = 1;
				}else{
					int probabilidad = Random.Range(0,100);
					if(probabilidad < porcentajeMuro){
						cueva[i,j] = 1;
					}else{
						cueva[i,j] = 0;
					}	
				}
			}
		}
		SuavizarMapa();
	}

	void SuavizarMapa(){
		for (int iteraciones = 0; iteraciones < iteracionesSuavizado; iteraciones++) {
			for (int i = 0; i < anchoCueva; i++) {
				for (int j = 0; j < alturaCueva; j++) {
					int cantidadVecinosMuro = ObtenerVecinosMuro(i,j);

					if(cantidadVecinosMuro > 4){
						cueva[i,j] = 1;
					}else {
						cueva[i,j] = 0;
					}
				}
			}	
		}
	}

	int ObtenerVecinosMuro(int x, int y){
		int cantidadMuros = 0;
		for (int i = x - 1; i <= x + 1; i++) {
			for (int j = y - 1; j <= y + 1; j++) {
				if(i >= 0 && i < anchoCueva && j >= 0 && j < alturaCueva){
					if(i != x || j != y){
						cantidadMuros += cueva[i,j];
					}	
				}else{
					cantidadMuros++;
				}
			}
		}
		return cantidadMuros;
	}

	void OnDrawGizmos(){
		if(cueva != null){
			for (int i = 0; i < anchoCueva; i++) {//Filas
				for (int j = 0; j < alturaCueva; j++) {//Columnas
					Gizmos.color = (cueva[i,j] == 0)? Color.white : Color.black;
					Gizmos.DrawCube(new Vector3(i,j,0f), new Vector3(0.9f, 0.9f, 0.9f));
				}
			}
		}
	}
}
