#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
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
using FTOptix.MicroController;
using FTOptix.Retentivity;
using FTOptix.System;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.UI;
using FTOptix.SerialPort;
using FTOptix.Core;
#endregion

public class ODBCLoggerQuery : BaseNetLogic
{
    public override void Start()
    {
        // Enable periodic query when the screen object is loaded at runtime
        LogicObject.GetVariable("EnablePeriodicQuery").Value = true;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void VerifyRecordsCount()
    {
        // Create a store object - this is the ODBC store
        var myStore = Project.Current.Get<Store>("DataStores/ODBCDatabase1");
        object[,] resultSet;
        string[] header;

        // Get the number of times the ODBC logger has been triggered and calculate the expected number of records
        int expectedRecordsCount = LogicObject.GetVariable("LoggerTriggerCount").Value;
        int recordsCount;

        try
        {
            // Execute the query and get the result set
            myStore.Query("SELECT COUNT(*) FROM ODBCLogger", out header, out resultSet);

            // TEMP - TO DELETE
            Log.Info($"expectedRecordsCount: {expectedRecordsCount}");

            // Get the number of records from the result set
            recordsCount = Convert.ToInt32(resultSet[0, 0]);
        }
        catch (Exception ex)
        {
            Log.Error($"Error executing query: {ex.Message}");
            recordsCount = -1;
        }

        // Compare the expected and actual number of records
        if (recordsCount == expectedRecordsCount)
        {
            LogicObject.GetVariable("RecordsCountOK").Value = true;
            Log.Info($"RecordsCountOK. Expected number of records: {expectedRecordsCount}, Actual number of records: {recordsCount}");
        }
        else
        {
            LogicObject.GetVariable("RecordsCountOK").Value = false;
            Log.Info($"ERROR! Expected number of records: {expectedRecordsCount}, Actual number of records: {recordsCount}");
        }

        // Disable periodic query so that the query is only executed once
        LogicObject.GetVariable("EnablePeriodicQuery").Value = false; 
    }

}
