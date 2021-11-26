using System;
using UnityEngine;
public class ConsoleToFile : MonoBehaviour
{
	string fileName = "";
	private void OnEnable() => Application.logMessageReceived += Log;
	private void OnDisable() => Application.logMessageReceived -= Log;
	public void Log(string logString, string stackTrace, LogType type)
	{
		if(fileName == "")
		{
			string path = System.Environment.GetFolderPath(
				System.Environment.SpecialFolder.Desktop) + "/Unity_Logs";
			System.IO.Directory.CreateDirectory(path);
			string formattedDate = DateTime.Now.ToString().Replace("/", "-").Replace(":","-");
			fileName = path + "/log- " + formattedDate + ".txt";
		}
		string formattedLogString = "[" + DateTime.Now + " | " + type.ToString() + "] " + logString;
		try
		{
			System.IO.File.AppendAllText(fileName, formattedLogString + "\n");
		}
		catch{}
	}
}