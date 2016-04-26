using UnityEngine;
using System.Collections;

public class Rational {

	private Polynomial up;
	private Polynomial down;

	public Rational(Polynomial polynomial) {
		this.up = polynomial;
		this.down = new Polynomial(0);
		this.down.AddValue(0, 1); // p(x) = 1
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

	private bool IsPolynomial() {
		for (int i = 0; i < this.down.Order + 1; i++) {
			if (i != 0 && this.down[i] != 0) {
				return false;
			}
		}
		return true;
	}


	public override string ToString() {
        string result = "";
        result += this.up + "\n";
        for (int i = 0; i < 7*this.up.Order; i++) {
        	result += "-";
        }
        result += "\n";
        result += this.down + "\n";
        return result;
    }
}
