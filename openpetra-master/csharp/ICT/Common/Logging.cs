//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       christiank, timop
//
// Copyright 2004-2020 by OM International
//
// This file is part of OpenPetra.org.
//
// OpenPetra.org is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra.org is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Text;

using Ict.Common.Exceptions;
using System.Data;

namespace Ict.Common
{
    /// <summary>
    /// Logging Type describes the destination of the logging messages
    /// </summary>
    public enum TLoggingType
    {
        /// <summary>
        /// to the console so that you can follow while program is running
        /// </summary>
        ToConsole = 1,

        /// <summary>
        /// to log file for later analysis
        /// </summary>
        ToLogfile = 2,

        /// <summary>
        /// Statusbar is the alternative for Forms where the user does not have a console
        /// </summary>
        ToStatusBar = 4
    };

    /// <summary>
    /// The TLogging class provides general logging functionality.
    /// Logging output can currently go to the Console, to a file or to both at the same time.
    /// </summary>
    public class TLogging
    {
        /// <summary>
        /// the debuglevel that is required for stacktrace to be printed;
        /// this is related to the mono bug described in the code
        /// </summary>
        public const int DEBUGLEVEL_TRACE = 10;

        /// <summary>
        /// the debuglevel that is required for saving some detailed log files for the reporting
        /// </summary>
        public const int DEBUGLEVEL_REPORTING = 5;

        /// <summary>
        /// the debuglevel that is required for saving some detailed log files for the co-ordinated DB Access.
        /// </summary>
        public const int DEBUGLEVEL_COORDINATED_DB_ACCESS = 3;

        /// <summary>
        /// Prefix that can be used for logging messages that are purely informational.
        /// </summary>
        /// <remarks>
        /// You can prefix any string that you want to be written to the log with this. (The prefix is not translatable.)
        /// </remarks>
        public const string LOG_PREFIX_INFO = "INFO: ";

        /// <summary>
        /// Prefix that can be used for logging messages that are warnings.
        /// </summary>
        /// <remarks>
        /// You can prefix any string that you want to be written to the log with this. (The prefix is not translatable.)
        /// </remarks>
        public const string LOG_PREFIX_WARNING = "WARNING: ";

        /// <summary>
        /// Prefix that can be used for logging messages that are errors.
        /// </summary>
        /// <remarks>
        /// You can prefix any string that you want to be written to the log with this. (The prefix is not translatable.)
        /// </remarks>
        public const string LOG_PREFIX_ERROR = "ERROR: ";

        /// <summary>
        /// some log messages will be only displayed at a certain DebugLevel
        /// </summary>
        [ThreadStatic]
        public static int DebugLevel = 0;

        /// <summary>
        /// No logging of messages to Console.Error? Set this to true if the PetraServer runs as a Windows Service
        /// to prevent Bug #5846!
        /// </summary>
        [ThreadStatic]
        private static bool FNoLoggingToConsoleError = false;

        /// <summary>DL is a abbreviated synonym for DebugLevel (more convenient)</summary>
        public static int DL
        {
            get
            {
                return DebugLevel;
            }
        }

        /// <summary>
        /// No logging of messages to Console.Error? Set this to true if the PetraServer runs as a Windows Service
        /// to prevent Bug #5846!
        /// </summary>
        public static bool NoLoggingToConsoleError
        {
            get
            {
                return FNoLoggingToConsoleError;
            }

            set
            {
                FNoLoggingToConsoleError = value;
            }
        }

        /// <summary>
        /// this is the default prefix for the username
        /// </summary>
        public const string DEFAULTUSERNAMEPREFIX = "MiB";

        /// <summary>
        /// this is used for statusbar updates
        /// </summary>
        public delegate void TStatusCallbackProcedure(string msg);

        /// <summary>
        /// his is used for Windows Forms logging
        /// </summary>
        public delegate void TLogNewMessageCallback();

