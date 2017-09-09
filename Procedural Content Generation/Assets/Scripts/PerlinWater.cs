using UnityEngine;
using System.Collections;

public class PerlinWater : MonoBehaviour {

	public GameObject prefabCuboAgua;
	public int tamanio = 50;
	public float escala = 0.1f;
	public float modificadorEscala = 5f;

	void Start () 
	{
		for(int X = -tamanio/2; X < tamanio/2; X++)
		{
			for(int Z = -tamanio/2; Z < tamanio/2; Z++)
			{
				GameObject cubo = Instantiate(prefabCuboAgua, new Vector3(transform.position.x + X, 0, transform.position.z + Z), Quaternion.identity) as GameObject;
				cubo.transform.parent = transform;
			}
		}
	}

	void Update () 
	{
		MoverTransform ();
	}

	void MoverTransform()
	{
		float altura = modificadorEscala * Mathf.PerlinNoise (Time.time + (transform.position.x * escala), Time.time + (transform.position.z * escala));

		Vector3 nuevoVector = new Vector3 (transform.position.x, altura, transform.position.z);
		transform.position = nuevoVector;

		foreach (Transform hijo in transform) 
		{
			altura = modificadorEscala * Mathf.PerlinNoise(Time.time+(hijo.transform.position.x*escala), Time.time+(hijo.transform.position.z*escala));

			ConfigurarColor(hijo, altura);
			AplicarAltura(hijo, altura);
		}
	}

	void ConfigurarColor(Transform hijo, float altura)
	{
		hijo.GetComponent<Renderer> ().material.color = new Color (0, altura, altura, 0);
	}

	void AplicarAltura(Transform hijo, float altura)
	{
		Vector3 NewVector = new Vector3 (hijo.transform.position.x, altura, hijo.transform.position.z);

		hijo.transform.position = NewVector;
	}
}﻿