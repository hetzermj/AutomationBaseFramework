using System;
using System.Diagnostics;
using System.Threading;

namespace UIAutomationClientBase.Elements
{
	public class Application
	{
		public ProcessStartInfo StartInfo { get; private set; }
		public string ProcessName { get; private set; }
		public int ProcessId { get; private set; }
		public DesktopApp ApplicationUnderTest { get; private set; }

		/// <summary>
		/// Creates a new instance of the Application object.
		/// </summary>
		/// <param name="app"></param>
		public Application(DesktopApp app)
		{
			ApplicationUnderTest = app;
			StartInfo = new ProcessStartInfo();
			StartInfo.FileName = app.GetApplicationLocation();
			ProcessName = app.GetProcessName();
		}

		/// <summary>
		/// Kills the Application Process using the ProcessId property.
		/// </summary>
		public void KillApplication()
		{
			Process.GetProcessById(ProcessId).Kill();
			ProcessId = -1; //	Setting to -1 afterwards because the PID will always be a positive number in the system.
		}

		/// <summary>
		/// Launches the Application. Does not attach to the Process ID!
		/// </summary>
		/// <param name="app"></param>
		public void LaunchApplication()
		{
			Process.Start(StartInfo);
		}

		/// <summary>
		/// <para>Attempts to attach to a process in the allotted time or else an Exception is thrown.</para>
		/// </summary>
		/// <param name="app"></param>
		/// <param name="MaxSecondsToWait"></param>
		public void AttachToProcess(int MaxSecondsToWait = 30)
		{
			bool found = false;
			int counter = 0;
			do
			{
				foreach (Process _pcs in Process.GetProcesses())
				{
					if (_pcs != null && _pcs.ProcessName.Equals(this.ProcessName))
					{
						ProcessId = _pcs.Id;
						found = true;
						return;
					}
				}
				Thread.Sleep(1000);
				counter++;
			} while (!found && (counter <= MaxSecondsToWait));

			if (!found)
			{
				throw new Exception($"Process [{this.ProcessName}] was not found in the allotted time!!!");
			}
		}
				

	}

	public enum DesktopApp
	{
		Calculator,
		WhiteWinFormsTestApp
	};

	public static class Extensions
	{
		/// <summary>
		/// Kills all instances of a Process by name.
		/// </summary>
		/// <param name="app"></param>
		/// <param name="tries"></param>
		public static void KillAllInstancesOfProcess(this DesktopApp app, int tries = 10)
		{
			for (int i = 0; i < tries; i++)
			{
				Thread.Sleep(250);
				var processes = Process.GetProcessesByName(app.GetProcessName());
				if (processes != null && processes.Length > 0)
					for (int j = 0; j < processes.Length; j++)
						if (processes[j] != null)
							processes[j].Kill();
			}
		}

		/// <summary>
		/// Gets the Application's Process' Name as it would appear in the task manager.
		/// </summary>
		/// <param name="app"></param>
		/// <returns></returns>
		public static string GetProcessName(this DesktopApp app)
		{
			switch (app)
			{
				case DesktopApp.WhiteWinFormsTestApp: return "WindowsFormsTestApplication";
				case DesktopApp.Calculator: return "calc";
				default: throw new Exception("The Processes enum provided is not implemented!");
			}
		}

		/// <summary>
		/// Retrieves the full path location of the Application's executable file.
		/// </summary>
		/// <param name="deskApp"></param>
		/// <returns></returns>
		public static string GetApplicationLocation(this DesktopApp deskApp)
		{
			switch (deskApp)
			{
				case DesktopApp.Calculator: return @"C:\Windows\System32\calc.exe";
				case DesktopApp.WhiteWinFormsTestApp: return @"C:\Automation_Data\WindowsFormsTestApplication.exe";
				default: throw new Exception("The DesktopApps enum provided is not implemented!");
			}
		}
	}
}
