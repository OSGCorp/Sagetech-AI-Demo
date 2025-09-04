//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       alanb, timop
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
using System.Collections.Specialized;
using System.Data;
using System.IO;

using Ict.Common;
using Ict.Common.Data;
using Ict.Common.DB;

using Ict.Petra.Shared.MCommon;
using Ict.Petra.Shared.MPartner.Mailroom.Data;

using Ict.Petra.Server.App.Core.Security;
using Ict.Petra.Server.MPartner.Mailroom.Data.Access;

namespace Ict.Petra.Server.MCommon.FormTemplates.WebConnectors
{
    /// <summary>
    /// methods related to form templates in the MCommon namespace
    /// </summary>
    public static class TFormTemplatesWebConnector
    {
        #region Upload and Download a Template

        /// <summary>
        /// Uploads a finance form template to the specified data row
        /// </summary>
        /// <param name="AFormCode">Form Code</param>
        /// <param name="AFormName">Form name</param>
        /// <param name="ALanguageCode">language code</param>
        /// <param name="ATemplateText">The template as a base64 string</param>
        /// <param name="AUserID">User ID who is doing the upload</param>
        /// <param name="AUploadDateTime">Local date and time from the user PC that is doing the upload</param>
        /// <param name="ATemplateFileExtension">File extension of template file (eg docx)</param>
        /// <returns>True if the table record was modified successfully</returns>
        [RequireModulePermission("FINANCE-1")]
        public static bool UploadFinanceFormTemplate(String AFormCode, String AFormName, String ALanguageCode, String ATemplateText,
            String AUserID, DateTime AUploadDateTime, String ATemplateFileExtension)
        {
            return UploadFormTemplate(AFormCode, AFormName, ALanguageCode, ATemplateText, AUserID, AUploadDateTime, ATemplateFileExtension);
        }

        /// <summary>
        /// Uploads a partner form template to the specified data row.  The FormCode for this is always PARTNER
        /// </summary>
        /// <param name="AFormName">Form name</param>
        /// <param name="ALanguageCode">language code</param>
        /// <param name="ATemplateText">The template as a base64 string</param>
        /// <param name="AUserID">User ID who is doing the upload</param>
        /// <param name="AUploadDateTime">Local date and time from the user PC that is doing the upload</param>
        /// <param name="ATemplateFileExtension">File extension of template file (eg docx)</param>
        /// <returns>True if the table record was modified successfully</returns>
        [RequireModulePermission("PTNRUSER")]
        public static bool UploadPartnerFormTemplate(String AFormName, String ALanguageCode, String ATemplateText,
            String AUserID, DateTime AUploadDateTime, String ATemplateFileExtension)
        {
            return UploadFormTemplate(MCommonConstants.FORM_CODE_PARTNER,
                AFormName,
                ALanguageCode,
                ATemplateText,
                AUserID,
                AUploadDateTime,
                ATemplateFileExtension);
        }

        /// <summary>
        /// Uploads a personnel form template to the specified data row.  The FormCode for this is always PERSONNEL
        /// </summary>
        /// <param name="AFormName">Form name</param>
        /// <param name="ALanguageCode">language code</param>
        /// <param name="ATemplateText">The template as a base64 string</param>
        /// <param name="AUserID">User ID who is doing the upload</param>
        /// <param name="AUploadDateTime">Local date and time from the user PC that is doing the upload</param>
        /// <param name="ATemplateFileExtension">File extension of template file (eg docx)</param>
        /// <returns>True if the table record was modified successfully</returns>
        [RequireModulePermission("PERSONNEL")]
        public static bool UploadPersonnelFormTemplate(String AFormName, String ALanguageCode, String ATemplateText,
            String AUserID, DateTime AUploadDateTime, String ATemplateFileExtension)
        {
            return UploadFormTemplate(MCommonConstants.FORM_CODE_PERSONNEL,
                AFormName,
                ALanguageCode,
                ATemplateText,
                AUserID,
                AUploadDateTime,
                ATemplateFileExtension);
        }

