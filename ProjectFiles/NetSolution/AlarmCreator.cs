#region StandardUsing
using System;
using System.Collections.Generic;
using System.Linq;
using FTOptix.CoreBase;
using FTOptix.HMIProject;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.NetLogic;
using FTOptix.OPCUAServer;
using FTOptix.UI;
using FTOptix.Alarm;
using FTOptix.S7TCP;
using FTOptix.CommunicationDriver;
using FTOptix.NativeUI;
//using FTOptix.RAEthernetIP;
using FTOptix.Recipe;
using FTOptix.EventLogger;
using FTOptix.Report;
using FTOptix.Retentivity;
using FTOptix.OPCUAClient;
using FTOptix.System;
using FTOptix.SerialPort;
using FTOptix.MicroController;
using FTOptix.ODBCStore;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.DataLogger;
using FTOptix.WebUI;
using EventHandler = FTOptix.CoreBase.EventHandler;
#endregion

public class AlarmCreator : BaseNetLogic
{
    [ExportMethod]
    public void CreateAlarms()
    {
        var ConfigAlarmsSize = LogicObject.GetVariable("ConfigAlarmsSize");
        var NumberOfIBAAlarms = LogicObject.GetVariable("IbaSize");
        var DriverName = LogicObject.GetVariable("DriverName");
        var TotalAlarmsInProject = LogicObject.GetVariable("TotalAlarmsInProject");

        TotalAlarmsInProject.Value = CLX.Length * (ConfigAlarmsSize.Value * 3 + NumberOfIBAAlarms.Value * 2);

        
        for (int i=0; i<CLX.Length; i++)
        {
            string driverPath = DriverName.Value + "/" + CLX[i] + "/Tags/";
            CreateConfigAlarms(driverPath, CLX[i], ConfigAlarmsSize.Value);
            CreateDatabindBetweenAlarmAndAndTag_Config(driverPath, CLX[i], ConfigAlarmsSize.Value);
            CreateIBAAlarms(driverPath, CLX[i], NumberOfIBAAlarms.Value);
            CreateDatabindBetweenAlarmAndAndTag_IBA(driverPath, CLX[i], NumberOfIBAAlarms.Value);
        }
    }

    private void CreateDatabindBetweenAlarmAndAndTag_Config(string driverPath, string CLX, int ConfigAlarmsSize)
    {
        var alarms = Project.Current.GetObject("Alarms");
        var commDrivers = Project.Current.GetObject("CommDrivers");
        var destinationEnable = commDrivers.Get<IUAVariable>(driverPath + "Program:S30_DBA/UniqoEnable");
        
        for (uint i = 0; i < ConfigAlarmsSize; ++i)
        {   
            var destinationInputVariable = commDrivers.Get<IUAVariable>(driverPath + "Controller Tags/" + "Config_Alarms/" + i + "/TRIP_Input");
            //var destinationInputVariable = commDrivers.Get<IUAVariable>(driverPath + "Controller Tags/" + "Config_Alarms[" + i + "]/TRIP_Input");
            var sourceInputVariable = alarms.Get("ConfigAlarms_"+ CLX).Get<DigitalAlarm>("ConfigAlarm[" + i +"]_Trip").InputValueVariable;
            var sourceEnableVariable = alarms.Get("ConfigAlarms_"+ CLX).Get<DigitalAlarm>("ConfigAlarm[" + i +"]_Trip").EnabledVariable;
            if (sourceInputVariable != null && destinationInputVariable != null)
                sourceInputVariable.SetDynamicLink(destinationInputVariable);
                sourceEnableVariable.SetDynamicLink(destinationEnable);        

            destinationInputVariable = commDrivers.Get<IUAVariable>(driverPath + "Controller Tags/" + "Config_Alarms/" + i + "/HIHI_Input");
            sourceInputVariable = alarms.Get("ConfigAlarms_"+ CLX).Get<ExclusiveLevelAlarmController>("ConfigAlarm[" + i +"]_HIHI").InputValueVariable;
            sourceEnableVariable = alarms.Get("ConfigAlarms_"+ CLX).Get<ExclusiveLevelAlarmController>("ConfigAlarm[" + i +"]_HIHI").EnabledVariable;
            if (sourceInputVariable != null && destinationInputVariable != null)
                sourceInputVariable.SetDynamicLink(destinationInputVariable);
                sourceEnableVariable.SetDynamicLink(destinationEnable); 

            destinationInputVariable = commDrivers.Get<IUAVariable>(driverPath + "Controller Tags/" + "Config_Alarms/" + i + "/HI_Input");
            sourceInputVariable = alarms.Get("ConfigAlarms_"+ CLX).Get<NonExclusiveLevelAlarmController>("ConfigAlarm[" + i +"]_HI").InputValueVariable;
            sourceEnableVariable = alarms.Get("ConfigAlarms_"+ CLX).Get<NonExclusiveLevelAlarmController>("ConfigAlarm[" + i +"]_HI").EnabledVariable;
            if (sourceInputVariable != null && destinationInputVariable != null)
                sourceInputVariable.SetDynamicLink(destinationInputVariable);
                sourceEnableVariable.SetDynamicLink(destinationEnable);

              
                    
        }
    }
   

