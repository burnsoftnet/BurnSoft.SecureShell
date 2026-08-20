using System;
using System.IO;

namespace BurnSoft.SecureShell.UnitTest.Settings
{
    internal class GeneralSettings
    {
        public static string IpAddress = "192.168.1.49";
        public static string Uid = "testUser";
        public static string Pwd = "toor";
        public static string Cmd = "cd /var/log/; ls -l";
        public static string UploadFile_path = $"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data")}\\";
        public static string uploadFile_file = "test.json";
        public static string uploadFile_remote_path = "/home/testUser/Downloads/RequiredFiles/";
        public static string uploadFile_remote_file = "test.log";
        public static string downloadFile_remote_path = "/home/testUser/Downloads/logs/log/";
        public static string downloadFile_remote_file = "syslog";
        public static string getfilefromHost = "/home/testUser/Downloads/logs/log/syslog";
    }
}
