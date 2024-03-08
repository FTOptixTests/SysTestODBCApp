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
using System.Threading;
#endregion

public class SelectQueryFromODBC : BaseNetLogic
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
    public void SelectQuery()
    {   
        // Delay between initializing the screen object and executing the query so the proper CLX values are read
        Thread.Sleep(2000);

        // Get the photoeye values from controllers
        int[] PE = {LogicObject.GetVariable("PE1").Value, LogicObject.GetVariable("PE2").Value,
                    LogicObject.GetVariable("PE3").Value, LogicObject.GetVariable("PE4").Value};
        int[] PE_odbc;
        PE_odbc = new int[4] {0, 0, 0, 0};

        // Create a store object - this is the ODBC store
        var myStore = Project.Current.Get<Store>("DataStores/ODBCDatabase1");
        object[,] resultSet;
        string[] header;
        string selectQuery = "";

        // resultSet [i,0] = Timestamp
        // resultSet [i,1] = 0
        // resultSet [i,2] = 1
        // resultSet [i,3] = 123
        // resultSet [i,4] = ++1

        try 
        {
            // Execute a query based on what the photoeye values are
            for (var i = 0; i < PE.Length; i++ )
            {
                if (PE[i] == 0)
                {
                    selectQuery = "SELECT * FROM Table1 WHERE Variable1 = 0 and Variable3 = 123";
                    myStore.Query(selectQuery, out header, out resultSet);
                    PE_odbc[i] = Convert.ToInt32(resultSet[0, 1]);
                }
                else
                {
                    selectQuery = "SELECT * FROM Table1 WHERE Variable2 = 1 and Variable3 = 123";   
                    myStore.Query(selectQuery, out header, out resultSet);
                    PE_odbc[i] = Convert.ToInt32(resultSet[0, 2]);
                }  
            }
            LogicObject.GetVariable("PE1_ODBC").Value = PE_odbc[0];
            LogicObject.GetVariable("PE2_ODBC").Value = PE_odbc[1];
            LogicObject.GetVariable("PE3_ODBC").Value = PE_odbc[2];
            LogicObject.GetVariable("PE4_ODBC").Value = PE_odbc[3];
        }
        catch (Exception e)
        {
            Log.Error($"Error executing query: {e.Message}");
        }
        
        // Disable periodic query so that the query is only executed once
        LogicObject.GetVariable("EnablePeriodicQuery").Value = false;        
    }  
}