    private void CreateDatabindBetweenAlarmAndAndTag_IBA(string driverPath, string CLX, int NumberOfIBAAlarms)
    {
        var alarms = Project.Current.GetObject("Alarms");
        var commDrivers = Project.Current.GetObject("CommDrivers");
        var j = 0;
        var destinationEnable = commDrivers.Get<IUAVariable>(driverPath + "Program:S30_DBA/UniqoEnable");

        for (uint i = 0; i < NumberOfIBAAlarms; ++i)
        {   
            if(i % 6 == 0){
                j=0;
            }

            var destinationInputVariable = commDrivers.Get<IUAVariable>(driverPath + "Controller Tags/" + "Uniqo_Alarms_DA");
            var sourceInputVariable = alarms.Get("IBA_Alarms_" + CLX).Get<DigitalAlarm>("IBA_DigitalAlarm_" + i).InputValueVariable;
            var sourceEnableVariable = alarms.Get("IBA_Alarms_"+ CLX).Get<DigitalAlarm>("IBA_DigitalAlarm_" + i).EnabledVariable;
            if (sourceInputVariable != null && destinationInputVariable != null)
                sourceInputVariable.SetDynamicLink(destinationInputVariable, i);
                sourceEnableVariable.SetDynamicLink(destinationEnable); 

            destinationInputVariable = commDrivers.Get<IUAVariable>(driverPath + "Program:S30_DBA/Uniqo_Alarms_AA");

            if (j==0)
            {
                sourceInputVariable = alarms.Get("IBA_Alarms_"+ CLX).Get<ExclusiveLevelAlarmController>("IBA_AnalogAlarm_" + i).InputValueVariable;
                sourceEnableVariable = alarms.Get("IBA_Alarms_"+ CLX).Get<ExclusiveLevelAlarmController>("IBA_AnalogAlarm_" + i).EnabledVariable;

            }else if (j==1)
            {
                sourceInputVariable = alarms.Get("IBA_Alarms_"+ CLX).Get<NonExclusiveLevelAlarmController>("IBA_AnalogAlarm_" + i).InputValueVariable;
                sourceEnableVariable = alarms.Get("IBA_Alarms_" + CLX).Get<NonExclusiveLevelAlarmController>("IBA_AnalogAlarm_" + i).EnabledVariable;

            }else if (j==2)
            {
                sourceInputVariable = alarms.Get("IBA_Alarms_" + CLX).Get<ExclusiveDeviationAlarmController>("IBA_AnalogAlarm_" + i).InputValueVariable;
                sourceEnableVariable = alarms.Get("IBA_Alarms_" + CLX).Get<ExclusiveDeviationAlarmController>("IBA_AnalogAlarm_" + i).EnabledVariable;

            }else if(j==3)
            {
                sourceInputVariable = alarms.Get("IBA_Alarms_"+ CLX).Get<NonExclusiveDeviationAlarmController>("IBA_AnalogAlarm_" + i).InputValueVariable;
                sourceEnableVariable = alarms.Get("IBA_Alarms_" + CLX).Get<NonExclusiveDeviationAlarmController>("IBA_AnalogAlarm_" + i).EnabledVariable;

            }else if(j==4)
            {
                sourceInputVariable = alarms.Get("IBA_Alarms_"+ CLX).Get<ExclusiveRateOfChangeAlarmController>("IBA_AnalogAlarm_" + i).InputValueVariable;
                sourceEnableVariable = alarms.Get("IBA_Alarms_" + CLX).Get<ExclusiveRateOfChangeAlarmController>("IBA_AnalogAlarm_" + i).EnabledVariable;

            }else if(j==5)
            {
                sourceInputVariable = alarms.Get("IBA_Alarms_"+ CLX).Get<NonExclusiveRateOfChangeAlarmController>("IBA_AnalogAlarm_" + i).InputValueVariable;
                sourceEnableVariable = alarms.Get("IBA_Alarms_" + CLX).Get<NonExclusiveRateOfChangeAlarmController>("IBA_AnalogAlarm_" + i).EnabledVariable;

            }   

            if (sourceInputVariable != null && destinationInputVariable != null)
                sourceEnableVariable.SetDynamicLink(destinationEnable);
                sourceInputVariable.SetDynamicLink(destinationInputVariable, i);

            j++ ;    
        }
    }

    
    private void CreateConfigAlarms(string driverPath, string CLX, int ConfigAlarmsSize)
    {

        var alarmFolder = InformationModel.MakeObject<FTOptix.Core.Folder>("ConfigAlarms_" + CLX);
        Project.Current.GetObject("Alarms").Add(alarmFolder);
        for (int i = 0; i < ConfigAlarmsSize; ++i)
        {
           
            var alarm_da = InformationModel.MakeObject<DigitalAlarm>("ConfigAlarm[" + i + "]_Trip");
            var alarm_aa1 = InformationModel.MakeObject<ExclusiveLevelAlarmController>("ConfigAlarm[" + i + "]_HIHI");
            var alarm_aa2 = InformationModel.MakeObject<NonExclusiveLevelAlarmController>("ConfigAlarm[" + i + "]_HI");
            alarm_da.Message = CLX + "_ConfigAlarm[" + i + "]_Trip";

            alarm_aa1.HighHighLimit = 100;
            alarm_aa1.HighLimit = 50; 
            alarm_aa1.LowLimit = -20;
            alarm_aa1.LowLowLimit = -50;
            alarm_aa1.Message = CLX + "_ConfigAlarm[" + i + "]_HIHI";

            alarm_aa2.HighHighLimit = 300;
            alarm_aa2.HighLimit = 30; 
            alarm_aa2.LowLimit = -20;
            alarm_aa2.LowLowLimit = -50;
            alarm_aa2.Message = CLX + "_ConfigAlarm[" + i + "]_HI";

            alarmFolder.Add(alarm_da);
            alarmFolder.Add(alarm_aa1);
            alarmFolder.Add(alarm_aa2);
                        
        }
    }

