using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIAutomationClient;
using UIAutomationClientBase.Elements;

namespace UIAutomationClientBase.Patterns
{
	public class WindowPattern
	{
		private WindowElement _element = null;
		private IUIAutomationWindowPattern _WindowPattern = null;
		//public bool IsModal { get; private set; }

		public WindowPattern(WindowElement element)
		{
			Initialize(element);
		}

		/// <summary>
		/// <para>Sets/Resets the Pattern and its properties.</para>
		/// <para>Useful in this class for resetting the Pattern's properties after an action is performed on the Element.</para>
		/// </summary>
		private void Initialize(WindowElement element)
		{
			this._element = element;

			int counter = 0;
			do
			{
				try
				{
					_WindowPattern = (IUIAutomationWindowPattern)element.IUIElement.GetCurrentPattern(UIA_PatternIds.UIA_WindowPatternId);
				}
				catch (COMException e) { }
				catch (InvalidOperationException ex) { }
				Thread.Sleep(10);
			} while (_WindowPattern == null && counter++ < 50);

			if (_WindowPattern == null)
			{
				throw new Exception($"Pattern is not available for Element where Name=[{_element.Name}] - AutomationId=[{_element.AutomationId}] - ClassName=[{_element.ClassName}].");
			}

			this.SetProperties();
		}

		private void SetProperties()
		{
			//this.IsModal = _WindowPattern.CurrentIsModal == 1;			
		}

		/// <summary>
		/// Closes the Window.
		/// </summary>
		public void Close()
		{
			_WindowPattern.Close();
		}

		private bool IsWindowReady()
		{
			return _WindowPattern.CurrentWindowInteractionState.HasFlag(WindowInteractionState.WindowInteractionState_ReadyForUserInteraction);
		}

		/// <summary>
		/// Waits for a period of time until the Window is Ready For User Interaction.
		/// </summary>
		/// <param name="TimeOutInSeconds"></param>
		public void WaitForWindowReadyState(int TimeOutInSeconds = 30)
		{
			for (int i = 0; i != TimeOutInSeconds; i++)
			{
				if (IsWindowReady())
					return;
				else
				{
					Console.WriteLine($"Window ready state: [{IsWindowReady()}]");
					Console.WriteLine($"Iteration: [{i}]");
					Thread.Sleep(1000);
				}
				if (i >= TimeOutInSeconds)
					throw new Exception("Hit Window Ready State Timeout!!!");
			}

		}

		public void Maximize()
		{
			_WindowPattern.SetWindowVisualState(WindowVisualState.WindowVisualState_Maximized);
			WaitForWindowReadyState();
			for (int i = 0; i < 100; i++)
			{
				if (_WindowPattern.CurrentWindowVisualState == WindowVisualState.WindowVisualState_Maximized) break;
				else Thread.Sleep(50);
			}
		}

	}
}