        /// <summary>
        /// Uploads a conference form template to the specified data row.  The FormCode for this is always CONFERENCE
        /// </summary>
        /// <param name="AFormName">Form name</param>
        /// <param name="ALanguageCode">language code</param>
        /// <param name="ATemplateText">The template as a base64 string</param>
        /// <param name="AUserID">User ID who is doing the upload</param>
        /// <param name="AUploadDateTime">Local date and time from the user PC that is doing the upload</param>
        /// <param name="ATemplateFileExtension">File extension of template file (eg docx)</param>
        /// <returns>True if the table record was modified successfully</returns>
        [RequireModulePermission("CONFERENCE")]
        public static bool UploadConferenceFormTemplate(String AFormName, String ALanguageCode, String ATemplateText,
            String AUserID, DateTime AUploadDateTime, String ATemplateFileExtension)
        {
            return UploadFormTemplate(MCommonConstants.FORM_CODE_CONFERENCE,
                AFormName,
                ALanguageCode,
                ATemplateText,
                AUserID,
                AUploadDateTime,
                ATemplateFileExtension);
        }

        /// <summary>
        /// Main private helper method that uploads a form template to the specified data row
        /// </summary>
        /// <param name="AFormCode">Form Code</param>
        /// <param name="AFormName">Form name</param>
        /// <param name="ALanguageCode">language code</param>
        /// <param name="ATemplateText">The template as a base64 string</param>
        /// <param name="AUserID">User ID who is doing the upload</param>
        /// <param name="AUploadDateTime">Loacal date and time from the user PC that is doing the upload</param>
        /// <param name="ATemplateFileExtension">File extension of template file (eg docx)</param>
        /// <returns>True if the table record was modified successfully</returns>
        private static bool UploadFormTemplate(String AFormCode, String AFormName, String ALanguageCode, String ATemplateText,
            String AUserID, DateTime AUploadDateTime, String ATemplateFileExtension)
        {
            TDBTransaction Transaction = new TDBTransaction();
            bool submitOK = false;

            string strSQL = String.Format("UPDATE PUB_{0} SET ", PFormTable.GetTableDBName());

            strSQL += String.Format("{0}='{1}', ", PFormTable.GetTemplateDocumentDBName(), ATemplateText);
            strSQL += String.Format("{0}='{1}', ", PFormTable.GetTemplateUploadedByUserIdDBName(), AUserID);
            strSQL += String.Format("{0}='{1}', ", PFormTable.GetTemplateUploadDateDBName(), AUploadDateTime.ToString("yyyy-MM-dd"));
            strSQL += String.Format("{0}={1}, ", PFormTable.GetTemplateUploadTimeDBName(),
                Convert.ToInt32((AUploadDateTime - new DateTime(AUploadDateTime.Year, AUploadDateTime.Month, AUploadDateTime.Day)).TotalSeconds));
            strSQL += String.Format("{0}='{1}', ", PFormTable.GetTemplateFileExtensionDBName(), ATemplateFileExtension);
            strSQL += String.Format("{0}='TRUE' ", PFormTable.GetTemplateAvailableDBName());
            strSQL += "WHERE ";
            strSQL += String.Format("{0}='{1}' AND ", PFormTable.GetFormCodeDBName(), AFormCode);
            strSQL += String.Format("{0}='{1}' AND ", PFormTable.GetFormNameDBName(), AFormName);
            strSQL += String.Format("{0}='{1}'", PFormTable.GetFormLanguageDBName(), ALanguageCode);

            DBAccess.WriteTransaction(
                ref Transaction,
                ref submitOK,
                delegate
                {
                    int nRowsAffected = Transaction.DataBaseObj.ExecuteNonQuery(strSQL, Transaction, null, true);
                    submitOK = (nRowsAffected == 1);

                });

            return submitOK;
        }

        /// <summary>
        /// Returns a single row PFormTable that contains the full template text as a base64 string.
        /// The parameters are the three elements of the table primary key.
        /// Use for Finance form downloads
        /// </summary>
        [RequireModulePermission("FINANCE-1")]
        public static PFormTable DownloadFinanceFormTemplate(String AFormCode, String AFormName, String ALanguageCode)
        {
            return DownloadFormTemplate(AFormCode, AFormName, ALanguageCode);
        }

        /// <summary>
        /// Returns a single row PFormTable that contains the full template text as a base64 string.
        /// The parameters are two of the three elements of the table primary key.
        /// Use for Partner form downloads
        /// </summary>
        [RequireModulePermission("PTNRUSER")]
        public static PFormTable DownloadPartnerFormTemplate(String AFormName, String ALanguageCode)
        {
            return DownloadFormTemplate(MCommonConstants.FORM_CODE_PARTNER, AFormName, ALanguageCode);
        }

