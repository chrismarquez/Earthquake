using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		double[] A = new double[] {4, -1};
		double[] B = new double[] {3};
		double[] C = new double[] {2};
		double[] D = new double[] {2, -1};
		double[] E = new double[] {1, -1};
		double[] F = new double[] {1};

		Rational a = new Rational(new Polynomial(A));
		Rational b = new Rational(new Polynomial(B));
		Rational c = new Rational(new Polynomial(C));
		Rational d = new Rational(new Polynomial(D));
		Rational e = new Rational(new Polynomial(E));
		Rational f = new Rational(new Polynomial(F));

		Rational[][] m = new Rational[3][];
		for (int i = 0; i < 3; i++) { 
			m[i] = new Rational[3];
		}
		m[0][0] = a; m[0][1] = f; m[0][2] = c;
		m[1][0] = b; m[1][1] = d; m[1][2] = b;
		m[2][0] = c; m[2][1] = f; m[2][2] = e;
		Matrix M = new Matrix(m);
		Debug.Log(M);
		Debug.Log(M.GetDeterminant());
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
}

