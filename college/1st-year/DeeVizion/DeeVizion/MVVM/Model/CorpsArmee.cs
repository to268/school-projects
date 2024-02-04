using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

namespace DeeVizion.MVVM.Model
{
    public class CorpsArmee : Crud<CorpsArmee>
    {
        private int idCorps;
        public int IdCorps
        {
            get { return idCorps; }
        }

        private String libelleCorps;
        public String LibelleCorps
        {
            get { return libelleCorps; }
            set
            {
                libelleCorps = value;
                Update();
            }
        }

        public CorpsArmee()
        {
            idCorps = 0;
            libelleCorps = "";
        }

        public CorpsArmee(int idCorps, String libelleCorps)
        {
            this.idCorps = idCorps;
            this.libelleCorps = libelleCorps;
        }

        public void Create()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"INSERT INTO [IUT-ACY\\guillton].CORPS_D_ARMEE VALUES ({IdCorps},'{access.EscapeQuotes(LibelleCorps)}');"
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
                        $"SELECT * FROM [IUT-ACY\\guillton].CORPS_D_ARMEE WHERE ID_CORPS={IdCorps};"
                    );

                    if (reader.HasRows)
                    {
                        // We Expect only a row to be found
                        reader.Read();

                        idCorps = reader.GetInt32(0);
                        libelleCorps = reader.GetString(1);
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
                $"UPDATE [IUT-ACY\\guillton].CORPS_D_ARMEE SET LIBELLE_CORPS='{access.EscapeQuotes(LibelleCorps)}' WHERE ID_CORPS={IdCorps};"
            );
        }

        public void Delete()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"DELETE FROM [IUT-ACY\\guillton].CORPS_D_ARMEE WHERE ID_CORPS={IdCorps};"
            );
        }

        public List<CorpsArmee> FindAll()
        {
            List<CorpsArmee> corpsArmees = new List<CorpsArmee>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData("SELECT * FROM [IUT-ACY\\guillton].CORPS_D_ARMEE;");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            corpsArmees.Add(new CorpsArmee(reader.GetInt32(0), reader.GetString(1)));
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

            return corpsArmees;
        }

        public List<CorpsArmee> FindBySelection(String criteres)
        {
            List<CorpsArmee> corpsArmees = new List<CorpsArmee>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].CORPS_D_ARMEE WHERE LIBELLE_CORPS='{access.EscapeQuotes(criteres)}';"
                    );

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            corpsArmees.Add(new CorpsArmee(reader.GetInt32(0), reader.GetString(1)));
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

            return corpsArmees;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is CorpsArmee))
                return false;

            CorpsArmee corpsArmee = (CorpsArmee)obj;
            return corpsArmee.IdCorps == IdCorps && corpsArmee.LibelleCorps == LibelleCorps;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return LibelleCorps;
        }
    }
}
