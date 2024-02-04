using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeeVizion.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DeeVizion.MVVM.Model.Tests
{
    [TestClass()]
    public class CorpsArmeeTests
    {
        private CorpsArmee corps;

        private List<CorpsArmee> corpsArmees;
        private DataAccess access;
        private SqlDataReader reader;

        private const int id = 19;
        private const string testLibelle = "blabla";

        [TestInitialize]
        public void Init()
        {
            corps = new CorpsArmee(id, testLibelle);

            corpsArmees = new List<CorpsArmee>();
            access = new DataAccess();

            // Insert test records
            corps.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Delete test records
            corps.Delete();

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
                        $"SELECT * FROM [IUT-ACY\\guillton].CORPS_D_ARMEE WHERE ID_CORPS={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // IdCorps
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // LibelleCorps
                        Assert.AreEqual(testLibelle, reader.GetString(1));
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
                        $"SELECT * FROM [IUT-ACY\\guillton].CORPS_D_ARMEE WHERE ID_CORPS={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // IdCorps
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // LibelleCorps
                        Assert.AreEqual(testLibelle, reader.GetString(1));
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
            corps.LibelleCorps = testLibelle + testLibelle;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT LIBELLE_CORPS FROM [IUT-ACY\\guillton].CORPS_D_ARMEE WHERE ID_CORPS={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // LibelleCorps
                        Assert.AreEqual(testLibelle + testLibelle, reader.GetString(0));
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
            corps.Delete();
            
            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].CORPS_D_ARMEE WHERE ID_CORPS={id};"
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