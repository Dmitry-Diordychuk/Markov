using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class StatesMatrix
    {
        //public static List<double> Intensity = new List<double>();
        public int Dimension { get; set; }
        protected double[,] matrix;
        public StatesMatrix(int d )
        {
            Dimension = d;
            matrix = new double[d,d];
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

    }
}
