using UnityEngine;
using System.Collections;

public class NumericalMethods {

	public static double VonMises(Polynomial p, double Xo, double err) {
		double precision = 99.99;
		double xOld, x, currEN = 100;
		x = Xo;
		xOld = x;
		if (Mathf.Abs((float) p.Derivative(Xo, precision)) < 0.00001) {
			throw new UnityException("Indetermined");
		}
		while(currEN > err) {
			x =  - p.f(x) / p.Derivative(x, precision);
			currEN = Mathf.Abs((float)((x - xOld) / x * 100));
			xOld = x;
		}
		return x;

	}

	public static double[] SyntheticDivision(Polynomial p, double div) {
		double[] polynomial = p.ToArray();
		double hold = 0;
		for (int i = polynomial.Length - 1; i > 0; i--) {
			hold += polynomial[i];
			hold *= div;
		}
		hold += polynomial[0];
		if (hold == 0.0) {
			return polynomial;
		} else {
			throw new UnityException("Not a root of p(x)");
		}
	}
}
/*bool sinteticDiv(double div, double const* p, int size) { //Precond: p is ordered in descendent degree
	double hold = 0;
	for (int i = 0; i < size - 1; i++) { //Divide by all terms of degree > 0
		hold += p[i];
		hold *= div;
	}
	hold += p[size - 1];
	return hold == 0.0;
}*/