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
using System.Threading;
using FTOptix.System;
using FTOptix.SerialPort;
using FTOptix.MicroController;
using FTOptix.ODBCStore;
using FTOptix.WebUI;
#endregion

public class CPURead : BaseNetLogic
{
    private static DateTime lastTime;
    private static TimeSpan lastTotalProcessorTime;
    private static DateTime curTime;
    private static TimeSpan curTotalProcessorTime;
    Process[] pp;
    Process p;
    Process[] p3;
    Process p2;
    
    private double CPUUsage;
    [ExportMethod]
    public void CPUusage()
    {
         string processName = "FTOptixRuntime";
         string processName2 = "CoreServiceHost";
         double CPUProcessRead= CPUUsageRead(processName, processName2);
           if (CPUProcessRead>0.0)    
         LogicObject.GetVariable("CPURead").Value = CPUProcessRead;
         
               
    }

    public double CPUUsageRead(string processName, string processName2)
    {
           

          //  Process[] pp = Process.GetProcessesByName(processName);
          
            pp = Process.GetProcessesByName(processName);
            p = pp[0];
          //  Process p = pp[0];

           // Process[] p3 = Process.GetProcessesByName(processName2);
            //Process p2 = p3[0];

            p3 = Process.GetProcessesByName(processName2);
            p2 = p3[0];
            
                    
                    
                        curTime = DateTime.Now;
                        curTotalProcessorTime = p.TotalProcessorTime +p2.TotalProcessorTime ;
                        
                        CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                        
                        Console.WriteLine("{0} CPU: {1}%", processName, Convert.ToInt32(CPUUsage * 100));
                        
                        lastTime = curTime;
                        lastTotalProcessorTime = curTotalProcessorTime;
                        return CPUUsage*100;
                   
                


    }
     

}
