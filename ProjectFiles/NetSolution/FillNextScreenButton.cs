#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Core;
using FTOptix.NetLogic;
using FTOptix.RAEtherNetIP;
using FTOptix.CommunicationDriver;
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

public class FillNextScreenButton : BaseNetLogic
{
    [ExportMethod]
    public void FillNextScreenButtonOnAllScreens()
    {
        int clx = 1;
        string screenPrefix = "Screen0";

        for (int i = 0; i < 55; i++)
        {
            string screen = (i == 0) ? "HomeScreen" : $"{screenPrefix}{i:00}";
            Log.Info($"{screen}");

            string sourcePath = $"UI/Screens/{screen}/Layout/CommonScreenContent/NextScreen/Text/StringFormatter1/Source0/DynamicLink";
            string tagLinkPath = $"/Objects/SysTestBaseApp/CommDrivers/RAEtherNet_IPDriver/CLX{clx}/Tags/Program&:Photoeye/NextScreenNumber";

            Project.Current.GetVariable(sourcePath).Value = tagLinkPath;

            sourcePath = $"UI/Screens/{screen}/Layout/CommonScreenContent/NextScreen/MouseDownEventHandler1/MethodsToCall/MethodContainer1/InputArguments/NewPanel/NextScreen1/Source/DynamicLink";
            Project.Current.GetVariable(sourcePath).Value = tagLinkPath;

            clx = (clx >= 4) ? 1 : clx + 1;
        }
    }
}
