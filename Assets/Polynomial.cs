using UnityEngine;
using System.Collections;

public class Polynomial {

	private double[] polynomial;
	private int order;

	public int Order {
		get {
			return this.order;
		}
	}

	public Polynomial(int order) {
		this.polynomial = new double[order + 1];
		this.order = order;
	}

	public Polynomial(double[] polynomial) {
		this.polynomial = polynomial;
		this.order = polynomial.Length - 1;
	}

	public void AddValue(int index, int data) {
		this.polynomial[index] += data;
	}

	public static Polynomial operator + (Polynomial a, Polynomial b) {
		int order = (a.order > b.order ? a.order : b.order);
		double[] sum = new double[order + 1];
		for (int i = 0; i < order + 1; i++) {
			if (i >= a.polynomial.Length) {
				sum[i] = b.polynomial[i];	
			} else if (i >= b.polynomial.Length){
				sum[i] = a.polynomial[i];
			} else {
				sum[i] = a.polynomial[i] + b.polynomial[i];
			}	
		}
		return new Polynomial(sum);
	}

	public static Polynomial operator - (Polynomial a, Polynomial b) {
		int order = (a.order > b.order ? a.order : b.order);
		double[] sum = new double[order + 1];
		for (int i = 0; i < order + 1; i++) {
			if (i >= a.polynomial.Length) {
				sum[i] = b.polynomial[i];	
			} else if (i >= b.polynomial.Length){
				sum[i] = a.polynomial[i];
			} else {
				sum[i] = a.polynomial[i] - b.polynomial[i];
			}	
		}
		return new Polynomial(sum);
	}

	public static Polynomial operator * (Polynomial a, Polynomial b) {
		int order = a.order + b.order;
		double[] product = new double[order + 1];
		for (int i = 0; i < a.order + 1; i++) {
			for (int j = 0; j < b.order + 1; j++) {
				product[i + j] += a.polynomial[i] * b.polynomial[j];
			}
		}
		return new Polynomial(product);
	}

	public static Polynomial operator * (Polynomial a, double c) {
		double[] product = a.polynomial;
		for (int i = 0; i < a.polynomial.Length; i++) {
			product[i] *= c;
		}
		return new Polynomial(product);
	}

	public static Polynomial operator / (Polynomial a, double c) {
		double[] product = a.polynomial;
		for (int i = 0; i < a.polynomial.Length; i++) {
			product[i] /= c;
		}
		return new Polynomial(product);
	}


	public override string ToString() {
        string result = "";
        for (int i = 0; i < this.order + 1; i++) {
        	if (this.polynomial[i] == -1) {
        		result += "-" + (i == 0 ? " ": (i == 1 ? "λ " : "λ" + ExpFormat(i) + " "));
        	} else if (this.polynomial[i] != 1) {
        		result += this.polynomial[i] + (i == 0 ? " ": (i == 1 ? "λ " : "λ" + ExpFormat(i) + " "));
        	} else if (this.polynomial[i] == 1 && i == 0){
        		result += 1 + " ";
        	} else {
        		result += (i == 0 ? " ": (i == 1 ? "λ " : "λ" + ExpFormat(i) + " "));
        	}
        	result += i < this.order ? " + " : "";
        }
        return result;
    }

    private string ExpFormat(int i) {
    	switch(i) {
    		case 0:
    			return "";
    		case 1:
    			return "";
    		case 2:
    			return "\u00B2";
    		case 3:
    			return "\u00B3";
    		case 4:
    			return "\u2074";
			case 5:
				return "\u2075";
			case 6:
				return "\u2076";
			case 7:
				return "\u2077";
			case 8:
				return "\u2078";
			case 9:
				return "\u2079";
			default:
				return "^" + i;
    	}

    }

    public double this [int i]  {
    	get {
    		return this.polynomial[i];
    	}
    }
}
