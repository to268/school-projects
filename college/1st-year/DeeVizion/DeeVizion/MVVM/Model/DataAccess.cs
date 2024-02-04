using System;
using System.Data.SqlClient;
using System.Windows;

namespace DeeVizion.MVVM.Model
{
    public class DataAccess
    {
        private SqlConnection connection;

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
        }

        public bool Connect()
        {
            bool isSuccessfull = false;

            try
            {
                connection = new SqlConnection();
                connection.ConnectionString =
                    "Data Source=srv-jupiter.iut-acy.local;" +
                    "Initial Catalog=BT1;" +
                    "Integrated Security=SSPI;";
                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                    isSuccessfull = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            if (isSuccessfull)
                isConnected = true;
            
            return isSuccessfull;
        }

        public void Disconnect()
        {
            if (!isConnected)
                return;
                    
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Disconnection Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            
            isConnected = false;
        }

        public SqlDataReader GetData(String query)
        {
            SqlDataReader reader = null;

            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return reader;
        }

        public bool SetData(String query)
        {
            bool isSuccessfull = false;

            try
            {
                if (Connect())
                {
                    int modifiedLines;
                    SqlCommand command = new SqlCommand(query, connection);

                    modifiedLines = command.ExecuteNonQuery();

                    if (modifiedLines > 0)
                        isSuccessfull = true;

                    Disconnect();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return isSuccessfull;
        }

        public String EscapeQuotes(String text)
        {
            return text.Replace("'", "''");
        }
    }
}
