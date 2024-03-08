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
using FTOptix.WebUI;
#endregion

public class LogTestStartStop : BaseNetLogic
{
    private void logInfoStartStopTest(ref UAManagedCore.IUAVariable TestStatus, ref UAManagedCore.IUAVariable testRunningInfo, string testName){
        if(TestStatus.Value > 0 && testRunningInfo.Value == false){
            testRunningInfo.Value = true;
            Log.Info($"[System Test] {testName} Test started.");
        } else if(TestStatus.Value == 0 && testRunningInfo.Value == true){
            testRunningInfo.Value = false;
            Log.Info($"[System Test] {testName} Test stopped.");
        }
    }

    public override void Start()
    {
        //ScreenChange Test Start/Stop
        var ScreenChgRunning = Project.Current.GetVariable("Model/Variables/Features/CustomeEventLogger/CLX1ScreenChangeRunning");
        var ScreenChgStatus = Project.Current.GetVariable("CommDrivers/RAEtherNet_IPDriver/CLX1/Tags/Controller Tags/CustomEventLogger/ScreenChangeStatus");
        ScreenChgStatus.VariableChange += (s,e) =>{
            logInfoStartStopTest(ref ScreenChgStatus, ref ScreenChgRunning, "ScreenChange");
        };

        //ScrewToScreen Test Start/Stop
        var ScrwToScrnRunning = Project.Current.GetVariable("Model/Variables/Features/CustomeEventLogger/CLX1ScrewToScreenRunning");
        var ScrwToScrnStatus = Project.Current.GetVariable("CommDrivers/RAEtherNet_IPDriver/CLX1/Tags/Controller Tags/CustomEventLogger/ScrewToScreenStatus");
        ScrwToScrnStatus.VariableChange += (s,e) =>{
            logInfoStartStopTest(ref ScrwToScrnStatus, ref ScrwToScrnRunning, "ScrewToScreen");
        };

        //ScreenToScrew Test Start/Stop
        var ScrnToScrwRunning = Project.Current.GetVariable("Model/Variables/Features/CustomeEventLogger/CLX1ScreenToScrewRunning");
        var ScrnToScrwStatus = Project.Current.GetVariable("CommDrivers/RAEtherNet_IPDriver/CLX1/Tags/Controller Tags/CustomEventLogger/ScreenToScrewStatus");
        ScrnToScrwStatus.VariableChange += (s,e) =>{
            logInfoStartStopTest(ref ScrnToScrwStatus, ref ScrnToScrwRunning, "ScreenToScrew");
        };

        //PowerUp Test Start/Stop
        var PowerUpRunning = Project.Current.GetVariable("Model/Variables/Features/CustomeEventLogger/CLX1PowerUpRunning");
        var PowerUpStatus = Project.Current.GetVariable("CommDrivers/RAEtherNet_IPDriver/CLX1/Tags/Controller Tags/CustomEventLogger/PowerUpStatus");
        PowerUpStatus.VariableChange += (s,e) =>{
            logInfoStartStopTest(ref PowerUpStatus, ref PowerUpRunning, "PowerUp");
        };
    }
}
