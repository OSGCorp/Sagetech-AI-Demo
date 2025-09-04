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
using System.Collections.Generic;
using System.Collections.Specialized;

using Ict.Common;
using Ict.Common.Data;
using Ict.Common.DB;
using Ict.Common.Verification;
using Ict.Petra.Server.MCommon.Data.Cascading;
using Ict.Petra.Shared;
using Ict.Petra.Shared.MPartner;
using Ict.Petra.Shared.MPartner.Mailroom.Data;
using Ict.Petra.Server.MPartner.Mailroom.Data.Access;
using Ict.Petra.Shared.MPartner.Partner.Data;
using Ict.Petra.Server.MPartner.Partner.Data.Access;
using Ict.Petra.Server.MPartner.Common;
using Ict.Petra.Server.MCommon;

namespace Ict.Petra.Server.MPartner.Extracts
{
    /// <summary>
    /// Contains Partner Module Extracts handling Business Objects.
    /// These Business Objects handle the retrieval, verification and saving of data.
    ///
    /// @Comment These Business Objects can be instantiated by other Server Objects
    ///          (usually UIConnectors).
    /// </summary>
    public static class TExtractsHandling
    {
        /// <summary>
        /// Creates a new extract with the given extract name and extract description in the
        /// m_extract_master data table. The Extract Id and the Extract Name must both be
        /// unique in the Petra Database.
        /// </summary>
        /// <param name="AExtractName">Name of the Extract to be created.</param>
        /// <param name="AExtractDescription">Description of the Extract to be created.</param>
        /// <param name="ANewExtractId">Extract Id of the created Extract, or -1 if the
        /// creation of the Extract was not successful.</param>
        /// <param name="AExtractAlreadyExists">True if there is already an extract with
        /// the given name, otherwise false.</param>
        /// <returns>True if the new Extract was created, otherwise false.</returns>
        public static bool CreateNewExtract(String AExtractName,
            String AExtractDescription,
            out Int32 ANewExtractId,
            out Boolean AExtractAlreadyExists)
        {
            TDBTransaction Transaction = new TDBTransaction();
            Boolean ReturnValue = false;
            bool SubmissionOK = false;
            MExtractMasterTable NewExtractMasterDT = null;
            MExtractMasterRow TemplateRow = null;
            Boolean ExtractAlreadyExists = false;
            Int32 NewExtractId = -1;

            TLogging.LogAtLevel(9, "CreateNewExtract called!");

            DBAccess.WriteTransaction(ref Transaction, ref SubmissionOK,
                delegate
                {
                    // Check if there is already an extract with the extract name
                    if (!CheckExtractExists(AExtractName))
                    {
                        // The extract name is unique. So create the new extract...
                        NewExtractMasterDT = new MExtractMasterTable();

                        TemplateRow = (MExtractMasterRow)NewExtractMasterDT.NewRowTyped(true);
                        TemplateRow.ExtractName = AExtractName;
                        TemplateRow.ExtractDesc = AExtractDescription;
                        TemplateRow.ExtractId = -1;   // initialize id negative so sequence can be used

                        NewExtractMasterDT.Rows.Add(TemplateRow);

                        MExtractMasterAccess.SubmitChanges(NewExtractMasterDT, Transaction);

                        // Get the Extract Id
                        TemplateRow = (MExtractMasterRow)NewExtractMasterDT.Rows[0];
                        NewExtractId = TemplateRow.ExtractId;

                        SubmissionOK = true;
                        ReturnValue = true;
                    }
                    else
                    {
                        ExtractAlreadyExists = true;
                        ReturnValue = false;
                    }

                    if (ReturnValue)
                    {
                        SubmissionOK = true;
                    }
                    else
                    {
                        SubmissionOK = false;
                    }
                });

            // Get the Extract Id
//            if (NewExtractMasterDT != null)
//            {
//                TemplateRow = (MExtractMasterRow)NewExtractMasterDT.Rows[0];
//                ANewExtractId = TemplateRow.ExtractId;
//            }
            ANewExtractId = NewExtractId;

            AExtractAlreadyExists = ExtractAlreadyExists;

            return ReturnValue;
        }

