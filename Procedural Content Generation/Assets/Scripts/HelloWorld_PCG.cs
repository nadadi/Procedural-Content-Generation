using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelloWorld_PCG : MonoBehaviour {
	public int seed = 1;

	private string palabraFinal;
	private List<string> letras = new List<string>();

	void Awake(){
		letras.Add("H");
		letras.Add("o");
		letras.Add("l");
		letras.Add("a");
		letras.Add("M");
		letras.Add("u");
		letras.Add("n");
		letras.Add("d");
		letras.Add("o");
	}

	void Start(){
		for (int i = 0; i < 5; i++) {
			Random.seed = Random.Range(int.MinValue, int.MaxValue);

			Debug.Log("--------------------------------------");
			palabraFinal = "";

			for (int j = 0; j < letras.Count; j++) {
				palabraFinal += letras[j];
			}
			Debug.Log("Nuestra palabra inicialmente es: " +palabraFinal);

			palabraFinal = "";

			while(letras.Count > 0){
				int index = Random.Range(0,letras.Count);
				palabraFinal += letras[index];
				letras.RemoveAt(index);
			}
			Debug.Log("Nuestra palabra finalmente es: " +palabraFinal);
			LlenarListaLetras();
		}
	}

	void LlenarListaLetras(){
		letras.Add("H");
		letras.Add("o");
		letras.Add("l");
		letras.Add("a");
		letras.Add("M");
		letras.Add("u");
		letras.Add("n");
		letras.Add("d");
		letras.Add("o");
	}
}
