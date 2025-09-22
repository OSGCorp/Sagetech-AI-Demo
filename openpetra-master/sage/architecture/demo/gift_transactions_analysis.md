# Gift.Transactions.cs - Comprehensive Class Exploration

## **Class Overview**

**File**: `csharp/ICT/Petra/Server/lib/MFinance/Gift/Gift.Transactions.cs`  
**Size**: 299.5kb, 6,963 lines  
**Primary Class**: `TGiftTransactionWebConnector`  
**Namespace**: `Ict.Petra.Server.MFinance.Gift.WebConnectors`  

This is the **core server-side connector** for managing gift transactions in OpenPetra's finance module, serving as the primary interface between client applications and the gift transaction database.

## **Architecture & Design Pattern**

### **Web Connector Pattern**
- **Server-side web connector** with REST/SOAP service endpoints
- **Security-enforced methods** with `[RequireModulePermission("FINANCE-1")]` attributes
- **Database transaction management** using `TDBTransaction` and `TDataBase` classes
- **Typed DataSet approach** using `GiftBatchTDS` for strongly-typed data manipulation

### **Key Design Principles**
1. **Comprehensive Validation**: Every method validates arguments extensively
2. **Transaction Safety**: All database operations wrapped in transactions
3. **Exception Handling**: Proper logging and exception propagation
4. **Separation of Concerns**: Clear distinction between data access, business logic, and validation

## **Core Functionality Areas**

### **1. Gift Batch Management**

#### **Batch Creation**
```csharp
[RequireModulePermission("FINANCE-1")]
public static GiftBatchTDS CreateAGiftBatch(Int32 ALedgerNumber, DateTime ADateEffective, string ABatchDescription)
```
- Creates new gift batches with consecutive batch numbers
- Validates ledger existence and permissions
- Automatically assigns batch numbers and default values
- Immediate database persistence

#### **Batch Loading**
```csharp
public static GiftBatchTDS LoadAGiftBatchForYearPeriod(Int32 ALedgerNumber, Int32 AYear, Int32 APeriod, String ABatchStatus, out String ACurrencyCode)
```
- Flexible batch loading with multiple filter options:
  - **Year/Period filtering**: Current, forwarding, or specific periods
  - **Status filtering**: Unposted, Posted, Cancelled
  - **Single batch loading**: Load specific batch by number
- **Calculated totals**: Automatic calculation of batch totals from gift details

#### **Batch Maintenance**
```csharp
public static bool MaintainBatches(string action, Int32 ALedgerNumber, Int32 ABatchNumber, ...)
```
- **CRUD operations**: Create, Edit, Delete batches
- **Status validation**: Prevents modification of posted batches
- **Period validation**: Ensures effective dates match accounting periods

### **2. Recurring Gift Processing**

#### **Recurring Batch Creation**
```csharp
public static GiftBatchTDS CreateARecurringGiftBatch(Int32 ALedgerNumber)
```
- Creates templates for recurring donations
- Supports scheduled gift processing

#### **Recurring Gift Submission**
```csharp
public static bool SubmitRecurringGiftBatch(Int32 ALedgerNumber, Int32 ARecurringBatchNumber, DateTime AEffectiveDate, ...)
```
- **Complex processing logic**:
  - Validates donation periods (StartDonations/EndDonations)
  - Applies exchange rates for multi-currency support
  - Creates actual gift batches from recurring templates
  - Handles tax deductibility calculations

### **3. Gift Transaction Processing**

#### **Transaction Loading**
```csharp
public static bool LoadGiftTransactionsForBatch(Int32 ALedgerNumber, Int32 ABatchNumber, ...)
public static bool LoadGiftTransactionAndDetails(Int32 ALedgerNumber, Int32 ABatchNumber, Int32 AGiftTransactionNumber, ...)
```
- **Granular loading**: Load entire batches, specific transactions, or individual details
- **Status checking**: Returns whether batch is unposted for editing
- **Related data**: Loads associated partner and motivation data

