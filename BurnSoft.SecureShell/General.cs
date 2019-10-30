using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnSoft.SecureShell
{
    internal class General
    {
        #region "Error handline"
        private static string ClassLocation => "BurnSoft.SecureShell.General";
        private static string ErrorMessage(string sLocation, Exception ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        private static string ErrorMessage(string sLocation, OverflowException ex) => $"{ClassLocation}.{sLocation} - {ex.Message}";
        #endregion
        public static string RsaKey => $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}";
    }
}
