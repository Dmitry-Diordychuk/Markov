using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load( object sender, EventArgs e )
        {
            SetStartingTable();
        }

        private void SetStartingTable()
        {
            //Prevent to auto adding rows on the end of the table
            dataGridView1.AllowUserToAddRows = false;
            //Add 3 starting colums and 3 starting rows
            for( int i = 1; i <= Table.RowsAndCols; i++ )
            {
                AddDimension( i );
            }
        }

        private void AddDimension( int i )
        {
            DataGridViewTextBoxColumn stateCol = new DataGridViewTextBoxColumn();
            //Col
            stateCol.HeaderText = i.ToString();
            stateCol.Width = Table.CellSize;
            dataGridView1.Columns.Add( stateCol );
            //Row
            dataGridView1.Rows.Add();
            dataGridView1.Rows[i - 1].HeaderCell.Value = ( i ).ToString();
        }

        private void AddButton_Click( object sender, EventArgs e )
        {
            if( Table.RowsAndCols < 10 )
                AddDimension( ++Table.RowsAndCols );
        }

        private void DeleteButton_Click( object sender, EventArgs e )
        {
            if( Table.RowsAndCols > 0 )
            {
                dataGridView1.Rows.RemoveAt( --Table.RowsAndCols );
                dataGridView1.Columns.RemoveAt( Table.RowsAndCols );
            }
        }


        private void CalculateButton_Click( object sender, EventArgs e )
        {
            //Transfer data in array
            StatesMatrix matrix = new StatesMatrix( Table.RowsAndCols );
            int parseResult;
            for(int i = 0; i < Table.RowsAndCols; i++ )
            {
                for(int j = 0; j < Table.RowsAndCols; j++ )
                {
                    if( dataGridView1.Rows[i].Cells[j].Value != null )
                    {
                        if( int.TryParse( dataGridView1.Rows[i].Cells[j].Value.ToString(), out parseResult ) == true )
                        {
                            matrix[i, j] = parseResult;
                        }
                        else
                        {
                            MessageBox.Show( "Wrong input! Please put in table only numbers." );
                        }
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }

                }
            }

            KolmogorovMatrix kolmogorov = new KolmogorovMatrix( matrix );
            double[] array = kolmogorov.Solve();
            int n = 0;
            foreach( var x in array )
            {
                listView1.Items.Add( $"P{n}(t) = " + x.ToString() );
                listView1.Items.Add( $"time{n}:" + ( ( ( x * ( 1 - x ) ) / 0.0025 ) * 2.7 ).ToString() );
            }
            
        }
    }
}
