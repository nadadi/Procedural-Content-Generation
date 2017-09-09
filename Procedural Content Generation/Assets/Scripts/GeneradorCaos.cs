using UnityEngine;
using System.Collections;

public class GeneradorCaos : MonoBehaviour {
	public int anchoCueva = 5;
	public int alturaCueva = 5;
	public int semilla = 26;
	public bool semillaAleatoria = false;
	[Range(0,100)]
	public int porcentajeMuro = 50;

	private int[,] cueva;

	void Start(){
		CrearCueva();
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			CrearCueva();
		}
	}

	void CrearCueva(){
		cueva = new int[anchoCueva, alturaCueva];
		LlenarCuevaAleatoriamente();
	}

	void LlenarCuevaAleatoriamente(){
		Random.seed = (semillaAleatoria)?Random.Range(int.MinValue,int.MaxValue):semilla;

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
