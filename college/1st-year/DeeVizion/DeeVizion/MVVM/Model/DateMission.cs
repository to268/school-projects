using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;

namespace DeeVizion.MVVM.Model
{
    public class DateMission : Crud<DateMission>
    {
        private int idDateMission;
        /// <summary>
        /// Initialisation de l'identifiant de la date
        /// </summary>
        public int IdDateMission
        {
            get { return idDateMission; }
        }

        private DateTime dateMission;
        /// <summary>
        /// Initialisation de la date
        /// </summary>
        public DateTime LaDateMission
        {
            get { return dateMission; }
            set
            {
                dateMission = value;
                Update();
            }
        }

        /// <summary>
        /// Constructeur de la classe DateMission sans aucun parametres
        /// </summary>
        public DateMission()
        {
            idDateMission = 0;
            dateMission = new DateTime();
        }

        /// <summary>
        /// Constructeur de la classe DateMission avec parametres
        /// </summary>
        /// <param name="idDateMission">Identifiant de la date mission</param>
        /// <param name="dateMission">Libellé de la date de la mission</param>
        public DateMission(int idDateMission, DateTime dateMission)
        {
            this.idDateMission = idDateMission;
            this.dateMission = dateMission;
        }

        /// <summary>
        /// Insertion des valeurs dans la base de données, dans la table [IUT-ACY\\guillton].DATE_MISSION
        /// </summary>
        public void Create()
        {
            DataAccess access = new DataAccess();
            String date = access.EscapeQuotes(LaDateMission.ToString("yyyy-MM-dd"));
            access.SetData(
                $"SET DATEFORMAT ymd; INSERT INTO [IUT-ACY\\guillton].DATE_MISSION VALUES ({IdDateMission},'{date}');"
            );
        }

        /// <summary>
        /// Recuperation des données de la table [IUT-ACY\\guillton].DATE_MISSION 
        /// </summary>
        public void Read()
        {
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SET DATEFORMAT ymd; SELECT * FROM [IUT-ACY\\guillton].DATE_MISSION WHERE ID_DATE={IdDateMission};"
                    );

                    if (reader.HasRows)
                    {
                        // We Expect only a row to be found
                        reader.Read();

                        idDateMission = reader.GetInt32(0);
                        dateMission = reader.GetDateTime(1);
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

        /// <summary>
        /// Mise à jour des données dans la tables [IUT-ACY\\guillton].DATE_MISSION
        /// </summary>
        public void Update()
        {
            DataAccess access = new DataAccess();
            String date = access.EscapeQuotes(LaDateMission.ToString("yyyy-MM-dd"));
            access.SetData(
                $"SET DATEFORMAT ymd; UPDATE [IUT-ACY\\guillton].DATE_MISSION SET DATE_MISSION='{date}' WHERE ID_DATE={idDateMission};"
            );
        }

        /// <summary>
        /// Supression des données dans la tables [IUT-ACY\\guillton].DATE_MISSION selon un identifiant
        /// </summary>
        public void Delete()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"DELETE FROM [IUT-ACY\\guillton].DATE_MISSION WHERE ID_DATE={IdDateMission};"
            );
        }

        /// <summary>
        /// Recherche de toute les occurences de dates missions dans [IUT-ACY\\guillton].DATE_MISSION
        /// </summary>
        /// <returns>List<DateMission></returns>
        public List<DateMission> FindAll()
        {
            
            List<DateMission> dates = new List<DateMission>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData("SELECT * FROM [IUT-ACY\\guillton].DATE_MISSION;");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            dates.Add(new DateMission(reader.GetInt32(0), reader.GetDateTime(1)));
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

            return dates;
        }

        /// <summary>
        /// Recherche des occurences de dates de missions dans [IUT-ACY\\guillton].DATE_MISSION correspondante à la date donné en paramètres
        /// </summary>
        /// <param name="criteres">Dates</param>
        /// <returns>List<DateMission></returns>
        public List<DateMission> FindBySelection(String criteres)
        {
            List<DateMission> dates = new List<DateMission>();
            DataAccess access = new DataAccess();
            String date = access.EscapeQuotes(LaDateMission.ToString("yyyy-MM-dd"));
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData($"SET DATEFORMAT ymd; SELECT * FROM [IUT-ACY\\guillton].DATE_MISSION WHERE DATE_MISSION='{date}';");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            dates.Add(new DateMission(reader.GetInt32(0), reader.GetDateTime(1)));
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

            return dates;
        }
        
        /// <summary>
        /// Compare deux dates par leur identifiant et leur libellés
        /// </summary>
        /// <param name="obj">Date à comparé</param>
        /// <returns>Booléen</returns>
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is DateMission))
                return false;

            DateMission dateMissionObj = (DateMission)obj;
            return dateMissionObj.IdDateMission == IdDateMission && dateMissionObj.LaDateMission == LaDateMission;
        }

        /// <summary>
        /// Retourne l'emplacement de la données
        /// </summary>
        /// <returns>Chaine de caracteres correspondante au code de l'emplacemnt de la données</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return LaDateMission.ToString("dd-MM-yyyy");
        }
    }
}
