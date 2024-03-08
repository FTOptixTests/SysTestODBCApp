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
//using FTOptix.UI;
using FTOptix.SerialPort;
using FTOptix.Core;
using System.Collections.Generic;
using FTOptix.WebUI;
#endregion

public class SignPEintoDisplayTableinScrewToScreen : BaseNetLogic
{
    //SysTestBaseApp/UI/Screens/Screen004_Read_DINT
    //SysTestBaseApp/Model/Variables/ScrewToScreen/Int/IntPE1SumOpacity
    //SysTestBaseApp/UI/Screens/Screen028_read_STRING/Layout
    public string ScreenPath = "UI/Screens/Screen038_read_ARRAY/Layout";
     public string PopUp = "UI/Screens/Popup";   
    //SysTestBaseApp/UI/Screens/Screen038_read_ARRAY/Layout
    //SysTestBaseApp/UI/Screens/Screen004_Read_DINT/Layout
    //SysTestBaseApp/UI/Screens/Screen032_read_SINT/Layout
      //SysTestBaseApp/UI/Screens/Screen031_read_INT
      //SysTestBaseApp/UI/Screens/Screen029_read_REAL/Layout/ScreenContent/UniqueScreenContent
    //SysTestBaseApp/UI/Screens/Screen029_read_REAL/Layout/ScreenContent/UniqueScreenContent/TypeValueGroup1
    //SysTestBaseApp/UI/Screens/Screen030_read_BOOL
    [ExportMethod]
    public void Method1()
    {
        string DynamicLinkPath = "";
        string TextBoxPath = "";
        // Insert code to be executed by the method
        //SysTestBaseApp/UI/Screens/Popup/BOOL/TypeValueGroup1/TypeValueDispay1/Rectangle1/TextBox1
       /* for (int m=1;m<9;++m)
        {
            for (int l=0;l<25;++l)
            {
                    
                    TextBoxPath = $"UI/Screens/Popup/BOOL/TypeValueGroup1/TypeValueDispay{m}/Rectangle1/TextBox{l+1}/Text/DynamicLink";
                   if(m<5)
                   {
                        DynamicLinkPath = $"Model/Variables/ScrewToScreen/Bool/BoolPE{m}SumOpacity/TypeExpectedSum1/Expression/ExpressionEvaluator1/Source{l}/DynamicLink";
                   }
                   else
                   {
                        DynamicLinkPath = $"Model/Variables/ScrewToScreen/Bool/BoolPE{m-4}SumFillColor/TypeExpectedSum1/Expression/ExpressionEvaluator1/Source{l}/DynamicLink";
                   }
                   
                    Project.Current.GetVariable(DynamicLinkPath).Value =  Project.Current.GetVariable(TextBoxPath).Value;


            }
        }*/
        //SysTestBaseApp/Model/Variables/ScrewToScreen/Bool/BoolPE1SumOpacity
        //SysTestBaseApp/UI/Screens/Popup/INT/TypeValueGroup1/TypeValueDispay1
        //SysTestBaseApp/UI/Screens/Popup/STRING/TypeValueGroup1/TypeValueDispay1
        for (int j=1;j<5;j++)
        {
            for (int i = 1; i<26; i++)
            {
                DynamicLinkPath =ScreenPath + $"/Photoeyes/PhotoEyeBarScrewToScreen_type1/Rectangle{j}/Opacity/KeyValueConverter1/Source/ExpressionEvaluator1/Source{i}/DynamicLink";
                TextBoxPath =PopUp + $"/ARRAY/TypeValueGroup1/TypeValueDispay{j}/Rectangle1/TextBox{i}/Text/DynamicLink";
                Project.Current.GetVariable(TextBoxPath).Value = Project.Current.GetVariable(DynamicLinkPath).Value;

            }
            for (int k = 1; k<26; k++)
            {
                DynamicLinkPath = ScreenPath + $"/Photoeyes/PhotoEyeBarScrewToScreen_type1/Rectangle{j}/FillColor/KeyValueConverter1/Source/ExpressionEvaluator1/Source{k}/DynamicLink";
                TextBoxPath = PopUp + $"/ARRAY/TypeValueGroup1/TypeValueDispay{j+4}/Rectangle1/TextBox{k}/Text/DynamicLink";
                Project.Current.GetVariable(TextBoxPath).Value = Project.Current.GetVariable(DynamicLinkPath).Value;

            }    
        }
    }
}
