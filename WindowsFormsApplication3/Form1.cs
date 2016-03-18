using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Net.Sockets;
using System.Net;
using MetroFramework;

using WindowsFormsApplication14;

namespace WindowsFormsApplication1
{
    public partial class Form1 : MetroForm
    {
        #region Variables
        
        List<MetroFramework.Controls.MetroTextBox> textos = new List<MetroFramework.Controls.MetroTextBox>();

        private static readonly List<Socket> _clientSockets = new List<Socket>();
        private const int _BUFFER_SIZE = 2048;
        private const int _PORT = 2000;
        private static readonly byte[] _buffer = new byte[_BUFFER_SIZE];
        public static string temp { get; set; }
        public string prevString = "";
        public string cambioOperario = "";
        baseDatos basedatos = new baseDatos("Database=espejo;Data Source=localhost;user Id=alejandro;Password=ima1;");

            

        #endregion
        public Form1()
        {

            InitializeComponent();
            timer4.Start();


            timer2.Enabled = true;
            this.StyleManager = metroStyleManager1;

            textos.Add(txordenpai1); textos.Add(txordenpai2); textos.Add(txordenpai3); textos.Add(txordenpai4); textos.Add(txordenpai5);
            textos.Add(txordenpai6); textos.Add(txordenpai7); textos.Add(txordenpai8); textos.Add(txordenpai9); textos.Add(txordenpai10);
            textos.Add(sku1); textos.Add(sku6); textos.Add(sku8); textos.Add(sku9); textos.Add(sku7); textos.Add(sku5);
            textos.Add(sku4); textos.Add(sku2); textos.Add(sku3); textos.Add(sku10);
            textos.Add(txlotepai1); textos.Add(txlotepai2); textos.Add(txlotepai3); textos.Add(txlotepai4); textos.Add(txlotepai5); textos.Add(txlotepai6);
            textos.Add(txlotepai7); textos.Add(txlotepai8); textos.Add(txlotepai9); textos.Add(txlotepai10);


        }

        #region Temas Skins
        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
           

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;

        }


        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            SKU_fromDB();
        }
        #region Funciones Logica 
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Int32 s = 1;
            Int32 s2 = 1;

            string cmnd = basedatos.consultarDato("texbox", "content", "id", "comando");
            Console.Write(cmnd);
            if (e.KeyCode == Keys.F5 && dataGridView1.SelectedCells.Count > 0 && cmnd.Equals(""))//Gen Text
            {
                s = dataGridView1.SelectedCells[0].RowIndex;
                s2 = dataGridView1.SelectedCells[0].ColumnIndex;

                StringBuilder genTxt = new StringBuilder();

                genTxt.Append("gtx:" + s.ToString() + "," + s2.ToString() + ",");

                foreach (DataGridViewCell cell in dataGridView1.Rows[s].Cells) {
                    genTxt.Append(cell.Value + ",");
                }
                genTxt.Remove(genTxt.Length - 1, 1);

                basedatos.cambiardato("texbox", "content", "id", genTxt.ToString(), "comando");
                Console.Write(genTxt.ToString());

            }
            if (e.KeyCode == Keys.F9 && dataGridView1.SelectedCells.Count > 0 && cmnd.Equals("")) // Borrar Row
            {
                s = dataGridView1.SelectedCells[0].RowIndex;

                string borrarRow = "blinea:" + s.ToString();
                basedatos.cambiardato("texbox", "content", "id", borrarRow, "comando");
            }
            if (!cmnd.Equals("") && (e.KeyCode == Keys.F9 || e.KeyCode == Keys.F5)) { MessageBox.Show("SERVIDOR OCUPADO"); }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                //SKU_fromDB();           
            }
            catch { }
        }         
        public void StringToDGV(string s) {
            if (!s.Equals(prevString))
            {
                string[] oldRows = prevString.Split(';');
                string[] newRows = s.Split(';');
                string[] cambOP = cambioOperario.Split(';');
                
                for (int i = 0; i < newRows.Length; i++)
                {
                    string[] cells = newRows[i].Split(',');

                    if (i < oldRows.Length && i>0)
                    {
                        dataGridView1.Rows.Insert(i, cells);
                        dataGridView1.Rows.RemoveAt(i-1);
                    }
                    else if (i >= oldRows.Length)
                    {
                        dataGridView1.Rows.Add(cells);
                    }
                }
                prevString = s;
            }

        }
        public void SKU_fromDB()
        {
            for (int i = 0; i < 30; i++)
            {
                textos[i].Text = basedatos.consultarDato("texbox", "content", "id", textos[i].Name);
                //textos[i].Text = textos[i].Text.ToUpper();
                
            }
        }
        private void SKU_TextChanged(object sender, EventArgs e)
        {      
            MetroFramework.Controls.MetroTextBox txtBx = sender as MetroFramework.Controls.MetroTextBox;
            basedatos.cambiardato("texbox", "content", "id", txtBx.Text, txtBx.Name);            
        }
        public void DGVCell_RCV(string s)
        {
            string[] splitString = s.Split(',');
            dataGridView1.Rows[Int32.Parse(splitString[1])].Cells[Int32.Parse(splitString[2])].Value = splitString[3];

            
        }
        public string makeRow(int rowIndex)
        {
            StringBuilder s = new StringBuilder();
            
            foreach (DataGridViewCell cell in dataGridView1.Rows[rowIndex].Cells)
            {
                
                s.Append(cell.Value);
                s.Append(",");
            }

            s.Remove(s.Length - 1, 1);
            return s.ToString();
        }
        #endregion
        private void metroButton1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(cambioOperario);
        }
        public string DGVtoString()
        {
            StringBuilder s = new StringBuilder();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach(DataGridViewCell cell in row.Cells)
                {
                    s.Append(cell.Value);
                    s.Append(",");
                }
                s.Remove(s.Length - 1, 1);
                s.Append(";");
            }
            s.Remove(s.Length - 1, 1);

            return s.ToString();
        }
        private void cellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cambioOperario = DGVtoString();
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            //StringToDGV(basedatos.consultarDato("texbox", "content", "id", "gtx"));
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            StringToDGV(metroTextBox1.Text);
        }
    }
}
