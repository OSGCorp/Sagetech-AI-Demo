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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

using Ict.Common;
using Ict.Common.DB;
using Ict.Common.Data;
using Ict.Common.Exceptions;

using Ict.Petra.Server.App.Core.Security;
using Ict.Petra.Server.MCommon;
using Ict.Petra.Server.MFinance.Account.Data.Access;
using Ict.Petra.Server.MPartner.Common;
using Ict.Petra.Server.MPartner.Mailroom.Data.Access;
using Ict.Petra.Server.MPartner.Partner.Data.Access;

using Ict.Petra.Shared;
using Ict.Petra.Shared.MFinance.Account.Data;
using Ict.Petra.Shared.MPartner;
using Ict.Petra.Shared.MPartner.Mailroom.Data;
using Ict.Petra.Shared.MPartner.Partner.Data;
using Ict.Petra.Shared.Security;

namespace Ict.Petra.Server.MPartner.Partner.ServerLookups.WebConnectors
{
    /// <summary>
    /// Performs server-side lookups for the Client in the MPartner.ServerLookups
    /// sub-namespace.
    /// </summary>
    public class TPartnerServerLookups
    {
        /// <summary>
        /// Gets the ShortName of a Partner.
        ///
        /// </summary>
        /// <param name="APartnerKey">PartnerKey of Partner to find the short name for</param>
        /// <param name="APartnerShortName">ShortName for the found Partner ('' if Partner
        /// doesn't exist or PartnerKey is 0)</param>
        /// <param name="APartnerClass">Partner Class of the found Partner (FAMILY if Partner
        /// doesn't exist or PartnerKey is 0)</param>
        /// <param name="AMergedPartners">Set to false if the function should return 'false' if
        /// the Partner' Partner Status is MERGED</param>
        /// <returns>true if Partner was found in DB (except if AMergedPartners is false
        /// and Partner is MERGED) or PartnerKey is 0, otherwise false
        /// </returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean GetPartnerShortName(Int64 APartnerKey,
            out String APartnerShortName,
            out TPartnerClass APartnerClass,
            Boolean AMergedPartners = true)
        {
            Boolean ReturnValue = false;
            TStdPartnerStatusCode PartnerStatus = TStdPartnerStatusCode.spscACTIVE;
            string PartnerShortName = String.Empty;
            TPartnerClass PartnerClass = TPartnerClass.FAMILY;

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetPartnerShortName");

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    ReturnValue = MCommonMain.RetrievePartnerShortName(APartnerKey, out PartnerShortName,
                        out PartnerClass, out PartnerStatus, Transaction);
                });

            db.CloseDBConnection();

            if ((!AMergedPartners)
                && (PartnerStatus == TStdPartnerStatusCode.spscMERGED))
            {
                ReturnValue = false;
            }

            APartnerShortName = PartnerShortName;
            APartnerClass = PartnerClass;

            return ReturnValue;
        }

        /// <summary>
        /// Gets the Gift Destination of a Partner.
        /// </summary>
        /// <param name="APartnerKey"></param>
        /// <param name="AGiftDate"></param>
        /// <returns></returns>
        [RequireModulePermission("PTNRUSER")]
        public static Int64 GetPartnerGiftDestination(Int64 APartnerKey, DateTime ? AGiftDate)
        {
            Int64 PartnerField = 0;

            if ((APartnerKey == 0) || !AGiftDate.HasValue)
            {
                return 0;
            }

            string GetPartnerGiftDestinationSQL = String.Format("SELECT DISTINCT pgd.p_field_key_n as FieldKey" +
                "  FROM PUB_p_partner_gift_destination pgd " +
                "  WHERE pgd.p_partner_key_n = {0}" +
                "    And ((pgd.p_date_effective_d <= '{1:yyyy-MM-dd}' And pgd.p_date_expires_d IS NULL)" +
                "         Or ('{1:yyyy-MM-dd}' BETWEEN pgd.p_date_effective_d And pgd.p_date_expires_d))",
                APartnerKey,
                AGiftDate);

            DataTable GiftDestTable = null;
            string PartnerGiftDestinationTable = "PartnerGiftDestination";

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetPartnerGiftDestination");

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    try
                    {
                        GiftDestTable = db.SelectDT(GetPartnerGiftDestinationSQL, PartnerGiftDestinationTable, Transaction);

                        if ((GiftDestTable != null) && (GiftDestTable.Rows.Count > 0))
                        {
                            PartnerField = (Int64)GiftDestTable.DefaultView[0].Row["FieldKey"];
                        }
                    }
                    catch (Exception ex)
                    {
                        TLogging.LogException(ex, Utilities.GetMethodSignature());
                        throw;
                    }
                });
            db.CloseDBConnection();

            return PartnerField;
        }

        /// <summary>
        /// Verifies the existence of a Partner.
        /// </summary>
        /// <param name="APartnerKey">PartnerKey of Partner to find the short name for.</param>
        /// <param name="AValidPartnerClasses">Pass in a array of valid PartnerClasses that the
        /// Partner is allowed to have (eg. [PERSON, FAMILY], or an empty array ( [] ).</param>
        /// <param name="APartnerExists">True if the Partner exists in the database or if PartnerKey is 0.</param>
        /// <param name="APartnerShortName">ShortName for the found Partner ('' if Partner
        /// doesn't exist or PartnerKey is 0).</param>
        /// <param name="APartnerClass">Partner Class of the found Partner (FAMILY if Partner
        /// doesn't exist or PartnerKey is 0).</param>
        /// <param name="APartnerStatus">Partner Status</param>
        /// <returns>true if Partner was found in DB (except if AValidPartnerClasses isn't
        /// an empty array and the found Partner isn't of a PartnerClass that is in the
        /// Set) or PartnerKey is 0, otherwise false.</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean VerifyPartner(Int64 APartnerKey,
            TPartnerClass[] AValidPartnerClasses,
            out bool APartnerExists,
            out String APartnerShortName,
            out TPartnerClass APartnerClass,
            out TStdPartnerStatusCode APartnerStatus)
        {
            bool ReturnValue = false;
            bool PartnerExists = false;
            string PartnerShortName = null;
            TPartnerClass PartnerClass = TPartnerClass.BANK;
            TStdPartnerStatusCode PartnerStatus = TStdPartnerStatusCode.spscACTIVE;

            TDBTransaction ReadTransaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("VerifyPartner");

            db.ReadTransaction(
                ref ReadTransaction,
                AEncapsulatedDBAccessCode : delegate
                {
                    ReturnValue = PartnerExists = MCommonMain.RetrievePartnerShortName(APartnerKey,
                        out PartnerShortName, out PartnerClass, out PartnerStatus, ReadTransaction);
                });
            db.CloseDBConnection();

            APartnerShortName = PartnerShortName;
            APartnerClass = PartnerClass;
            APartnerExists = PartnerExists;
            APartnerStatus = PartnerStatus;

//          TLogging.LogAtLevel(7, "TPartnerServerLookups.VerifyPartner: " + Convert.ToInt32(AValidPartnerClasses.Length));

            if (AValidPartnerClasses.Length != 0)
            {
                Boolean classIsGood = false;

                foreach (TPartnerClass goodClass in AValidPartnerClasses)
                {
                    if (APartnerClass == goodClass)
                    {
                        classIsGood = true;
                        break;
                    }
                }

                ReturnValue &= classIsGood;
            }

//            AIsMergedPartner = (PartnerStatus == TStdPartnerStatusCode.spscMERGED);

            return ReturnValue;
        }

        /// <summary>Test whether a Partner is Active.</summary>
        /// <returns>True if this Status is listed as being active</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean PartnerHasActiveStatus(Int64 APartnerKey)
        {
            Boolean IsActive = false;

            TDBTransaction readTransaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("PartnerHasActiveStatus");

            db.ReadTransaction(
                ref readTransaction,
                delegate
                {
                    PPartnerTable partnerTbl = PPartnerAccess.LoadByPrimaryKey(APartnerKey, readTransaction);

                    if (partnerTbl.Rows.Count > 0)
                    {
                        PPartnerStatusTable statusTbl = PPartnerStatusAccess.LoadByPrimaryKey(partnerTbl[0].StatusCode, readTransaction);

                        if (statusTbl.Rows.Count > 0)
                        {
                            IsActive = statusTbl[0].PartnerIsActive;
                        }
                    }
                });
            db.CloseDBConnection();
            return IsActive;
        }

        /// <summary>Is this the key of a valid Gift Recipient?</summary>
        /// <param name="APartnerKey"></param>
        /// <returns>True if this is a valid key to a partner that's linked to a Cost Centre (in any ledger)
        /// or if this is a local site key.</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean PartnerIsLinkedToCC(Int64 APartnerKey)
        {
            TDBTransaction ReadTransaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("PartnerIsLinkedToCC");
            Boolean Ret = false;

            db.ReadTransaction(
                ref ReadTransaction,
                delegate
                {
                    string Query = "SELECT COUNT(*) FROM a_ledger, a_valid_ledger_number " +
                                   "WHERE a_ledger.p_partner_key_n = " + APartnerKey +
                                   " OR a_valid_ledger_number.p_partner_key_n = " + APartnerKey;

                    if (Convert.ToInt32(db.ExecuteScalar(Query, ReadTransaction)) > 0)
                    {
                        Ret = true;
                    }
                });
            db.CloseDBConnection();
            return Ret;
        }

        /// <summary>Is this Recipient, if of type CC, linked?</summary>
        /// <param name="ALedgerNumber">The ledger where the partner should be linked to a cost center.
        /// This can be overwritten by the parameter LedgerForPartnerCCLinks in the config file</param>
        /// <param name="APartnerKey"></param>
        /// <returns>True if this is a valid key to a partner of type CC
        /// that is linked (in the specified ledger)</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean PartnerOfTypeCCIsLinked(Int32 ALedgerNumber, Int64 APartnerKey)
        {
            // For multi ledger setups, the partners are sometimes only linked in one ledger
            ALedgerNumber = TAppSettingsManager.GetInt32("LedgerForPartnerCCLinks", ALedgerNumber);

            bool PartnerAndLinksCombinationIsValid = true;
            int NumPartnerWithTypeCostCentre = 0;
            int NumPartnerLinks = 0;

            AValidLedgerNumberTable LinksTbl = null;

            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("PartnerOfTypeCCIsLinked");

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    string sQL = String.Format("SELECT COUNT(*)" +
                        " FROM public.{0}, public.{1}" +
                        " WHERE {0}.{2} = {1}.{3}",
                        PPartnerTable.GetTableDBName(),
                        PPartnerTypeTable.GetTableDBName(),
                        PPartnerTable.GetPartnerKeyDBName(),
                        PPartnerTypeTable.GetPartnerKeyDBName());

                    if (APartnerKey > 0)
                    {
                        sQL += String.Format(" AND {0}.{1} = {2}",
                            PPartnerTable.GetTableDBName(),
                            PPartnerTable.GetPartnerKeyDBName(),
                            APartnerKey);
                    }

                    sQL += String.Format(" AND {0}.{1} = '{2}'",
                        PPartnerTypeTable.GetTableDBName(),
                        PPartnerTypeTable.GetTypeCodeDBName(),
                        MPartnerConstants.PARTNERTYPE_COSTCENTRE);

                    NumPartnerWithTypeCostCentre = Convert.ToInt32(db.ExecuteScalar(sQL, Transaction));

                    if (APartnerKey > 0)
                    {
                        LinksTbl = AValidLedgerNumberAccess.LoadByPrimaryKey(ALedgerNumber, APartnerKey, Transaction);
                    }
                    else
                    {
                        LinksTbl = AValidLedgerNumberAccess.LoadViaALedger(ALedgerNumber, Transaction);
                    }

                    NumPartnerLinks = LinksTbl.Count;

                    if ((NumPartnerWithTypeCostCentre > 0) && (NumPartnerLinks == 0))
                    {
                        PartnerAndLinksCombinationIsValid = false;
                    }
                });
            db.CloseDBConnection();

            return PartnerAndLinksCombinationIsValid;
        }

        /// <summary>Does this Recipient have a valid current gift destination?</summary>
        /// <param name="APartnerKey"></param>
        /// <param name="AGiftDate"></param>
        /// <returns>True if this is partner has a valid gift destination
        /// that is linked (in the specified ledger)</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean PartnerHasCurrentGiftDestination(Int64 APartnerKey, DateTime ? AGiftDate)
        {
            return GetPartnerGiftDestination(APartnerKey, AGiftDate) > 0;
        }

        /// <summary>
        /// Verifies the existence of a Partner and checks if the current user can modify it.
        /// </summary>
        /// <param name="APartnerKey">PartnerKey of Partner to find the short name for</param>
        /// <param name="APartnerShortName">ShortName for the found Partner ('' if Partner
        ///  doesn't exist or PartnerKey is 0)</param>
        /// <param name="APartnerClass">Partner Class of the found Partner (FAMILY if Partner
        ///  doesn't exist or PartnerKey is 0)</param>
        /// <param name="AIsMergedPartner">true if the Partner' Partner Status is MERGED,
        ///  otherwise false</param>
        /// <param name="AUserCanAccessPartner">true if the current user has the rights to
        /// access this partner</param>
        /// <returns>true if Partner was found in DB or Partner key = 0, otherwise false</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean VerifyPartnerAndGetDetails(Int64 APartnerKey,
            out String APartnerShortName,
            out TPartnerClass APartnerClass,
            out Boolean AIsMergedPartner,
            out Boolean AUserCanAccessPartner)
        {
            APartnerShortName = "";
            APartnerClass = TPartnerClass.FAMILY; // Default. This is not really correct but the best compromise if PartnerKey is 0 or Partner isn't found since we have an enum here.
            AIsMergedPartner = false;
            AUserCanAccessPartner = false;

            TDBTransaction ReadTransaction;
            Boolean NewTransaction;
            StringCollection RequiredColumns;
            PPartnerTable PartnerTable;
            Boolean ReturnValue = true;
            TStdPartnerStatusCode PartnerStatus = TStdPartnerStatusCode.spscACTIVE;

            // initialise outout Arguments
            if (APartnerKey != 0)
            {
                // only some fields are needed
                RequiredColumns = new StringCollection();
                RequiredColumns.Add(PPartnerTable.GetPartnerShortNameDBName());
                RequiredColumns.Add(PPartnerTable.GetPartnerClassDBName());
                RequiredColumns.Add(PPartnerTable.GetStatusCodeDBName());
                RequiredColumns.Add(PPartnerTable.GetRestrictedDBName());
                RequiredColumns.Add(PPartnerTable.GetUserIdDBName());
                RequiredColumns.Add(PPartnerTable.GetGroupIdDBName());
                TDataBase db = DBAccess.Connect("VerifyPartnerAndGetDetails");
                ReadTransaction = db.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                    out NewTransaction);
                try
                {
                    PartnerTable = PPartnerAccess.LoadByPrimaryKey(APartnerKey, RequiredColumns, ReadTransaction, null, 0, 0);
                }
                finally
                {
                    if (NewTransaction)
                    {
                        ReadTransaction.Commit();
                        TLogging.LogAtLevel(7, "TPartnerServerLookups.VerifyPartner: committed own transaction.");
                    }
                    db.CloseDBConnection();
                }

                if (PartnerTable.Rows.Count == 0)
                {
                    ReturnValue = false;
                }
                else
                {
                    // since we loaded by primary key there must just be one partner row
                    APartnerShortName = PartnerTable[0].PartnerShortName;
                    APartnerClass = SharedTypes.PartnerClassStringToEnum(PartnerTable[0].PartnerClass);
                    PartnerStatus = SharedTypes.StdPartnerStatusCodeStringToEnum(PartnerTable[0].StatusCode);

                    // check if user can access partner
                    if (Ict.Petra.Server.MPartner.Common.TSecurity.CanAccessPartner(PartnerTable[0]) == TPartnerAccessLevelEnum.palGranted)
                    {
                        AUserCanAccessPartner = true;
                    }

                    // check if partner is merged
                    if (PartnerStatus == TStdPartnerStatusCode.spscMERGED)
                    {
                        AIsMergedPartner = true;
                    }
                    else
                    {
                        AIsMergedPartner = false;
                    }

                    ReturnValue = true;
                }
            }
            else
            {
                // Return result as valid if Partner Key is 0.
                ReturnValue = true;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Verifies the existence of a Partner
        /// </summary>
        /// <param name="APartnerKey">PartnerKey of Partner to find the short name for</param>
        /// <returns>true if Partner was found in DB or Partner key = 0, otherwise false</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean VerifyPartner(Int64 APartnerKey)
        {
            TDBTransaction ReadTransaction;
            Boolean NewTransaction;
            PPartnerTable PartnerTable;
            Boolean ReturnValue = true;
            TDataBase db = DBAccess.Connect("VerifyPartner");

            // initialise outout Arguments
            if (APartnerKey != 0)
            {
                ReadTransaction = db.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                    out NewTransaction);
                try
                {
                    PartnerTable = PPartnerAccess.LoadByPrimaryKey(APartnerKey, ReadTransaction);
                }
                finally
                {
                    if (NewTransaction)
                    {
                        ReadTransaction.Commit();
                        TLogging.LogAtLevel(7, "TPartnerServerLookups.VerifyPartner: committed own transaction.");
                    }
                    db.CloseDBConnection();
                }

                if (PartnerTable.Rows.Count == 0)
                {
                    ReturnValue = false;
                }
                else
                {
                    ReturnValue = true;
                }
            }
            else
            {
                // Return result as valid if Partner Key is 0.
                ReturnValue = true;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Verifies the existence of a Partner at a given location
        /// </summary>
        /// <param name="APartnerKey">PartnerKey of Partner to be verified</param>
        /// <param name="ALocationKey">Location Key of Partner to be verified</param>
        /// <param name="AAddressIsCurrent"></param>
        /// <param name="AAddressIsMailing"></param>
        /// <returns>true if Partner was found in DB at given location, otherwise false</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean VerifyPartnerAtLocation(Int64 APartnerKey,
            TLocationPK ALocationKey, out bool AAddressIsCurrent, out bool AAddressIsMailing)
        {
            AAddressIsCurrent = false;
            AAddressIsMailing = false;

            TDBTransaction ReadTransaction;
            Boolean NewTransaction;
            PPartnerLocationTable PartnerLocationTable;
            Boolean ReturnValue = true;

            TDataBase db = DBAccess.Connect("VerifyPartnerAtLocation");
            ReadTransaction = db.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                out NewTransaction);
            try
            {
                PartnerLocationTable = PPartnerLocationAccess.LoadByPrimaryKey(APartnerKey,
                    ALocationKey.SiteKey,
                    ALocationKey.LocationKey,
                    ReadTransaction);
            }
            finally
            {
                if (NewTransaction)
                {
                    ReadTransaction.Commit();
                }
                db.CloseDBConnection();
            }

            if (PartnerLocationTable.Rows.Count == 0)
            {
                ReturnValue = false;
            }
            else
            {
                PPartnerLocationRow Row = (PPartnerLocationRow)PartnerLocationTable.Rows[0];

                // check if the partner location is either current
                AAddressIsCurrent = true;

                if ((Row.DateEffective > DateTime.Today)
                    || ((Row.DateGoodUntil != null)
                        && (Row.DateGoodUntil < DateTime.Today)))
                {
                    AAddressIsCurrent = false;
                }

                // check if the partner location is a mailing address
                AAddressIsMailing = Row.SendMail;

                ReturnValue = true;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Returns information about a Partner that was Merged and about the
        /// Partner it was merged into.
        /// </summary>
        /// <param name="AMergedPartnerPartnerKey">PartnerKey of Merged Partner.</param>
        /// <param name="AMergedPartnerPartnerShortName">ShortName of Merged Partner.</param>
        /// <param name="AMergedPartnerPartnerClass">Partner Class of Merged Partner.</param>
        /// <param name="AMergedIntoPartnerKey">PartnerKey of Merged-Into Partner. (Only
        /// populated if that information is available.)</param>
        /// <param name="AMergedIntoPartnerShortName">ShortName of Merged-Into Partner. (Only
        /// populated if that information is available.)</param>
        /// <param name="AMergedIntoPartnerClass">PartnerClass of Merged-Into Partner. (Only
        /// populated if that information is available.)</param>
        /// <param name="AMergedBy">User who performed the Partner Merge operation. (Only
        /// populated if that information is available.)</param>
        /// <param name="AMergeDate">Date on which the Partner Merge operation was done. (Only
        /// populated if that information is available.)</param>
        /// <returns>True if (1) Merged Partner exists and (2) its Status is MERGED,
        /// otherwise false.</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean MergedPartnerDetails(Int64 AMergedPartnerPartnerKey,
            out String AMergedPartnerPartnerShortName,
            out TPartnerClass AMergedPartnerPartnerClass,
            out Int64 AMergedIntoPartnerKey,
            out String AMergedIntoPartnerShortName,
            out TPartnerClass AMergedIntoPartnerClass,
            out String AMergedBy,
            out DateTime AMergeDate)
        {
            TDBTransaction ReadTransaction;
            Boolean NewTransaction;
            StringCollection RequiredColumns;
            PPartnerTable MergedPartnerTable;
            PPartnerTable PartnerMergedIntoTable;
            PPartnerMergeTable PartnerMergeTable;
            Boolean ReturnValue = false;

            // Initialise out Arguments
            AMergedPartnerPartnerShortName = "";
            AMergedPartnerPartnerClass = TPartnerClass.FAMILY;  // Default. This is not really correct but the best compromise if PartnerKey is 0 or Partner isn't found since we have an enum here.
            AMergedIntoPartnerKey = -1;
            AMergedIntoPartnerShortName = "";
            AMergedIntoPartnerClass = TPartnerClass.FAMILY;     // Default. This is not really correct but the best compromise if PartnerKey is 0 or Partner isn't found since we have an enum here.
            AMergedBy = "";
            AMergeDate = DateTime.MinValue;

            if (AMergedPartnerPartnerKey != 0)
            {
                /*
                 * First we look up the Partner that was Merged to get some details of it.
                 */

                // only some fields are needed
                RequiredColumns = new StringCollection();
                RequiredColumns.Add(PPartnerTable.GetPartnerShortNameDBName());
                RequiredColumns.Add(PPartnerTable.GetStatusCodeDBName());
                RequiredColumns.Add(PPartnerTable.GetPartnerClassDBName());

                TDataBase db = DBAccess.Connect("MergedPartnerDetails");
                ReadTransaction = db.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                    out NewTransaction);

                try
                {
                    MergedPartnerTable = PPartnerAccess.LoadByPrimaryKey(
                        AMergedPartnerPartnerKey, RequiredColumns, ReadTransaction, null, 0, 0);

                    if (MergedPartnerTable.Rows.Count == 0)
                    {
                        ReturnValue = false;
                    }
                    else
                    {
                        if (SharedTypes.StdPartnerStatusCodeStringToEnum(
                                MergedPartnerTable[0].StatusCode) != TStdPartnerStatusCode.spscMERGED)
                        {
                            ReturnValue = false;
                        }
                        else
                        {
                            AMergedPartnerPartnerShortName = MergedPartnerTable[0].PartnerShortName;
                            AMergedPartnerPartnerClass = SharedTypes.PartnerClassStringToEnum(MergedPartnerTable[0].PartnerClass);

                            /*
                             * Now we look up the Partner that was Merged in the PartnerMerge DB Table
                             * to get the information about the Merged-Into Partner.
                             */
                            PartnerMergeTable = PPartnerMergeAccess.LoadByPrimaryKey(AMergedPartnerPartnerKey, ReadTransaction);

                            if (PartnerMergeTable.Rows.Count == 0)
                            {
                                /*
                                 * Although we didn't find the Merged Partner in the PartnerMerge
                                 * DB Table it is still a valid, Merged Partner, so we return true
                                 * in this case.
                                 */
                                ReturnValue = true;
                            }
                            else
                            {
                                /*
                                 * Now we look up the Merged-Into Partner to get some details of it.
                                 */
                                PartnerMergedIntoTable = PPartnerAccess.LoadByPrimaryKey(
                                    PartnerMergeTable[0].MergeTo, RequiredColumns,
                                    ReadTransaction, null, 0, 0);

                                if (PartnerMergedIntoTable.Rows.Count == 0)
                                {
                                    ReturnValue = false;
                                }
                                else
                                {
                                    AMergedIntoPartnerKey = PartnerMergeTable[0].MergeTo;
                                    AMergedBy = PartnerMergeTable[0].MergedBy;
                                    AMergeDate = PartnerMergeTable[0].MergeDate.Value;
                                    AMergedIntoPartnerShortName = PartnerMergedIntoTable[0].PartnerShortName;
                                    AMergedIntoPartnerClass = SharedTypes.PartnerClassStringToEnum(PartnerMergedIntoTable[0].PartnerClass);

                                    ReturnValue = true;
                                }
                            }
                        }
                    }
                }
                finally
                {
                    if (NewTransaction)
                    {
                        ReadTransaction.Commit();
                        TLogging.LogAtLevel(7, "TPartnerServerLookups.MergedPartnerDetails: committed own transaction.");
                    }
                    db.CloseDBConnection();
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Retrieves information about a Partner.
        /// </summary>
        /// <remarks>
        /// The information returned can be of different scope. Scope refers to the amount/detail
        /// of information that is returned. The scope is defined with the
        /// <paramref name="APartnerInfoScope" /> parameter. Larger scope means longer time is
        /// needed for the retrieval of the data and a larger DataSet to transfer to the Client
        /// (if this Method is called from the Client)!
        /// </remarks>
        /// <param name="APartnerKey">PartnerKey of Partner to find the short name for</param>
        /// <param name="APartnerInfoScope">Defines the scope of data that should be returned
        /// for the Partner.</param>
        /// <param name="APartnerInfoDS">Typed DataSet of Type <see cref="PartnerInfoTDS" /> that
        /// contains the Partner Information that was requested for the Partner.</param>
        /// <returns>True if Partner was found in DB, otherwise false.
        /// </returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean PartnerInfo(Int64 APartnerKey,
            TPartnerInfoScopeEnum APartnerInfoScope,
            out PartnerInfoTDS APartnerInfoDS)
        {
            return PartnerInfo(APartnerKey, null, APartnerInfoScope, out APartnerInfoDS);
        }

        /// <summary>
        /// Retrieves information about a Partner.
        /// </summary>
        /// <remarks>
        /// The information returned can be of different scope. Scope refers to the amount/detail
        /// of information that is returned. The scope is defined with the
        /// <paramref name="APartnerInfoScope" /> parameter. Larger scope means longer time is
        /// needed for the retrieval of the data and a larger DataSet to transfer to the Client
        /// (if this Method is called from the Client)!
        /// </remarks>
        /// <param name="APartnerKey">PartnerKey of Partner to find the short name for</param>
        /// <param name="ALocationKey" >Location Key of the Location that the information should be
        /// retrieved for.</param>
        /// <param name="APartnerInfoScope">Defines the scope of data that should be returned
        /// for the Partner.</param>
        /// <param name="APartnerInfoDS">Typed DataSet of Type <see cref="PartnerInfoTDS" /> that
        /// contains the Partner Information that was requested for the Partner.</param>
        /// <returns>True if Partner was found in DB, otherwise false.
        /// </returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean PartnerInfo(Int64 APartnerKey, TLocationPK ALocationKey,
            TPartnerInfoScopeEnum APartnerInfoScope,
            out PartnerInfoTDS APartnerInfoDS)
        {
            const string DATASET_NAME = "PartnerInfo";

            Boolean ReturnValue = false;
            TDBTransaction ReadTransaction = new TDBTransaction();

            APartnerInfoDS = new PartnerInfoTDS(DATASET_NAME);
            Boolean newTransaction = false;

            TDataBase DBConnectionObj = DBAccess.Connect("TPartnerServerLookups.PartnerInfo");
            try
            {
                ReadTransaction = DBConnectionObj.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted, out newTransaction);

                switch (APartnerInfoScope)
                {
                    case TPartnerInfoScopeEnum.pisHeadOnly :

                        throw new NotImplementedException();

                    case TPartnerInfoScopeEnum.pisPartnerLocationAndRestOnly:

                        ReturnValue = TServerLookups_PartnerInfo.PartnerLocationAndRestOnly(APartnerKey,
                        ALocationKey, ref APartnerInfoDS, ReadTransaction);

                        break;

                    case TPartnerInfoScopeEnum.pisPartnerLocationOnly:

                        ReturnValue = TServerLookups_PartnerInfo.PartnerLocationOnly(APartnerKey,
                        ALocationKey, ref APartnerInfoDS, ReadTransaction);

                        break;

                    case TPartnerInfoScopeEnum.pisLocationPartnerLocationAndRestOnly:

                        ReturnValue = TServerLookups_PartnerInfo.LocationPartnerLocationAndRestOnly(APartnerKey,
                        ALocationKey, ref APartnerInfoDS, ReadTransaction);

                        break;

                    case TPartnerInfoScopeEnum.pisLocationPartnerLocationOnly:

                        ReturnValue = TServerLookups_PartnerInfo.LocationPartnerLocationOnly(APartnerKey,
                        ALocationKey, ref APartnerInfoDS, ReadTransaction);

                        break;

                    case TPartnerInfoScopeEnum.pisPartnerAttributesOnly:

                        ReturnValue = TServerLookups_PartnerInfo.PartnerAttributesOnly(APartnerKey,
                        ref APartnerInfoDS, ReadTransaction);

                        break;

                    case TPartnerInfoScopeEnum.pisFull:

                        ReturnValue = TServerLookups_PartnerInfo.AllPartnerInfoData(APartnerKey,
                        ref APartnerInfoDS, ReadTransaction);

                        break;
                } // switch

            } // try
            finally
            {
                if (newTransaction)
                {
                    ReadTransaction.Rollback();
                }

                DBConnectionObj.CloseDBConnection();
            }

            return ReturnValue;
        }

        /// <summary>Retrieves receipting fields from a partner key.</summary>
        /// <param name="APartnerKey"></param>
        /// <param name="AReceiptEachGift"></param>
        /// <param name="AReceiptLetterFrequency"></param>
        /// <param name="AEmailGiftStatement"></param>
        /// <param name="AAnonymousDonor"></param>
        [RequireModulePermission("FINANCE-2")]
        public static bool GetPartnerReceiptingInfo(
            Int64 APartnerKey,
            out bool AReceiptEachGift,
            out String AReceiptLetterFrequency,
            out bool AEmailGiftStatement,
            out bool AAnonymousDonor)
        {
            TDBTransaction ReadTransaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetPartnerReceiptingInfo");
            PPartnerTable PartnerTbl = null;

            db.ReadTransaction(
                ref ReadTransaction,
                delegate
                {
                    PartnerTbl = PPartnerAccess.LoadByPrimaryKey(APartnerKey, ReadTransaction);
                });
            db.CloseDBConnection();

            if (PartnerTbl.Rows.Count > 0)
            {
                PPartnerRow Row = PartnerTbl[0];
                AReceiptEachGift = Row.ReceiptEachGift;
                AReceiptLetterFrequency = Row.ReceiptLetterFrequency;
                AEmailGiftStatement = Row.EmailGiftStatement;
                AAnonymousDonor = Row.AnonymousDonor;
                return true;
            }
            else
            {
                AReceiptEachGift = false;
                AReceiptLetterFrequency = "";
                AEmailGiftStatement = false;
                AAnonymousDonor = false;
                return false;
            }
        }

        /// <summary>
        /// Retrieves the description of an extract.
        /// </summary>
        /// <param name="AExtractName">The name which identifies the extract</param>
        /// <param name="AExtractDescription">The description of the extract</param>
        /// <returns>true if the extract was found and the description was retrieved</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean GetExtractDescription(String AExtractName, out String AExtractDescription)
        {
            TDBTransaction ReadTransaction = new TDBTransaction();
            Boolean ReturnValue = false;

            AExtractDescription = "Can not retrieve description";

            TLogging.LogAtLevel(9, "TPartnerServerLookups.GetExtractDescription called!");

            MExtractMasterTable ExtractMasterDT = new MExtractMasterTable();

            // Load data
            MExtractMasterTable TemplateExtractDT = new MExtractMasterTable();
            MExtractMasterRow TemplateRow = TemplateExtractDT.NewRowTyped(false);
            TemplateRow.ExtractName = AExtractName;

            TDataBase db = DBAccess.Connect("GetExtractDescription");
            db.ReadTransaction( ref ReadTransaction,
                delegate
                {
                    ExtractMasterDT = MExtractMasterAccess.LoadUsingTemplate(TemplateRow, ReadTransaction);
                });
            db.CloseDBConnection();

            if (ExtractMasterDT.Rows.Count < 1)
            {
                ReturnValue = false;
                TLogging.LogAtLevel(7, "TPartnerServerLookups.TPartnerServerLookups.GetExtractDescription: m_extract_master DB Table is empty");
            }
            else
            {
                MExtractMasterRow ExtractRow = ExtractMasterDT.Rows[0] as MExtractMasterRow;

                if (ExtractRow != null)
                {
                    AExtractDescription = ExtractRow.ExtractDesc;
                    ReturnValue = true;
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets the foundation status of the partner.
        /// This function should only be called for partners of type organisation.
        /// </summary>
        /// <param name="APartnerKey">Partner key of the partner to retrieve the foundation status.
        /// Partner must be an organisation.</param>
        /// <param name="AIsFoundation">true if the partner (organisation) is a foundation. Otherwise false</param>
        /// <returns>True, if an entry of the partner was found in table p_organisation.
        /// False, if there is no partner with the partner key or the partner is not an organisation</returns>
        /// <exception>EOPAppException if we don't find a partner or if the partner is not an organisation</exception>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean GetPartnerFoundationStatus(Int64 APartnerKey, out Boolean AIsFoundation)
        {
            TDBTransaction ReadTransaction;
            Boolean NewTransaction;
            Boolean ReturnValue = false;

            AIsFoundation = false;

            TLogging.LogAtLevel(9, "TPartnerServerLookups.GetPartnerFoundationStatus called!");

            POrganisationTable OrganisationDT = new POrganisationTable();
            TDataBase db = DBAccess.Connect("GetPartnerFoundationStatus");
            ReadTransaction = db.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                out NewTransaction);

            // Load data
            try
            {
                OrganisationDT = POrganisationAccess.LoadByPrimaryKey(APartnerKey, ReadTransaction);
            }
            finally
            {
                if (NewTransaction)
                {
                    ReadTransaction.Commit();
                    TLogging.LogAtLevel(7, "TPartnerServerLookups.GetPartnerFoundationStatus: committed own transaction.");
                }
            }

            db.CloseDBConnection();

            if (OrganisationDT.Rows.Count < 1)
            {
                // Do we have a partner?
                TStdPartnerStatusCode PartnerStatus;
                String PartnerShortName;
                TPartnerClass PartnerClass;

                if (MCommonMain.RetrievePartnerShortName(APartnerKey, out PartnerShortName, out PartnerClass, out PartnerStatus, ReadTransaction))
                {
                    // we have a partner but it's not an organisation
                    throw new EOPAppException(
                        "TPartnerServerLookups.GetPartnerFoundationStatus: p_organisation DB Table is empty. The partner key does not refer to an organisation!");
                }
                else
                {
                    // we don't have a valid partner key
                    throw new EOPAppException(
                        "TPartnerServerLookups.GetPartnerFoundationStatus: p_organisation DB Table is empty. The partner key is not valid!");
                }
            }
            else
            {
                POrganisationRow ExtractRow = OrganisationDT.Rows[0] as POrganisationRow;

                if (ExtractRow != null)
                {
                    AIsFoundation = ExtractRow.Foundation;
                    ReturnValue = true;
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Get a list of the last used partners from the current user.
        /// </summary>
        /// <param name="AMaxPartnersCount">Maxinum numbers of Partners to return</param>
        /// <param name="APartnerClasses">List of partner classes which kind of partners the result should contain.
        /// If it contains "*" then all recent partners will be returned otherwise only the partners whose
        /// partner class is in APartnerClasses.</param>
        /// <param name="ARecentlyUsedPartners">List of the last used partner names and partner keys</param>
        /// <returns>true if call was successfull</returns>
        [RequireModulePermission("PTNRUSER")]
        public static Boolean GetRecentlyUsedPartners(int AMaxPartnersCount,
            ArrayList APartnerClasses,
            out Dictionary <long, string>ARecentlyUsedPartners)
        {
            TDBTransaction ReadTransaction;
            Boolean NewTransaction;

            ARecentlyUsedPartners = new Dictionary <long, string>();

            TLogging.LogAtLevel(9, "TPartnerServerLookups.GetRecentlyUsedPartner called!");

            PRecentPartnersTable RecentPartnersDT = new PRecentPartnersTable();
            TDataBase db = DBAccess.Connect("GetRecentlyUsedPartners");

            ReadTransaction = db.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                out NewTransaction);

            // Load the recently used partners from this user
            try
            {
                RecentPartnersDT = PRecentPartnersAccess.LoadViaSUser(UserInfo.GetUserInfo().UserID, ReadTransaction);
            }
            finally
            {
                if (NewTransaction)
                {
                    ReadTransaction.Commit();
                    TLogging.LogAtLevel(7, "TPartnerServerLookups.GetRecentUsedPartners: committed own transaction.");
                }
            }

            PPartnerTable PartnerDT = new PPartnerTable();

            // Sort the users by date and time they have been last used

            System.Data.DataRow[] RecentPartnerRows = RecentPartnersDT.Select("",
                TTypedDataTable.GetLabel(PRecentPartnersTable.TableId, PRecentPartnersTable.ColumnwhenDateId) + " DESC, " +
                TTypedDataTable.GetLabel(PRecentPartnersTable.TableId, PRecentPartnersTable.ColumnwhenTimeId) + " DESC");

            for (int Counter = 0; Counter < RecentPartnersDT.Rows.Count; ++Counter)
            {
                PRecentPartnersRow RecentPartnerRow = (PRecentPartnersRow)RecentPartnerRows[Counter];

                ReadTransaction = db.GetNewOrExistingTransaction(
                    IsolationLevel.ReadCommitted,
                    out NewTransaction);

                // Get the partner name from the recently used partner
                try
                {
                    PartnerDT = PPartnerAccess.LoadByPrimaryKey(RecentPartnerRow.PartnerKey, ReadTransaction);
                }
                finally
                {
                    if (NewTransaction)
                    {
                        ReadTransaction.Commit();
                        TLogging.LogAtLevel(7, "TPartnerServerLookups.GetRecentUsedPartners: committed own transaction.");
                    }
                }

                db.CloseDBConnection();

                if (PartnerDT.Rows.Count > 0)
                {
                    /* Check the partner class.
                     * If we want this partner then add it to the ARecentlyUsedPartners list
                     * otherwise skip it.
                     */

                    PPartnerRow PartnerRow = (PPartnerRow)PartnerDT.Rows[0];

                    foreach (Object CurrentPartnerClass in APartnerClasses)
                    {
                        string TmpString = CurrentPartnerClass.ToString();

                        if ((TmpString == "*")
                            || (TmpString == PartnerRow.PartnerClass))
                        {
                            // String contains Name and type like this: J. Miller (type FAMILY)
                            ARecentlyUsedPartners.Add(PartnerRow.PartnerKey,
                                PartnerRow.PartnerShortName + " (type " + PartnerRow.PartnerClass + ")");
                        }
                    }
                }

                if (ARecentlyUsedPartners.Count >= AMaxPartnersCount)
                {
                    break;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the family partner key of a person record.
        /// This function should only be called for partners of type person.
        /// </summary>
        /// <param name="APersonKey">Partner key of the person to retrieve the family key for
        /// Partner must be a person.</param>
        /// <returns>Family partner key of the person. A Person must always have a family that it is related to.
        /// False, if there is no partner with the partner key or the partner is not an organisation</returns>
        /// <exception>EOPAppException if we don't find a partner or if the partner is not an organisation</exception>
        [RequireModulePermission("PTNRUSER")]
        public static Int64 GetFamilyKeyForPerson(Int64 APersonKey)
        {
            TDBTransaction ReadTransaction;
            Boolean NewTransaction;
            Int64 ReturnValue = 0;
            TDataBase db = DBAccess.Connect("GetFamilyKeyForPerson");

            ReadTransaction = db.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                out NewTransaction);

            PPersonTable PersonDT = new PPersonTable();

            try
            {
                PersonDT = PPersonAccess.LoadByPrimaryKey(APersonKey, ReadTransaction);
            }
            finally
            {
                if (NewTransaction)
                {
                    ReadTransaction.Commit();
                }
            }

            db.CloseDBConnection();

            if (PersonDT.Rows.Count == 1)
            {
                ReturnValue = ((PPersonRow)PersonDT.Rows[0]).FamilyKey;
            }
            else
            {
                // we don't have a valid partner key
                throw new EOPAppException(
                    "TPartnerServerLookups.GetFamilyKeyForPerson: The partner key is not valid!");
            }

            return ReturnValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>the country code for this installation of OpenPetra.</returns>
        [RequireModulePermission("PTNRUSER")]
        public static string GetCountryCodeFromSiteLedger()
        {
            bool NewTransaction;
            TDataBase db = DBAccess.Connect("GetCountryCodeFromSiteLedger");
            TDBTransaction ReadTransaction = db.GetNewOrExistingTransaction(IsolationLevel.ReadCommitted,
                out NewTransaction);

            string CountryCode = TAddressTools.GetCountryCodeFromSiteLedger(ReadTransaction);

            if (NewTransaction)
            {
                ReadTransaction.Commit();
            }

            db.CloseDBConnection();

            return CountryCode;
        }

        /// <summary>
        /// Can be used to recover basic p_unit information
        /// </summary>
        /// <param name="APartnerKey">The Partner Key of the UNIT</param>
        /// <param name="AUnitName">Returns the Unit short name</param>
        /// <param name="ACountryCode">Returns the country code stored for this unit.  It may be 99.</param>
        /// <returns></returns>
        [RequireModulePermission("PTNRUSER")]
        public static bool GetUnitNameAndCountryCodeFromPartnerKey(Int64 APartnerKey, out string AUnitName, out string ACountryCode)
        {
            string PartnerShortName = null;
            string CountryCode = null;
            TDBTransaction Transaction = new TDBTransaction();
            TDataBase db = DBAccess.Connect("GetUnitNameAndCountryCodeFromPartnerKey");

            db.ReadTransaction(
                ref Transaction,
                delegate
                {
                    // SELECT p.p_partner_short_name_c, u.p_country_c FROM p_partner p JOIN p_unit u ON p.p_partner_key_n=u.p_partner_key_n
                    // WHERE p_partner_key_n=?
                    string sql = String.Format("SELECT p.{0}, u.{1} FROM {2} p JOIN {3} u ON p.{4}=u.{5} WHERE p.{4}={6}",
                        PPartnerTable.GetPartnerShortNameDBName(),
                        PUnitTable.GetCountryCodeDBName(),
                        PPartnerTable.GetTableDBName(),
                        PUnitTable.GetTableDBName(),
                        PPartnerTable.GetPartnerKeyDBName(),
                        PUnitTable.GetPartnerKeyDBName(),
                        APartnerKey);
                    DataTable t = db.SelectDT(sql, "UnitInfo", Transaction);

                    if (t.Rows.Count > 0)
                    {
                        PartnerShortName = Convert.ToString(t.Rows[0][0]);
                        CountryCode = Convert.ToString(t.Rows[0][1]);
                    }
                });

            db.CloseDBConnection();

            AUnitName = PartnerShortName;
            ACountryCode = CountryCode;

            return AUnitName != null && ACountryCode != null;
        }
    }
}
