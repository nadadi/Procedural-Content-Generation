using UnityEngine;
using System.Collections;

public class NodoCircuito {
	private int x;
	private int y;
	private NodoCircuito hermanoIzquierda;
	private NodoCircuito hermanoDerecha;

	public NodoCircuito(){

	}

	public NodoCircuito(int _x, int _y){
		x = _x;
		y = _y;
	}

	public NodoCircuito(int _x, int _y, NodoCircuito _hermanoIzq, NodoCircuito _hermanoDer){
		x = _x;
		y = _y;
		hermanoIzquierda = _hermanoIzq;
		hermanoDerecha = _hermanoDer;
	}

	public Vector2 GetCoordenadas(){
		return new Vector2(x,y);
	}

	public void SetCoordenadas(int _x, int _y){
		x = _x;
		y = _y;
	}

	public NodoCircuito GetHermanoIzquierda(){
		return hermanoIzquierda;
	}

	public void SetHermanoIzquierda(NodoCircuito nuevoHermanoIzquierda){
		hermanoIzquierda = nuevoHermanoIzquierda;
	}

	public NodoCircuito GetHermanoDerecha(){
		return hermanoDerecha;
	}

	public void SetHermanoDerecha(NodoCircuito nuevoHermanoDerecha){
		hermanoDerecha = nuevoHermanoDerecha;
	}
}
