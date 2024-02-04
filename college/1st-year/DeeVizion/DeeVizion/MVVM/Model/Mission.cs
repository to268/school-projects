using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows;

namespace DeeVizion.MVVM.Model
{
    public class Mission : Crud<Mission>
    {
        private int idMission;
        /// <summary>
        /// Initialisation de l'identifiant de la mission
        /// </summary>
        public int IdMission
        {
            get { return idMission; }
        }

        private String libelleMission;
        /// <summary>
        /// Initialisation du libellé de la mission
        /// </summary>
        public String LibelleMission
        {
            get { return libelleMission; }
            set
            {
                libelleMission = value;
                Update();
            }
        }

        /// <summary>
        /// Constructeur de la classe Mission sans parametres
        /// </summary>
        public Mission()
        {
            idMission = 0;
            libelleMission = "";
        }

        /// <summary>
        /// Constructeur de la classe Mission avec parametres
        /// </summary>
        /// <param name="idMission">Identifiant de la mission</param>
        /// <param name="libelleMision">Libellé de la date de la mission</param>
        public Mission(int idMission, string libelleMision)
        {
            this.idMission = idMission;
            this.libelleMission = libelleMision;
        }

        /// <summary>
        /// Insertion des valeurs dans la base de données, dans la table [IUT-ACY\\guillton].MISSION
        /// </summary>
        public void Create()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"INSERT INTO [IUT-ACY\\guillton].MISSION VALUES ({IdMission},'{access.EscapeQuotes(LibelleMission)}');"
            );
        }

        /// <summary>
        /// Recuperation des données de la table [IUT-ACY\\guillton].MISSION 
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
                        $"SELECT * FROM [IUT-ACY\\guillton].MISSION WHERE ID_MISSION={IdMission};"
                    );

                    if (reader.HasRows)
                    {
                        // We Expect only a row to be found
                        reader.Read();

                        idMission = reader.GetInt32(0);
                        libelleMission = reader.GetString(1);
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
        /// Mise à jour des données dans la tables [IUT-ACY\\guillton].MISSION
        /// </summary>
        public void Update()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"UPDATE [IUT-ACY\\guillton].MISSION SET LIBELLE_MISSION='{access.EscapeQuotes(LibelleMission)}' WHERE ID_MISSION={idMission};"
            );
        }
        
        /// <summary>
        /// Supression des données dans la tables [IUT-ACY\\guillton].MISSION selon un identifiant
        /// </summary>
        public void Delete()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"DELETE FROM [IUT-ACY\\guillton].MISSION WHERE ID_MISSION={IdMission};"
            );
        }

        /// <summary>
        /// Recherche de toute les occurences de missions dans [IUT-ACY\\guillton].MISSION
        /// </summary>
        /// <returns>List<Mission></returns>
        public List<Mission> FindAll()
        {
            List<Mission> missions = new List<Mission>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData("SELECT * FROM [IUT-ACY\\guillton].MISSION;");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            missions.Add(new Mission(reader.GetInt32(0), reader.GetString(1)));
                        }
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

            return missions;
        }

        /// <summary>
        /// Recherche des occurences de missions dans [IUT-ACY\\guillton].MISSION correspondante à la mission donné en paramètres
        /// </summary>
        /// <param name="criteres">Dates</param>
        /// <returns>List<Mission></returns>
        public List<Mission> FindBySelection(String criteres)
        {
            List<Mission> missions = new List<Mission>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].MISSION WHERE LIBELLE_MISSION='{access.EscapeQuotes(LibelleMission)}';"
                    );

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            missions.Add(new Mission(reader.GetInt32(0), reader.GetString(1)));
                        }
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

            return missions;
        }

        /// <summary>
        /// Compare deux mission par leur identifiant et leur libellés
        /// </summary>
        /// <param name="obj">Mission à comparé</param>
        /// <returns>Booléen</returns>
        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Mission))
                return false;

            Mission mission = (Mission)obj;
            return mission.IdMission == IdMission && mission.LibelleMission == LibelleMission;
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
            return LibelleMission;
        }
    }
}
