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

public class CountNodesLogic : BaseNetLogic
{
    [ExportMethod]
   public void CountNodes()
    {       
         if( LogicObject.GetVariable("show_BaseData").Value) 
        Log.Info("Total " + CountNodesRecursive(Project.Current).ToString() + " nodes in project");        
        ListNodes(Project.Current);
        if( LogicObject.GetVariable("show_UI").Value)         
        ListNodes(Project.Current.Get("UI"));
        if( LogicObject.GetVariable("show_Model").Value)
        {
            ListNodes(Project.Current.Get("Model"));
           // ListNodes(Project.Current.Get("Model/Screens"));

        }   
        
        if( LogicObject.GetVariable("show_Converters").Value)   
        ListNodes(Project.Current.Get("Converters"));
        if( LogicObject.GetVariable("show_Alarms").Value)   
        ListNodes(Project.Current.Get("Alarms"));
        if( LogicObject.GetVariable("show_Recipes").Value)   
        ListNodes(Project.Current.Get("Recipes"));
        if( LogicObject.GetVariable("show_Loggers").Value)   
        ListNodes(Project.Current.Get("Loggers"));
        if( LogicObject.GetVariable("show_DataStores").Value)   
        ListNodes(Project.Current.Get("DataStores"));
        if( LogicObject.GetVariable("show_Reports").Value)   
        ListNodes(Project.Current.Get("Reports"));
        if( LogicObject.GetVariable("show_OPC_UA").Value)
        {
            //ListNodes(Project.Current.Get("OPC-UA/OPCUAClient1/Objects/Uniqo_EOI5_Server/CommDrivers/LogixDriver1"),"OPCUA server tags");
            ListNodes(Project.Current.Get("OPC-UA"));

        } 
        
        if( LogicObject.GetVariable("show_CommDriversTags").Value)
       {
           Log.Info("CLX1 driver");
           ListNodes(Project.Current.Get("CommDrivers/RAEtherNet_IPDriver/CLX1"));           
           
           ListNodes(Project.Current.Get("CommDrivers/RAEtherNet_IPDriver/CLX1/Tags"),"CommDrivers/RAEtherNet_IPDriver/CLX1/Tags");
                      

           Log.Info("CLX2 driver");
           ListNodes(Project.Current.Get("CommDrivers/RAEtherNet_IPDriver/CLX2"));
           
           ListNodes(Project.Current.Get("CommDrivers/RAEtherNet_IPDriver/CLX2/Tags"),"CommDrivers/RAEtherNet_IPDriver/CLX2/Tags");
           
           
           Log.Info("CLX3 driver");
           ListNodes(Project.Current.Get("CommDrivers/RAEtherNet_IPDriver/CLX3"));
          
           ListNodes(Project.Current.Get("CommDrivers/RAEtherNet_IPDriver/CLX3/Tags"),"CommDrivers/RAEtherNet_IPDriver/CLX3/Tags");
          

           Log.Info("CLX4 driver");
           ListNodes(Project.Current.Get("CommDrivers/RAEtherNet_IPDriver/CLX4"));
          
           ListNodes(Project.Current.Get("CommDrivers/RAEtherNet_IPDriver/CLX4/Tags"),"CommDrivers/RAEtherNet_IPDriver/CLX4/Tags");
           


        }
           
         if( LogicObject.GetVariable("show_NetLogic").Value)
        ListNodes(Project.Current.Get("NetLogic"));
        if( LogicObject.GetVariable("show_Users").Value)
        ListNodes(Project.Current.Get("Users"));
        if( LogicObject.GetVariable("show_Translations").Value)
        ListNodes(Project.Current.Get("Translations"));
        if( LogicObject.GetVariable("show_Retentivity").Value)
       ListNodes(Project.Current.Get("Retentivity"));

       // Log.Info(CountNodesRecursive(Project.Current.Get("Model")).ToString() + " nodes in project");



    }

    private int CountNodesRecursive(IUANode node)
    {
        int result = node.Children.Count;

        foreach (var c in node.Children)
            result += CountNodesRecursive(c);

        return result;
    }

    private void ListNodes(IUANode modelReference)
    {
      
         foreach (var childNode in modelReference.Children)
         {
            Log.Info( modelReference.BrowseName.ToString() +" -> " + childNode.BrowseName.ToString() + " contains "+ CountNodesRecursive(childNode).ToString()+ " Nodes");   
             
         }
    }
    private void ListNodes(IUANode modelReference, string path)
    {
        
         foreach (var childNode in modelReference.Children)
         {
            Log.Info( path.ToString() +" -> " + childNode.BrowseName.ToString() + " contains "+ CountNodesRecursive(childNode).ToString()+ " Nodes");   
             
         }
    }
    
}
