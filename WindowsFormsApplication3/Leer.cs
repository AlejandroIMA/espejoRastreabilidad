using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using MetroFramework;

namespace BlocNotasToDatagridview
{


    class Leer
    {
        public void lecturaArchivo(DataGridView tabla, char caracter, string ruta)
        {
            StreamReader objReader = new StreamReader(ruta);
            string sLine = "";
            int fila = 0;
            tabla.Rows.Clear();


            do
            {
                sLine = objReader.ReadLine();
                if ((sLine != null))
                {
                    if (fila == 0)
                    {
                        tabla.ColumnCount = sLine.Split(caracter).Length;
                        nombrarTitulo(tabla, sLine.Split(caracter));
                        fila += 1;
                    }
                    else
                    {
                        agregarFilaDatagridview(tabla, sLine, caracter);
                        fila += 1;
                    }

                }
            }

            while (!(sLine == null));
            objReader.Close();
        }
        public void addchivo(DataGridView tabla, char caracter, string ruta)
        {
            StreamReader objReader = new StreamReader(ruta);
            string sLine = "";
            //tabla.Rows.Clear();
            tabla.AllowUserToAddRows = false;

            do
            {
                sLine = objReader.ReadLine();
                if ((sLine != null))
                {

                    agregarFilaDatagridview(tabla, sLine, caracter);


                }
            }

            while (!(sLine == null));
            objReader.Close();
        }
        public void addchivomdf(DataGridView tabla, char caracter, string ruta,string name)
        {
            StreamReader objReader = new StreamReader(ruta);
            string sLine = "";
            //tabla.Rows.Clear();
            tabla.AllowUserToAddRows = false;

            do
            {
                sLine = objReader.ReadLine()+caracter+name;
                if ((sLine != null))
                {

                    agregarFilaDatagridview(tabla, sLine, caracter);


                }
            }

            while (false);
            objReader.Close();
        }


        public static void nombrarTitulo(DataGridView tabla, string[] titulos)
        {
            int x = 0;
            for (x = 0; x <= tabla.ColumnCount - 1; x++)
            {
                tabla.Columns[x].HeaderText = titulos[x];
            }
        }


        public static void agregarFilaDatagridview(DataGridView tabla, string linea, char caracter)
        {
            string[] arreglo = linea.Split(caracter);
            tabla.Rows.Add(arreglo);
        }
        public string buscar(string sku, DataGridView tabla)
        {
            string cajas;
           
            cajas = "0";
            for (int i = 0; i < tabla.Rows.Count - 1; i++)
            {

                if (sku.Equals(tabla.Rows[i].Cells[0].Value.ToString()))
                {
                    
                    cajas = tabla.Rows[i].Cells[4].Value.ToString();
                    if(cajas.Equals(""))
                    {
                        cajas = "0";
                    }
                    break;

                }
            }



            return cajas;
        }
        public string buscardes(string sku, DataGridView tabla)
        {
            string cajas;

            cajas = "0";
            for (int i = 0; i < tabla.Rows.Count - 1; i++)
            {

                if (sku.Equals(tabla.Rows[i].Cells[0].Value.ToString()))
                {
                    cajas = tabla.Rows[i].Cells[1].Value.ToString();
                    break;

                }
            }



            return cajas;
        }
        public void guaradardata(string ruta, DataGridView tabla)
        {

            TextWriter sw = new StreamWriter(ruta);

            for (int x = 0; x <= tabla.ColumnCount - 1; x++)
            {
                if (x < tabla.ColumnCount - 1)
                {
                    sw.Write(tabla.Columns[x].HeaderText + ",");
                }
                else
                {
                    sw.Write(tabla.Columns[x].HeaderText);
                }
            }
            sw.WriteLine("");
            for (int i = 0; i < tabla.Rows.Count - 1; i++)
            {
                for (int j = 0; j < tabla.Columns.Count; j++)
                {
                    if (j < tabla.ColumnCount - 1)
                    {
                        sw.Write(tabla.Rows[i].Cells[j].Value.ToString() + ",");
                    }
                    else { sw.Write(tabla.Rows[i].Cells[j].Value.ToString()); }

                }
                sw.WriteLine("");

            }
            sw.Close();

        }


        public void captura_programacion(List<MetroFramework.Controls.MetroTextBox> tabla, char caracter, string ruta)
        {
            ruta = @"D:\p.txt";
            StreamReader objReader = new StreamReader(ruta);
            string sLine = "";
            string[] array=sLine.Split(',');
            for (int i = 0; i < sLine.Split(',').Length;i++ )
                {
                    tabla[i].Text = array[i];
            
            }
                sLine = objReader.ReadToEnd();
            objReader.Close();
        }

        //---------------------------------------------------------------------------------------------------------------------------
        public void TCP_Lectura(DataGridView tabla, NetworkStream stream)
        {
            StreamReader objReader = new StreamReader(stream);
            string sLine = "";
            string[] Filas;
            tabla.Rows.Clear();
            
            sLine = objReader.ReadLine();
            Filas = sLine.Split(';');

            for (int i = 0; i > Filas.Length; i++)
            {
                if (Filas[i] != null)
                {
                    agregarFilaDatagridview(tabla, Filas[i], ',');
                }
            }
            
            objReader.Close();
        }
    }
}
