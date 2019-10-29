using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_SecureShell
{
    public class General
    {
        /// <summary>
        /// Determines whether the specified value has value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="errOut">The error out.</param>
        public static void HasValue(string value, string errOut = "")
        {
            Debug.Print(errOut);
            Debug.Print($"Value returned is: {value}");
            bool notNull = (value.Length > 0);
            Assert.AreEqual(notNull, true);
        }
        /// <summary>
        /// Determines whether the specified value has value.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="errOut">The error out.</param>
        public static void HasValue(bool value, string errOut = "")
        {
            Debug.Print(errOut);
            Assert.AreEqual(value, true);
        }
        /// <summary>
        /// Determines whether [has false value] [the specified value].
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <param name="errOut">The error out.</param>
        public static void HasFalseValue(bool value, string errOut = "")
        {
            Debug.Print(errOut);
            Assert.AreEqual(value, false);
        }
    }
}
