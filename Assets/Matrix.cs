using UnityEngine;
using System.Collections.Generic;

public class Matrix {
	private Rational[][] matrix;
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
		this.n = n;
		this.matrix = new Rational[this.m = m][];
		for (int i = 0; i < this.matrix.Length; i++) {
			this.matrix[i] = new Rational[n];
		}
	}

	public Matrix(Rational[][] matrix) {
		this.matrix = matrix;
		this.m = matrix.Length;
		this.n = matrix[0].Length;
		//Debug.Log("m: " + this.m + ", n: " + this.n);
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
		Rational[][] sum = new Rational[A.M][];
		for (int i = 0; i < sum.Length; i++) {
			sum[i] = new Rational[A.N];
		}
		for (int i = 0; i < A.M; i++) {
			for (int j = 0; j < A.N; j++) {
				sum[i][j] = A.matrix[i][j] + B.matrix[i][j];
			}
		}
		return new Matrix(sum);
	}

	public static Matrix operator - (Matrix A, Matrix B) {
		Rational[][] sum = new Rational[A.M][];
		for (int i = 0; i < sum.Length; i++) {
			sum[i] = new Rational[A.N];
		}
		for (int i = 0; i < A.M; i++) {
			for (int j = 0; j < A.N; j++) {
				sum[i][j] = A.matrix[i][j] - B.matrix[i][j];
			}
		}
		return new Matrix(sum);
	}

	public static Matrix operator * (Matrix A, Matrix B) {
		Rational[][] result = new Rational[A.M][];
		for (int i = 0; i < result.Length; i++) {
			result[i] = new Rational[B.N];
		}
		for (int i = 0; i < A.M; i++) {
			for (int j = 0; j < B.N; j++) {
				Rational slot = new Rational(new Polynomial(0));
				for (int k = 0; k < A.N; k++) {
					slot += A.matrix[i][k] * B.matrix[k][j];
				}
				result[i][j] = slot;
			}
		}
		return new Matrix(result);
	}
	//Only applies to square matrices
	public Matrix GetInverse() {
		if (this.m == this.n) {

			Rational[][] augMatrix = new Rational[n][];

			for (int i = 0; i < augMatrix.Length; i++) {
				augMatrix[i] = new Rational[2 * n];
			}

			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					augMatrix[i][j] = this.matrix[i][j];
				}
			}

			for (int i = 0; i < n; i++) {
				for (int j = n; j < 2*n; j++) {
					if (i + n == j) {
						augMatrix[i][j] = new Rational(new Polynomial());
					} else {
						augMatrix[i][j] = new Rational(new Polynomial(0));
					}
				}
			}
			Debug.Log(new Matrix(augMatrix)); //Checked up to here

			for (int j = 0; j < n; j++) {
				Debug.Log(new Matrix(augMatrix));
				int swapper = 0;
				while(augMatrix[j][j].IsZero()) {
					if (j + ++swapper >= n) {
						break;
					}
					Matrix.SwapRow(ref augMatrix, j, j + swapper);
				}
				Debug.Log(new Matrix(augMatrix));
				if (j + swapper >= n) {
					continue;
				}
				Matrix.RowByScalar(ref augMatrix, j, augMatrix[j][j]);
				Debug.Log(new Matrix(augMatrix));
				for (int i = 0; i < n; i++) {
					if (!augMatrix[i][j].IsZero() && i != j) {
						Matrix.AddRowMultiple(ref augMatrix, i, j, augMatrix[i][j] * -1);
					}
				}
			}
			Debug.Log(new Matrix(augMatrix));
			Rational[][] inverse = new Rational[n][];
			for (int i = 0; i < n; i++) {
				inverse[i] = new Rational[n];
			}
			for (int i = n; i < 2*n; i++) {
				for (int j = n; j < 2*n; j++) {
					inverse[i-n][j-n] = augMatrix[i - n][j];
				}
			}
			return new Matrix(inverse);
		} else {
			return null;
		}
	}

	public Polynomial GetDeterminant() {
		Rational determinant = new Rational(new Polynomial());
		if (this.n == 2) {
			determinant = this.matrix[0][0] * this.matrix[1][1] - this.matrix[0][1] * this.matrix[1][0];
		} else {
			Rational[][] detMatrix = new Rational[n][];

			for (int i = 0; i < detMatrix.Length; i++) {
				detMatrix[i] = new Rational[n];
			}

			for (int i = 0; i < n; i++) {
				for (int j = 0; j < n; j++) {
					detMatrix[i][j] = this.matrix[i][j];
				}
			}

			for (int j = 0; j < n; j++) {
				int swapper = 0;
				while(detMatrix[j][j].IsZero()) {
					if (j + ++swapper >= n) {
						break;
					}
					Matrix.SwapRow(ref detMatrix, j, j + swapper);
					determinant *= -1;
				}
				if (j + swapper >= n) {
					continue;
				}
				determinant *= detMatrix[j][j];
				determinant = new Rational(determinant.LongDivision());
				Matrix.RowByScalar(ref detMatrix, j, detMatrix[j][j]);
				for (int i = j; i < n; i++) {
					if (!detMatrix[i][j].IsZero() && i != j) {
						Matrix.AddRowMultiple(ref detMatrix, i, j, detMatrix[i][j] * -1);
					}
				}
			}
			for (int i = 0; i < n; i++) {
				determinant *= detMatrix[i][i];
				determinant = new Rational(determinant.LongDivision());
			}
		}
		Debug.Log(determinant);
		return determinant.LongDivision();
	}

	/*double getDeterminant(double** matrix, int n) {
	double determinant = 1;
	if (n == 2) {
		return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
	} else {
		double** detMatrix = copyMatrix(matrix, n, n);
		for (int j = 0; j < n; j++) {
			int swapper = 0;
			while(detMatrix[j][j] == 0) {
				if (j + ++swapper >= n) {
					break;
				}
				swapRow(detMatrix, j, j + swapper);
				determinant *= -1;

			}
			if (j + swapper >= n) {
				continue;
			}
			determinant *= detMatrix[j][j];
			rowByScalar(detMatrix, j, n + 1, detMatrix[j][j]);
			for (int i = j; i < n; i++) {
				if (detMatrix[i][j] != 0 && i != j) {
					addRowMultiple(detMatrix, n, i, j, -detMatrix[i][j]);
				}
			}
		}
		for (int i = 0; i < n; i++) {
			determinant *= detMatrix[i][i];
		}
		deleteMatrix(detMatrix, n, n);
		return determinant;
	}
}*/

	public static void RowByScalar(ref Rational[][] matrix, int row, Rational scalar) {
		//Debug.Log(new Matrix(matrix));
		for (int i = 0; i < matrix[0].Length; i++) {
			//Debug.Log("matrix at i, j : " + row + ", " + i + " ->" + matrix[row][i]);
			//Debug.Log("scalar ->" + scalar);

			matrix[row][i] /= scalar;
			matrix[row][i] = new Rational(matrix[row][i].LongDivision());
			//Debug.Log(new Matrix(matrix));
		}
	}

	public static void SwapRow(ref Rational[][] matrix, int rowA, int rowB) {
		Rational[] temp = new Rational[matrix[0].Length];
		for (int i = 0; i < matrix[0].Length; i++) {
			temp[i] = matrix[rowA][i];
		}
		for (int i = 0; i < matrix.Length; i++) {
			matrix[rowA][i] = matrix[rowB][i];
			matrix[rowB][i] = temp[i];
		}
	}

	public static void AddRowMultiple(ref Rational[][] matrix, int rowA, int rowB, Rational c) {
		for (int j = 0; j < matrix[0].Length; j++) {
			matrix[rowA][j] += matrix[rowB][j] * c;
			matrix[rowA][j] = new Rational(matrix[rowA][j].LongDivision());
		}
	}

	public override string ToString() {
		string line = "";
		for (int i = 0; i < this.m; i++) {
			for (int j = 0; j < this.n; j++) {
				line += this.matrix[i][j] + " ";
			}
			line += "\n";
		}
		return line;
	}
}
