#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.WebUI;
using FTOptix.CoreBase;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.Recipe;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.ODBCStore;
using FTOptix.Report;
using FTOptix.OPCUAClient;
using FTOptix.RAEtherNetIP;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.UI;
using FTOptix.SerialPort;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.Core;
#endregion

public class VerifyODBC : BaseNetLogic
{
    public override void Start()
    {
        // Logic to execute when the screen object is loaded at runtime
        
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }


    [ExportMethod]
    public void VerifyDatabase()
    {   
        // Create a store object - this is the ODBC store
        var myStore = Project.Current.Get<Store>("DataStores/ODBCDatabase1");
        object[,] resultSet;
        string[] header;

        // Expected number of records in the table
        int expectedRecordsCount = 1000;
        int recordsCount = -1;

        // Initialize variables
        bool recordsCountOK = false;
        bool recordsSelectOK = false;

        try
        {
            // Execute the query and get the result set
            myStore.Query("SELECT COUNT(*) FROM odbc_read_big_db", out header, out resultSet);

            // Get the number of records from the result set
            recordsCount = Convert.ToInt32(resultSet[0, 0]);
        }
        catch (Exception e)
        {
            Log.Error($"ERROR: {e.Message}");
            recordsCount = -1;
        }

        // Compare the expected and actual number of records
        if (recordsCount == expectedRecordsCount)
        {
            recordsCountOK = true;
            Log.Info("Records count in the ODBC database verified - OK");
        }
        else
        {
            recordsCountOK = false;
            Log.Error($"ERROR! Expected number of records: {expectedRecordsCount}, Actual number of records: {recordsCount}");
        }
        
        // Get the 1st and the 1000th record from the result set and if that doesn't throw an exception, set the flag to true; if not - set the flag to false
        try
        {
            string selectQuery = "SELECT * FROM odbc_read_big_db";
            myStore.Query(selectQuery, out header, out resultSet);
            Log.Info($"Record {0}: {resultSet[0, 0]}, {resultSet[0, 1]}, {resultSet[0, 2]}...");
            Log.Info($"Record {recordsCount - 1}: {resultSet[recordsCount - 1, 0]}, {resultSet[recordsCount - 1, 1]}, {resultSet[recordsCount - 1, 2]}...");
            recordsSelectOK = true;
            Log.Info("Select records verified - OK");
        }
        catch (Exception e)
        {
            Log.Error($"ERROR: {e.Message}");
            recordsSelectOK = false;
        }

        if (recordsCountOK == true && recordsSelectOK == true)
        {
            LogicObject.GetVariable("SelectOKCountOK").Value = true;
        }
        else
        {
            LogicObject.GetVariable("SelectOKCountOK").Value = false;
        }
    }
}
