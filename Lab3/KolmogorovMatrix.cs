using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Lab3
{
    class KolmogorovMatrix
    {
        int Dimension { get; set; }

        private double[,] matrix;
        private double[] B;
        public double[] Solution { get; set; }
        public KolmogorovMatrix(int d )
        {
            Dimension = d;
            matrix = new double[d,d];
            Solution = new double[d];
        }
        public double this[int i, int j]
        {
            get
            {
                return matrix[i, j];
            }
            set
            {
                matrix[i, j] = value;
            }
        }

        public KolmogorovMatrix(StatesMatrix statesMatrix)
            : this(statesMatrix.Dimension)
        {
            double sum;
            for( int i = 0; i < statesMatrix.Dimension; i++ )
            {
                sum = 0;
                for( int j = 0; j < statesMatrix.Dimension; j++ )
                {
                    if( statesMatrix[i, j] == 0 && statesMatrix[j, i] == 0 )
                    {
                        this.matrix[i, j] = 0;
                    }
                    else if( statesMatrix[i, j] != 0 && statesMatrix[j, i] == 0 )
                    {
                        this.matrix[i, j] = -statesMatrix[i, j];
                        sum = Math.Abs( sum - statesMatrix[i, j] );
                    }
                    else if( statesMatrix[i, j] == 0 && statesMatrix[j, i] != 0 )
                    {
                        this.matrix[i, j] = statesMatrix[j, i];
                        sum = Math.Abs( sum + statesMatrix[j, i] );
                    }
                    else if( statesMatrix[i, j] != 0 && statesMatrix[j, i] != 0 )
                    {
                        this.matrix[i, j] = statesMatrix[j, i] - statesMatrix[i, j];
                        sum = Math.Abs( sum + statesMatrix[j, i] - statesMatrix[i, j] );
                    }
                }
                StatesMatrix.Intensity.Add( sum );
            }
            for( int j = 0; j < this.Dimension; j++ )
                matrix[this.Dimension - 1, j] = 1;

            B = new double[Dimension];
            for( int i = 0; i < this.Dimension; i++ )
                if( i != Dimension - 1 )
                    B[i] = 0;
                else
                    B[i] = 1;
        }

        public double[] Solve()
        {
            Matrix<double> A = Matrix<double>.Build.DenseOfArray( matrix );
            var b = Vector<double>.Build.Dense( this.B );
            var x = A.Solve( b );
            return x.AsArray();
        }
    }
}