        [ThreadStatic]
        private static TLogWriter ULogWriter = null;
        [ThreadStatic]
        private static String ULogFileName;
        [ThreadStatic]
        private static String UUserNamePrefix = DEFAULTUSERNAMEPREFIX;
        [ThreadStatic]
        private static String ULogTextAsString = null;
        [ThreadStatic]
        private static Int32 ULogPageNumber = 1;
        [ThreadStatic]
        private static TLogNewMessageCallback UNewMessageCallback = null;

        /// <summary>
        /// This can provide information about the context of the program situation when a log message is displayed.
        /// Use SetContext for setting and resetting the context information.
        ///
        /// </summary>
        [ThreadStatic]
        private static String Context;

        /// <summary>
        /// This is a procedure that is called with the text as a parameter. It can be used to update a status bar.
        /// </summary>
        [ThreadStatic]
        private static TStatusCallbackProcedure StatusBarProcedure;

        /// <summary>
        /// This is variable indicates if StatusBarProcedure was set to a valid value. Although it could be out of date...
        /// </summary>
        [ThreadStatic]
        private static bool StatusBarProcedureValid;

        /// <summary>
        /// property for the prefix that describes the
        /// </summary>
        public static string UserNamePrefix
        {
            get
            {
                return UUserNamePrefix;
            }

            set
            {
                UUserNamePrefix = value;
                ULogWriter.LogtextPrefix = UUserNamePrefix;
            }
        }

        #region TLogging

        /// reset the static variables for each Web Request call.
        public static void ResetStaticVariables()
        {
            DebugLevel = 0;
            FNoLoggingToConsoleError = false;
            ULogWriter = null;
            ULogFileName = String.Empty;
            UUserNamePrefix = DEFAULTUSERNAMEPREFIX;
            ULogTextAsString = null;
            ULogPageNumber = 1;
            UNewMessageCallback = null;
            Context = String.Empty;
            StatusBarProcedure = null;
            StatusBarProcedureValid = false;
        }


        /// <summary>
        /// Creates a Console-only logger.
        /// </summary>
        public TLogging()
        {
            TLogging.Context = "";
            SetStatusBarProcedure(null);
        }

        /// <summary>
        /// Creates a logger that can log both to Console or file.
        /// </summary>
        /// <param name="AFileName">File to which the output should be written if logging to the logfile is requested.</param>
        /// <param name="ASuppressDateAndTime">Set to true to suppress the logging of date and time in log files (default= false).</param>
        public TLogging(String AFileName, bool ASuppressDateAndTime = false)
        {
            if (Path.GetFullPath(AFileName) == TLogWriter.GetLogFileName())
            {
                return;
            }

            TLogging.Context = "";

            if ((ULogWriter == null) || (TLogWriter.GetLogFileName() == String.Empty))
            {
                ULogWriter = new TLogWriter(AFileName);
                ULogWriter.SuppressDateAndTime = ASuppressDateAndTime;
                ULogFileName = AFileName;
            }
            else
            {
                throw new Exception("TLogging.Create: only use one log file at the time! old name: " +
                    TLogWriter.GetLogFileName() + "; new name: " +
                    Path.GetFullPath(AFileName));
            }

            SetStatusBarProcedure(null);
        }

