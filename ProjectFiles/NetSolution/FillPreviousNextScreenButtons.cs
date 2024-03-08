    #region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.UI;
using FTOptix.RAEtherNetIP;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.System;
using FTOptix.Alarm;
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.DataLogger;
using FTOptix.SerialPort;
using FTOptix.Report;
using FTOptix.ODBCStore;
using FTOptix.WebUI;
using FTOptix.OPCUAClient;
#endregion

public class FillPreviousNextScreenButtons : BaseNetLogic
{
    [ExportMethod]
    public void Method1()
    {
        string sourcePath = "UI/Screens/Screen010/Layout/CommonScreenContent/NavigateBack/MouseClickEventHandler1/MethodsToCall/MethodContainer1/InputArguments/NewPanel";
        string tagValue = "/Objects/SysTestBaseApp/UI/Screens/Screen009";
        
        //Project.Current.GetVariable(sourcePath).Value = tagValue;
        Project.Current.GetVariable(sourcePath).SetDynamicLink(Project.Current.GetVariable(tagValue));
        Project.Current.GetVariable(sourcePath).Value = tagValue;
    }
}
