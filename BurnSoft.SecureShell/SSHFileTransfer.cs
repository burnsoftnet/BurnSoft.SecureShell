using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        #region "Error handline"
        private string ClassLocation => "BurnSoft.SecureShell.SSHCommand";
        private string ErrorMessage(string sLocation, Exception ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        private string ErrorMessage(string sLocation, OverflowException ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        #endregion
        #region "Event Handlers"
        public event EventHandler<int> UploadStatus;
        public event EventHandler<int> DownloadStatus;
        public event EventHandler<string> CurrentFile;

        protected virtual void OnUploadStatus(int e)
        {
            UploadStatus?.Invoke(this, e);
        }

        protected virtual void OnDownloadStatus(int e)
        {
            DownloadStatus?.Invoke(this, e);
        }

        protected virtual void OnCurrentFile(string e)
        {
            CurrentFile?.Invoke(this, e);
        }
        #endregion
        #region "Helper functions"
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
        public bool DownloadFile(string host, string uid, string pwd, string remoteFileAndPath, string localFileAndPath, out string errOut)
        {
            bool bAns = false;
            errOut = @"";
            try
            {
                FileInfo toPath = new FileInfo(localFileAndPath);
                ConnectionInfo connectionInfo = new ConnectionInfo(host,uid, new PasswordAuthenticationMethod(uid, pwd), new PrivateKeyAuthenticationMethod(General.RsaLey));

                ScpClient client = new ScpClient(connectionInfo);

                client.Connect();

                client.Downloading += delegate (object sender, ScpDownloadEventArgs e)
                {
                    OnCurrentFile(e.Filename);
                    OnDownloadStatus(CalcPercentage(e.Downloaded, e.Size));
                };

                client.Download(remoteFileAndPath, toPath);
                bAns = true;

            }
            catch (Exception e)
            {
                errOut = ErrorMessage("DownloadFile", e);
            }
            return bAns;
        }

    }
}
