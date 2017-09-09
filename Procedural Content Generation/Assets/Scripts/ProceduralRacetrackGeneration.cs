using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralRacetrackGeneration : MonoBehaviour {
	[Range(3f,25f)]
	public int cantidadPuntosCircuito = 3;
	[Range(3f,100f)]
	public int anchoMapa = 10;
	[Range(3f,100f)]
	public int altoMapa = 10;

	public int seed = 1;

	public bool seedAleatoria = false;

	private List<NodoCircuito> nodosCircuito = new List<NodoCircuito>();
	private Vector2[] puntosCircuito;
	private Vector2 puntoCentral;
	private int[,] mapa;

	void Awake(){
		CrearPuntosCircuito();
		MarcarNodoCentral();
		CrearCircuito();
	}

	void CrearPuntosCircuito(){
		puntosCircuito = new Vector2[cantidadPuntosCircuito];
		mapa = new int[anchoMapa, altoMapa];
		Random.seed = (seedAleatoria)?Random.Range(int.MinValue, int.MaxValue):seed;

		for (int i = 0; i < cantidadPuntosCircuito; i++) {
			int x = Random.Range(0, anchoMapa);
			int y = Random.Range(0, altoMapa);

			puntosCircuito[i] = new Vector2(x,y);
			mapa[x,y] = 1;
		}
	}

	void MarcarNodoCentral(){
		float distMin = int.MaxValue;
		int indice = 0;

		mapa[(int)anchoMapa/2, (int)altoMapa/2] = 3;

		for (int i = 0; i < cantidadPuntosCircuito; i++) {
			float distanciaAlCentro = Vector2.Distance(new Vector2(anchoMapa/2, altoMapa/2), puntosCircuito[i]);
			if(distanciaAlCentro < distMin){
				indice = i;
				distMin = distanciaAlCentro;
			}
		}

		puntoCentral = new Vector2(puntosCircuito[indice].x, puntosCircuito[indice].y);
		mapa[(int)puntosCircuito[indice].x, (int)puntosCircuito[indice].y] = 2;
	}

	void CrearCircuito(){
		//Se crea la lista de Nodos primero.
		for (int i = 0; i < cantidadPuntosCircuito; i++) {
			if(mapa[(int)puntosCircuito[i].x, (int)puntosCircuito[i].y] != 2){
				NodoCircuito nuevoNodo = new NodoCircuito((int)puntosCircuito[i].x, (int)puntosCircuito[i].y);
				nodosCircuito.Add(nuevoNodo);	
			}
		}

		Debug.Log("Hay : " +nodosCircuito.Count + " nodos libres.");

		//Se generan las conexiones entre los nodos cercanos.
		for (int i = 0; i < nodosCircuito.Count; i++) {
			if(mapa[(int)puntosCircuito[i].x, (int)puntosCircuito[i].y] == 2){
				continue;
			}

			List<NodoCircuito> nodosCercanos = EncontrarNodosCercanos(puntosCircuito[i]);

			/*nodosCircuito[i].SetHermanoIzquierda(nodosCercanos[0]);	
			nodosCercanos[0].SetHermanoDerecha(nodosCircuito[i]);
			nodosCircuito[i].SetHermanoDerecha(nodosCercanos[1]);
			nodosCercanos[1].SetHermanoIzquierda(nodosCircuito[i]);*/

			if(nodosCircuito[i].GetHermanoIzquierda() == null){
				nodosCircuito[i].SetHermanoIzquierda(nodosCercanos[0]);	
			}
			if(nodosCercanos[0].GetHermanoDerecha() == null){
				nodosCercanos[0].SetHermanoDerecha(nodosCircuito[i]);
			}
			if(nodosCircuito[i].GetHermanoDerecha() == null){
				nodosCircuito[i].SetHermanoDerecha(nodosCercanos[1]);
			}
			if(nodosCercanos[1].GetHermanoIzquierda() == null){
				nodosCercanos[1].SetHermanoIzquierda(nodosCircuito[i]);
			}

			Debug.Log("Nodo " +nodosCircuito[i].GetCoordenadas() + " tiene a " +nodosCercanos[0].GetCoordenadas() + " como hermano derecho y a " +nodosCercanos[1].GetCoordenadas() + " como hermano izquierdo.");
		}
	}

	List<NodoCircuito> EncontrarNodosCercanos(Vector2 punto){
		/*float distNodoDerecho = int.MaxValue;
		float distNodoIzquierdo = int.MaxValue;
		int indiceNodoDerecho = 0;
		int indiceNodoIZquierdo = 0;

		for (int i = 0; i < cantidadPuntosCircuito; i++) {
			//Esta a la derecha.
			if(puntosCircuito[i].x > punto.x){
				if(Mathf.Abs(puntosCircuito[i].x - punto.x) < distNodoDerecho){
					distNodoDerecho = Mathf.Abs(puntosCircuito[i].x - punto.x);
					indiceNodoDerecho = i;
				}
			}else{
				if(Mathf.Abs(puntosCircuito[i].x - punto.x) < distNodoIzquierdo){
					distNodoIzquierdo = Mathf.Abs(puntosCircuito[i].x - punto.x);
					indiceNodoIZquierdo = i;
				}
			}
		}

		List<NodoCircuito> nodos = new List<NodoCircuito>();
		NodoCircuito nodoDerecho = new NodoCircuito((int)puntosCircuito[indiceNodoDerecho].x, (int)puntosCircuito[indiceNodoDerecho].y);
		NodoCircuito nodoIzquierdo = new NodoCircuito((int)puntosCircuito[indiceNodoIZquierdo].x, (int)puntosCircuito[indiceNodoIZquierdo].y);
		nodos.Add(nodoDerecho);
		nodos.Add(nodoIzquierdo);

		return nodos;*/


		float distPrimerCercano = int.MaxValue;
		float distSegundoCercano = int.MaxValue;
		int indicePrimerCercano = 0;
		int indiceSegundoCercano = 0;

		//for (int i = 0; i < cantidadPuntosCircuito; i++) {
		for (int i = 0; i < cantidadPuntosCircuito; i++) {
			Debug.Log("Punto analizado: " +punto + " vs punto actual: " +puntosCircuito[i]);
			if(puntosCircuito[i] != punto && mapa[(int)puntosCircuito[i].x, (int)puntosCircuito[i].y] != 2){
				Debug.Log("Pase la verificación.");
				if(Vector2.Distance(punto, puntosCircuito[i]) < distPrimerCercano){
					distSegundoCercano = distPrimerCercano;
					indiceSegundoCercano = indicePrimerCercano;
					distPrimerCercano = Vector2.Distance(punto, puntosCircuito[i]);
					indicePrimerCercano = i;
				}else if(Vector2.Distance(punto, puntosCircuito[i]) < distSegundoCercano){
					distSegundoCercano = Vector2.Distance(punto, puntosCircuito[i]);
					indiceSegundoCercano = i;
				}
			}
		}

		List<NodoCircuito> nodos = new List<NodoCircuito>();
		NodoCircuito primerNodo = new NodoCircuito((int)puntosCircuito[indicePrimerCercano].x, (int)puntosCircuito[indicePrimerCercano].y);
		NodoCircuito segundoNodo = new NodoCircuito((int)puntosCircuito[indiceSegundoCercano].x, (int)puntosCircuito[indiceSegundoCercano].y);
		nodos.Add(primerNodo);
		nodos.Add(segundoNodo);

		return nodos;
	}

	void OnDrawGizmos(){
		if(mapa != null){
			for (int x = 0; x < anchoMapa; x++) {
				for (int y = 0; y < altoMapa; y++) {
					Gizmos.color = (mapa[x,y] == 0)?Color.white:Color.black;
					if(mapa[x,y] == 2)
						Gizmos.color = Color.red;
					else if(mapa[x,y] == 3)
						Gizmos.color = Color.blue;
					Gizmos.DrawCube(new Vector3(x,y,0f), Vector3.one * 0.9f);
					if(mapa[x,y] == 1){
						Gizmos.color = Color.red;
						Gizmos.DrawLine(new Vector3(x,y,0f), new Vector3(puntoCentral.x, puntoCentral.y, 0f));	
					}
				}
			}
		}

		if(nodosCircuito != null){
			foreach (NodoCircuito nodo in nodosCircuito) {
				Gizmos.color = Color.cyan;
				Gizmos.DrawLine(new Vector3(nodo.GetCoordenadas().x, nodo.GetCoordenadas().y, 0f), new Vector3(nodo.GetHermanoIzquierda().GetCoordenadas().x, nodo.GetHermanoIzquierda().GetCoordenadas().y, 0f));
				Gizmos.DrawLine(new Vector3(nodo.GetCoordenadas().x, nodo.GetCoordenadas().y, 0f), new Vector3(nodo.GetHermanoDerecha().GetCoordenadas().x, nodo.GetHermanoDerecha().GetCoordenadas().y, 0f));
			}
		}
	}
}
