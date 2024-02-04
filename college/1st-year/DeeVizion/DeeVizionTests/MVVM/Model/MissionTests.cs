using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeeVizion.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DeeVizion.MVVM.Model.Tests
{
    [TestClass()]
    public class MissionTests
    {
        private Mission mission;

        private List<Mission> missions;
        private DataAccess access;
        private SqlDataReader reader;

        private const int id = 19;
        private const string libelleMission = "blabla";

        [TestInitialize]
        public void Init()
        {
            mission = new Mission(id, libelleMission);

            missions = new List<Mission>();
            access = new DataAccess();

            // Insert test records
            mission.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Delete test records
            mission.Delete();

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
                        $"SELECT * FROM [IUT-ACY\\guillton].MISSION WHERE ID_MISSION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // IdMission
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // LibelleMission
                        Assert.AreEqual(libelleMission, reader.GetString(1));
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
                        $"SELECT * FROM [IUT-ACY\\guillton].MISSION WHERE ID_MISSION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // IdMission
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // LibelleMission
                        Assert.AreEqual(libelleMission, reader.GetString(1));
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
            mission.LibelleMission = libelleMission + libelleMission;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT LIBELLE_MISSION FROM [IUT-ACY\\guillton].MISSION WHERE ID_MISSION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // LibelleMission
                        Assert.AreEqual(libelleMission + libelleMission, reader.GetString(0));
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
            mission.Delete();
            
            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].MISSION WHERE ID_MISSION={id};"
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