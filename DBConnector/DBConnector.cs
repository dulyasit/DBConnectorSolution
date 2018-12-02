using System;
using System.Collections.Generic;
using System.Web.Configuration;
using MySql.Data.MySqlClient;

namespace DBConnector
{
    public class DBConnector
    {
        /*
MySqlConnection connection;
string cs = @"server=server ip;userid=username;password=userpass;database=databse";
connection = new MySqlConnection(cs);
connection.Open();

MySqlCommand command = new MySqlCommand();
string SQL = "INSERT INTO `twMCUserDB` (`mc_userName`, `mc_userPass`, `tw_userName`, `tw_userPass`) VALUES ('@mcUserName', '@mcUserPass', '@twUserName', '@twUserPass')";
command.CommandText = SQL;
command.Parameters.Add("@mcUserName", mcUserNameNew);
command.Parameters.Add("@mcUserPass", mcUserPassNew);
command.Parameters.Add("@twUserName", twUserNameNew);
command.Parameters.Add("@twUserPass", twUserPassNew);
command.Connection = connection;
command.ExecuteNonQuery();
connection.Close();
        */
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnector()
        {
            Initialize();
        }
        private void Initialize()
        {
            /*
            server = "localhost";
            database = "dbparty";
            uid = "partyuser";
            password = "partyuser";
            */
            server = "";
            database = "";
            uid = "";
            password = "";
        }
        public bool OpenConnection()
        {
            try
            {
                string uid = WebConfigurationManager.AppSettings["user"];
                string password = WebConfigurationManager.AppSettings["pass"];
                string database = WebConfigurationManager.AppSettings["database"];
                string server = WebConfigurationManager.AppSettings["server"];
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                connection = new MySqlConnection(connectionString);
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<string>[] Select()
        {
            string query = "SELECT * FROM bank_name";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["bank_code"] + "");
                    list[1].Add(dataReader["bank_name"] + "");
                    list[2].Add(dataReader["last_update"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        //Insert statement
        public void Insert()
        {
            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete()
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