    private void CreateIBAAlarms(string driverPath, string CLX, int NumberOfIBAAlarms)
    {

        var alarmFolder = InformationModel.MakeObject<FTOptix.Core.Folder>("IBA_Alarms_" + CLX);
        Project.Current.GetObject("Alarms").Add(alarmFolder);
        var j = 0;

        for (int i = 0; i < NumberOfIBAAlarms; ++i)
        {   
            if(i % 6 == 0){
                j=0;
            }
           
            var alarm_da = InformationModel.MakeObject<DigitalAlarm>("IBA_DigitalAlarm_" + i);
            alarm_da.Message = CLX + "_DA_" + i;
            alarmFolder.Add(alarm_da);
            
            if (j==0)
            {
                var alarm_aa_ex = InformationModel.MakeObject<ExclusiveLevelAlarmController>("IBA_AnalogAlarm_" + i);
                alarm_aa_ex.HighHighLimit = 9;
                alarm_aa_ex.HighLimit = 8.4; 
                alarm_aa_ex.LowLimit = -20;
                alarm_aa_ex.LowLowLimit = -50;
                alarm_aa_ex.Message = CLX + "_AA_Exc_Lvl_" + i;
                alarmFolder.Add(alarm_aa_ex);

            }
            else if (j==1)
            {
                var alarm_aa_nex = InformationModel.MakeObject<NonExclusiveLevelAlarmController>("IBA_AnalogAlarm_" + i);
                alarm_aa_nex.HighHighLimit = 8.8;
                alarm_aa_nex.HighLimit = 7.22; 
                alarm_aa_nex.LowLimit = -20;
                alarm_aa_nex.LowLowLimit = -50;
                alarm_aa_nex.Message = CLX + "_AA_NonExc_Lvl_" + i;
                alarmFolder.Add(alarm_aa_nex);
            }
            else if (j==2)
            {
                var alarm_aa_exd = InformationModel.MakeObject<ExclusiveDeviationAlarmController>("IBA_AnalogAlarm_" + i);
                alarm_aa_exd.HighHighLimit = 6;
                alarm_aa_exd.HighLimit = 4.444; 
                alarm_aa_exd.LowLimit = -20;
                alarm_aa_exd.LowLowLimit = -50;
                alarm_aa_exd.Message = CLX + "_AA_ExcDev_" + i;
                alarmFolder.Add(alarm_aa_exd);
            }
            else if (j==3)
            {
                var alarm_aa_nexd = InformationModel.MakeObject<NonExclusiveDeviationAlarmController>("IBA_AnalogAlarm_" + i);
                alarm_aa_nexd.HighHighLimit = 5;
                alarm_aa_nexd.HighLimit = 4.98; 
                alarm_aa_nexd.LowLimit = -20;
                alarm_aa_nexd.LowLowLimit = -50;
                alarm_aa_nexd.Message = CLX + "_AA_NonExcDev_" + i;
                alarmFolder.Add(alarm_aa_nexd);
            }
            else if (j==4)
            {
                var alarm_aa_erate = InformationModel.MakeObject<ExclusiveRateOfChangeAlarmController>("IBA_AnalogAlarm_" + i);
                alarm_aa_erate.PollingTime = 950;
                alarm_aa_erate.HighHighLimit = 5;
                alarm_aa_erate.HighLimit = 3.98; 
                alarm_aa_erate.Message = CLX + "_AA_ExcRateof_" + i;
                alarmFolder.Add(alarm_aa_erate);
            }
            else if (j==5)
            {
                var alarm_aa_nerate = InformationModel.MakeObject<NonExclusiveRateOfChangeAlarmController>("IBA_AnalogAlarm_" + i);
                alarm_aa_nerate.PollingTime = 1100;
                alarm_aa_nerate.HighHighLimit = 5;
                alarm_aa_nerate.HighLimit = 3.98; 
                alarm_aa_nerate.Message = CLX + "_AA_NonExcRateof_" + i;
                alarmFolder.Add(alarm_aa_nerate);
            }
            j++;      
        }
    }

    private static string[] CLX = new string[] { "CLX1", "CLX2", "CLX3", "CLX4"};

}
