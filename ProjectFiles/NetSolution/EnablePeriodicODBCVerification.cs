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

public class EnablePeriodicODBCVerification : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        bool periodicQueryEnabled = LogicObject.GetVariable("PeriodicQueryToggled").Value;

        if (periodicQueryEnabled == true)
        {
            LogicObject.GetVariable("StartPeriodicODBCVerification").Value = true;
        }
        else
        {
            LogicObject.GetVariable("StartPeriodicODBCVerification").Value = false;
        }
        
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }
}
