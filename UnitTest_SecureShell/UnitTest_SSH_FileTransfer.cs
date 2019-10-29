using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BurnSoft.SecureShell;
using System.Diagnostics;

namespace UnitTest_SecureShell
{
    [TestClass]
    public class UnitTest_SSH_FileTransfer
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
        /// The upload file path
        /// </summary>
        private string uploadFile_path;
        /// <summary>
        /// The upload file file
        /// </summary>
        private string uploadFile_file;
        /// <summary>
        /// The upload file remote path
        /// </summary>
        private string uploadFile_remote_path;
        /// <summary>
        /// The upload file remote file
        /// </summary>
        private string uploadFile_remote_file;
        /// <summary>
        /// The download file remote path
        /// </summary>
        private string downloadFile_remote_path;
        /// <summary>
        /// The download file remote file
        /// </summary>
        private string downloadFile_remote_file;
        /// <summary>
        /// The getfilefrom host
        /// </summary>
        private string getfilefromHost;
        [TestInitialize]
        public void Init()
        {
            ip = TestContext.Properties["ip"].ToString();
            uid = TestContext.Properties["uid"].ToString();
            pwd = TestContext.Properties["pwd"].ToString();
            cmd = TestContext.Properties["cmd"].ToString();
            uploadFile_path = TestContext.Properties["uploadFile_path"].ToString();
            uploadFile_file = TestContext.Properties["uploadFile_file"].ToString();
            uploadFile_remote_path = TestContext.Properties["uploadFile_remote_path"].ToString();
            uploadFile_remote_file = TestContext.Properties["uploadFile_remote_file"].ToString();
            downloadFile_remote_path = TestContext.Properties["downloadFile_remote_path"].ToString();
            downloadFile_remote_file = TestContext.Properties["downloadFile_remote_file"].ToString();
            getfilefromHost = TestContext.Properties["getfilefromHost"].ToString();
        }

        /// <summary>
        /// Tests the method static upload file to host.
        /// </summary>
        [TestMethod]
        public void TestMethod_UploadFile()
        {
            SSHFileTransfer ssh = new SSHFileTransfer();
            ssh.CurrentFile += (sender, e) =>
            {
                Debug.Print(e);
            };
            ssh.UploadStatus += (sender, e) =>
            {
                Debug.Print(e.ToString());
            };
            bool value = ssh.UploadFile(ip, uid, pwd, uploadFile_path, uploadFile_remote_file, uploadFile_file, out errOut);
            General.HasValue(value, errOut);
        }
        /// <summary>
        /// Tests the method static download file from host.
        /// </summary>
        [TestMethod]
        public void TestMethod_DownloadFile()
        {
            SSHFileTransfer ssh = new SSHFileTransfer();
            ssh.CurrentFile += (sender, e) =>
            {
                Debug.Print(e);
            };
            ssh.UploadStatus += (sender, e) =>
            {
                Debug.Print(e.ToString());
            };
            bool value = ssh.DownloadFile(ip, uid, pwd, $"{downloadFile_remote_path}{downloadFile_remote_file}", $"{uploadFile_path}{downloadFile_remote_file}", out errOut);
            General.HasValue(value, errOut);
        }
        /// <summary>
        /// Tests the method static download directory from host.
        /// </summary>
        [TestMethod]
        public void TestMethod_DownloadDirectory()
        {
            SSHFileTransfer ssh = new SSHFileTransfer();
            ssh.CurrentFile += (sender, e) =>
            {
                Debug.Print(e);
            };
            ssh.UploadStatus += (sender, e) =>
            {
                Debug.Print(e.ToString());
            };
            bool value = ssh.DownloadDirectory(ip, uid, pwd, downloadFile_remote_path, uploadFile_path,
                out errOut);
            General.HasValue(value, errOut);
        }
    }
}