        /// <summary>
        /// Call this method on Windows Forms apps to send the 'console' output to a dynamic string
        /// </summary>
        public static void SendConsoleOutputToString(TLogNewMessageCallback ANewMessageCallback)
        {
            ULogTextAsString = "Logging started..." + Environment.NewLine;
            UNewMessageCallback = ANewMessageCallback;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ALogFileMsg"></param>
        /// <returns></returns>
        public bool CanWriteLogFile(out String ALogFileMsg)
        {
            return ULogWriter.CanWriteLogFile(out ALogFileMsg);
        }

        /// <summary>
        /// returns the name of the current log file
        /// </summary>
        /// <returns>the path of the current log file</returns>
        public static String GetLogFileName()
        {
            return System.IO.Path.GetFullPath(ULogFileName);
        }

        /// <summary>
        /// Set the context of the program situation. It is displayed in the next log messages.
        /// </summary>
        /// <param name="context">This will be displayed in the following calls to Log; can be reset with an empty string
        /// </param>
        /// <returns>void</returns>
        public static void SetContext(String context)
        {
            TLogging.Context = context;
        }

        /// <summary>
        /// This sets the procedure that is called with the text as an parameter. It can be used to update a status bar.
        ///
        /// </summary>
        /// <returns>void</returns>
        public static void SetStatusBarProcedure(TStatusCallbackProcedure callbackfn)
        {
            //TLogging.StatusBarProcedure = callbackfn;
            //StatusBarProcedureValid = (callbackfn != null);
            StatusBarProcedureValid = false;
        }

        /// <summary>
        /// Gets the current console log text.  Throws an exception if the console text is being written to a console.
        ///   You should call <see cref="SendConsoleOutputToString"/> to send output to this string
        /// </summary>
        /// <returns></returns>
        public static String GetConsoleLog()
        {
            if (ULogTextAsString == null)
            {
                throw new InvalidOperationException("The console log has not been set to text output ");
            }
            else
            {
                return ULogTextAsString;
            }
        }

        /// <summary>
        /// Logs a message. Output goes to both Screen and Logfile and - if a callback has been set up with
        /// <see cref="SetStatusBarProcedure"/> or gets passed in with
        /// <paramref name="ACustomStatusCallbackProcedure"/> - to a Status Bar of a Form, too.
        /// </summary>
        /// <param name="Text">Log message</param>
        /// <param name="ACustomStatusCallbackProcedure">Optional instance of a custom callback procedure for writing
        /// to the StatusBar of a Form (default = null).</param>
        public static void Log(string Text, TStatusCallbackProcedure ACustomStatusCallbackProcedure = null)
        {
            TLoggingType loggingType = TLoggingType.ToConsole;

            if (ULogWriter != null)
            {
                loggingType |= TLoggingType.ToLogfile;
            }

            if (StatusBarProcedureValid || (ACustomStatusCallbackProcedure != null))
            {
                loggingType |= TLoggingType.ToStatusBar;
            }

            Log(Text, loggingType);
        }

        /// <summary>
        /// Log if level is this high
        /// </summary>
        /// <param name="Level"></param>
        /// <param name="Text"></param>
        public static void LogAtLevel(Int32 Level, string Text)
        {
            if (TLogging.DebugLevel >= Level)
            {
                TLogging.Log(Text);
            }
        }

        /// <summary>
        /// Log if level is this high. Output destination can be selected with the Loggingtype flag.
        /// </summary>
        /// <param name="ALevel"></param>
        /// <param name="AText"></param>
        /// <param name="ALoggingType"></param>
        public static void LogAtLevel(Int32 ALevel, string AText, TLoggingType ALoggingType)
        {
            if (TLogging.DebugLevel >= ALevel)
            {
                TLogging.Log(AText, ALoggingType);
            }
        }

        /// <summary>
        /// Logs a message. Output destination can be selected with the Loggingtype flag.
        /// </summary>
        /// <param name="Text">Log message</param>
        /// <param name="ALoggingType">Determines the output destination.
        /// Note: More than one output destination can be chosen!</param>
        /// <param name="ACustomStatusCallbackProcedure">Optional instance of a custom callback procedure for writing
        /// to the StatusBar of a Form (default = null).</param>
        public static void Log(string Text, TLoggingType ALoggingType,
            TStatusCallbackProcedure ACustomStatusCallbackProcedure = null)
        {
            if (((ALoggingType & TLoggingType.ToConsole) != 0) && (ULogTextAsString != null) && (ULogTextAsString.Length > 0))
            {
                // log to static string for Windows Forms app
                if (ULogTextAsString.Length > 16384)
                {
                    TruncateLogString(8192);
                }

                ULogTextAsString += (Utilities.CurrentTime() + "  " + Text + Environment.NewLine);

                if ((TLogging.Context != null) && (TLogging.Context.Length != 0))
                {
                    ULogTextAsString += ("  Context: " + TLogging.Context + Environment.NewLine);
                }

                // Tell our caller that there is a new message
                if (UNewMessageCallback != null)
                {
                    // Can this fail if the program has closed??
                    UNewMessageCallback();
                }
            }
            else if (!FNoLoggingToConsoleError)
            {
                try
                {
                    if (((ALoggingType & TLoggingType.ToConsole) != 0)
                         // only in Debugmode write the messages for the statusbar also on the console (e.g. reporting progress)
                         || (((ALoggingType & TLoggingType.ToStatusBar) != 0) && (TLogging.DebugLevel != 0)))
                    {
                        Console.Error.WriteLine(Utilities.CurrentTime() + "  " + Text);

                        if (!string.IsNullOrEmpty(TLogging.Context))
                        {
                            Console.Error.WriteLine("  Context: " + TLogging.Context);
                        }
                    }
                }
                catch (System.NotSupportedException)
                {
                    // ignore this exception: System.NotSupportedException: Stream does not support writing
                }
            }

            if (((ALoggingType & TLoggingType.ToStatusBar) != 0)
                && (Text.IndexOf("SELECT") == -1))    // Don't print sql statements to the statusbar
            {
                if (!string.IsNullOrEmpty(TLogging.Context))
                {
                    Text += "; Context: " + TLogging.Context;
                }

                if (ACustomStatusCallbackProcedure == null)
                {
                    if (TLogging.StatusBarProcedureValid)
                    {
                        StatusBarProcedure(Text);
                    }
                }
                else
                {
                    ACustomStatusCallbackProcedure(Text);
                }
            }

            if ((ALoggingType & TLoggingType.ToLogfile) != 0)
            {
                if (ULogWriter != null)
                {
                    TLogWriter.Log(Text);

                    if (TLogging.Context.Length != 0)
                    {
                        TLogWriter.Log("  Context: " + TLogging.Context);
                    }
                }
                else
                {
                    // I found it was better to write the actual logging message,
                    // even if the logwriter is not setup up correctly
                    new TLogging("temp.log");
                    TLogWriter.Log(Text);

                    if (TLogging.Context.Length != 0)
                    {
                        TLogWriter.Log("  Context: " + TLogging.Context);
                    }

                    ULogWriter = null;
                    ULogFileName = null;

                    // now throw an exception, because it is not supposed to work like this
                    throw new ENoLoggingToFile_WrongConstructorUsedException();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public static String StackTraceToText(StackTrace st)
        {
            StackFrame sf;
            Int32 Counter;
            String msg = "";

            for (Counter = 0; Counter <= st.FrameCount - 1; Counter += 1)
            {
                sf = st.GetFrame(Counter);

                if ((sf.GetMethod().Name == "WndProc") && (sf.GetFileLineNumber() == 0))
                {
                    break;
                }

                msg = msg + "    at " + sf.GetMethod().ToString();

                if (sf.GetFileLineNumber() != 0)
                {
                    msg = msg + " (" + sf.GetFileName() + ": " + sf.GetFileLineNumber().ToString() + ')';
                }

                msg = msg + Environment.NewLine;
            }

            return msg;
        }

        /// <summary>
        /// log the current stack trace
        /// </summary>
        /// <param name="ALoggingtype">destination of logging</param>
        public static void LogStackTrace(TLoggingType ALoggingtype = TLoggingType.ToConsole | TLoggingType.ToLogfile)
        {
            String msg = StackTraceToText(new StackTrace(true));

            msg = msg + "in Appdomain " + AppDomain.CurrentDomain.FriendlyName + Environment.NewLine;
            TLogging.Log(msg, ALoggingtype);
        }

        /// <summary>
        /// Logs a number of messages in one go. Output goes to both Screen and Logfile.
        /// </summary>
        /// <param name="AEx">The exception to log</param>
        /// <param name="AMethodSignature">The exception to log</param>
        /// <returns>void</returns>
        public static void LogException(Exception AEx, String AMethodSignature = "")
        {
            ArrayList AList = new ArrayList();

            AList.Add(String.Format("Method:{0} - Unexpected error!{1}{1}{2}",
                    AMethodSignature,
                    Environment.NewLine,
                    AEx.ToString()));

            Log(AList, true);
        }

        /// <summary>
        /// Logs a number of messages in one go. Output goes to both Screen and Logfile.
        /// </summary>
        /// <param name="aList">An ArrayList containing a number of Log messages</param>
        /// <param name="isException">If set to TRUE, an information which states that all
        /// following Log messages are Exceptions is written before
        /// the Log messages are logged.</param>
        /// <returns>void</returns>
        public static void Log(ArrayList aList, bool isException)
        {
            Log(aList, isException, TLoggingType.ToConsole | TLoggingType.ToLogfile);
        }

        /// <summary>
        /// Logs a number of messages in one go. Output destination can be selected
        /// with the Loggingtype flag.
        /// </summary>
        /// <param name="aList">An ArrayList containing a number of Log messages</param>
        /// <param name="isException">If set to TRUE, an information which states that all
        /// following Log messages are Exceptions is written before
        /// the Log messages are logged.</param>
        /// <param name="Loggingtype">logging destination (eg. console, logfile etc)</param>
        /// <returns>void</returns>
        public static void Log(ArrayList aList, bool isException, TLoggingType Loggingtype)
        {
            string additionalInfo;

            additionalInfo = "";

            if (isException)
            {
                additionalInfo = "The application has encountered an Exception: The details are as follows:" + Environment.NewLine;
            }

            Log(Environment.NewLine +
                DateTime.Now.ToLongDateString() + ", " +
                DateTime.Now.ToLongTimeString() + " : " + additionalInfo,
                Loggingtype);

            for (int Counter = 0; Counter <= (aList.Count - 1); Counter++)
            {
                Log(aList[Counter].ToString(), Loggingtype);
            }
        }

        private static void TruncateLogString(Int32 ANewSize)
        {
            int newStartPos = ULogTextAsString.Length - ANewSize;

            if (newStartPos > 1024)
            {
                ULogPageNumber++;

                string newString = ULogTextAsString.Substring(newStartPos);
                int pos = newString.IndexOf(Environment.NewLine);

                if (pos >= 0)
                {
                    newString = newString.Substring(pos + Environment.NewLine.Length);
                }

                ULogTextAsString = String.Format("Log was truncated.  Starting Page {0} ...{1}{2}",
                    ULogPageNumber.ToString(), Environment.NewLine, newString);
            }
        }

        /// <summary>
        /// Writes the contents of a DataTable to the log file.
        /// Useful to have this available for debugging.
        /// </summary>
        /// <param name="DT"></param>
        public static void LogDataTable(DataTable DT)
        {
            Log(String.Format("{0} contains:", DT.TableName), TLoggingType.ToLogfile);
            var items = new string[DT.Columns.Count];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = DT.Columns[i].ToString();
            }

            Log(String.Join(";", items), TLoggingType.ToLogfile);

            foreach (DataRow r in DT.Rows)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = r[i].ToString();
                }

                Log(String.Join(";", items), TLoggingType.ToLogfile);
            }
        }
    }
    #endregion

    /// <summary>
    /// This Exception is thrown if the TLogging class was created using the Create()
    /// constructor (without the FileName parameter) and a logging request is made
    /// that would write to a Logfile.
    /// </summary>
    public class ENoLoggingToFile_WrongConstructorUsedException : EOPAppException
    {
        /// <summary>
        /// Initializes a new instance of this Exception Class.
        /// </summary>
        public ENoLoggingToFile_WrongConstructorUsedException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of this Exception Class with a specified error message.
        /// </summary>
        /// <param name="AMessage">The error message that explains the reason for the <see cref="Exception" />.</param>
        public ENoLoggingToFile_WrongConstructorUsedException(String AMessage) : base(AMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of this Exception Class with a specified error message and a reference to the inner <see cref="Exception" /> that is the cause of this <see cref="Exception" />.
        /// </summary>
        /// <param name="AMessage">The error message that explains the reason for the <see cref="Exception" />.</param>
        /// <param name="AInnerException">The <see cref="Exception" /> that is the cause of the current <see cref="Exception" />, or a null reference if no inner <see cref="Exception" /> is specified.</param>
        public ENoLoggingToFile_WrongConstructorUsedException(string AMessage, Exception AInnerException) : base(AMessage, AInnerException)
        {
        }
    }
}
