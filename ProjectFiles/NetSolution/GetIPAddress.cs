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
using System.Linq;
using System.Text;
using System.Net; 
using System.Collections.Generic;
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

public class GetIPAddress : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        GetIP();
    }   

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void GetIP()
    {
        string ipv4Address = String.Empty;
        var ModelFolder = Project.Current.Get("Model/Variables/Config/IP");
        IUAVariable EOI = ModelFolder.GetVariable("EOI_Nr");
        IUAVariable IPAddress = ModelFolder.GetVariable("IP");
        
        foreach (IPAddress currentIPAddress in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (currentIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                ipv4Address = currentIPAddress.ToString();
                break;
            }
        }

        IPAddress.Value = ipv4Address;

        Dictionary<string, int> ipAddressMap = new Dictionary<string, int>
        {
            { "192.168.1.211", 1 },
            { "192.168.1.212", 2 },
            { "192.168.1.213", 3 },
            { "192.168.1.214", 4 }
        };

        if (ipAddressMap.TryGetValue(ipv4Address, out int eoiValue))
        {
            EOI.Value = eoiValue;
        }
        else
        {
            EOI.Value = string.IsNullOrEmpty(ipv4Address) ? 0 : 5;
        }
   }
}
