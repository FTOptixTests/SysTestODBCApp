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
using FTOptix.Store;
using FTOptix.ODBCStore;
using FTOptix.System;
using FTOptix.Alarm;
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
using FTOptix.DataLogger;
using FTOptix.SerialPort;
using FTOptix.Report;
using FTOptix.WebUI;
using FTOptix.OPCUAClient;
#endregion

public class FillScreenChangePhotoeyes : BaseNetLogic
{   
    [ExportMethod]
    public void FillScreenChangePE()
    {   
        string SourcePath = "UI/Screens/Screen006/Layout/Photoeyes/ScreenChangeBar/PE2/PE";
        string TagLinkPath = "/Objects/SysTestBaseApp/CommDrivers/RAEtherNet_IPDriver/CLX2/Tags/Controller Tags/Screen6_PE2";
        var PE_Path = Project.Current.GetVariable(SourcePath);
        var Tag_Path = Project.Current.GetVariable(TagLinkPath);
        
        string screen = "Screen00";
        for (int i = 0; i < 55; i++)
        {
            screen = (i < 10) ? "Screen00" : "Screen0";
            Log.Info($"{screen}{i}");
            for(int PE = 1; PE<5; PE++)
            {   if(i==0)
                {
                    SourcePath = $"UI/Screens/HomeScreen/Layout/Photoeyes/ScreenChangeBar/PE{PE}/PE";
                    TagLinkPath = $"/Objects/SysTestBaseApp/CommDrivers/RAEtherNet_IPDriver/CLX{PE}/Tags/Controller Tags/ScreenHome_PE{PE}";    
                }
                else
                {
                    SourcePath = $"UI/Screens/{screen}{i}/Layout/Photoeyes/ScreenChangeBar/PE{PE}/PE";
                    TagLinkPath = $"/Objects/SysTestBaseApp/CommDrivers/RAEtherNet_IPDriver/CLX{PE}/Tags/Controller Tags/Screen{i}_PE{PE}";
                }
                PE_Path = Project.Current.GetVariable(SourcePath);
                Tag_Path = Project.Current.GetVariable(TagLinkPath);
                PE_Path.SetDynamicLink(Tag_Path);
                Project.Current.GetVariable($"{SourcePath}"+"/DynamicLink").Value = TagLinkPath;
            }
            
        }
    }
}
