using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BritAutomation
{
    public class cReporter
    {
        public static void ReportError(String Message)
        {
            try
            {
                Console.WriteLine(Message);
                Assert.IsTrue(false, Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public static void ReportMessage(String Message)
        {
            try
            {
                Console.WriteLine(Message);
                Assert.IsTrue(true, Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
