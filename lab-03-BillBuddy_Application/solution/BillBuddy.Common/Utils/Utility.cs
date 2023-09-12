using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using GigaSpaces.Core;
using BillBuddy.Common.Utils;

namespace BillBuddy.Common.Utils
{
    public static class Utility
    {
        private static object rootSynch = new object();
        private static readonly Random randomizer = new Random();
        private static readonly string _deployPath;

        public static ITextBoxLogger Logger { get; set; }

        public static string BillBuddyUrl
        {
            get { return "jini://*/*/BillBuddySpace?groups=$(XapNet.Groups)"; }
        }
        public static string DeploymentPath
        {
            get { return _deployPath; }
        }

        public static string BillBuddySpaceName
        {
            get { return "BillBuddySpace"; }
        }

        public static string BinPath
        {
            get { return Path.GetFullPath(Path.Combine(_deployPath, "..\\Bin")); }
        }

        public static double NextDouble
        {
            get { return randomizer.NextDouble(); }
        }

        static Utility()
        {
            _deployPath = "..\\..\\..\\..\\..\\..\\..\\Deploy";
        }

        private static String ExpandMacros(String key)
        {
            PropertyInfo xapNetEnvironmentProperty = typeof(GigaSpacesFactory)
                .GetProperty("XapNetEnvironment", BindingFlags.NonPublic | BindingFlags.Static);

            Object xapNetEnvironment = xapNetEnvironmentProperty.GetValue(null, null);

            MethodInfo expandMacrosMethod = xapNetEnvironment.GetType()
                    .GetMethod("ExpandMacros", BindingFlags.NonPublic | BindingFlags.Instance);

            Object result = expandMacrosMethod.Invoke(xapNetEnvironment, new object[] { key });

            return (String)result;
        }

        public static T FetchRandomEnumValue<T>() where T : struct
        {
            T[] array = Enum.GetValues(typeof(T)).Cast<T>().ToArray();

            return array[(int)((array.Length - 1) * randomizer.NextDouble())];
        }

        public static void LogMessage(string message, params object[] args)
        {
            Log(LogType.Message, null, message, args);
        }

        public static void LogHeader(string message, params object[] args)
        {
            Log(LogType.Header, null, message, args);
        }

        public static void LogError(Exception ex, string message, params object[] args)
        {
            Log(LogType.Error, ex, message, args);
        }

        public static void Log(LogType type, Exception ex, string message, params object[] args)
        {
            if (Logger != null)
            {
                switch (type)
                {
                    case LogType.Header:
                        Logger.LogHeader(message);
                        break;
                    case LogType.Error:
                        Logger.LogError(message, ex);
                        break;
                    default:
                        Logger.LogMessage(message, args);
                        break;
                }
            }
            else
            {
                Console.WriteLine(message, args);
            }
        }
    }
}
