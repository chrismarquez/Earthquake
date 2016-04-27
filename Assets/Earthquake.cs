		using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Earthquake : MonoBehaviour {

	public InputField peso, k;
	public GameObject piso;
	public GameObject anchor;
	private List<Piso> pisos;

	void Awake() {
		
		pisos = new List<Piso> ();


	}

	public void Test(Text a, Text b) {
		
	}

	public void AddFloor(){


		/*
		this.weight = int.Parse(weight);
		this.k = int.Parse(k);
		this.cantNiveles++;*/
		Debug.Log (peso.text);
		Debug.Log (k.text);

		Vector3 pos = anchor.transform.position;
		pos.y = pos.y + pisos.Count * 50;

		GameObject go = Instantiate (piso, pos, anchor.transform.rotation) as GameObject;
		go.transform.parent = anchor.transform.parent;
		pisos.Add(new Piso(int.Parse(peso.text), int.Parse(k.text), go));

	}

	public double[] Finalizar(){
		/*
		pisos = new double[niveles];
		return pisos;
		*/

		double[] resultado = new double[pisos.Count];
		for (int i = 0; i < pisos.Count; i++) {
		
			resultado[i] = pisos[i].Peso;
		}
		return resultado;
	}

	public void Destruir(){

		for (int i = 0; i < pisos.Count; i++) {
			
			Destroy (pisos [i].piso);

		}

		pisos = new List<Piso> ();
	}
}
