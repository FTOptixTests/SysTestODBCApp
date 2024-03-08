#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.RAEtherNetIP;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
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

public class UpdateXYChart : BaseNetLogic
{
   private int returnValueForSmallArray(int valueFromBigArray)
    {
        if(valueFromBigArray > 0)
        {
            return -70;
        }
        else{
            return 70;
        }
    }

    [ExportMethod]
    public void changedValuesXYChart()
    {
        var XYChart_X0 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray1_0_0");
        var XYChart_Y0 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray1_0_1");
        var XYChart_X1 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray1_1_0");
        var XYChart_Y1 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray1_1_1");
        var XYChart_X2 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray1_2_0");
        var XYChart_Y2 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray1_2_1");

        var XYChart_Small_X0 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray2_0_0");
        var XYChart_Small_Y0 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray2_0_1");
        var XYChart_Small_X1 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray2_1_0");
        var XYChart_Small_Y1 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray2_1_1");
        var XYChart_Small_X2 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray2_2_0");
        var XYChart_Small_Y2 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartArray2_2_1");

        var positionBuffer1 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartBuffer1");
        var positionBuffer2 = Project.Current.GetVariable("Model/Variables/Features/XYChart/XYChartBuffer2");

        int tempX0Value = XYChart_X0.Value;

        if(tempX0Value == 0)
        {  
            XYChart_X0.Value = 100;
            XYChart_Y0.Value = 100;
            XYChart_X1.Value = -100;
            XYChart_Y1.Value = 100;
            XYChart_X2.Value = -100;
            XYChart_Y2.Value = -100;
            positionBuffer1.Value = 100;
            positionBuffer2.Value = -100;
        }
        else
        {
            
            var tempY0Value = XYChart_Y0.Value;

            XYChart_X0.Value = XYChart_X1.Value;
            XYChart_Y0.Value = XYChart_Y1.Value;
            XYChart_X1.Value = XYChart_X2.Value;
            XYChart_Y1.Value = XYChart_Y2.Value;
            XYChart_X2.Value = positionBuffer1.Value;
            XYChart_Y2.Value = positionBuffer2.Value;
            positionBuffer1.Value = tempX0Value;
            positionBuffer2.Value = tempY0Value;
        }

        XYChart_Small_X0.Value = returnValueForSmallArray(XYChart_X0.Value);
        XYChart_Small_Y0.Value = returnValueForSmallArray(XYChart_Y0.Value);
        XYChart_Small_X1.Value = returnValueForSmallArray(XYChart_X1.Value);
        XYChart_Small_Y1.Value = returnValueForSmallArray(XYChart_Y1.Value);
        XYChart_Small_X2.Value = returnValueForSmallArray(XYChart_X2.Value);
        XYChart_Small_Y2.Value = returnValueForSmallArray(XYChart_Y2.Value);
    }
}
