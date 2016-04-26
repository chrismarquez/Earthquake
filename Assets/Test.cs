using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		double[] a , b;
		a = new double[] {4, -1};
		b = new double[] {2, -1};

		Polynomial A = new Polynomial(a);
		Polynomial B = new Polynomial(b);
		print("A: " + A);
		print("B: " + B);
		Rational Q = new Rational(A);
		Rational R = new Rational(B);
		print(Q + R);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
