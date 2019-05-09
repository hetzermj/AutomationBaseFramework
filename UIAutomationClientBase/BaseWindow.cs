using System;
using System.Runtime.InteropServices;
using System.Threading;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;
using UIAutomationClient;
using UIAutomationClientBase.Elements;

namespace UIAutomationClientBase
{
	public class BaseWindow
	{


		public BaseWindow(Application AppUnderTest, int TimeOut = WINDOWTIMEOUT)
		{
			Window = null;
			AUTOCLASS = null;
			ROOT = null;
			this.AppUnderTest = null;
			Thread.Sleep(10);
			GC.Collect();
			Thread.Sleep(10);
			this.AppUnderTest = AppUnderTest;
			AUTOCLASS = new CUIAutomationClass();
			ROOT = new Element(new CUIAutomationClass().GetRootElement());
		}

		public const int WINDOWTIMEOUT = 30;
		public CUIAutomationClass AUTOCLASS = null;
		public Element ROOT = null;
		public WindowElement Window = null;
		public Application AppUnderTest = null;
		

		/// <summary>
		/// Attempts to find the window starting from the Root Element (Desktop).
		/// </summary>
		/// <param name="Condition"></param>
		/// <param name="Scope"></param>
		/// <param name="SetMaximize"></param>
		/// <param name="TimeOutInSeconds"></param>
		public void SetWindow(SearchCondition Condition, bool SetMaximize, int TimeOutInSeconds = WINDOWTIMEOUT)
		{
			bool found = false;
			IUIAutomationElement windowToFind = null;
			DateTime dtStart = DateTime.Now;
			DateTime dtCurrent;
			Element[] childrenOfRoot = null;

			for (int i = 0; i < 200; i++)
			{
				windowToFind = null;
				childrenOfRoot = null;
				new BaseWindow(this.AppUnderTest);

				childrenOfRoot = ROOT.GetAllElements(TreeScope.TreeScope_Children);								

				var el = FindElementInArray(childrenOfRoot, Condition, this.AppUnderTest.ProcessId);
				if (el != null)
					windowToFind = el.IUIElement;

				if (windowToFind == null)
				{
					for (int j = 0; j < childrenOfRoot.Length; j++)
					{
						if (childrenOfRoot[j].ProcessId == this.AppUnderTest.ProcessId)
						{
							var child = childrenOfRoot[j].GetRelativeElementUntilFound(ElementRelative.FirstChild, Condition, 30);
							if (child != null && !child.IsNull)
								windowToFind = child.IUIElement;
						}
						if (windowToFind != null)// && !windowToFind.IsNull)
						{
							break;
						}
					}
				}
				else
				{
					break;
				}

				dtCurrent = DateTime.Now;
				if (windowToFind == null)// || windowToFind.IsNull)
				{
					Console.WriteLine($"Window does not exist yet.");
					Console.WriteLine($"CheckForWindow Method - Iteration: [{i}] - Seconds Spent: [{(dtCurrent - dtStart).TotalSeconds}]");
					Thread.Sleep(1000);
				}
				else
					break;

				if ((dtCurrent - dtStart).TotalSeconds > TimeOutInSeconds)
					break;
			}

			if (windowToFind == null)// || windowToFind.IsNull)
				throw new Exception($"Window was not found!!!");
			else
				Window = new WindowElement(windowToFind);//.IUIElement);

			Window.Pattern_Window().WaitForWindowReadyState(TimeOutInSeconds);

			Console.WriteLine($"Window identified - Window Name: [{Window.Name}] - Window Id: [{Window.AutomationId}]");

			if (SetMaximize)
				Window.Pattern_Window().Maximize();
			else
				Window.SetFocus();
			Thread.Sleep(200);
			Window.Pattern_Window().WaitForWindowReadyState(TimeOutInSeconds);

			DateTime dtEnd = DateTime.Now;
			Console.WriteLine($"Total Time for SetWindow Method: {(dtEnd - dtStart).ToString()}");
		}	
		
