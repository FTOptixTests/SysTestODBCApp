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
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.RAEtherNetIP;
using FTOptix.System;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.DataLogger;
using FTOptix.SerialPort;
using FTOptix.WebUI;
using FTOptix.OPCUAClient;
#endregion

public class RuntimeNetLogic1 : BaseNetLogic
{
    public override void Start()
    {
        

        textBox1 = (TextBox)Owner;

         task?.Dispose();
            task = new DelayedTask(() => {SetText() ; }, 2000, LogicObject);   //Recipe delay for data acquisition
            task.Start();

            var recipeNumber = LogicObject.GetVariable("RecipeNr");
            
             recipeNumber.VariableChange += RecipeNrChanged;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    private void RecipeNrChanged(object sender, VariableChangeEventArgs e)
    {
        var recipeNumber = LogicObject.GetVariable("RecipeNr");
       
        SetText();
    }

    public void SetText()
    {
        var recipeNumber = LogicObject.GetVariable("RecipeNr");
        textBox1.Text = (recipeNumber.Value) + "_rec";
    }
    private TextBox textBox1;
    private DelayedTask task;
    

}

