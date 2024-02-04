using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeeVizion.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DeeVizion.MVVM.Model.Tests
{
    [TestClass()]
    public class AffectationTests
    {
        private CorpsArmee corpsArmee;
        private Division division;
        private Mission mission;
        private DateMission dateMission;
        private Affectation affectation;

        private List<Affectation> affectations;
        private DataAccess access;
        private SqlDataReader reader;

        private const int id = 19;
        private const string commentaire = "blabla";

        [TestInitialize]
        public void Init()
        {
            corpsArmee = new CorpsArmee(id, "Bodybuilder");
            division = new Division(id, id, "Div1");
            mission = new Mission(id, "Miss1");
            dateMission = new DateMission(id, DateTime.Today);
            affectation = new Affectation(division, mission, dateMission, commentaire);

            affectations = new List<Affectation>();
            access = new DataAccess();

            // Insert test records
            corpsArmee.Create();
            division.Create();
            mission.Create();
            dateMission.Create();
            affectation.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Delete test records
            affectation.Delete();
            dateMission.Delete();
            mission.Delete();
            division.Delete();
            corpsArmee.Delete();

            if (reader != null)
                reader.Close();

            access.Disconnect();
        }

        [TestMethod()]
        public void CreateTest()
        {
            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].ETRE_AFFECTE_A WHERE ID_DIVISION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // Division
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // Mission
                        Assert.AreEqual(id, reader.GetInt32(1));

                        // Date Mission
                        Assert.AreEqual(id, reader.GetInt32(2));

                        // Commentaire
                        Assert.AreEqual(commentaire, reader.GetString(3));
                    }

                    else
                        Assert.Fail("No data");
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Connexion error: " + e.Message);
            }
        }

        [TestMethod()]
        public void ReadTest()
        {
            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].ETRE_AFFECTE_A WHERE ID_DIVISION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // Division
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // Mission
                        Assert.AreEqual(id, reader.GetInt32(1));

                        // Date Mission
                        Assert.AreEqual(id, reader.GetInt32(2));

                        // Commentaire
                        Assert.AreEqual(commentaire, reader.GetString(3));
                    }

                    else
                        Assert.Fail("No data");
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Connexion error: " + e.Message);
            }
        }

        [TestMethod()]
        public void UpdateTest()
        {
            affectation.Commentaire = commentaire + commentaire;
            
            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT COMMENTAIRE FROM [IUT-ACY\\guillton].ETRE_AFFECTE_A WHERE ID_DIVISION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // Commentaire
                        Assert.AreEqual(commentaire + commentaire, reader.GetString(0));
                    }

                    else
                        Assert.Fail("No data");
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Connexion error: " + e.Message);
            }
        }

        [TestMethod()]
        public void DeleteTest()
        {
            affectation.Delete();
            
            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].ETRE_AFFECTE_A WHERE ID_DIVISION={id};"
                    );

                    Assert.IsFalse(reader.HasRows);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Connexion error: " + e.Message);
            }
        }
    }
}