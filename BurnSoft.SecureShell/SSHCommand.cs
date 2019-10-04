using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.IO;

namespace BurnSoft.SecureShell
{
    /// <summary>
    /// Connect to a host and execute a ssh command on that host
    /// </summary>
    public class SSHCommand
    {
        #region "Error handline"
        private static string ClassLocation => "BurnSoft.SecureShell.SSHCommand";
        private static string ErrorMessage(string sLocation, Exception ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        private static string ErrorMessage(string sLocation,  OverflowException ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        #endregion  

        public static string RunCommand(string host, string uid, string pwd, string cmd, out string errOut)
        {
            string sAns = @"";
            errOut = @"";
            try
            {
                ConnectionInfo connectionInfo = new ConnectionInfo(host, uid, new PasswordAuthenticationMethod(uid, pwd), new PrivateKeyAuthenticationMethod(General.RsaLey));
                MemoryStream outputlisting = new MemoryStream();

                using (SshClient client = new SshClient(connectionInfo))
                {
                    PipeStream input = new PipeStream();
                    client.Connect();
                    Shell shell = client.CreateShell(input, Console.OpenStandardOutput(), new MemoryStream());
                    shell.Start();

                    var command = client.CreateCommand(cmd);
                    var asyncExecute = command.BeginExecute();
                    command.OutputStream.CopyTo(outputlisting);
                    command.EndExecute(asyncExecute);
                    client.Disconnect();
                }

                outputlisting.Position = 0;
                StreamReader sr = new StreamReader(outputlisting);
                sAns = sr.ReadToEnd();
                sr.Dispose();
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("RunCommand", e);
            }
            return sAns;
        }
    }
}