        /// <summary>
        /// Deletes an Extract.
        /// </summary>
        /// <param name="AExtractId">ExtractId of the Extract that should get
        /// deleted.</param>
        /// <param name="AExtractNotDeletable">True if the Deletable Flag of the
        /// Extract if false.</param>
        /// <param name="AVerificationResult">Nil if all verifications are OK,
        /// otherwise filled with a TVerificationResult object.</param>
        /// <returns>True if the Extract was deleted, otherwise false.</returns>
        public static bool DeleteExtract(int AExtractId,
            out bool AExtractNotDeletable,
            out TVerificationResult AVerificationResult)
        {
            TDBTransaction Transaction = new TDBTransaction();
            bool SubmissionOK = false;
            MExtractMasterTable ExtractMasterDT;
            Boolean ExtractNotDeletable = false;
            TVerificationResult VerificationResult = null;

            DBAccess.WriteTransaction(ref Transaction, ref SubmissionOK,
                delegate
                {
                    ExtractMasterDT = MExtractMasterAccess.LoadByPrimaryKey(AExtractId,
                        Transaction);

                    if (ExtractMasterDT.Rows.Count == 1)
                    {
                        if (ExtractMasterDT[0].Deletable)
                        {
                            MExtractMasterCascading.DeleteByPrimaryKey(AExtractId,
                                Transaction, true);
                            SubmissionOK = true;
                        }
                        else
                        {
                            ExtractNotDeletable = true;
                            SubmissionOK = false;
                        }
                    }
                    else
                    {
                        VerificationResult = new TVerificationResult(
                            "TExtractsHandling.DeleteExtract", "Extract with Extract Id " + AExtractId.ToString() +
                            "doesn't exist!", TResultSeverity.Resv_Critical);
                        SubmissionOK = false;
                    }
                });

            AVerificationResult = VerificationResult;
            AExtractNotDeletable = ExtractNotDeletable;

            return SubmissionOK;
        }

        /// <summary>
        /// Checks whether an Extract with a certain Extract Name exists.
        /// </summary>
        /// <param name="AExtractName">Name of the Extract to check for.</param>
        /// <returns>True if an Extract with the specified Extract Name exists,
        /// otherwise false.</returns>
        public static bool CheckExtractExists(string AExtractName)
        {
            TDBTransaction Transaction = new TDBTransaction();
            Boolean ReturnValue = false;
            MExtractMasterRow TemplateRow;

            DBAccess.ReadTransaction(
                ref Transaction,
                delegate
                {
                    // Check if there is already an extract with the extract name
                    TemplateRow = new MExtractMasterTable().NewRowTyped(false);
                    TemplateRow.ExtractName = AExtractName;

                    if (MExtractMasterAccess.CountUsingTemplate(TemplateRow, null, Transaction) > 0)
                    {
                        ReturnValue = true;
                    }
                });

            return ReturnValue;
        }

