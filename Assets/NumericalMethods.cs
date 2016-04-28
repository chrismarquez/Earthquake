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
			x +=  - p.f(x) / p.Derivative(x, precision);
			currEN = Mathf.Abs((float)((x - xOld) / x * 100));
			xOld = x;
		}
		return x;
	}

	public static double[] ExtractRoots(Polynomial p) {
		int size = p.Order;
		double initialValue = 0.9;
		double[] roots = new double[p.Order];
		Debug.Log(p.Order);
		for (int i = 0; i < size; i++) {
			Debug.Log(i);
			roots[i] = NumericalMethods.VonMises(p, initialValue, 0.001);
			Polynomial q = new Polynomial(1);
			q.AddValue(0, -roots[i]);
			q.AddValue(1, 1);
			Debug.Log(p);
			p = NumericalMethods.LongDivision(p, q);
			initialValue = roots[i];
		}
		return roots;
	}

	public static Polynomial LongDivision(Polynomial Up, Polynomial Down) {
		//Debug.Log(this.up.Order + ", " + this.down.Order);
		if (Down.Order == 0) {
			return Up / Down[0];
		}
		double[] result = new double[Up.Order - Down.Order + 1];
		double[] up = Up.ToArray();
		double[] down = Down.ToArray();
		//Debug.Log(new Polynomial(up));
		for (int i = Up.Order; i >= Down.Order; i--) {
			int j = Down.Order;
			//Debug.Log(i - j + ", " + i + ", " + j);
			result[i - j] = up[i] / down[j];
			int k = i;
			int a = i - j;
			while(j >= 0) {
				up[k--] += result[a] * down[j--] * -1;
			}
			//Debug.Log(new Polynomial(up));
		}
		return new Polynomial(result);
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