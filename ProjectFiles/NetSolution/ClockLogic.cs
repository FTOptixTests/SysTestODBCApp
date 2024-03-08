#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.NativeUI;
using FTOptix.CoreBase;
using FTOptix.Recipe;
using FTOptix.EventLogger;
using FTOptix.DataLogger;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.RAEtherNetIP;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.SerialPort;
using FTOptix.Report;
using FTOptix.ODBCStore;
using FTOptix.WebUI;
using FTOptix.OPCUAClient;
#endregion

public class ClockLogic : BaseNetLogic
{
	public override void Start()
	{
		periodicTask = new PeriodicTask(UpdateTime, 1000, LogicObject);
		periodicTask.Start();
	}

	public override void Stop()
	{
		periodicTask.Dispose();
		periodicTask = null;
	}

	private void UpdateTime()
	{
		var timeVar = LogicObject.GetVariable("Time");
		timeVar.Value = DateTime.Now;
	}

	private PeriodicTask periodicTask;
}