        /// <summary>
        /// Checks whether an Extract with a certain Extract Id exists.
        /// </summary>
        /// <param name="AExtractId"></param>
        ///  <returns>True if an Extract with the specified Extract Id exists,
        /// otherwise false.</returns>
        public static bool CheckExtractExists(int AExtractId)
        {
            if (GetExtractKeyCount(AExtractId) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the Key Count of an Extract.
        /// </summary>
        /// <param name="AExtractId">ExtractId of the Extract that the KeyCount should
        /// get returned for.</param>
        /// <returns>The Key Count of the Extract, or -1 if the Extract with the given
        /// Extract Id doesn't exist.</returns>
        public static Int32 GetExtractKeyCount(int AExtractId)
        {
            TDBTransaction Transaction = new TDBTransaction();
            Int32 KeyCount = 0;

            DBAccess.ReadTransaction(
                ref Transaction,
                delegate
                {
                    MExtractMasterTable ExtractDT = MExtractMasterAccess.LoadByPrimaryKey(AExtractId, Transaction);

                    if (ExtractDT.Rows.Count == 1)
                    {
                        KeyCount = ExtractDT[0].KeyCount;
                    }
                    else
                    {
                        KeyCount = -1;
                    }
                });

            return KeyCount;
        }

        /// <summary>
        /// Updates the KeyCount on an Extract.
        /// </summary>
        /// <param name="AExtractId">ExtractId of the Extract that the KeyCount should
        /// get updated for.</param>
        /// <param name="ACount">New Key Count.</param>
        /// <param name="AVerificationResult">Contains errors or DB call exceptions, if there are any.</param>
        /// <returns>True if the updating was successful, otherwise false.</returns>
        public static bool UpdateExtractKeyCount(int AExtractId, int ACount,
            out TVerificationResultCollection AVerificationResult)
        {
            TDBTransaction Transaction = new TDBTransaction();
            bool SubmissionOK = false;
            TVerificationResultCollection VerificationResult = new TVerificationResultCollection();

            DBAccess.WriteTransaction(ref Transaction, ref SubmissionOK,
                delegate
                {
                    MExtractMasterTable ExtractMasterDT = MExtractMasterAccess.LoadByPrimaryKey(AExtractId,
                        Transaction);

                    if (ExtractMasterDT.Rows.Count == 1)
                    {
                        ExtractMasterDT[0].KeyCount = ACount;

                        MExtractMasterAccess.SubmitChanges(ExtractMasterDT, Transaction);
                        SubmissionOK = true;
                    }
                    else
                    {
                        VerificationResult.Add(new TVerificationResult(
                                "TExtractsHandling.UpdateExtractCount", "Extract with Extract Id " + AExtractId.ToString() +
                                " doesn't exist!", TResultSeverity.Resv_Critical));
                    }
                });

            AVerificationResult = VerificationResult;

            return SubmissionOK;
        }

        /// <summary>
        /// Adds a Partner to an Extract, if they are not already present
        /// </summary>
        /// <param name="APartnerKey">PartnerKey of Partner</param>
        /// <param name="AExtractId">ExtractId of the Extract that the Partner should
        /// get added to.</param>
        /// <returns>True if the Partner was added to the Extract, otherwise false.</returns>
        public static bool AddPartnerToExtract(Int64 APartnerKey,
            int AExtractId)
        {
            TLocationPK BestAddressPK;

            BestAddressPK = TMailing.GetPartnersBestLocation(APartnerKey);
//          TLogging.LogAtLevel(8, "TExtractsHandling.AddPartnerToExtract:  BestAddressPK: " + BestAddressPK.SiteKey.ToString() + ", " + BestAddressPK.LocationKey.ToString());

            if (BestAddressPK.LocationKey != -1)
            {
                return AddPartnerToExtract(APartnerKey, BestAddressPK,
                    AExtractId);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a Partner to an Extract, if they are not already present
        /// </summary>
        /// <param name="APartnerKey">PartnerKey of Partner</param>
        /// <param name="ALocationPK">Location PK of a Partner's PartnerLocation
        /// (usually the LocationPK of the 'Best Address' of the Partner).</param>
        /// <param name="AExtractId">ExtractId of the Extract that the Partner should
        /// get added to.</param>
        /// <returns>True if the Partner was added to the Extract, otherwise false.</returns>
        public static bool AddPartnerToExtract(Int64 APartnerKey,
            TLocationPK ALocationPK, int AExtractId)
        {
            TDBTransaction Transaction = new TDBTransaction();
            bool SubmissionOK = false;
            MExtractTable TemplateTable;
            MExtractRow TemplateRow;
            MExtractRow NewRow;

            if (APartnerKey > 0)
            {
                DBAccess.WriteTransaction(ref Transaction, ref SubmissionOK,
                    delegate
                    {
                        /*
                         * First check whether the Partner isn't already in that Extract
                         */
                        TemplateTable = new MExtractTable();
                        TemplateRow = TemplateTable.NewRowTyped(false);
                        TemplateRow.ExtractId = AExtractId;
                        TemplateRow.PartnerKey = APartnerKey;

                        if (MExtractAccess.CountUsingTemplate(TemplateRow, null, Transaction) == 0)
                        {
                            /*
                             * Add Partner to Extract.
                             */
                            NewRow = TemplateTable.NewRowTyped(false);
                            NewRow.ExtractId = AExtractId;
                            NewRow.PartnerKey = APartnerKey;
                            NewRow.SiteKey = ALocationPK.SiteKey;
                            NewRow.LocationKey = ALocationPK.LocationKey;
                            TemplateTable.Rows.Add(NewRow);

                            MExtractAccess.SubmitChanges(TemplateTable, Transaction);

                            SubmissionOK = true;
                        }
                        else
                        {
                            // Partner is already in that Extract -> Partner does not get added.
                            SubmissionOK = false;
                        }
                    });
            }
            else
            {
                // Invalid PartnerKey -> return false;
                SubmissionOK = false;
            }

            return SubmissionOK;
        }

        /// <summary>
        /// create an extract from a list of best addresses
        /// </summary>
        /// <param name="AExtractName">Name of the Extract to be created.</param>
        /// <param name="AExtractDescription">Description of the Extract to be created.</param>
        /// <param name="ANewExtractId">Extract Id of the created Extract, or -1 if the
        /// creation of the Extract was not successful.</param>
        /// <param name="APartnerKeysTable"></param>
        /// <param name="APartnerKeyColumn">number of the column that contains the partner keys</param>
        /// <param name="AAddressFilterAdded">true if location key fields exist in APartnerKeysTable</param>
        /// <param name="AKeyCount">The number of keys that were actually added to the extract (any duplicates are excluded)</param>
        /// <param name="AIgnoredKeyList">A reference to a List of long.  The server will fill this with the partner keys that were ignored.  Does not include duplicates.</param>
        /// <param name="AIgnoreDuplicates"></param>
        /// <param name="AIgnoreInactive">true if inactive partners should be ignored</param>
        /// <param name="AIgnoreNonMailingLocations">true to ignore if the partner's best address is a non-mailing location</param>
        /// <param name="AIgnoreNoSolicitations">true to ignore partners where the No Solicitations flag is set</param>
        /// <returns>True if the new Extract was created, otherwise false.</returns>
        public static bool CreateExtractFromListOfPartnerKeys(
            String AExtractName,
            String AExtractDescription,
            out Int32 ANewExtractId,
            DataTable APartnerKeysTable,
            Int32 APartnerKeyColumn,
            bool AAddressFilterAdded,
            out Int32 AKeyCount,
            out List <long>AIgnoredKeyList,
            bool AIgnoreDuplicates = true,
            bool AIgnoreInactive = false,
            bool AIgnoreNonMailingLocations = false,
            bool AIgnoreNoSolicitations = false)
        {
            if (AAddressFilterAdded)
            {
                // if address filter was added then site key is in third and location in fourth column
                return CreateExtractFromListOfPartnerKeys(AExtractName,
                    AExtractDescription,
                    out ANewExtractId,
                    APartnerKeysTable,
                    APartnerKeyColumn,
                    2,
                    3,
                    out AKeyCount,
                    out AIgnoredKeyList,
                    AIgnoreDuplicates,
                    AIgnoreInactive,
                    AIgnoreNonMailingLocations,
                    AIgnoreNoSolicitations);
            }
            else
            {
                // if no address filter was added (no location keys were added): set location and site key to -1
                return CreateExtractFromListOfPartnerKeys(AExtractName,
                    AExtractDescription,
                    out ANewExtractId,
                    APartnerKeysTable,
                    APartnerKeyColumn,
                    -1,
                    -1,
                    out AKeyCount,
                    out AIgnoredKeyList,
                    AIgnoreDuplicates,
                    AIgnoreInactive,
                    AIgnoreNonMailingLocations,
                    AIgnoreNoSolicitations);
            }
        }

        /// <summary>
        /// create an extract from a list of best addresses
        /// </summary>
        /// <param name="AExtractName">Name of the Extract to be created.</param>
        /// <param name="AExtractDescription">Description of the Extract to be created.</param>
        /// <param name="ANewExtractId">Extract Id of the created Extract, or -1 if the
        /// creation of the Extract was not successful.</param>
        /// <param name="APartnerKeysTable"></param>
        /// <param name="APartnerKeyColumn">number of the column that contains the partner keys</param>
        /// <param name="ASiteKeyColumn">number of the column that contains the site keys</param>
        /// <param name="ALocationKeyColumn">number of the column that contains the location keys</param>
        /// <param name="AKeyCount">The number of keys that were actually added to the extract (any duplicates are excluded)</param>
        /// <param name="AIgnoredKeyList">A reference to a List of long.  The server will fill this with the partner keys that were ignored.  Does not include duplicates.</param>
        /// <param name="AIgnoreDuplicates"></param>
        /// <param name="AIgnoreInactive">true if inactive partners should be ignored</param>
        /// <param name="AIgnoreNonMailingLocations">true to ignore if the partner's best address is a non-mailing location</param>
        /// <param name="AIgnoreNoSolicitations">true to ignore partners where the No Solicitations flag is set</param>
        /// <returns>True if the new Extract was created, otherwise false.</returns>
        public static bool CreateExtractFromListOfPartnerKeys(
            String AExtractName,
            String AExtractDescription,
            out Int32 ANewExtractId,
            DataTable APartnerKeysTable,
            Int32 APartnerKeyColumn,
            Int32 ASiteKeyColumn,
            Int32 ALocationKeyColumn,
            out Int32 AKeyCount,
            out List <long>AIgnoredKeyList,
            bool AIgnoreDuplicates = true,
            bool AIgnoreInactive = false,
            bool AIgnoreNonMailingLocations = false,
            bool AIgnoreNoSolicitations = true)
        {
            bool ReturnValue = false;
            bool ExtractAlreadyExists;

            ANewExtractId = -1;

            // create new extract master record
            ReturnValue = CreateNewExtract(AExtractName,
                AExtractDescription,
                out ANewExtractId,
                out ExtractAlreadyExists);

            if (ReturnValue)
            {
                ExtendExtractFromListOfPartnerKeys(ANewExtractId,
                    APartnerKeysTable,
                    APartnerKeyColumn,
                    ASiteKeyColumn,
                    ALocationKeyColumn,
                    out AKeyCount,
                    out AIgnoredKeyList,
                    AIgnoreDuplicates,
                    AIgnoreInactive,
                    AIgnoreNonMailingLocations,
                    AIgnoreNoSolicitations);
            }
            else
            {
                AKeyCount = 0;
                AIgnoredKeyList = new List <long>();
            }

            return ReturnValue;
        }

        /// <summary>
        /// create an extract from a list of best addresses
        /// </summary>
        /// <param name="AExtractId">Extract Id of the Extract to extend</param>
        /// <param name="APartnerKeysTable"></param>
        /// <param name="APartnerKeyColumn">number of the column that contains the partner keys</param>
        /// <param name="AAddressFilterAdded">true if location key fields exist in APartnerKeysTable</param>
        /// <param name="AKeyCount">The number of keys that were actually added to the extract (any duplicates are excluded)</param>
        /// <param name="AIgnoredKeyList">A reference to a List of long.  The server will fill this with the partner keys that were ignored.  Does not include duplicates.</param>
        /// <param name="AIgnoreDuplicates">true if duplicates should be looked out for. Can be set to false if called only once and not several times per extract.</param>
        /// <param name="AIgnoreInactive">true if inactive partners should be ignored</param>
        /// <param name="AIgnoreNonMailingLocations">true to ignore if the partner's best address is a non-mailing location</param>
        /// <param name="AIgnoreNoSolicitations">true to ignore partners where the No Solicitations flag is set</param>
        public static void ExtendExtractFromListOfPartnerKeys(
            Int32 AExtractId,
            DataTable APartnerKeysTable,
            Int32 APartnerKeyColumn,
            bool AAddressFilterAdded,
            out Int32 AKeyCount,
            out List <long>AIgnoredKeyList,
            bool AIgnoreDuplicates,
            bool AIgnoreInactive = false,
            bool AIgnoreNonMailingLocations = false,
            bool AIgnoreNoSolicitations = true)
        {
            if (AAddressFilterAdded)
            {
                // if address filter was added then site key is in third and location in fourth column
                ExtendExtractFromListOfPartnerKeys(AExtractId, APartnerKeysTable, APartnerKeyColumn, 2, 3, out AKeyCount, out AIgnoredKeyList,
                    AIgnoreDuplicates, AIgnoreInactive, AIgnoreNonMailingLocations, AIgnoreNoSolicitations);
            }
            else
            {
                // if no address filter was added (no location keys were added): set location and site key to -1
                ExtendExtractFromListOfPartnerKeys(AExtractId, APartnerKeysTable, APartnerKeyColumn, -1, -1, out AKeyCount, out AIgnoredKeyList,
                    AIgnoreDuplicates, AIgnoreInactive, AIgnoreNonMailingLocations, AIgnoreNoSolicitations);
            }
        }

        /// <summary>
        /// extend an extract from a list of best addresses
        /// </summary>
        /// <param name="AExtractId">Extract Id of the Extract to extend</param>
        /// <param name="APartnerKeysTable"></param>
        /// <param name="APartnerKeyColumn">number of the column that contains the partner keys</param>
        /// <param name="ASiteKeyColumn">number of the column that contains the site keys</param>
        /// <param name="ALocationKeyColumn">number of the column that contains the location keys</param>
        /// <param name="AKeyCount">The number of keys that were actually added to the extract (any duplicates are excluded)</param>
        /// <param name="AIgnoredKeyList">A reference to a List of long.  If not null the server will fill it with the partner keys that were ignored.  Does not include duplicates.</param>
        /// <param name="AIgnoreDuplicates">true if duplicates should be looked out for. Can be set to false if called only once and not several times per extract.</param>
        /// <param name="AIgnoreInactive">true if inactive partners should be ignored</param>
        /// <param name="AIgnoreNonMailingLocations">true to ignore if the partner's best address is a non-mailing location</param>
        /// <param name="AIgnoreNoSolicitations">true to ignore partners where the No Solicitations flag is set</param>
        public static void ExtendExtractFromListOfPartnerKeys(
            Int32 AExtractId,
            DataTable APartnerKeysTable,
            Int32 APartnerKeyColumn,
            Int32 ASiteKeyColumn,
            Int32 ALocationKeyColumn,
            out Int32 AKeyCount,
            out List <long>AIgnoredKeyList,
            bool AIgnoreDuplicates,
            bool AIgnoreInactive,
            bool AIgnoreNonMailingLocations,
            bool AIgnoreNoSolicitations)
        {
            int RecordCounter = 0;
            PPartnerLocationTable PartnerLocationKeysTable;
            Int64 PartnerKey;

            List <long>ignoredKeyList = new List <long>();

            TDBTransaction Transaction = new TDBTransaction();
            bool SubmissionOK = true;

            DBAccess.WriteTransaction(ref Transaction, ref SubmissionOK,
                delegate
                {
                    // Pre-process the table to remove partner rows that do not match the filter requirements
                    for (int i = APartnerKeysTable.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = APartnerKeysTable.Rows[i];
                        Int64 partnerKey = Convert.ToInt64(dr[APartnerKeyColumn]);

                        // Get a partner record containing our fields of interest
                        StringCollection fields = new StringCollection();
                        fields.Add(PPartnerTable.GetStatusCodeDBName());
                        fields.Add(PPartnerTable.GetNoSolicitationsDBName());
                        DataTable dt = PPartnerAccess.LoadByPrimaryKey(partnerKey, fields, Transaction);

                        if (dt.Rows.Count > 0)
                        {
                            if (AIgnoreInactive || AIgnoreNoSolicitations)
                            {
                                bool isActive = false;
                                bool isNoSolicitation = false;
                                object o = dt.Rows[0][PPartnerTable.GetStatusCodeDBName()];

                                if (o != null)
                                {
                                    TStdPartnerStatusCode statusCode = SharedTypes.StdPartnerStatusCodeStringToEnum(o.ToString());
                                    isActive = (statusCode == TStdPartnerStatusCode.spscACTIVE);
                                }

                                o = dt.Rows[0][PPartnerTable.GetNoSolicitationsDBName()];

                                if (o != null)
                                {
                                    isNoSolicitation = Convert.ToBoolean(o);
                                }

                                if ((AIgnoreInactive && !isActive) || (AIgnoreNoSolicitations && isNoSolicitation))
                                {
                                    ignoredKeyList.Add(partnerKey);
                                    APartnerKeysTable.Rows.Remove(dr);
                                }
                            }
                        }
                        else
                        {
                            ignoredKeyList.Add(partnerKey);
                        }
                    }

                    MExtractTable ExtractTable = new MExtractTable();
                    ExtractTable = MExtractAccess.LoadViaMExtractMaster(AExtractId, Transaction);

                    // Location Keys need to be determined as extracts do not only need partner keys but
                    // also Location Keys.
                    DetermineBestLocationKeys(APartnerKeysTable, APartnerKeyColumn, ASiteKeyColumn,
                        ALocationKeyColumn, out PartnerLocationKeysTable,
                        Transaction);

                    // use the returned table which contains partner and location keys to build the extract
                    foreach (PPartnerLocationRow PartnerLocationRow in PartnerLocationKeysTable.Rows)
                    {
                        PartnerKey = PartnerLocationRow.PartnerKey;

                        if (PartnerKey > 0)
                        {
                            if (AIgnoreNonMailingLocations)
                            {
                                // The PartnerLocationRow only contains the PK fields so now we need to get the SendMail column
                                StringCollection fields = new StringCollection();
                                fields.Add(PPartnerLocationTable.GetSendMailDBName());

                                PPartnerLocationTable t =
                                    PPartnerLocationAccess.LoadByPrimaryKey(PartnerKey, PartnerLocationRow.SiteKey,
                                        PartnerLocationRow.LocationKey, fields, Transaction);

                                if ((t != null) && (t.Rows.Count > 0) && (((PPartnerLocationRow)t.Rows[0]).SendMail == false))
                                {
                                    ignoredKeyList.Add(PartnerKey);
                                    continue;
                                }
                            }

                            RecordCounter += 1;
                            TLogging.LogAtLevel(1, "Preparing Partner " + PartnerKey + " (Record Number " + RecordCounter + ")");

                            // add row for partner to extract and fill with contents
                            MExtractRow NewRow = ExtractTable.NewRowTyped();
                            NewRow.ExtractId = AExtractId;
                            NewRow.PartnerKey = PartnerKey;
                            NewRow.SiteKey = Convert.ToInt64(PartnerLocationRow[PPartnerLocationTable.GetSiteKeyDBName()]);
                            NewRow.LocationKey = Convert.ToInt32(PartnerLocationRow[PPartnerLocationTable.GetLocationKeyDBName()]);

                            // only add row if it does not already exist for this partner
                            if (AIgnoreDuplicates || !ExtractTable.Rows.Contains(new object[] { NewRow.ExtractId, NewRow.PartnerKey, NewRow.SiteKey }))
                            {
                                ExtractTable.Rows.Add(NewRow);
                            }
                        }
                    }

                    if (ExtractTable.Rows.Count > 0)
                    {
                        // update field in extract master for quick access to number of partners in extract
                        MExtractMasterTable ExtractMaster = MExtractMasterAccess.LoadByPrimaryKey(AExtractId, Transaction);
                        ExtractMaster[0].KeyCount = ExtractTable.Rows.Count;

                        ExtractTable.ThrowAwayAfterSubmitChanges = true; // no need to keep data as this increases speed significantly

                        MExtractAccess.SubmitChanges(ExtractTable, Transaction);

                        MExtractMasterAccess.SubmitChanges(ExtractMaster, Transaction);
                    }
                });

            AKeyCount = RecordCounter;
            AIgnoredKeyList = ignoredKeyList;
        }

        /// <summary>
        /// Determine location keys for partners needed for extract, depending on if location information
        /// was retrieved by query or not.
        /// </summary>
        /// <param name="APartnerKeysTable"></param>
        /// <param name="APartnerKeyColumn"></param>
        /// <param name="ASiteKeyColumn"></param>
        /// <param name="ALocationKeyColumn"></param>
        /// <param name="APartnerLocationKeysTable"></param>
        /// <param name="ATransaction"></param>
        private static void DetermineBestLocationKeys(
            DataTable APartnerKeysTable,
            Int32 APartnerKeyColumn,
            Int32 ASiteKeyColumn,
            Int32 ALocationKeyColumn,
            out PPartnerLocationTable APartnerLocationKeysTable,
            TDBTransaction ATransaction)
        {
            Int64 PartnerKey;
            Int64 PreviousPartnerKey;
            Int32 NumberOfPartnerRows = APartnerKeysTable.Rows.Count;
            DataRow partnerRow;

            List <TLocationPK>LocationKeyList = new List <TLocationPK>();

            APartnerLocationKeysTable = new PPartnerLocationTable();

            // don't go further if table is empty
            if (APartnerKeysTable.Rows.Count == 0)
            {
                return;
            }

            // If location column exists then check if there is more than one location key for a partner.
            // If so then determine the best of the found addresses.
            if (ALocationKeyColumn >= 0)
            {
                PreviousPartnerKey = -1;

                // Rows are sorted by partner key. Create a list of location keys per partner and determine
                // the best of those addresses.
                for (int ii = NumberOfPartnerRows - 1; ii >= 0; ii--)
                {
                    partnerRow = APartnerKeysTable.Rows[ii];

                    PartnerKey = Convert.ToInt64(partnerRow[APartnerKeyColumn]);

                    if ((PartnerKey != PreviousPartnerKey)
                        && (PreviousPartnerKey != -1))
                    {
                        if (!DetermineAndAddBestLocationKey(PreviousPartnerKey, LocationKeyList,
                                ref APartnerLocationKeysTable, ATransaction))
                        {
                            // if no address could be found then remove this partner
                            APartnerKeysTable.Rows[ii + 1].Delete();
                        }
                        else
                        {
                            // add first location for next partner
                            LocationKeyList.Clear();
                            LocationKeyList.Add(new TLocationPK(Convert.ToInt64(partnerRow[ASiteKeyColumn]),
                                    Convert.ToInt32(partnerRow[ALocationKeyColumn])));
                        }
                    }
                    else
                    {
                        // add location for this partner
                        LocationKeyList.Add(new TLocationPK(Convert.ToInt64(partnerRow[ASiteKeyColumn]),
                                Convert.ToInt32(partnerRow[ALocationKeyColumn])));
                    }

                    // prepare for next round of loop
                    PreviousPartnerKey = PartnerKey;
                }

                // process last partner key after loop through all records
                if (!DetermineAndAddBestLocationKey(PreviousPartnerKey, LocationKeyList,
                        ref APartnerLocationKeysTable, ATransaction))
                {
                    // if no address could be found then remove this partner
                    APartnerKeysTable.Rows[0].Delete();
                }
            }
            else
            {
                // If no location information was retrieved with earlier query then find best address
                // for partner.
                for (int ii = NumberOfPartnerRows - 1; ii >= 0; ii--)
                {
                    partnerRow = APartnerKeysTable.Rows[ii];
                    PartnerKey = Convert.ToInt64(partnerRow[APartnerKeyColumn]);

                    if (!DetermineAndAddBestLocationKey(PartnerKey, LocationKeyList,
                            ref APartnerLocationKeysTable, ATransaction))
                    {
                        // if no address could be found then remove this partner
                        partnerRow.Delete();
                    }
                }
            }
        }

        /// <summary>
        /// Determine best location for partner out of a list of possible locations. Or simply find best one
        /// if no suggestion is made.
        /// </summary>
        /// <param name="APartnerKey"></param>
        /// <param name="ALocationKeyList"></param>
        /// <param name="APartnerLocationKeysTable"></param>
        /// <param name="ATransaction"></param>
        /// <returns>True if the address was found and added, otherwise false.</returns>
        private static Boolean DetermineAndAddBestLocationKey(
            Int64 APartnerKey,
            List <TLocationPK>ALocationKeyList,
            ref PPartnerLocationTable APartnerLocationKeysTable,
            TDBTransaction ATransaction)
        {
            PPartnerLocationTable AllPartnerLocationTable;
            PPartnerLocationTable FilteredPartnerLocationTable = new PPartnerLocationTable();
            TLocationPK LocationPK = new TLocationPK();
            PPartnerLocationRow PartnerLocationKeyRow;
            PPartnerLocationRow PartnerLocationRowCopy;
            TLocationPK BestLocationPK;

            if (ALocationKeyList.Count == 0)
            {
                // no list suggested: find best address in db for this partner
                BestLocationPK = TMailing.GetPartnersBestLocation(APartnerKey);
            }
            else if (ALocationKeyList.Count == 1)
            {
                // only one location suggested: take this one
                BestLocationPK = ALocationKeyList[0];
            }
            else
            {
                // Process location key list related to partner.
                // In order to use Calculations.DetermineBestAddress we need to first retrieve full data
                // for all suggested records from the db. Therefore load all locations for this partner
                // and then create a table of the ones that are suggested.
                AllPartnerLocationTable = PPartnerLocationAccess.LoadViaPPartner(APartnerKey, ATransaction);

                foreach (PPartnerLocationRow PartnerLocationRow in AllPartnerLocationTable.Rows)
                {
                    LocationPK.SiteKey = PartnerLocationRow.SiteKey;
                    LocationPK.LocationKey = PartnerLocationRow.LocationKey;

                    if (ALocationKeyList.Contains(LocationPK))
                    {
                        PartnerLocationRowCopy = (PPartnerLocationRow)FilteredPartnerLocationTable.NewRow();
                        DataUtilities.CopyAllColumnValues(PartnerLocationRow, PartnerLocationRowCopy);
                        FilteredPartnerLocationTable.Rows.Add(PartnerLocationRowCopy);
                    }
                }

                BestLocationPK = Calculations.DetermineBestAddress(FilteredPartnerLocationTable);
            }

            // create new row, initialize it and add it to the table
            if (BestLocationPK.LocationKey != -1)
            {
                PartnerLocationKeyRow = (PPartnerLocationRow)APartnerLocationKeysTable.NewRow();
                PartnerLocationKeyRow[PPartnerLocationTable.GetPartnerKeyDBName()] = APartnerKey;
                PartnerLocationKeyRow[PPartnerLocationTable.GetSiteKeyDBName()] = BestLocationPK.SiteKey;
                PartnerLocationKeyRow[PPartnerLocationTable.GetLocationKeyDBName()] = BestLocationPK.LocationKey;

                // only add row if it does not already exist
                if (!APartnerLocationKeysTable.Rows.Contains(
                        new object[] { PartnerLocationKeyRow.PartnerKey, PartnerLocationKeyRow.SiteKey, PartnerLocationKeyRow.LocationKey }))
                {
                    APartnerLocationKeysTable.Rows.Add(PartnerLocationKeyRow);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
