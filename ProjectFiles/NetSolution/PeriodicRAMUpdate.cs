#region Using directives
using UAManagedCore;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.DataLogger;
using FTOptix.UI;
using FTOptix.SerialPort;
using FTOptix.Report;
using FTOptix.ODBCStore;
using FTOptix.WebUI;
using FTOptix.OPCUAClient;
#endregion

public class PeriodicRAMUpdate : BaseNetLogic
{
    public override void Start()
    {
        duration = LogicObject.GetVariable("Period");
        if (duration == null)
            throw new CoreConfigurationException("Unable to find Period variable");

        enabledVariable = LogicObject.GetVariable("Enabled");
        if (enabledVariable == null)
            throw new CoreConfigurationException("Unable to find Enabled variable");

        actionMethodInvocation = LogicObject.Get("Action") as MethodInvocation;
        if (actionMethodInvocation == null)
            throw new CoreConfigurationException("Unable to find Action method invocation object");

        enabledVariable.VariableChange += EnabledVariable_VariableChange;
        actionTask = new PeriodicTask(PeriodicMethodInvocation, duration.Value, LogicObject);

        if (enabledVariable.Value)
            actionTask.Start();
    }

    public override void Stop()
    {
        enabledVariable.VariableChange -= EnabledVariable_VariableChange;
    }

    private void EnabledVariable_VariableChange(object sender, VariableChangeEventArgs e)
    {
        if (!e.NewValue)
            StopPeriodicTask();
        else
            RestartPeriodicTask();
    }

    private void StopPeriodicTask()
    {
        actionTask?.Dispose();
        actionTask = null;
    }

    private void RestartPeriodicTask()
    {
        StopPeriodicTask();
        actionTask = new PeriodicTask(PeriodicMethodInvocation, duration.Value, LogicObject);
        actionTask.Start();
    }

    private void PeriodicMethodInvocation()
    {
        actionMethodInvocation.Invoke();
    }

    private PeriodicTask actionTask;
    private IUAVariable duration;
    private IUAVariable enabledVariable;
    private MethodInvocation actionMethodInvocation;
}