#### **Data Relationships**
The class manages complex relationships between:
- **Gift Batches** → **Gifts** → **Gift Details**
- **Donors** → **Recipients** → **Motivations**
- **Cost Centers** → **Accounting Periods** → **Exchange Rates**

### **4. Posting Engine**

#### **Batch Posting Preparation**
```csharp
public static GiftBatchTDS PrepareGiftBatchForPosting(Int32 ALedgerNumber, Int32 ABatchNumber, ...)
```
- **Pre-posting validation**:
  - Batch status verification
  - Period validation against effective dates
  - Exchange rate validation
  - Hash total verification
  - Cost center validation for recipients

#### **Critical Validation Checks**
1. **Period Validation**: Ensures batch period matches GL effective date
2. **Exchange Rate Validation**: Verifies corporate exchange rates exist
3. **Hash Total Validation**: Compares batch total against hash total
4. **Cost Center Validation**: Verifies recipient cost center assignments
5. **Motivation Validation**: Ensures valid motivation codes

### **5. Multi-Currency Support**

#### **Currency Handling**
- **Base Currency**: Ledger's primary currency
- **Transaction Currency**: Currency of the gift batch
- **International Currency**: For reporting consolidation
- **Exchange Rate Management**: Daily and corporate rates

#### **Currency Calculations**
```csharp
// Example from SubmitRecurringGiftBatch
amtTrans = recGiftDetail.GiftAmount;
amtBase = GLRoutines.Divide((decimal)amtTrans, AExchangeRateToBase);
detail.GiftTransactionAmount = amtTrans;
detail.GiftAmount = amtBase;
detail.GiftAmountIntl = TransactionInIntlCurrency ? amtTrans : GLRoutines.Divide((decimal)amtBase, AExchangeRateIntlToBase);
```

### **6. Recipient & Cost Center Management**

#### **Cost Center Resolution**
```csharp
public static string RetrieveCostCentreCodeForRecipient(Int32 ALedgerNumber, Int64 ARecipientPartnerKey, ...)
```
- **Complex logic** for determining appropriate cost centers:
  1. Partner-specific cost center links
  2. Valid ledger number assignments
  3. Motivation detail defaults
- **International operations**: Supports ILT (Inter-Ledger Transfers)

#### **Recipient Validation**
```csharp
public static bool IsRecipientLedgerNumberSetupForILT(Int32 ALedgerNumber, Int64 ARecipientPartnerKey, ...)
```
- Validates international gift recipients
- Ensures proper cost center setup for cross-ledger operations

### **7. Banking Integration**

#### **Banking Details Management**
```csharp
public static GiftBatchTDS LoadBankingDetailsOfPartner(Int64 APartnerKey)
```
- Loads donor banking information
- Supports SEPA direct debit integration
- IBAN formatting for international standards

#### **SEPA Support**
- Integration with `TSEPAWriterDirectDebit` for European operations
- IBAN validation and formatting
- Direct debit mandate management

### **8. Data Import/Export**

#### **Import Capabilities**
```csharp
public static DataTable GetEsrDefaults()
public static Boolean CommitEsrDefaults(DataTable AEsrDefaults)
```
- **ESR (Swiss payment) import**: Specialized for Swiss banking
- **CSV import**: General gift data import
- **Configuration management**: ESR defaults for import processing

#### **Export Functions**
```csharp
public static bool ExportAllGiftBatchData(...)
```
- Exports gift data for external processing
- Multiple format support
- Progress tracking for large exports

### **9. Donor History & Analysis**

#### **Historical Data Loading**
```csharp
public static GiftBatchTDS LoadDonorRecipientHistory(Hashtable requestParams, ...)
```
- **Complex query system** using SQL templates
- **Flexible filtering**:
  - Donor/Recipient combinations
  - Date ranges
  - Amount thresholds
- **Performance optimization**: Uses temporary tables for large datasets

#### **Donor Analytics**
```csharp
public static GiftBatchTDSAGiftDetailTable LoadDonorLastPostedGift(Int64 ADonorPartnerKey, ...)
public static Boolean DonorHasGiven(Int32 ALedgerNumber, Int64 ADonorPartnerKey)
```
- Last gift analysis for donor relationship management
- Donor giving history verification