		public void SetWindowSimple(Element startingElement, SearchCondition Condition, TreeScope treeScope, int TimeOutInSeconds = WINDOWTIMEOUT)
		{
			bool found = false;
			IUIAutomationElement windowToFind = null;
			DateTime dtStart = DateTime.Now;
			DateTime dtCurrent;
			Element[] childrenOfRoot = null;

			for (int i = 0; i < 200; i++)
			{
				windowToFind = null;
				childrenOfRoot = null;
				new BaseWindow(this.AppUnderTest);

				windowToFind = startingElement.GetControl(treeScope, Condition).IUIElement;				

				if (windowToFind != null)
				{					
					break;
				}

				dtCurrent = DateTime.Now;
				if (windowToFind == null)
				{
					Console.WriteLine($"Window does not exist yet.");
					Console.WriteLine($"CheckForWindow Method - Iteration: [{i}] - Seconds Spent: [{(dtCurrent - dtStart).TotalSeconds}]");
					Thread.Sleep(1000);
				}
				else
					break;
				
				if ((dtCurrent - dtStart).TotalSeconds > TimeOutInSeconds)
					break;
			}

			if (windowToFind == null)
				throw new Exception($"Window was not found!!!");
			else
				Window = new WindowElement(windowToFind);

			Window.Pattern_Window().WaitForWindowReadyState(TimeOutInSeconds);

			Console.WriteLine($"Window identified - Window Name: [{Window.Name}] - Window Id: [{Window.AutomationId}]");			

			DateTime dtEnd = DateTime.Now;
			Console.WriteLine($"Total Time for SetWindow Method: {(dtEnd - dtStart).ToString()}");
		}

		public static Element FindElementInArray(Element[] Elements, SearchCondition Condition, int ProcessId)
		{
			//Console.WriteLine($"--- Start of FindElementInArray Method ---");
			//Console.WriteLine($"--- Looking for Condition - By: [{Condition.By}] - ByValue: [{Condition.ByValue}] - LocalizedControlType: [{Condition.ControlType}] ---");
			//Console.WriteLine($"--- Printing properties of elements in the array below ---");
			//for (int i = 0; i < Elements.Length; i++)
			//{
			//	Console.WriteLine($"-- Element [{i}] - Name: [{Elements[i].Name}] - AutomationId: [{Elements[i].AutomationId}] - LocalizedControlType: [{Elements[i].LocalizedControlType}] - ProcessId: [{Elements[i].ProcessId}]");
			//}
			Element matchElement = null;
			switch (Condition.By)
			{
				case By.AutomationId:
					try
					{
						matchElement = Array.Find(Elements, x => (
							x.AutomationId == Condition.ByValue
							&& x.LocalizedControlType == Condition.ControlType.GetControlTypeValue())
							&& x.ProcessId == ProcessId);
					}
					catch (COMException e) { }
					break;
				case By.ClassName:
					try
					{
						matchElement = Array.Find(Elements, x => (
							x.ClassName == Condition.ByValue
							&& x.LocalizedControlType == Condition.ControlType.GetControlTypeValue())
							&& x.ProcessId == ProcessId);
					}
					catch (COMException e) { }
					break;
				case By.Name:
					try
					{
						matchElement = Array.Find(Elements, x => (
							x.Name == Condition.ByValue
							&& x.LocalizedControlType == Condition.ControlType.GetControlTypeValue())
							&& x.ProcessId == ProcessId);
					}
					catch (COMException e) { }
					break;
				default: throw new Exception("Given enum was not present in the switch statement!!!");
			}
			//Console.WriteLine($"--- End of FindElementInArray Method ---");
			return matchElement;
		}		

		/// <summary>
		/// Only to be used if the Window is not guaranteed to appear.
		/// </summary>
		/// <param name="Condition"></param>
		/// <param name="TimeOutInSeconds"></param>
		/// <returns></returns>
		public static bool CheckForWindow(Application AppUnderTest, SearchCondition Condition, double TimeOutInSeconds = 30)
		{
			bool found = false;
			IUIAutomationElement windowToFind = null;
			Element Root = null;
			Element[] childrenOfRoot = null;

			DateTime dtStart = DateTime.Now;
			DateTime dtCurrent;

			for (int i = 0; i < 100; i++)
			{
				windowToFind = null;
				Root = new Element(new CUIAutomationClass().GetRootElement());

				childrenOfRoot = Root.GetAllElements(TreeScope.TreeScope_Children);
				var el = FindElementInArray(childrenOfRoot, Condition, AppUnderTest.ProcessId);
				if (el != null)
					windowToFind = el.IUIElement;

				if (windowToFind == null)
				{
					for (int j = 0; j < childrenOfRoot.Length; j++)
					{
						if (childrenOfRoot[j].ProcessId == AppUnderTest.ProcessId)
						{
							var child = childrenOfRoot[j].GetRelativeElementUntilFound(ElementRelative.FirstChild, Condition, 30);
							if (child != null && !child.IsNull)
								windowToFind = child.IUIElement;
						}
						if (windowToFind != null)// && !windowToFind.IsNull)
						{
							found = true;
							break;
						}
					}
				}
				else
				{
					found = true;
					break;
				}

				dtCurrent = DateTime.Now;
				if (!found)
				{
					Console.WriteLine($"CheckForWindow Method - Window Not Yet Found!");
					Thread.Sleep(250);
				}
				else
				{
					Console.WriteLine($"CheckForWindow Method - Window Found!");
				}
				Console.WriteLine($"CheckForWindow Method - Iteration: [{i}] - Seconds Spent: [{(dtCurrent - dtStart).TotalSeconds}]");
				if (found)
					break;
				if ((dtCurrent - dtStart).TotalSeconds > TimeOutInSeconds)
				{
					Console.WriteLine($"CheckForWindow Method - Did not locate window in Process ID [{AppUnderTest.ProcessId}] in the alotted time - Kicking out!");
					return false;
				}

			}

			return found;
		}		




