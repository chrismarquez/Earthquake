using UnityEngine;
using System.Collections.Generic;

public class Matrix {
	private Rational[,] matrix;
	int m, n;

	public int M {
		get {
			return this.m;
		}
	}

	public int N {
		get {
			return this.n;
		}
	}

	public Matrix(int n) : this(n, n) {

	}

	public Matrix(int m, int n) {
		this.matrix = new Rational[this.m = m, this.n = n];
	}

	public Matrix(Rational[,] matrix) {
		this.matrix = matrix;
		this.m = matrix.Rank;
		this.n = matrix.GetLength(0);
		Debug.Log("m: " + this.m + ", n: " + this.n);
	}

	//Only for square matrices
	/*
	public void SetIdentity() {
		if (this.m == this.n) {
			for (int i = 0; i < n; i++) {
				this.matrix[i][i] = 1;
			}
		} else {
			Debug.LogError("Matrix is not a square matrix");
		}
	}*/

	public static Matrix operator + (Matrix A, Matrix B) {
		Rational[,] sum = new Rational[A.M, A.N];
		for (int i = 0; i < A.M; i++) {
			for (int j = 0; j < A.N; j++) {
				sum[i,j] = A.matrix[i,j] + B.matrix[i,j];
			}
		}
		return new Matrix(sum);
	}

	public static Matrix operator - (Matrix A, Matrix B) {
		Rational[,] sum = new Rational[A.M, A.N];
		for (int i = 0; i < A.M; i++) {
			for (int j = 0; j < A.N; j++) {
				sum[i,j] = A.matrix[i,j] - B.matrix[i,j];
			}
		}
		return new Matrix(sum);
	}

	public static Matrix operator * (Matrix A, Matrix B) {
		Rational[,] result = new Rational[A.M, B.N];
		for (int i = 0; i < A.M; i++) {
			for (int j = 0; j < B.N; j++) {
				Rational slot = new Rational(new Polynomial(0));
				for (int k = 0; k < A.N; k++) {
					slot += A.matrix[i,k] * B.matrix[k,j];
				}
				result[i,j] = slot;
			}
		}
		return new Matrix(result);
	}

	//Only applies to square matrices
	public Matrix GetInverse() {
		if (this.m == this.n) {
			return null;
		} else {
			return null;
		}
	}

	public void RowByScalar(int row, double scalar) {
		for (int i = 0; i < this.matrix.GetLength(0); i++) {
			this.matrix[row,i] /= scalar;
		}
	}

	public void SwapRow(int rowA, int rowB) {
		
	}

	public override string ToString() {
		string line = "";
		for (int i = 0; i < this.m; i++) {
			for (int j = 0; j < this.n; j++) {
				line += this.matrix[i,j] + " ";
			}
			line += "\n";
		}
		return line;
	}
}
