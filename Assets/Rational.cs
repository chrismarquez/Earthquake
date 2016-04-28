using UnityEngine;
using System.Collections;

public class Rational {

	private Polynomial up;
	private Polynomial down;

	public Rational(Polynomial polynomial) {
		this.up = polynomial;
		this.down = new Polynomial();
	}

	public Rational(Polynomial up, Polynomial down) {
		this.up = up;
		this.down = down;
	}

	public static Rational operator + (Rational a, Rational b) {
		a.up *= b.down;
		b.up *= a.down;
		a.down = b.down = a.down * b.down;
		return new Rational(a.up + b.up, a.down);
	}

	public static Rational operator - (Rational a, Rational b) {
		a.up *= b.down;
		b.up *= a.down;
		a.down = b.down = a.down * b.down;
		return new Rational(a.up - b.up, a.down);
	}

	public static Rational operator * (Rational a, Rational b) {
		Polynomial up = a.up * b.up;
		Polynomial down = a.down * b.down;
		return new Rational(up, down);
	}

	public static Rational operator * (Rational a, double c) {
		Polynomial up = a.up;
		up *= c;
		return new Rational(up, a.down);
	}

	public static Rational operator / (Rational a, Rational b) {
		Polynomial up = a.up * b.down;
		Polynomial down = a.down * b.up;
		return new Rational(up, down);
	}

	public static Rational operator / (Rational a, double c) {
		Polynomial up = a.up;
		up /= c;
		return new Rational(up, a.down);
	}

	public bool IsZero() {
		return this.up.IsZero();
	}

	private bool IsPolynomial() {
		for (int i = 0; i < this.down.Order + 1; i++) {
			if (this.down[i] != 0) {
				return false;
			}
		}
		return true;
	}

	public Polynomial LongDivision() {
		//Debug.Log(this.up.Order + ", " + this.down.Order);
		if (this.down.Order == 0) {
			return this.up / this.down[0];
		}
		double[] result = new double[this.up.Order - this.down.Order + 1];
		double[] up = this.up.ToArray();
		double[] down = this.down.ToArray();
		Debug.Log(new Polynomial(up));
		for (int i = this.up.Order; i >= this.down.Order; i--) {
			int j = this.down.Order;
			Debug.Log(i - j + ", " + i + ", " + j);
			result[i - j] = up[i] / down[j];
			int k = i;
			int a = i - j;
			while(j >= 0) {
				up[k--] += result[a] * down[j--] * -1;
			}
			Debug.Log(new Polynomial(up));
		}
		return new Polynomial(result);
	}


	public override string ToString() {
		if (IsPolynomial()) {
			string result = "";
			result += this.up;
			return result;
		} else {
			string result = "";
	        result += "((" + this.up + ") / (" + this.down + "))";
	        return result;
		}
        
    }
}
