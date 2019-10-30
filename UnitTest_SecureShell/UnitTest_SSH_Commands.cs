using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BurnSoft.SecureShell;
namespace UnitTest_SecureShell
{
    [TestClass]
    public class UnitTest_SSH_Commands
    {
        /// <summary>
        /// The error out
        /// </summary>
        private string errOut;
        /// <summary>
        /// Gets or sets the test context.
        /// </summary>
        /// <value>The test context.</value>
        public TestContext TestContext { get; set; }
        /// <summary>
        /// The ip
        /// </summary>
        private string ip;
        /// <summary>
        /// The uid
        /// </summary>
        private string uid;
        /// <summary>
        /// The password
        /// </summary>
        private string pwd;
        /// <summary>
        /// The command
        /// </summary>
        private string cmd;
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            ip = TestContext.Properties["ip"].ToString();
            uid = TestContext.Properties["uid"].ToString();
            pwd = TestContext.Properties["pwd"].ToString();
            cmd = TestContext.Properties["cmd"].ToString();
        }
        /// <summary>
        /// Defines the test method TestMethod_RunCommand.
        /// </summary>
        [TestMethod]
        public void TestMethod_RunCommand()
        {
            string value = SSHCommand.RunCommand(ip, uid, pwd, cmd, out errOut);
            General.HasValue(value, errOut);
        }
        /// <summary>
        /// Defines the test method TestMethod_SSHAlive.
        /// </summary>
        [TestMethod]
        public void TestMethod_SSHAlive()
        {
            bool value = SSHCommand.SSHAlive(ip, uid, pwd, out errOut);
            General.HasValue(value, errOut);
        }
    }
}
