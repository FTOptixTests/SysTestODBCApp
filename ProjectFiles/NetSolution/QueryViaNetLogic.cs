#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.DataLogger;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.Alarm;
using FTOptix.Recipe;
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.Core;
using FTOptix.ODBCStore;
#endregion

public class QueryViaNetLogic: BaseNetLogic
{   
    // Specify how many records to insert into the database with each test cycle
    int recordsPerInsert = 10;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
    }
    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    private string GetTableName()
    {
        // Get the name of the table based on which test scenario is running
        bool screenChangeWithLogger = LogicObject.GetVariable("ScreenChangeWithLogger").Value;
        string tableName = "";
        if (screenChangeWithLogger)
        {
            tableName = "ODBCLogger";
        }
        else
        {
            tableName = "Table1";
        }
        return tableName;
    }

    private int GetExpectedRecordsCount()
    {
        // Get the expected number of records in the table based on which test scenario is running
        bool screenChangeWithLogger = LogicObject.GetVariable("ScreenChangeWithLogger").Value;
        int expectedRecordsCount = 0;
        if (screenChangeWithLogger)
        {   
            // // Get the number of times the ODBC logger has been triggered and calculate the expected number of records
            int loggerTriggerCount = LogicObject.GetVariable("LoggerTriggerCount").Value;
            expectedRecordsCount = loggerTriggerCount;

            Log.Info($"LoggerTriggerCount: {loggerTriggerCount}, expectedRecordsCount: {expectedRecordsCount}");
        }
        else
        {
            // Get the number of times the insert query has been executed and calculate the expected number of records
            int insertCount = LogicObject.GetVariable("InsertCount").Value;
            expectedRecordsCount = insertCount * recordsPerInsert;

            // TEMP - TO DELETE
            Log.Info($"insertCount: {insertCount}, expectedRecordsCount: {expectedRecordsCount}");
        }
        return expectedRecordsCount;
    }


    [ExportMethod]
    public void DeleteQuery()
    {   
        // Get the name of the table to delete from
        string tableName = GetTableName();

        // Create a store object - this is the ODBC store
        var myStore = Project.Current.Get<Store>("DataStores/ODBCDatabase1");
        object[,] resultSet;
        string[] header;

        // TEMP - TO DELETE
        Log.Info($"Deleting from {tableName}");

        try 
        {
            // Execute a query to delete all rows from the table
            myStore.Query($"DELETE FROM {tableName}", out header, out resultSet);
        }
        catch (Exception e)
        {
            Log.Error($"Error deleting from {tableName}: {e.Message}");
        }
    }

    [ExportMethod]
        public void InsertQuery()
    {   
        // Get the values of the variables
        int var1 = LogicObject.GetVariable("Variable1").Value;
        int var2 = LogicObject.GetVariable("Variable2").Value;
        int var3 = LogicObject.GetVariable("Variable3").Value;
        int var4 = LogicObject.GetVariable("Variable4").Value;
        string var5 = LogicObject.GetVariable("CLX1_Name").Value;
        string var6 = LogicObject.GetVariable("CLX2_Name").Value;
        string var7 = LogicObject.GetVariable("CLX3_Name").Value;
        string var8 = LogicObject.GetVariable("CLX4_Name").Value;

        // Create a store object - this is the ODBC store
        var myStore = Project.Current.Get<Store>("DataStores/ODBCDatabase1");

        // Get the table object from the store
        var tbl = myStore.Tables.Get<Table>("Table1");

        // Create a new row in the table
        object[,] rawValues = new object[1, 9];
        rawValues[0, 0] = DateTime.Now;
        rawValues[0, 1] = var1;
        rawValues[0, 2] = var2;
        rawValues[0, 3] = var3;
        rawValues[0, 4] = var4;
        rawValues[0, 5] = var5;
        rawValues[0, 6] = var6;
        rawValues[0, 7] = var7;
        rawValues[0, 8] = var8;

        // Define the columns to insert into
        string[] columns = new string[9] { "Timestamp", "Variable1", "Variable2", "Variable3", "Variable4", "CLX1_Name", "CLX2_Name", "CLX3_Name", "CLX4_Name"};

        // Insert the values into the table i-times
        for (int i = 0; i < recordsPerInsert; i++)
        {
            tbl.Insert(columns, rawValues);
        }   
    }

    [ExportMethod]
    public int CountRecords()
    {   
        // Create a store object - this is the ODBC store
        var myStore = Project.Current.Get<Store>("DataStores/ODBCDatabase1");
        object[,] resultSet;
        string[] header;

        // Get the name of the table to count from
        string tableName = GetTableName();

        // TEMP - TO DELETE
        Log.Info($"Counting from {tableName}");

        try 
        {
            // Execute a query to count all rows from the table 
            myStore.Query($"SELECT COUNT(*) FROM {tableName}", out header, out resultSet);

            // Get the value from the result set
            int recordsCount = Convert.ToInt32(resultSet[0, 0]);

            // String representation of the number of records - for display purposes
            LogicObject.GetVariable("RecordsCount").Value = resultSet[0, 0].ToString();

            return recordsCount;
        }
        catch (Exception e)
        {
            Log.Error($"Error counting from {tableName}: {e.Message}");
            
            // If there is an error, return null;
            return -1;
        }
    }

    [ExportMethod]
    public void VerifyRecordsCount()
    {   
        // Calculate the expected number of records
        int expectedRecordsCount = GetExpectedRecordsCount();

        // Get the actual number of records in the table
        int numberOfRecords = CountRecords();

        // Compare the expected and actual number of records
        if (numberOfRecords == expectedRecordsCount)
        {
            LogicObject.GetVariable("RecordsCountOK").Value = true;
        }
        else
        {
            LogicObject.GetVariable("RecordsCountOK").Value = false;
        }
    }

}
