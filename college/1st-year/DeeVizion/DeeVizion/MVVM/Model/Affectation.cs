using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DeeVizion.MVVM.Model
{
    public class Affectation : Crud<Affectation>
    {
        private List<Division> divisions;
        private List<Mission> missions;
        private List<DateMission> dateMissions;

        private Division division;
        public Division LaDivision
        {
            get { return division; }
            set
            {
                Delete();
                division = value;
                Create();
            }
        }

        private Mission mission;
        public Mission LaMission
        {
            get { return mission; }
            set
            {
                Delete();
                mission = value;
                Create();
            }
        }

        private DateMission dateMission;
        public DateMission LaDateMission
        {
            get { return dateMission; }
            set
            {
                Delete();
                dateMission = value;
                Create();
            }
        }

        private String commentaire;

        public String Commentaire
        {
            get { return commentaire; }
            set
            {
                commentaire = value;
                Update();
            }
        }

        public Affectation(List<Division> divisions, List<Mission> missions, List<DateMission> dateMissions)
        {
            this.divisions = divisions;
            this.missions = missions;
            this.dateMissions = dateMissions;
        }

        public Affectation(Division division, Mission mission, DateMission dateMission, String commentaire)
        {
            this.division = division;
            this.mission = mission;
            this.dateMission = dateMission;
            this.commentaire = commentaire;
        }

        public void Create()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"INSERT INTO [IUT-ACY\\guillton].ETRE_AFFECTE_A VALUES ({LaDivision.IdDivision},{LaMission.IdMission},{LaDateMission.IdDateMission},'{access.EscapeQuotes(Commentaire)}');"
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
                        $"SELECT * FROM [IUT-ACY\\guillton].ETRE_AFFECTE_A WHERE ID_DIVISION={LaDivision.IdDivision} AND ID_MISSION={LaMission.IdMission} AND ID_DATE={LaDateMission.IdDateMission};"
                    );

                    if (reader.HasRows)
                    {
                        // We Expect only a row to be found
                        reader.Read();
                        
                        Division currentDivision = divisions[reader.GetInt32(0) - 1];
                        Mission currentMission = missions[reader.GetInt32(1) - 1];
                        DateMission currentDateMission = dateMissions[reader.GetInt32(2) - 1];
                        String currentCommentaire = reader.GetString(3);
                        
                        division = currentDivision;
                        mission = currentMission;
                        dateMission = currentDateMission;
                        commentaire = currentCommentaire;
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
                $"UPDATE [IUT-ACY\\guillton].ETRE_AFFECTE_A SET COMMENTAIRE='{access.EscapeQuotes(Commentaire)}' WHERE ID_DIVISION={LaDivision.IdDivision} AND ID_MISSION={LaMission.IdMission} AND ID_DATE={LaDateMission.IdDateMission};"
            );
        }

        public void Delete()
        {
            DataAccess access = new DataAccess();
            access.SetData(
                $"DELETE FROM [IUT-ACY\\guillton].ETRE_AFFECTE_A WHERE ID_DIVISION={LaDivision.IdDivision} AND ID_MISSION={LaMission.IdMission} AND ID_DATE={LaDateMission.IdDateMission};"
            );
        }

        public List<Affectation> FindAll()
        {
            if (divisions == null || missions == null || dateMissions == null)
                throw new ArgumentNullException(
                    "Wrong constructor used this method only works if the object has been constructed with the lists");
                
            List<Affectation> affectations = new List<Affectation>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData("SELECT * FROM [IUT-ACY\\guillton].ETRE_AFFECTE_A;");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Division currentDivision = divisions[reader.GetInt32(0) - 1];
                            Mission currentMission = missions[reader.GetInt32(1) - 1];
                            DateMission currentDateMission = dateMissions[reader.GetInt32(2) - 1];
                            String currentCommentaire = reader.GetString(3);
                            affectations.Add(new Affectation(currentDivision, currentMission, currentDateMission, currentCommentaire));
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

            return affectations;
        }

        public List<Affectation> FindBySelection(String criteres)
        {
            List<Affectation> affectations = new List<Affectation>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].ETRE_AFFECTE_A WHERE COMMENTAIRE='{access.EscapeQuotes(criteres)}';"
                    );

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Division currentDivision = divisions[reader.GetInt32(0) - 1];
                            Mission currentMission = missions[reader.GetInt32(1) - 1];
                            DateMission currentDateMission = dateMissions[reader.GetInt32(2) - 1];
                            String currentCommentaire = reader.GetString(3);
                            affectations.Add(new Affectation(currentDivision, currentMission, currentDateMission, currentCommentaire));
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

            return affectations;
        }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Affectation))
                return false;

            Affectation affectation = (Affectation)obj;
            return affectation.LaDivision == LaDivision && affectation.LaMission == LaMission &&
                   affectation.LaDateMission == LaDateMission && affectation.Commentaire == Commentaire;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
