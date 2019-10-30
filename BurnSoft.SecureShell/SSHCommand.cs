using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using System.IO;
using System.Net.NetworkInformation;

namespace BurnSoft.SecureShell
{
    /// <summary>
    /// Connect to a host and execute a ssh command on that host
    /// </summary>
    public class SSHCommand
    {
        #region "Error handline"        
        /// <summary>
        /// Gets the class location.
        /// </summary>
        /// <value>The class location.</value>
        private static string ClassLocation => "BurnSoft.SecureShell.SSHCommand";
        /// <summary>
        /// Errors the message.
        /// </summary>
        /// <param name="sLocation">The s location.</param>
        /// <param name="ex">The ex.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string sLocation, Exception ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        /// <summary>
        /// Errors the message.
        /// </summary>
        /// <param name="sLocation">The s location.</param>
        /// <param name="ex">The ex.</param>
        /// <returns>System.String.</returns>
        private static string ErrorMessage(string sLocation,  OverflowException ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        #endregion  
        /// <summary>
        /// Runs the command.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="cmd">The command.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns>System.String.</returns>
        public static string RunCommand(string host, string uid, string pwd, string cmd, out string errOut)
        {
            string sAns = @"";
            errOut = @"";
            try
            {
                ConnectionInfo connectionInfo = new ConnectionInfo(host, uid, new PasswordAuthenticationMethod(uid, pwd), new PrivateKeyAuthenticationMethod(General.RsaKey));
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
        /// <summary>
        /// SSHs the alive.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="uid">The uid.</param>
        /// <param name="pwd">The password.</param>
        /// <param name="errOut">The error out.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SSHAlive(string host, string uid, string pwd, out string errOut)
        {
            bool bAns = false;
            errOut = @"";
            try
            {
                var connectionInfo = new ConnectionInfo(host,
                    uid,
                    new PasswordAuthenticationMethod(uid, pwd),
                    new PrivateKeyAuthenticationMethod(General.RsaKey));
                using (var client = new SshClient(connectionInfo))
                {
                    var input = new PipeStream();
                    client.Connect();
                    var shell = client.CreateShell(input, Console.OpenStandardOutput(), new MemoryStream());
                    shell.Start();
                    client.Disconnect();
                }
                bAns = true;
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("SSHAlive", e);
            }
            return bAns;
        }
        /// <summary>
        /// Devices the is up.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="errOut">The error out.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="Exception">No Host or IP Listed!</exception>
        public static bool DeviceIsUp(string host, out string errOut, int timeout = 5000)
        {
            bool bAns = false;
            errOut = @"";
            try
            {
                if (host.Equals(null)) throw new Exception("No Host or IP Listed!");
                Ping instance = new Ping();
                PingReply results;

                results = instance.Send(host, timeout);
                switch (results.Status)
                {
                    case IPStatus.Success:
                        bAns = true;
                        break;
                    case IPStatus.TtlExpired:
                        errOut = "TTL Expried in transit";
                        break;
                    case IPStatus.BadDestination:
                        errOut = "Bad Destination";
                        break;
                    default:
                        errOut = "Request timed out";
                        break;
                }
            }
            catch (Exception e)
            {
                errOut = ErrorMessage("DeviceIsUp", e);
            }
            return bAns;
        }
    }
}