        /// <summary>
        /// Returns a single row PFormTable that contains the full template text as a base64 string.
        /// The parameters are two of the three elements of the table primary key.
        /// Use for Partner form downloads
        /// </summary>
        [RequireModulePermission("PTNRUSER")]
        public static PFormTable DownloadSystemFormTemplate(String AFormName, String ALanguageCode)
        {
            return DownloadFormTemplate(MCommonConstants.FORM_CODE_SYSTEM, AFormName, ALanguageCode);
        }

        /// <summary>
        /// Returns a single row PFormTable that contains the full template text as a base64 string.
        /// The parameters are two of the three elements of the table primary key.
        /// Use for Personnel form downloads
        /// </summary>
        [RequireModulePermission("PERSONNEL")]
        public static PFormTable DownloadPersonnelFormTemplate(String AFormName, String ALanguageCode)
        {
            return DownloadFormTemplate(MCommonConstants.FORM_CODE_PERSONNEL, AFormName, ALanguageCode);
        }

        /// <summary>
        /// Returns a single row PFormTable that contains the full template text as a base64 string.
        /// The parameters are two of the three elements of the table primary key.
        /// Use for Personnel form downloads
        /// </summary>
        [RequireModulePermission("PERSONNEL")]
        public static PFormTable DownloadConferenceFormTemplate(String AFormName, String ALanguageCode)
        {
            return DownloadFormTemplate(MCommonConstants.FORM_CODE_CONFERENCE, AFormName, ALanguageCode);
        }

        /// <summary>
        /// main private helper method that returns a single row PFormTable that contains the full template text as a base64 string.
        /// The parameters are the three elements of the table primary key.
        /// </summary>
        private static PFormTable DownloadFormTemplate(String AFormCode, String AFormName, String ALanguageCode)
        {
            TDBTransaction Transaction = new TDBTransaction();
            PFormTable ReturnValue = new PFormTable();

            DBAccess.ReadTransaction(
                ref Transaction,
                delegate
                {
                    ReturnValue = PFormAccess.LoadByPrimaryKey(AFormCode, AFormName, ALanguageCode, Transaction);
                });

            return ReturnValue;
        }

        #endregion

        #region Get Forms for specified uses

        /// <summary>
        /// return a table with forms for given finance form code and form type code
        /// </summary>
        /// <param name="AFormCode">Form Code Filter</param>
        /// <param name="AFormTypeCode">Form Type Code Filter, ignore this filter if empty string</param>
        /// <returns>Result Form Table</returns>
        [RequireModulePermission("FINANCE-1")]
        public static PFormTable GetFinanceForms(String AFormCode, String AFormTypeCode)
        {
            return GetForms(AFormCode, AFormTypeCode);
        }

        /// <summary>
        /// return a table with forms for the partner module
        /// </summary>
        /// <returns>Result Form Table</returns>
        [RequireModulePermission("PTNRUSER")]
        public static PFormTable GetPartnerForms()
        {
            return GetForms(MCommonConstants.FORM_CODE_PARTNER, String.Empty);
        }

        /// <summary>
        /// return a table with forms for the partner module
        /// </summary>
        /// <returns>Result Form Table</returns>
        [RequireModulePermission("CONFERENCE")]
        public static PFormTable GetConferenceForms()
        {
            return GetForms(MCommonConstants.FORM_CODE_CONFERENCE, String.Empty);
        }

        /// <summary>
        /// return a table with forms for the personnel module
        /// </summary>
        /// <returns>Result Form Table</returns>
        [RequireModulePermission("PERSONNEL")]
        public static PFormTable GetPersonnelForms()
        {
            return GetForms(MCommonConstants.FORM_CODE_PERSONNEL, String.Empty);
        }