		public static void DeleteField_KeyCombo()
		{
			Thread.Sleep(100);
			Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
			Thread.Sleep(100);
			Keyboard.Instance.HoldKey(KeyboardInput.SpecialKeys.SHIFT);
			Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.TAB);
			Thread.Sleep(100);
			Keyboard.Instance.LeaveAllKeys();
			Thread.Sleep(100);
			Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.DELETE);
			Thread.Sleep(100);
		}

		#region COM Exception Functions

		/// <summary>
		/// Given a function & parameter this function will continue to try and call the function until it does not throw a COMException.
		/// Example use:
		/// StatusStrip ss = fn_DoActionWhileCOMException(GetStatusStrip, myStatusStripSC)
		/// </summary>
		/// <typeparam name="I">input parameter type</typeparam>
		/// <typeparam name="O">return type</typeparam>
		/// <param name="functionToCall"></param>
		/// <param name="sc"></param>
		/// <param name="maxAttempts"></param>
		/// <returns></returns>
		public static O fn_DoActionWhileCOMException<I, O>(Func<I, O> functionToCall, I param, int maxAttempts = 20)
		{
			int attempts = -1;
			while (++attempts != maxAttempts)
				try
				{
					return functionToCall.Invoke(param);
				}
				catch (COMException e)
				{
					Thread.Sleep(100);
				}

			throw new Exception("fn_DoActionWhileCOMException failed to wait for function to stop throwing COMException");
		}


		/// <summary>
		/// Given a function, this function will continue to try and call the function until it does not throw a COMException.
		/// Example use:
		/// StatusStrip ss = fn_DoActionWhileCOMException(GetStatusStrip, myStatusStripSC);
		/// </summary>
		/// <typeparam name="O">Return type</typeparam>
		/// <param name="functionToCall"></param>
		/// <param name="maxAttempts"></param>
		/// <returns></returns>
		public static O fn_DoActionWhileCOMException<O>(Func<O> functionToCall, int maxAttempts = 20)
		{
			int attempts = -1;
			while (++attempts != maxAttempts)
				try
				{
					return functionToCall.Invoke();
				}
				catch (COMException e)
				{
					Thread.Sleep(100);
				}

			throw new Exception("fn_DoActionWhileCOMException failed to wait for function to stop throwing COMException");
		}
		/// <summary>
		/// Given a function, this function will continue to try and call the function until it does not throw a COMException.
		/// Example use:
		/// StatusStrip ss = fn_DoActionWhileCOMException(GetStatusStrip, myStatusStripSC)
		/// </summary>
		/// <typeparam name="O">Return type</typeparam>
		/// <param name="functionToCall"></param>
		/// <param name="maxAttempts"></param>
		/// <returns></returns>
		public static void fn_DoActionWhileCOMException(Action functionToCall, int maxAttempts = 20)
		{
			int attempts = -1;
			while (++attempts != maxAttempts)
			{
				try
				{
					functionToCall.Invoke();
					return;
				}
				catch (COMException e)
				{
					Thread.Sleep(100);
				}
			}

			throw new Exception("fn_DoActionWhileCOMException failed to wait for function to stop throwing COMException");
		}
		public static void fn_DoActionWhileCOMException<I>(Action<I> functionToCall, I param, int maxAttempts = 20)
		{
			int attempts = -1;
			while (++attempts != maxAttempts)
			{
				try
				{
					functionToCall.Invoke(param);
					return;
				}
				catch (COMException e)
				{
					Thread.Sleep(100);
				}
			}

			throw new Exception("fn_DoActionWhileCOMException failed to wait for function to stop throwing COMException");
		}

		#endregion

		#region Retry Functions

		public static void fn_Retry(Action functionToCall, int maxAttempts = 3)
		{
			int attempts = 0;
			while (++attempts != maxAttempts)
			{
				try
				{
					functionToCall();
					return;
				}
				catch (Exception e)
				{
					Console.WriteLine("fn_Retry attempting Method: " + functionToCall.Method.Name + " Count: " + attempts);
					Console.WriteLine(e.ToString());
				}
			}

			functionToCall();
		}

		#endregion

	}


}
