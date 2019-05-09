using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestStack.White.InputDevices;
using UIAutomationClient;
using UIAutomationClientBase.Elements;

namespace UIAutomationClientBase.Patterns
{
	public class LegacyIAccessiblePattern
	{
		private Element _element { get; set; }
		private ControlElement _controlElement { get; set; }
		private IUIAutomationLegacyIAccessiblePattern _LegacyIAccessiblePattern { get; set; }
		public string Description { get; private set; }
		public string Name { get; private set; }
		public string Value { get; private set; }
		public uint State { get; private set; }

		public LegacyIAccessiblePattern(Element element)
		{
			Initialize(element);
		}

		/// <summary>
		/// <para>Retrieves the Pattern and its properties.</para>
		/// <para>Useful in this class for resetting the Pattern's properties after an action is performed on the Element.</para>
		/// </summary>
		private void Initialize(Element element)
		{
			this._element = element;
			this._controlElement = new ControlElement(element.IUIElement);

			int counter = 0;
			do
			{
				try
				{
					_LegacyIAccessiblePattern = (IUIAutomationLegacyIAccessiblePattern)_element.IUIElement.GetCurrentPattern(UIA_PatternIds.UIA_LegacyIAccessiblePatternId);
					if (_LegacyIAccessiblePattern != null)
						SetProperties();
				}
				catch (COMException e)
				{

				}

			} while (_LegacyIAccessiblePattern == null && counter++ < 10);

			if (_LegacyIAccessiblePattern == null)
				throw new Exception($"Pattern is not available for Element where Name=[{_element.Name}] - AutomationId=[{_element.AutomationId}] - ClassName=[{_element.ClassName}].");
		}

		private void SetProperties()
		{
			this.Description = _LegacyIAccessiblePattern.CurrentDescription;
			this.Name = _LegacyIAccessiblePattern.CurrentName;
			this.Value = _LegacyIAccessiblePattern.CurrentValue;
			this.State = _LegacyIAccessiblePattern.CurrentState;
		}



		/// <summary>
		/// This method will attempt to set the LegacyIAccessible Value of an element and then press TAB.
		/// </summary>
		/// <param name="element"></param>
		/// <param name="value"></param>
		public LegacyIAccessiblePattern SetValue(string value)
		{
			try { _LegacyIAccessiblePattern.SetValue(value); } catch (COMException e) { }
			Thread.Sleep(100);
			_element.IUIElement.SetFocus();
			Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
			return new LegacyIAccessiblePattern(_element);
		}

		/// <summary>
		/// <para>This method will attempt to set the LegacyIAccessible Value of an element.</para>
		/// <para>Does NOT press TAB.</para>
		/// </summary>
		/// <param name="element"></param>
		/// <param name="value"></param>
		public LegacyIAccessiblePattern SetValue2(string value)
		{
			try { _LegacyIAccessiblePattern.SetValue(value); } catch (COMException e) { }
			Thread.Sleep(100);
			_element.SetFocus();
			Thread.Sleep(100);
			return new LegacyIAccessiblePattern(_element);
		}

		/// <summary>
		/// This SetValue method attempts to Click the center of the element and send keys with a Keyboard instance.
		/// </summary>
		/// <param name="element"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public LegacyIAccessiblePattern SetValue3(string value)
		{
			try { _LegacyIAccessiblePattern.SetValue(""); } catch (COMException e) { }
			Thread.Sleep(100);
			_element.SetFocus();
			_controlElement.Click();
			Thread.Sleep(100);
			Keyboard.Instance.Enter(value);
			Thread.Sleep(10);
			Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
			return new LegacyIAccessiblePattern(_element);
		}

		public void CheckCurrentValue(string value)
		{
			if (this.Value != value)
			{
				Console.WriteLine($"Value was not properly set for the Element!!! Element Name: [{_element.Name}] - Element ID: [{_element.AutomationId}] - Intended Value: [{Value}] .");
				throw new Exception($"Value was not properly set for the Element!!! Element Name: [{_element.Name}] - Element ID: [{_element.AutomationId}] - Intended Value: [{Value}] .");
			}
		}

		/// <summary>
		/// Executes the Default Action of the Element.
		/// </summary>
		public void DoDefaultAction()
		{
			try { _LegacyIAccessiblePattern.DoDefaultAction(); } catch (COMException e) { }
			Initialize(_element);
		}



	}
}