        /// <summary>
        /// Main private method to return a table with forms for partner or finance
        /// </summary>
        /// <param name="AFormCode">Form Code Filter</param>
        /// <param name="AFormTypeCode">Form Type Code Filter, ignore this filter if empty string</param>
        /// <returns>Result Form Table.  Note: only those where the template is 'available' are returned</returns>
        private static PFormTable GetForms(String AFormCode, String AFormTypeCode)
        {
            PFormTable ResultTable = new PFormTable();
            PFormRow TemplateRow = ResultTable.NewRowTyped(false);

            // Check for a valid form code
            if ((AFormCode == MCommonConstants.FORM_CODE_PARTNER)
                || (AFormCode == MCommonConstants.FORM_CODE_PERSONNEL)
                || (AFormCode == MCommonConstants.FORM_CODE_CHEQUE)
                || (AFormCode == MCommonConstants.FORM_CODE_RECEIPT)
                || (AFormCode == MCommonConstants.FORM_CODE_REMITTANCE)
                || (AFormCode == MCommonConstants.FORM_CODE_CONFERENCE))
            {
                TemplateRow.FormCode = AFormCode;
                TemplateRow.TemplateAvailable = true;

                if (AFormTypeCode != "")
                {
                    TemplateRow.FormTypeCode = AFormTypeCode;
                }

                TDBTransaction Transaction = new TDBTransaction();

                DBAccess.ReadTransaction(
                    ref Transaction,
                    delegate
                    {
                        // This method only needs some of the columns - and definitely not the template itself!
                        StringCollection fieldList = new StringCollection();
                        fieldList.Add(PFormTable.GetFormCodeDBName());
                        fieldList.Add(PFormTable.GetFormTypeCodeDBName());
                        fieldList.Add(PFormTable.GetFormLanguageDBName());
                        fieldList.Add(PFormTable.GetFormNameDBName());
                        fieldList.Add(PFormTable.GetFormDescriptionDBName());
                        fieldList.Add(PFormTable.GetTemplateFileExtensionDBName());
                        fieldList.Add(PFormTable.GetAddressLayoutCodeDBName());
                        fieldList.Add(PFormTable.GetFormalityLevelDBName());

                        // Probably don't need these on the client
                        fieldList.Add(PFormTable.GetMinimumAmountDBName());
                        fieldList.Add(PFormTable.GetOptionsDBName());

                        ResultTable = PFormAccess.LoadUsingTemplate(TemplateRow, fieldList, Transaction);
                    });
            }

            return ResultTable;
        }

        #endregion

        #region Swiss Office Mail Sorting

        /// <summary>
        /// Reads the two definition files used by the CH sort algorithm
        /// </summary>
        /// <param name="APostalCodeDefinition">The content of the Post code definition file</param>
        /// <param name="ACountryDefinition">The content of the country definition file</param>
        /// <param name="AFailMessage">A message for the client in case of failure.</param>
        /// <returns>True if successful, otherwise false and a message is contained in AFailMessage</returns>
        [RequireModulePermission("PTNRUSER")]
        public static bool GetCHDefinitionFiles(out String APostalCodeDefinition, out String ACountryDefinition, out String AFailMessage)
        {
            APostalCodeDefinition = string.Empty;
            ACountryDefinition = string.Empty;

            // Load the table data - there is no static call for the 'demodata' folder but we work it out from the bin(30) folder
            string[] binDirItems = TAppSettingsManager.ApplicationDirectory.Split(new char[] { Path.DirectorySeparatorChar },
                StringSplitOptions.RemoveEmptyEntries);
            string binDir = binDirItems[binDirItems.Length - 1];
            string suffix = String.Empty;

            if (binDir.StartsWith("bin") && (binDir != "bin"))
            {
                // Looks like a real install so like bin30.  The folder we will be interested in will be custom30.
                suffix = binDir.Substring(3);
            }

            string pathToCSVFile = Path.Combine(TAppSettingsManager.ApplicationBaseDirectory,
                (suffix.Length == 0) ? "demodata" : "custom" + suffix,
                "formletters",
                "CountryDefinitions.CH.csv");

            if (File.Exists(pathToCSVFile))
            {
                using (StreamReader sr = new StreamReader(pathToCSVFile))
                {
                    ACountryDefinition = sr.ReadToEnd();
                    sr.Close();
                }
            }
            else
            {
                AFailMessage = String.Format(
                    Catalog.GetString(
                        "Could not find the special custom definition file '{0}' on the server.  This is required for sorting the labels."),
                    pathToCSVFile);
                return false;
            }

            pathToCSVFile = Path.Combine(TAppSettingsManager.ApplicationBaseDirectory,
                (suffix.Length == 0) ? "demodata" : "custom" + suffix,
                "formletters",
                "PostalGroupDefinitions.CH.csv");

            if (File.Exists(pathToCSVFile))
            {
                using (StreamReader sr = new StreamReader(pathToCSVFile))
                {
                    APostalCodeDefinition = sr.ReadToEnd();
                    sr.Close();
                }
            }
            else
            {
                AFailMessage = String.Format(
                    Catalog.GetString(
                        "Could not find the special custom definition file '{0}' on the server.  This is required for grouping the labels."),
                    pathToCSVFile);
                return false;
            }

            AFailMessage = string.Empty;
            return true;
        }

        #endregion
    }
}
