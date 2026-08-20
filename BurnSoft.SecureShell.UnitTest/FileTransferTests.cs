using BurnSoft.SecureShell.UnitTest.Settings;
using NUnit.Framework;
using System.Diagnostics;

namespace BurnSoft.SecureShell.UnitTest
{
    public class FileTransferTests
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
        [SetUp]
        public void Setup()
        {
            ip = GeneralSettings.IpAddress;
            uid = GeneralSettings.Uid;
            pwd = GeneralSettings.Pwd;
            cmd = GeneralSettings.Cmd;
            uploadFile_path = GeneralSettings.UploadFile_path;
            uploadFile_file = GeneralSettings.uploadFile_file;
            uploadFile_remote_path = GeneralSettings.uploadFile_remote_path;
            uploadFile_remote_file = GeneralSettings.uploadFile_remote_file;
            downloadFile_remote_path = GeneralSettings.downloadFile_remote_path;
            downloadFile_remote_file = GeneralSettings.downloadFile_remote_file;
            getfilefromHost = GeneralSettings.getfilefromHost;
        }

        [Test, Category("File Transfers - Upload")]
        public void UploadFileTest()
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
            bool value = ssh.UploadFile(ip, uid, pwd, uploadFile_remote_path, uploadFile_path, uploadFile_file, out errOut);
            if(value)
            {
                TestContext.WriteLine($"Transfer to {ip} was successful!");
            } else
            {
                TestContext.WriteLine(errOut);
                Assert.Fail();
            }
        }

        [Test, Category("File Transfers - Download")]
        public void DownloadFileTest()
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
            if (value)
            {
                TestContext.WriteLine($"Transfer from {ip} was successful!");
            }
            else
            {
                TestContext.WriteLine(errOut);
                Assert.Fail();
            }
        }

        [Test, Category("File Transfers - Download")]
        public void DownloadDirectoryeTest()
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
            if (value)
            {
                TestContext.WriteLine($"Transfer from {ip} was successful!");
            }
            else
            {
                TestContext.WriteLine(errOut);
                Assert.Fail();
            }
        }
    }
}
