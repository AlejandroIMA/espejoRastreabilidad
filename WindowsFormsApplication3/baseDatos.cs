using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication14
{
    class baseDatos
    {
        public string conexion { get; set; }
        public int contador = 0;
        MySqlConnection con;
        MySqlCommand coman = new MySqlCommand();


        public baseDatos(string cadenaconexion) //constructor de la clase se ejecuta al crear un objecto referenciado
        {
            conexion = cadenaconexion;
            con = new MySqlConnection(conexion);
            coman.Connection = con;
            

        }

        public bool existeDato(string tabladb, string tipodato, string valor)// consultar si existe un dato
        {
            try
            {
                con.Open();
                bool existe = false;
                coman.CommandText = "SELECT " + tipodato + " FROM " + tabladb + " WHERE " + tipodato + "=" + "'" + valor + "'";
                MySqlDataReader read;
                read = coman.ExecuteReader();
                if (read.Read())
                {
                    existe = true;
                }
                con.Close();
                return existe;
            }
            catch { return false; }

        }
        public void insertarFila(string tabla, string valores)//insertar fila 
        {
            try
            {
                con.Open();

                coman.CommandText = "insert into " + tabla + " values( " + valores + ");";
                coman.ExecuteReader();
                con.Close();
            }
            catch { }
        }
        public string consultarDato(string tabladb, string tipodato, string condicion, string valor)
        {

            string dato = "";
            try
            {
                con.Open();
                coman.CommandText = "SELECT " + tipodato + " FROM " + tabladb + " WHERE " + condicion + "=" + "'" + valor + "'";
                MySqlDataReader read;
                read = coman.ExecuteReader();
                if (read.Read())
                {
                    dato = read.GetString(tipodato);
                }
                con.Close();
            }
            catch { };
            return dato;

        }
        public void filtrofecha(DataGridView tabla, string tabladb, string f1, string f2, string h1, string h2)
        {
            string consulta = "";

            try
            {
                con.Open();
                consulta = "SELECT * FROM " + tabladb + " WHERE fecha between " + "'" + f1 + "'" + " and " + "'" + f2 + "'" + "AND hora between " + "'" + h1 + "'" + " and " + "'" + h2 + "'" + ";";

                MySqlDataAdapter data = new MySqlDataAdapter(consulta, con);
                DataSet ds = new DataSet();
                data.Fill(ds, tabladb);
                tabla.DataSource = ds;
                tabla.DataMember = tabladb;
                con.Close();
            }
            catch { MessageBox.Show("error base de datos"); }

        }
        public void getTabla(DataGridView tabla, string tabladb)
        {
            try
            {
                string consulta = "";
                con.Open();

                consulta = "SELECT * FROM " + tabladb + ";";

                MySqlDataAdapter data = new MySqlDataAdapter(consulta, con);
                DataSet ds = new DataSet();
                data.Fill(ds, tabladb);
                tabla.DataSource = ds;
                tabla.DataMember = tabladb;
                con.Close();

            }
            catch { }



        }
        public bool verificarPass(string password)// parte donde se verifica la contraseña
        {

            return consultarDato("usuario", "contraseña", "accesoValeo", "nombre") == password;

        }
        public void cambiarPass(string nuevoPass, string actualPass)
        {
            try
            {
                con.Open();

                coman.CommandText = "UPDATE usuario SET contraseña ='" + nuevoPass + "' WHERE contraseña ='" + actualPass + "' ;";
                coman.ExecuteReader();

                con.Close();
            }
            catch { };

        }

        public void cambiardato(string tabla, string columna1, string columna2, string nuevoPass, string actualPass)
        {
            try
            {
                con.Open();

                coman.CommandText = "UPDATE " + tabla + " SET " + columna1 + " ='" + nuevoPass + "' WHERE " + columna2 + " ='" + actualPass + "' ;";
                coman.ExecuteReader();

                con.Close();
            }
            catch { };

        }


        public void borrar(string tabladb, string tipodato, string valor, string fecha)
        {
            try
            {
                con.Open();

                coman.CommandText = "delete from " + tabladb + " where " + tipodato + "= '" + valor + "'and fecha = '" + fecha + "';";

                coman.ExecuteReader();

                con.Close();
            }
            catch { };
        }

        public void actualizar(string tabla, string colName, string valorAct, string ValorFiltro)
        {
            try
            {
                con.Open();
                coman.CommandText = "UPDATE " + tabla + " SET " + colName + " ='" + valorAct + "' WHERE " + colName + " ='" + ValorFiltro + "' ;";
                coman.ExecuteReader();

                con.Close();
            }
            catch { };

        }

    }


}

