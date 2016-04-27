using UnityEngine;
using System.Collections;

public class Piso {

	private int peso,
	k;

	public int Peso {

		get { 
			return peso;
		}
	}

	public GameObject piso;

	public Piso(int peso, int k, GameObject piso){
		this.peso = peso;
		this.k = k;
		this.piso = piso;
	}	 
}
