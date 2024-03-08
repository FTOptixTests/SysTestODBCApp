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
using FTOptix.EventLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.Report;
using FTOptix.OPCUAClient;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using System.Diagnostics;
using FTOptix.System;
using FTOptix.SerialPort;
using FTOptix.MicroController;
using FTOptix.ODBCStore;
using FTOptix.WebUI;
#endregion

public class MemoryRead : BaseNetLogic
{
    [ExportMethod]
    public void Ram()
    {
         string procesOdczytu = "FTOptixRuntime";
         double ramConsumption=0.0;
         Process.GetProcessesByName(procesOdczytu)[0].Refresh();
         ramConsumption = Convert.ToDouble((Process.GetProcessesByName(procesOdczytu)[0].WorkingSet64) / (double)(1024*1024));
         LogicObject.GetVariable("MemRed").Value = Convert.ToInt32(ramConsumption);
               
    }

   
}
