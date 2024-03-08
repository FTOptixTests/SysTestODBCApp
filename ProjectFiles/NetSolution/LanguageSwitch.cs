#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NativeUI;
using FTOptix.NetLogic;
using FTOptix.HMIProject;
using FTOptix.DataLogger;
using FTOptix.UI;
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
using FTOptix.Store;
//using FTOptix.EthernetIP;
using FTOptix.CoreBase;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.RAEtherNetIP;
using FTOptix.Core;
using FTOptix.Report;
using FTOptix.Retentivity;
using FTOptix.OPCUAClient;
using FTOptix.EventLogger;
using FTOptix.System;
using FTOptix.SerialPort;
using FTOptix.MicroController;
using FTOptix.ODBCStore;
#endregion

public class LanguageSwitch : BaseNetLogic
{
    [ExportMethod]
     public void IncreaseVal(string VarName, bool VarStatus )
    {
        var ModelFolder = Project.Current.Get("Model/Variables/Config");
        int NewLang = ModelFolder.GetVariable(VarName).Value; 
         if (NewLang >=1 && NewLang <4 && VarStatus == true)
                {
                    NewLang +=1;
                }
         else 
                {
                    NewLang=1;
                }
        ModelFolder.GetVariable(VarName).Value = NewLang;
       
       
       

        
    }
}
