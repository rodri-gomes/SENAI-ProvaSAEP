using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using ProvaSAEP.csharp;

namespace ProvaSAEP.csharp
{
    public class Funcoes
    {

        BancoDeDados Banco = new BancoDeDados();

        // LISTAGEM DOS AUTOMOVEIS POR ÁREA
        public static List<alocacao> Listagem_Automoveis(string Area)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT alocacao.id_alocacao, automoveis.id_automovel, ");
            sb.AppendLine("automoveis.modelo, automoveis.preco FROM");
            sb.AppendLine("alocacao INNER JOIN automoveis ON alocacao.automoveis = automoveis.id_automovel");
            sb.AppendLine("WHERE alocacao.area = '" + Area + "' ");
            //Create a list to store the result
            List<alocacao> lista_autos = new List<alocacao>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    alocacao listagemautos = new alocacao();
                    listagemautos.id = dataReader[0].ToString();
                    listagemautos.area = dataReader[1].ToString();
                    listagemautos.automoveis = dataReader[2].ToString();
                    listagemautos.concessionaria = dataReader[3].ToString();
                    lista_autos.Add(listagemautos);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return lista_autos;
            }
            else
            {
                return lista_autos;
            }
        }

        // LISTAGEM DE TODOS OS CLIENTES
        public static List<cliente> Clientes()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM clientes");

            //Create a list to store the result
            List<cliente> clientes = new List<cliente>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    cliente listagemclientes = new cliente();

                    listagemclientes.id = dataReader[0].ToString();
                    listagemclientes.nome = dataReader[1].ToString();

                    clientes.Add(listagemclientes);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return clientes;
            }
            else
            {
                return clientes;
            }
        }

        // LISTAGEM DE CONCESSIONÁRIAS POR ÁREA E AUTOMÓVEL ESCOLHIDO
        public static List<concessionaria> Concessionarias(string Area, string IdAuto)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT concessionarias.id_conces, concessionarias.concessionaria FROM alocacao");
            sb.AppendLine("INNER JOIN concessionarias ON ");
            sb.AppendLine("concessionarias.id_conces = alocacao.concessionaria");
            sb.AppendLine("WHERE alocacao.automoveis = '"+ IdAuto +"' ");
            sb.AppendLine("AND alocacao.area = '"+ Area +"' ");

            //Create a list to store the result
            List<concessionaria> Concess = new List<concessionaria>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    concessionaria ListaConces = new concessionaria();

                    ListaConces.id = dataReader[0].ToString();
                    ListaConces.nome = dataReader[1].ToString();

                    Concess.Add(ListaConces);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return Concess;
            }
            else
            {
                return Concess;
            }
        }

        // CONCLUI VENDA E REMOVE AUTOMOVEL DO BANCO DE DADOS
        public static void delete(string IdAloc)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DELETE FROM alocacao WHERE");
            sb.AppendLine("alocacao.id_alocacao = '" + IdAloc + "' ");


            if (BancoDeDados.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();

                BancoDeDados.CloseConnection();
            }
        }

        // VÊ SE TEM

        public static bool checagem(string Area)
        {
            string query = "SELECT Count(*) FROM alocacao WHERE area = '" + Area + "' ";

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, BancoDeDados.conn);
                //Create a data reader and Execute the command
                try
                {
                    //Recebe o numero de usuários encontrados com os parametros enviados
                    int count = int.Parse(cmd.ExecuteScalar().ToString());

                    //close Connection
                    BancoDeDados.CloseConnection();

                    //Se encontrou o usuário no BD seta a resposta para true
                    if (count > 0)
                    {
                        return true;
                    }
                    return false;

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}