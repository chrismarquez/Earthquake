using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Earthquake : MonoBehaviour {

	public InputField peso, k;
	public GameObject piso;
	public GameObject anchor;
	private List<Piso> pisos;
	private int contador=0;
	public Button AddButton;
	public Text[] textos;

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
		pos.y = pos.y + pisos.Count * 25;

		GameObject go = Instantiate (piso, pos, anchor.transform.rotation) as GameObject;
		go.transform.parent = anchor.transform.parent;
		pisos.Add (new Piso (int.Parse (peso.text), int.Parse (k.text), go));

		this.textos [this.contador].text = ("Piso " + (this.contador+1) + ": Peso = " + peso.text + "," + " K = " + k.text);
		//this.textos [contador] = ("Peso = " + peso.text + "Constante K = " + k.text);
		//this.texto1 = ("Peso = " + peso.text + "Constante K = " + k.text);
		//Debug.Log (this.texto1);

		contador++;
		Debug.Log (AddButton);
		if (contador >= 10) {
			AddButton.interactable = false;
		}
		Debug.Log (contador);
	}

	public double[] Finalizar(){
		/*
		pisos = new double[niveles];
		return pisos;
		*/
		double[] resultado = new double[pisos.Count];
		for (int i = 0; i < pisos.Count; i++) {
			resultado [i] = pisos [i].Peso;
			contador++;
		}
		return resultado;
	}

	public void Destruir(){
		GameObject piso = pisos [--this.contador].piso;
		Destroy (piso);
		pisos.Remove (pisos[this.contador]);
		if (contador < 10) {
			AddButton.interactable = true;
		}
		this.textos [this.contador].text = "";
	}
		
	public void Reiniciar(){
		for (int i = 0; i < pisos.Count; i++){
			Destroy (pisos [i].piso);
			this.textos [i].text = "";
		}
		contador = 0;
		AddButton.interactable = true;
		pisos = new List<Piso> ();

	}

}
