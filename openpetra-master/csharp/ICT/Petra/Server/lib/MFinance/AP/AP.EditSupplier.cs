//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
//       Tim Ingham
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
using Ict.Petra.Shared;
using Ict.Common;
using Ict.Common.DB;
using Ict.Common.Verification;
using Ict.Common.Remoting.Shared;
using Ict.Common.Remoting.Server;
using Ict.Petra.Shared.MFinance;
using Ict.Petra.Shared.MFinance.AP.Data;
using Ict.Petra.Server.MFinance.AP.Data.Access;

namespace Ict.Petra.Server.MFinance.AP.UIConnectors
{
    ///<summary>
    /// This UIConnector provides data for the finance Accounts Payable screens
    ///
    /// UIConnector Objects are instantiated by the Client's User Interface via the
    /// Instantiator classes.
    /// They handle requests for data retrieval and saving of data (including data
    /// verification).
    ///
    /// Their role is to
    ///   - retrieve (and possibly aggregate) data using Business Objects,
    ///   - put this data into///one* DataSet that is passed to the Client and make
    ///     sure that no unnessary data is transferred to the Client,
    ///   - optionally provide functionality to retrieve additional, different data
    ///     if requested by the Client (for Client screens that load data initially
    ///     as well as later, eg. when a certain tab on the screen is clicked),
    ///   - save data using Business Objects.
    ///
    /// @Comment These Objects would usually not be instantiated by other Server
    ///          Objects, but only by the Client UI via the Instantiator classes.
    ///          However, Server Objects that derive from these objects and that
    ///          are also UIConnectors are feasible.
    ///</summary>
    public class TSupplierEditUIConnector
    {
        private TDataBase FDataBase = null;

        /// <summary>
        /// constructor
        /// </summary>
        public TSupplierEditUIConnector(TDataBase ADataBase = null) : base()
        {
            FDataBase = DBAccess.Connect("TSupplierEditUIConnector", ADataBase);
        }

        /// <summary>
        /// check if there is already a supplier record for the given partner
        /// </summary>
        /// <param name="APartnerKey"></param>
        /// <returns></returns>
        public bool CanFindSupplier(Int64 APartnerKey)
        {
            TDBTransaction ReadTransaction;
            bool NewTransaction = false;
            bool ReturnValue = false;

            ReadTransaction = FDataBase.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                out NewTransaction);

            try
            {
                ReturnValue = AApSupplierAccess.Exists(APartnerKey, ReadTransaction);
            }
            finally
            {
                if (NewTransaction)
                {
                    ReadTransaction.Commit();
                }
            }
            return ReturnValue;
        }

        /// <summary>
        /// Passes data as a Typed DataSet to the Supplier Edit Screen
        /// </summary>
        public AccountsPayableTDS GetData(Int64 APartnerKey)
        {
            TDBTransaction ReadTransaction;

            // create the DataSet that will later be passed to the Client
            AccountsPayableTDS MainDS = new AccountsPayableTDS();

            ReadTransaction = FDataBase.BeginTransaction(IsolationLevel.RepeatableRead, 5);

            try
            {
                try
                {
                    // Supplier
                    AApSupplierAccess.LoadByPrimaryKey(MainDS, APartnerKey, ReadTransaction);

                    if (MainDS.AApSupplier.Rows.Count == 0)
                    {
                        // Supplier does not exist
                        throw new Exception("supplier does not exist");
                    }
                }
                catch (Exception Exp)
                {
                    ReadTransaction.Rollback();
                    ReadTransaction = new TDBTransaction();
                    TLogging.Log("TSupplierEditUIConnector.LoadData exception: " + Exp.ToString(), TLoggingType.ToLogfile);
                    TLogging.Log(Exp.StackTrace, TLoggingType.ToLogfile);
                    throw;
                }
            }
            finally
            {
                if (ReadTransaction != null)
                {
                    ReadTransaction.Commit();
                }
            }

            // Accept row changes here so that the Client gets 'unmodified' rows
            MainDS.AcceptChanges();

            // Remove all Tables that were not filled with data before remoting them.
            MainDS.RemoveEmptyTables();

            return MainDS;
        }

        /// <summary>
        /// store the AP supplier
        ///
        /// All DataTables contained in the Typed DataSet are inspected for added,
        /// changed or deleted rows by submitting them to the DataStore.
        /// </summary>
        /// <param name="AInspectDS">Typed DataSet that needs to contain known DataTables</param>
        /// <returns>TSubmitChangesResult.scrOK in case everything went fine, otherwise throws an Exception.</returns>
        public TSubmitChangesResult SubmitChanges(ref AccountsPayableTDS AInspectDS)
        {
            TDBTransaction SubmitChangesTransaction;

            if (AInspectDS != null)
            {
                // I won't allow any null fields related to discount:

                if (AInspectDS.AApSupplier[0].IsDefaultDiscountDaysNull())
                {
                    AInspectDS.AApSupplier[0].DefaultDiscountDays = 0;
                }

                if (AInspectDS.AApSupplier[0].IsDefaultDiscountPercentageNull())
                {
                    AInspectDS.AApSupplier[0].DefaultDiscountPercentage = 0;
                }

                bool NewTransaction;
                SubmitChangesTransaction = FDataBase.GetNewOrExistingTransaction(IsolationLevel.Serializable, out NewTransaction);

                try
                {
                    AApSupplierAccess.SubmitChanges(AInspectDS.AApSupplier, SubmitChangesTransaction);

                    if (NewTransaction)
                    {
                        SubmitChangesTransaction.Commit();
                    }
                }
                catch (Exception Exc)
                {
                    TLogging.Log("An Exception occured during the storing of the AP Supplier):" + Environment.NewLine + Exc.ToString());

                    if (NewTransaction)
                    {
                        SubmitChangesTransaction.Rollback();
                    }

                    throw;
                }
            }

            return TSubmitChangesResult.scrOK;
        }
    }
}
