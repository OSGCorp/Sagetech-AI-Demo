//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       christiank, timop
//
// Copyright 2004-2019 by OM International
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
using System.Data;
using Ict.Common;
using Ict.Common.Remoting.Shared;

namespace Ict.Common.Remoting.Server
{
    /**
     * The TPollClientTasks Class contains a Method that returns a DataTable that
     * contains ClientTasks for the currently connected Client.
     */
    public class TPollClientTasks
    {
        /// <summary>Holds a reference to the ClientTasksManager</summary>
        private TClientTasksManager FClientTasksManager;

        /// <summary>Holds Date and Time when the last Client call to 'PollClientTasks' was made.</summary>
        private DateTime FLastPollingTime;

        /// <summary>
        /// access polling time
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastPollingTime()
        {
            return FLastPollingTime;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public TPollClientTasks(TClientTasksManager AClientTasksManager)
        {
            FLastPollingTime = DateTime.Now;
            FClientTasksManager = AClientTasksManager;

            TLogging.LogAtLevel(10, "TPollClientTasks created");
        }

        /**
         * Called by the Client to obtain a DataTable that contains ClientTasks.
         *
         * @return DataTable containing the ClientTasks for the connected Client, or
         * null in case there are no ClientTasks for the connected Client.
         *
         */
        public DataTable PollClientTasks()
        {
            int UnusualNumberOfEntries = Convert.ToInt16(TAppSettingsManager.GetValue(
                    "Server.DEBUG.ClientTasks_UnusualNumberOfEntries", "5", false));
            DataTable ReturnValue = null;

            TLogging.LogAtLevel(4, "TPollClientTasks: PollClientTasks called");

            if (FClientTasksManager == null)
            {
                // TODO: ClientTasks should be stored in the database
                TLogging.LogAtLevel(4, "FClientTasksManager is null");
                return null;
            }

            FLastPollingTime = DateTime.Now;

            // Check whether new ClientTasks should be transferred to the Client
            if (FClientTasksManager.ClientTasksNewDataTableEmpty)
            {
                // This argument is set to null instead of transfering an empty DataTable to
                // reduce the number of bytes that are transfered to the Client!
                ReturnValue = null;

                TLogging.LogAtLevel(4, "TPollClientTasks: Client Tasks Table is empty!");
            }
            else
            {
                // Retrieve new ClientTasks DataTable and pass it on the the Client
                ReturnValue = FClientTasksManager.ClientTasksNewDataTable;

                //
                // Debugging
                //
                if (TLogging.DL >= 2)
                {
                    if (ReturnValue.Rows.Count >= UnusualNumberOfEntries)
                    {
                        TLogging.Log(String.Format(
                                "TPollClientTasks: Client Tasks Table has got a rather unusal number of entries (more than {0}): it holds {1} entries!!!   AppDomain: '{2}'",
                                UnusualNumberOfEntries, ReturnValue.Rows.Count, AppDomain.CurrentDomain.FriendlyName));

                        for (int Counter = 0; Counter < ReturnValue.Rows.Count; Counter++)
                        {
                            TLogging.Log(String.Format(
                                    "TPollClientTasks: Data of Entry #{0} for AppDomain '{1}': TaskGroup: '{2}'; TaskCode: '{3}'; TaskParameter1: '{4}', ; TaskParameter2: '{5}'",
                                    Counter, AppDomain.CurrentDomain.FriendlyName,
                                    ReturnValue.Rows[Counter]["TaskGroup"], ReturnValue.Rows[Counter]["TaskCode"],
                                    ReturnValue.Rows[Counter]["TaskParameter1"], ReturnValue.Rows[Counter]["TaskParameter2"]));
                        }
                    }
                }

                TLogging.LogAtLevel(4, "TPollClientTasks: Client Tasks Table has " + (ReturnValue.Rows.Count).ToString() + " entries!");
            }

            return ReturnValue;
        }
    }
}
