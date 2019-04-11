using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class StatesMatrix
    {
        int Dimension { get; set; }
        private int[,] matrix;
        public StatesMatrix(int d )
        {
            Dimension = d;
            matrix = new int[d,d];
        }
        public int this[int i, int j]
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
