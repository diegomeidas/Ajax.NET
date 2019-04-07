using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Core.Filtro;

namespace Core.Dal
{
    public class DataBaseClass
    {

        //string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;
        //IDbConnection conexao = new SqlConnection(stringConexao);
        //conexao.Open();




        private SqlConnection con;
        private string stringConexao = ConfigurationManager.ConnectionStrings["ConexaoDev"].ConnectionString;

        public SqlConnection Conectar()
        {
            
            con.Close();
            con = new SqlConnection(stringConexao);            

            try
            {
                con.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("Erro ao conectar: "+e.Message);
                throw new Exception("Erro ao conectar: " + e.Message);
            }           

            return con;
        }

        public void Close()
        {
            con.Close();
        }

        


            /*
            private MySqlConnection conn;
            //private MySqlCommand command;

            public MySqlConnection Conectar()
            {

                //Define string de conexão
                conn = new MySqlConnection("Persist Security Info=True;Server=localhost;Database=bdprojshop;Uid=root;Pwd=''");

                try
                {
                    conn.Open();
                }
                catch
                {
                    MessageBox.Show("Impossível estabelecer conexão", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                return conn;

            }
            */
    }
    }
