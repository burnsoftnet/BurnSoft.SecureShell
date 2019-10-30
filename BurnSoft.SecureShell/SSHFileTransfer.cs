using System;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Common;
namespace BurnSoft.SecureShell
{
    /// <summary>
    /// Class that handles the Secure FIle Transfer to the selected host
    /// </summary>
    public class SSHFileTransfer
    {
        #region "Error handling"        
        /// <summary>
        /// Gets the class location.
        /// </summary>
        /// <value>The class location.</value>
        private string ClassLocation => "BurnSoft.SecureShell.SSHCommand";
        /// <summary>
        /// Errors the message.
        /// </summary>
        /// <param name="sLocation">The s location.</param>
        /// <param name="ex">The ex.</param>
        /// <returns>System.String.</returns>
        private string ErrorMessage(string sLocation, Exception ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        /// <summary>
        /// Errors the message.
        /// </summary>
        /// <param name="sLocation">The s location.</param>
        /// <param name="ex">The ex.</param>
        /// <returns>System.String.</returns>
        private string ErrorMessage(string sLocation, OverflowException ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        #endregion
        #region "Event Handlers"        
        /// <summary>
        /// Occurs when [upload status].
        /// </summary>
        public event EventHandler<int> UploadStatus;
        /// <summary>
        /// Occurs when [download status].
        /// </summary>
        public event EventHandler<int> DownloadStatus;
        /// <summary>
        /// Occurs when [current file].
        /// </summary>
        public event EventHandler<string> CurrentFile;
        /// <summary>
        /// Called when [upload status].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnUploadStatus(int e)
        {
            UploadStatus?.Invoke(this, e);
        }
        /// <summary>
        /// Called when [download status].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnDownloadStatus(int e)
        {
            DownloadStatus?.Invoke(this, e);
        }
        /// <summary>
        /// Called when [current file].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnCurrentFile(string e)
        {
            CurrentFile?.Invoke(this, e);
        }
        #endregion
        #region "Helper functions"        
        /// <summary>
        /// Calculates the percentage.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="size">The size.</param>
        /// <returns>System.Int32.</returns>
        private int CalcPercentage(long current, long size)
        {
            int iAns = 0;
            try
            {
                iAns = Convert.ToInt32((current * 100) / size);
            }
            catch (Exception e)
            {
                iAns = 100;
            }
            return iAns;
        }
        #endregion
        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="remoteFileAndPath">The remote file and path.</param>
        /// <param name="localFileAndPath">The local file and path.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <example>
        /// SSHFileTransfer ssh = new SSHFileTransfer();<br/>
        ///        ssh.CurrentFile += (sender, e) =><br/>
        ///            {<br/>
        ///                Debug.Print(e);<br/>
        ///            };<br/>
        ///    ssh.UploadStatus += (sender, e) =><br/>
        ///            {<br/>
        ///                Debug.Print(e.ToString());<br/>
        ///            };<br/>
        ///bool value = ssh.DownloadFile("testmachine", "root", "toor", "/var/log/syslog", "c:\test\syslog.log", out errOut);<br/>
        /// </example>
        public bool DownloadFile(string host, string uid, string pwd, string remoteFileAndPath, string localFileAndPath, out string errOut)
        {
            bool bAns = false;
            errOut = @"";
            try
            {
                FileInfo toPath = new FileInfo(localFileAndPath);
                ConnectionInfo connectionInfo = new ConnectionInfo(host,uid, new PasswordAuthenticationMethod(uid, pwd), new PrivateKeyAuthenticationMethod(General.RsaKey));

                ScpClient client = new ScpClient(connectionInfo);

                client.Connect();

                client.Downloading += delegate (object sender, ScpDownloadEventArgs e)
                {
                    OnCurrentFile(e.Filename);
                    OnDownloadStatus(CalcPercentage(e.Downloaded, e.Size));
                };

                client.Download(remoteFileAndPath, toPath);
                bAns = true;
                client.Disconnect();
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("DownloadFile", e);
            }
            return bAns;
        }
        /// <summary>
        /// Downloads the directory.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="remotePath">The remote path.</param>
        /// <param name="localPath">The local path.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <example>
        /// SSHFileTransfer ssh = new SSHFileTransfer();<br/>
        /// ssh.CurrentFile += (sender, e) =><br/>
        ///    {<br/>
        /// Debug.Print(e);<br/>
        /// };<br/>
        /// ssh.UploadStatus += (sender, e) =><br/>
        /// {<br/>
        /// Debug.Print(e.ToString());<br/>
        /// };<br/>
        /// bool value = ssh.DownloadDirectory("testmachine", "root", "toor", "/root/Downloads/", "C:\Test\",out errOut);
        /// </example>
        public bool DownloadDirectory(string host, string uid, string pwd, string remotePath, string localPath, out string errOut)
        {
            bool bAns = false;
            errOut = @"";
            try
            {
                DirectoryInfo toPath = new DirectoryInfo(localPath);
                ConnectionInfo connectionInfo = new ConnectionInfo(host, uid, new PasswordAuthenticationMethod(uid, pwd), new PrivateKeyAuthenticationMethod(General.RsaKey));

                ScpClient client = new ScpClient(connectionInfo);

                client.Connect();

                client.Downloading += delegate (object sender, ScpDownloadEventArgs e)
                {
                    OnCurrentFile(e.Filename);
                    OnDownloadStatus(CalcPercentage(e.Downloaded, e.Size));
                };

                client.Download(remotePath, toPath);
                bAns = true;
                client.Disconnect();
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("DownloadDirectory", e);
            }
            return bAns;
        }
        /// <summary>
        /// Uploads the file from the local machine to the remote machine..
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="remotePath">The remote path.</param>
        /// <param name="localPath">The local path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <example>
        /// SSHFileTransfer ssh = new SSHFileTransfer(); <br/>
        /// ssh.CurrentFile += (sender, e) => <br/>
        ///     { <br/>
        /// Debug.Print(e); <br/>
        /// }; <br/>
        /// ssh.UploadStatus += (sender, e) => <br/>
        ///{ <br/>
        ///    Debug.Print(e.ToString()); <br/>
        ///}; <br/>
        ///bool value = ssh.UploadFile("testmachine", "root", "toor", "/root/Downloads/", "C:\Test\", "Test.json", out errOut);
        /// </example>
        public bool UploadFile(string host, string uid, string pwd, string remotePath, string localPath, string fileName, out string errOut)
        {
            bool bAns = false;
            errOut = @"";
            try
            {
                string openFile = $"{localPath}{fileName}";
                FileInfo fi = new FileInfo(openFile);
                ConnectionInfo connectionInfo = new ConnectionInfo(host, uid, new PasswordAuthenticationMethod(uid, pwd), new PrivateKeyAuthenticationMethod(General.RsaKey));
                MemoryStream outputlisting = new MemoryStream();

                if (fi != null)
                {
                    using (var client = new ScpClient(connectionInfo))
                    {
                        client.Connect();
                        client.ErrorOccurred += (sender, e) => throw new Exception(e.Exception.Message);
                        client.Uploading += delegate (object sender, ScpUploadEventArgs e)
                        {
                            OnCurrentFile(e.Filename);
                            OnUploadStatus(CalcPercentage(e.Uploaded, e.Size));
                        };
                        string uploadTo = $"{remotePath}";
                        client.Upload(fi, uploadTo);
                        client.Disconnect();
                        bAns = true;
                    }
                }
                

            }
            catch (Exception ex)
            {
                errOut = ErrorMessage("UploadFile", ex);
            }
            return bAns;
        }

    }
}
