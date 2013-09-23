using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewVirtualModePaging
{
	public partial class Form1 : Form
	{
		const int PAGE_SIZE = 5000;

		NameListCache _cache = null;

       

		public Form1()
		{
            

			InitializeComponent();

			_cache = new NameListCache( PAGE_SIZE );

			dataGridView1.CellValueNeeded += 
				new DataGridViewCellValueEventHandler( dataGridView1_CellValueNeeded );

			dataGridView1.VirtualMode = true;

			dataGridView1.RowCount = (int)_cache.TotalRowsNumber;
		}

		private void dataGridView1_CellValueNeeded( object sender, DataGridViewCellValueEventArgs e )
		{
			_cache.LoadPage( e.RowIndex );

            if (e.RowIndex > 50)
            { 
            
            }

			int rowIndex = e.RowIndex % _cache.PageSize;

            if (rowIndex > 2000)
            {
                var dddd = 4;
            }

          e.Value = _cache.CachedData[rowIndex][e.ColumnIndex];
		}
	}
}