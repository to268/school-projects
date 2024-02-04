using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeeVizion.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DeeVizion.MVVM.Model.Tests
{
    [TestClass()]
    public class DivisionTests
    {
        private Division division;
        private CorpsArmee corps;

        private List<Division> divisions;
        private DataAccess access;
        private SqlDataReader reader;

        private const int id = 19;
        private const string libelleDivision = "blabla";

        [TestInitialize]
        public void Init()
        {
            corps = new CorpsArmee(id, libelleDivision);
            division = new Division(id, corps.IdCorps, libelleDivision);

            divisions = new List<Division>();
            access = new DataAccess();

            // Insert test records
            corps.Create();
            division.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Delete test records
            division.Delete();
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
                        $"SELECT * FROM [IUT-ACY\\guillton].DIVISION WHERE ID_DIVISION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // IdDivision
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // IdCorps
                        Assert.AreEqual(corps.IdCorps, reader.GetInt32(1));

                        // LibelleDivision
                        Assert.AreEqual(libelleDivision, reader.GetString(2));
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
                        $"SELECT * FROM [IUT-ACY\\guillton].DIVISION WHERE ID_DIVISION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // IdDivision
                        Assert.AreEqual(id, reader.GetInt32(0));

                        // IdCorps
                        Assert.AreEqual(corps.IdCorps, reader.GetInt32(1));

                        // LibelleDivision
                        Assert.AreEqual(libelleDivision, reader.GetString(2));
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
            division.LibelleDivision = libelleDivision + libelleDivision;

            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT LIBELLE_DIVISION FROM [IUT-ACY\\guillton].DIVISION WHERE ID_DIVISION={id};"
                    );

                    if (reader.HasRows)
                    {
                        // We expect only a row to be found
                        reader.Read();

                        // LibelleDivision
                        Assert.AreEqual(libelleDivision + libelleDivision, reader.GetString(0));
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
            division.Delete();
            
            try
            {
                if (access.Connect())
                {
                    reader = access.GetData(
                        $"SELECT * FROM [IUT-ACY\\guillton].DIVISION WHERE LIBELLE_DIVISION={id};"
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