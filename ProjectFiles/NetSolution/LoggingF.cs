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
using FTOptix.Report;
//using FTOptix.EthernetIP;
using FTOptix.CoreBase;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.RAEtherNetIP;
using FTOptix.Core;
using FTOptix.Retentivity;
using FTOptix.OPCUAClient;
using FTOptix.EventLogger;
using FTOptix.System;
using FTOptix.SerialPort;
using FTOptix.MicroController;
using FTOptix.ODBCStore;
#endregion

public class LoggingF : BaseNetLogic
{
    [ExportMethod]
    public void PerformLogin(string username, string password, out bool loginResult)
    {
        var usersAlias = LogicObject.GetAlias("Users");
        if (usersAlias == null || usersAlias.NodeId == NodeId.Empty)
        {
            Log.Error("LoginFunc", "Missing Users alias");
            loginResult = false;
            return;
        }

        var user = usersAlias.Get<User>(username);
        if (user == null)
        {
            Log.Error("LoginFunc", "Could not find user " + username);
            loginResult = false;
            return;
        }

        try
        {
            user.PasswordVariable.RemoteRead();
            loginResult = Session.ChangeUser(username, password);
        }
        catch (Exception e)
        {
            Log.Error("LoginFunc", e.Message);
            loginResult = false;
        }
    }

}
