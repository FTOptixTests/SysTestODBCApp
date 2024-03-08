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
using FTOptix.UI;
using FTOptix.SerialPort;
using FTOptix.Core;
using System.Collections.Generic;
using FTOptix.WebUI;
#endregion



class DataItem
{
    public string BarPath { get; set; }
    public List<string> DataType { get; set; }
}


public class LinkPEinScrewToScreen : BaseNetLogic
{

    public string LogixDriverPath = "/Objects/SysTestBaseApp/CommDrivers/RAEtherNet_IPDriver/";

    [ExportMethod]
    public void Method1()
    {
        //Properties declaration
            
//SysTestBaseApp/UI/Screens/Screen028_read_STRING/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1
            
           // var SourcePath = Project.Current.GetVariable("Uniqo_EOI/Model/Screens/Screen004_read_DINT/PEHolder1/PhotoEyeBarScrewToScreen_type2/" + "Rectangle1/SourcePath/KeyValueConverter1/Source/ExpressionEvaluator1/Source0/DynamicLink");
          //  var SourcePath = Project.Current.GetVariable("SysTestBaseApp/UI/Screens/Screen028_read_STRING/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/" + "Rectangle1/SourcePath/KeyValueConverter1/Source/ExpressionEvaluator1/Source0/DynamicLink");
            string DynamicLinkPath = "";
            List<string> PropertiesList = new List<string>
            {
                "FillColor",
                "Opacity"
            };

            List<DataItem> dataSetList = new List<DataItem>();

            // Add elements to the list
            //List<string> DINT = SetBaseTypeList(4, "DINT");
            //List<string> REAL = SetBaseTypeList(29, "REAL");
            //List<string> BOOL = SetBaseTypeList(30, "BOOL");
            //List<string> INT = SetBaseTypeList(31, "INT");
            //List<string> SINT = SetBaseTypeList(32, "SINT");
            //List<string> LINT = SetBaseTypeList(33, "LINT");
            List<string> ARRAY = SetArrayList(38, "ARRAY");
            Log.Info("Array list set");
            //List<string> STRING = SetBaseTypeList(28, "STRING");
            // Add data sets to the list
            //dataSetList.Add(new DataItem { BarPath = "UI/Screens/Screen004_Read_DINT/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/", DataType = DINT});
            //dataSetList.Add(new DataItem { BarPath = "UI/Screens/Screen029_read_REAL/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/", DataType = REAL});
            //dataSetList.Add(new DataItem { BarPath = "UI/Screens/Screen030_read_BOOL/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/", DataType = BOOL});
           // dataSetList.Add(new DataItem { BarPath = "UI/Screens/Screen032_read_SINT/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/", DataType = INT});
            //dataSetList.Add(new DataItem { BarPath = "UI/Screens/Screen033_read_LINT/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/", DataType = SINT});
            //dataSetList.Add(new DataItem { BarPath = "UI/Screens/Screen033_read_LINT/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/", DataType = LINT});
            dataSetList.Add(new DataItem { BarPath = "UI/Screens/Screen038_read_ARRAY/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/", DataType = ARRAY});
             Log.Info("Array datasetList");
            //dataSetList.Add(new DataItem { BarPath = "UI/Screens/Screen028_read_STRING/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/", DataType = STRING});
            //SysTestBaseApp/UI/Screens/Screen028_read_STRING/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1
            // properties: Fill Color, Opacity
            foreach(string property in PropertiesList)
            {   
                //dataSetList contains tags for each source
                foreach (var dataItem in dataSetList)
                {   Log.Info(property);
                    // Display the data sets
                    Log.Info($"ScreenName: {dataItem.BarPath}, DataType: {dataItem.DataType}");
                    // Copy the list with Data to temporary list
                    List<String> ListToFillSources = new List<string>(dataItem.DataType);
                    //fix dataItem.BarPath to avoid hardcoding
                    if (property == "Opacity" && dataItem.BarPath !="UI/Screens/Screen038_read_ARRAY/Layout/Photoeyes/PhotoEyeBarScrewToScreen_type1/")
                    {
                        ListToFillSources = ModifySubstrings(ListToFillSources);
                    }
                    int absolute_index = 0;
                    for (int rect_index = 1; rect_index <5; rect_index++)
                    {
                        for (int i = 0; i<26; i++)
                        {
                          
                            DynamicLinkPath = dataItem.BarPath + $"Rectangle{rect_index}/{property}/KeyValueConverter1/Source/ExpressionEvaluator1/Source{i}/DynamicLink";
                           
                            Project.Current.GetVariable(DynamicLinkPath).Value = ListToFillSources[absolute_index];
                            absolute_index++;
                        }
                    }
                }
            }
            
      
    }
    public List<string> SetBaseTypeList(int PEExpected, string var_type)
    {   List<string> ListOfSources = new List<string>();
        char firstchar = var_type[0];
        //Rectangle1
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/ScrewToScreen_{var_type}_PEExpected[0,{PEExpected}]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PT00");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CT00");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PA[0]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}00");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PT01");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CT01");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PA[1]");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}01");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PT02");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CT02");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PA[2]");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}02");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PT03");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CT03");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PA[3]");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}03");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PT04");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CT04");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PA[4]");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}04");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PT05");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CT05");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PA[5]");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}05");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PT06");

        //Rectange2
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/ScrewToScreen_{var_type}_PEExpected[1,{PEExpected}]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CT06");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PA[6]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}06");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PT07");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CT07");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PA[7]");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}07");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PT08");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CT08");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PA[8]");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}08");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PT09");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CT09");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PA[9]");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}09");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PT10");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CT10");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PA[10]");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}10");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PT11");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CT11");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PA[11]");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}11");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PT12");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CT12");

        //Rectange3
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/ScrewToScreen_{var_type}_PEExpected[2,{PEExpected}]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PA[12]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}12");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PT13");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CT13");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PA[13]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}13");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PT14");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CT14");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PA[14]");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}14");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PT15");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CT15");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PA[15]");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}15");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PT16");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CT16");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PA[16]");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}16");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PT17");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CT17");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PA[17]");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}17");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PT18");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CT18");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PA[18]");

        //Rectange4
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/ScrewToScreen_{var_type}_PEExpected[3,{PEExpected}]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}18");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PT19");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CT19");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PA[19]");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}19");
        ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Program&:Photoeye/Scattered{var_type}_PT20");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CT20");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PA[20]");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}20");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PT21");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Controller Tags/Scattered{var_type}_CT21");
        ListOfSources.Add(LogixDriverPath + $"CLX2/Tags/Program&:Photoeye/Scattered{var_type}_PA[21]");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}21");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PT22");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CT22");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PA[22]");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}22");
        ListOfSources.Add(LogixDriverPath + $"CLX3/Tags/Program&:Photoeye/Scattered{var_type}_PT23");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CT23");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PA[23]");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}23");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PT24");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CT24");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Program&:Photoeye/Scattered{var_type}_PA[24]");
        ListOfSources.Add(LogixDriverPath + $"CLX4/Tags/Controller Tags/Scattered{var_type}_CS/{firstchar}24");
        return ListOfSources;        
    }

    public List<string> SetArrayList(int PEExpected, string var_type)
    {   List<string> ListOfSources = new List<string>();
        int j = 0;
        for (int index=0; index<100; index++)
            {
                if(index==0 || index==25 || index==50 || index==75)
                {
                    ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/ScrewToScreen_ARRAY_PEExpected[{j},{PEExpected}]");
                   
                    j++;      
                    
                }
              
                    ListOfSources.Add(LogixDriverPath + $"CLX1/Tags/Controller Tags/Scattered{var_type}_CT[1,1,{index}]");
                   
                    
                        
            }   
        return ListOfSources;
    }


    static List<string> ModifySubstrings(List<string> strings)
    {
        for (int i = 0; i < strings.Count; i++)
        {
            // Use the Replace method to modify the substring
            if(i>=0 && i<=25){
                strings[i] = strings[i].Replace("CLX2", "CLX1");
                strings[i] = strings[i].Replace("CLX3", "CLX1");
                strings[i] = strings[i].Replace("CLX4", "CLX1");
            }
            else if(i>=26 && i<=52){
                strings[i] = strings[i].Replace("CLX1", "CLX2");
                strings[i] = strings[i].Replace("CLX3", "CLX2");
                strings[i] = strings[i].Replace("CLX4", "CLX2");
            }
            else if(i>=53 && i<=79){
                strings[i] = strings[i].Replace("CLX1", "CLX3");
                strings[i] = strings[i].Replace("CLX2", "CLX3");
                strings[i] = strings[i].Replace("CLX4", "CLX3");
            }
            else{
                strings[i] = strings[i].Replace("CLX1", "CLX4");
                strings[i] = strings[i].Replace("CLX2", "CLX4");
                strings[i] = strings[i].Replace("CLX3", "CLX4");
            }  
            
        }
        return strings;
    }
}
