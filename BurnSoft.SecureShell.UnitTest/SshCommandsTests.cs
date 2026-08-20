using BurnSoft.SecureShell.UnitTest.Settings;
using NUnit.Framework;

namespace BurnSoft.SecureShell.UnitTest
{
    public class SshCommandsTests
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
        [SetUp]
        public void Setup()
        {

            ip = GeneralSettings.IpAddress;
            uid = GeneralSettings.Uid;
            pwd = GeneralSettings.Pwd;
            cmd = GeneralSettings.Cmd;
        }

        [Test, Category("Connectivity Test")]
        public void DeviceIsUpTest()
        {
            bool value = SSHCommand.DeviceIsUp(ip, out errOut);
            if (value)
            {
                TestContext.WriteLine($"{ip} is up and running");
            } else
            {
                TestContext.WriteLine(errOut);
                Assert.Fail();
            }
        }

        [Test, Category("Connectivity Test")]
        public void SSHAliveTest()
        {
            bool value = SSHCommand.SSHAlive(ip, uid, pwd, out errOut);
            if (value)
            {
                TestContext.WriteLine($"{ip} is up and running and was able to log in.");
            }
            else
            {
                TestContext.WriteLine(errOut);
                Assert.Fail();
            }
        }

        [Test, Category("Connectivity Test")]
        public void RunCommandTest()
        {
            string value = SSHCommand.RunCommand(ip, uid, pwd, cmd, out errOut);
            if (value.Length > 0)
            {
                TestContext.WriteLine($"{ip} is up and running and was able to log in and run command.");
                TestContext.WriteLine(value);
            }
            else
            {
                TestContext.WriteLine(errOut);
                Assert.Fail();
            }
        }
    }
}