## **Technical Implementation Details**

### **Database Access Patterns**

#### **Connection Management**
```csharp
TDataBase db = DBAccess.Connect("MethodName", ADataBase);
// Always includes proper cleanup
if (ADataBase == null) { db.CloseDBConnection(); }
```

#### **Transaction Patterns**
```csharp
db.WriteTransaction(ref Transaction, ref SubmissionOK, delegate {
    // All database operations within transaction
    // Validation and business logic
    // SubmissionOK = true only on success
});
```

### **Error Handling Strategy**

#### **Custom Exceptions**
- `EFinanceSystemInvalidLedgerNumberException`
- `EFinanceSystemInvalidBatchNumberException`
- `EFinanceSystemDataTableReturnedNoDataException`
- `EFinanceSystemDBTransactionNullException`

#### **Validation Patterns**
```csharp
#region Validate Arguments
if (ALedgerNumber <= 0)
{
    throw new EFinanceSystemInvalidLedgerNumberException(
        String.Format(Catalog.GetString("Function:{0} - The Ledger number must be greater than 0!"),
        Utilities.GetMethodName(true)), ALedgerNumber);
}
#endregion Validate Arguments
```

### **Security Implementation**

#### **Permission Requirements**
- **FINANCE-1**: Basic gift operations
- **FINANCE-3**: Administrative functions (ESR defaults)
- **Method-level security**: Each public method protected

#### **Data Validation**
- **Input sanitization**: All parameters validated
- **Business rule enforcement**: Status checks, period validation
- **Authorization checks**: User permissions verified

## **Performance Considerations**

### **Optimization Strategies**

1. **Selective Loading**: Methods load only required data
2. **Batch Processing**: Handles large datasets efficiently
3. **Connection Reuse**: Optional database connection parameter
4. **Caching**: Leverages OpenPetra's caching framework

### **Large Dataset Handling**
```csharp
// Uses pagination and temporary tables for large queries
db.SelectToTempTable(MainDS, sqlStmt, tempTableName, Transaction, parameters.ToArray(), 0, 0);
```

## **Integration Points**

### **Key Dependencies**

1. **General Ledger Integration**: 
   - `TGLTransactionWebConnector` for posting
   - `TFinancialYear` for period validation
   - `GLRoutines` for currency calculations

2. **Partner Management**:
   - `TPartnerServerLookups` for partner validation
   - Partner data access classes

3. **Finance Common**:
   - `TFinanceServerLookupWebConnector` for lookups
   - Exchange rate management
   - Cost center validation

### **Client Communication**
- **RESTful endpoints**: Accessible via HTTP/HTTPS
- **Typed DataSets**: Strong typing for client-server communication
- **Verification Results**: Comprehensive error reporting

## **Business Logic Highlights**

### **Complex Business Rules**

1. **Recurring Gift Processing**: 
   - Date-based activation/deactivation
   - Exchange rate application
   - Tax deductibility calculation

2. **Cost Center Assignment**:
   - Partner-specific overrides
   - Motivation detail defaults
   - International operations support

3. **Multi-Currency Operations**:
   - Base, transaction, and international currencies
   - Exchange rate validation and application
   - Rounding and precision handling

### **Audit & Compliance**
- **Complete audit trails**: All changes logged
- **Financial controls**: Hash totals, period validation
- **Tax compliance**: Tax deductibility calculations
- **International standards**: SEPA, IBAN support

## **Summary**

The `TGiftTransactionWebConnector` class represents a sophisticated, enterprise-grade implementation of gift transaction management. It successfully balances:

- **Functional Completeness**: Comprehensive gift processing capabilities
- **Security & Compliance**: Multi-layered validation and authorization
- **Performance**: Optimized database access and large dataset handling
- **Maintainability**: Clear separation of concerns and consistent patterns
- **International Support**: Multi-currency and cross-border operations

This class serves as an excellent example of how complex financial business logic can be implemented in a maintainable, secure, and performant manner within a distributed application architecture.
