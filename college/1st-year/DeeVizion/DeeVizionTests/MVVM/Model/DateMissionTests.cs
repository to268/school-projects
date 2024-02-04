using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeeVizion.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DeeVizion.MVVM.Model.Tests
{
    [TestClass()]
    public class DateMissionTests
    {
        private DateMission date;

        private List<DateMission> dates;
        private DataAccess access;
        private SqlDataReader reader;

        private const int id = 19;
        private DateTime dt = DateTime.Today;

        [TestInitialize]
        public void Init()
        {
            date = new DateMission(id, dt);

            dates = new List<DateMission>();
            access = new DataAccess();

            // Insert test records
            date.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Delete test records
            date.Delete();

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
                        $"SELECT * FROM [IUT-ACY\\guillton].DATE_MISSION WHERE ID_DATE={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // IdDate
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // DateMission
                        Assert.AreEqual(dt, DateTime.Today);
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
                        $"SELECT * FROM [IUT-ACY\\guillton].DATE_MISSION WHERE ID_DATE={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // IdDate
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // DateMission
                        Assert.AreEqual(dt, DateTime.Today);
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
            date.LaDateMission = new DateTime(2050,12,31);

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT DATE_MISSION FROM [IUT-ACY\\guillton].DATE_MISSION WHERE ID_DATE={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();
                        
                        // DateMission
                        Assert.AreEqual(new DateTime(2050, 12, 31), reader.GetDateTime(0));
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
            date.Delete();
            
            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].DATE_MISSION WHERE ID_DATE={id};"
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