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
        #region "Error handling"        
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
        /// <example>
        /// string value = SSHCommand.RunCommand("TestMachine", "root", "toor","cd /var/log/; ls -l", out errOut); <br/>
        /// <br/>
        /// <br/>
        /// <b>Results:</b><br/>
        /// -rw-r--r-- 1 root              root        106931 Jul 24 15:04 alternatives.log<br/>
        /// drwxr-x--- 2 root adm           4096 Feb 20  2019 apache2<br/>
        ///drwxr-xr-x 2 root root          4096 Jul 24 15:00 apt<br/>
        ///-rw-r----- 1 root adm          74601 Oct 30 08:01 auth.log<br/>
        ///-rw------- 1 root root         49237 Oct 30 07:32 boot.log<br/>
        ///-rw-r--r-- 1 root root             0 Jan 30  2019 bootstrap.log<br/>
        ///-rw-rw---- 1 root utmp           768 Oct 30 07:45 btmp<br/>
        ///drwxr-xr-x 2 root root          4096 Apr 19  2018 chkrootkit<br/>
        ///-rw-r----- 1 root adm        1301653 Oct 30 08:01 daemon.log<br/>
        ///-rw-r----- 1 root adm         462421 Oct 30 07:48 debug<br/>
        ///-rw-r--r-- 1 root root       1124294 Jul 24 15:05 dpkg.log<br/>
        ///drwxr-xr-x 2 dradis dradis        4096 Feb 20  2019 dradis<br/>
        ///drwxr-s--- 2 Debian-exim adm           4096 Feb 20  2019 exim4<br/>
        ///-rw-r--r-- 1 root root          4352 Jul 24 15:04 faillog<br/>
        ///-rw-r--r-- 1 root root          6155 Jul 24 15:05 fontconfig.log<br/>
        ///drwx--x--x 2 root Debian-gdm    4096 Sep 26  2018 gdm3<br/>
        ///drwx------ 3 inetsim inetsim       4096 Feb 20  2019 inetsim<br/>
        ///drwxr-xr-x 3 root root          4096 Feb 20  2019 installer<br/>
        ///-rw-r----- 1 root adm        1193710 Oct 30 07:43 kern.log<br/>
        ///-rw-rw-r-- 1 root utmp         39712 Oct 30 08:01 lastlog<br/>
        ///-rw-r--r-- 1 root root          1944 Oct 30 07:32 macchanger.log<br/>
        ///-rw-r----- 1 root adm        6531487 Oct 30 07:59 messages<br/>
        /// </example>
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
        /// <example>
        /// bool value = SSHCommand.SSHAlive("TestMachine", "root", "toor", out errOut);
        /// </example>
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
        /// <example>
        /// bool value = SSHCommand.DeviceIsUp("TestMachine", out errOut);
        /// </example>
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
