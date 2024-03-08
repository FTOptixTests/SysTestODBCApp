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
using FTOptix.WebUI;
#endregion

public class LogButtonClick : BaseNetLogic
{
    private string CutTextFromBrowsePath(string browsePath, string wordBeforeCuttedText, string wordAfterCuttedText){ 
        if(browsePath.Contains(wordBeforeCuttedText) && browsePath.Contains(wordAfterCuttedText)){
            int Start, End;
            Start = browsePath.IndexOf(wordBeforeCuttedText, 0) + wordBeforeCuttedText.Length;
            End = browsePath.IndexOf(wordAfterCuttedText, Start);
            return browsePath.Substring(Start, End - Start);
        }
        return "Error! Cannot get page name!";
    }

    private string CutTextFromBrowsePath(string browsePath, string wordBeforeCuttedText){
        if (browsePath.Contains(wordBeforeCuttedText)){
            int Start = browsePath.IndexOf(wordBeforeCuttedText, 0) + wordBeforeCuttedText.Length;;
            return browsePath.Substring(Start);
        }
        return "Error! Cannot get page name!";
    }

    private int IncreasClicksCounter(){
        var clicksCounter = Project.Current.GetVariable("Model/Variables/Features/CustomeEventLogger/ButtonClicksCounter");
        clicksCounter.Value++;
        return clicksCounter.Value;
    }

    private string generateAdditonalMessage(){
        string TextToLog = LogicObject.GetVariable("TextToLog").Value;
        string VariableValueToLog = LogicObject.GetVariable("VariableToLog").Value;
        string additionalMessage = "";
        if(TextToLog != "Here write additional message"){
            additionalMessage = " Additional Message: " + TextToLog;
            if(VariableValueToLog != "Here write additional message"){
                additionalMessage += VariableValueToLog;
            }
        }
        return additionalMessage;
    }


    [ExportMethod]
    public void LogMessageButtonNextScreen(string buttonPath)
    {
        int amountOfClicksOnPage = IncreasClicksCounter();

        string pageName = CutTextFromBrowsePath(buttonPath, "/PanelLoader1/", "/Layout");
        string additionalMessage = generateAdditonalMessage();
        
        Log.Info($"[System Tests] MOUSE's CLICK - counter: {amountOfClicksOnPage}. Screen Name: {pageName}. Button Name: NextScreen. {additionalMessage}");
    }

    [ExportMethod]
     public void LogMessageScrnToScrw(string buttonPath)  //Function logs screen name, button name, mouse's click and optional extra message
    {
        int amountOfClicksOnPage = IncreasClicksCounter();

        string pageName = CutTextFromBrowsePath(buttonPath,"/PanelLoader1/", "/Layout/");
        string buttonName = CutTextFromBrowsePath(buttonPath,"/ScreenToScrewButtons/");
        string additionalMessage = generateAdditonalMessage();

        Log.Info($"[System Tests] MOUSE's CLICK - counter: {amountOfClicksOnPage}. Screen Name: {pageName}. Button Name: {buttonName}. {additionalMessage}"); 
    }
    
    [ExportMethod]
    public void LogMessageSpecialButtons(string buttonPath)  //Function logs screen name, button name, mouse's click and optional extra message
    {
        int amountOfClicksOnPage = IncreasClicksCounter();

        string buttonName = CutTextFromBrowsePath(buttonPath,"/CommonScreenContent/");
        string pageName = CutTextFromBrowsePath(buttonPath,"/PanelLoader1/", "/Layout/");
        string additionalMessage = generateAdditonalMessage();
        
        Log.Info($"[System Tests] MOUSE's CLICK - counter: {amountOfClicksOnPage}. Screen Name: {pageName}. Button Name: {buttonName}. {additionalMessage}"); 
    }

    [ExportMethod]
    public void LogNewScreenName(string screenName){
        string additionalMessage = generateAdditonalMessage();
        Log.Info($"[System Tests] Screen Open: {screenName}.{additionalMessage}");
    }
}
