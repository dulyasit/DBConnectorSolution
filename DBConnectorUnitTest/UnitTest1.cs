using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DBConnectorUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /*
<add key="user" value="partyuser"/>
      <add key="pass" value="partyuser"/>
	  <add key="database" value="dbparty"/>
	  <add key="server" value="localhost"/>
        */
        [TestMethod]
        public void TestMethod1()
        {
            List<string>[] result;
            DBConnector.DBConnector db = new DBConnector.DBConnector();
            result = db.Select();
            Assert.IsTrue(result.Length > 0);
        }
        [TestMethod]
        public void TestMethod2()
        {            
            DBConnector.DBConnector db = new DBConnector.DBConnector();
            bool result = db.OpenConnection();
            Assert.IsTrue(result);
        }
    }
}
