using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace DeeVizion.MVVM.Model
{
    public class Division : Crud<Division>
    {
        private int idDivision;
        public int IdDivision
        {
            get { return idDivision; }
        }

        private int idCorps;
        public int IdCorps
        {
            get { return idCorps; }
            set
            {
                idCorps = value;
                Update();
            }
        }

        private String libelleDivision;
        public String LibelleDivision
        {
            get { return libelleDivision; }
            set
            {
                libelleDivision = value;
                Update();
            }
        }

        public Division()
        {
            idDivision = 0;
            idCorps = 0;
            libelleDivision = "";
        }

        public Division(int idDivision, int idCorps, String libelleDivision)
        {
            this.idDivision = idDivision;
            this.idCorps = idCorps;
            this.libelleDivision = libelleDivision;
        }

        public void Create()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"INSERT INTO [IUT-ACY\\guillton].DIVISION VALUES ({IdDivision},{IdCorps},'{access.EscapeQuotes(LibelleDivision)}');"
            );
        }

        public void Read()
        {
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].DIVISION WHERE ID_DIVISION={IdDivision};"
                    );

                    if (reader.HasRows)
                    {
                        // We Expect only a row to be found
                        reader.Read();

                        idDivision = reader.GetInt32(0);
                        idCorps = reader.GetInt32(1);
                        libelleDivision = reader.GetString(2);
                    }
                    else
                    {
                        MessageBox.Show("Row not found", "Row not found", MessageBoxButton.OK, 
                            MessageBoxImage.Error);
                    }

                    reader.Close();
                    access.Disconnect();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void Update()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"UPDATE [IUT-ACY\\guillton].DIVISION SET ID_CORPS={IdCorps},LIBELLE_DIVISION='{access.EscapeQuotes(LibelleDivision)}' WHERE ID_DIVISION={IdDivision};"
            );
        }

        public void Delete()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"DELETE FROM [IUT-ACY\\guillton].DIVISION WHERE ID_DIVISION={IdDivision};"
            );
        }

        public List<Division> FindAll()
        {
            List<Division> divisions = new List<Division>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData("SELECT * FROM [IUT-ACY\\guillton].DIVISION;");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            divisions.Add(new Division(reader.GetInt32(0), reader.GetInt32(1),
                                                reader.GetString(2)));
                    }
                    else
                    {
                        MessageBox.Show("No rows found", "No rows", MessageBoxButton.OK, 
                            MessageBoxImage.Warning);
                    }

                    reader.Close();
                    access.Disconnect();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return divisions;
        }

        public List<Division> FindBySelection(String criteres)
        {
            List<Division> divisions = new List<Division>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].DIVISION WHERE LIBELLE_DIVISION='{access.EscapeQuotes(LibelleDivision)}';"
                    );

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            divisions.Add(new Division(reader.GetInt32(0), reader.GetInt32(1),
                                                reader.GetString(2)));
                    }
                    else
                    {
                        MessageBox.Show("No rows found", "No rows", MessageBoxButton.OK, 
                            MessageBoxImage.Warning);
                    }

                    reader.Close();
                    access.Disconnect();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            return divisions;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Division))
                return false;

            Division division = (Division)obj;
            return division.IdDivision == IdDivision && division.IdCorps == IdCorps &&
                   division.LibelleDivision == LibelleDivision;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return LibelleDivision;
        }
    }
}
