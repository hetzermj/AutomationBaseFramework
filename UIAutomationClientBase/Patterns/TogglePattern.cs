using System;
using System.Runtime.InteropServices;
using UIAutomationClient;
using UIAutomationClientBase.Elements;

namespace UIAutomationClientBase.Patterns
{
	public class TogglePattern
	{
		private Element _element { get; set; }
		private IUIAutomationTogglePattern _TogglePattern { get; set; }
		public ToggleState CurrentToggleState { get; private set; }

		public TogglePattern(Element element)
		{
			Initialize(element);
		}

		/// <summary>
		/// <para>Sets/Resets the Pattern and its properties.</para>
		/// <para>Useful in this class for resetting the Pattern's properties after an action is performed on the Element.</para>
		/// </summary>
		private void Initialize(Element element)
		{
			this._element = element;
			int counter = 0;
			do
			{
				try
				{
					_TogglePattern = (IUIAutomationTogglePattern)_element.IUIElement.GetCurrentPattern(UIA_PatternIds.UIA_TogglePatternId);
				}
				catch (COMException e)
				{

				}

			} while (_TogglePattern == null && counter++ < 10);
			if (_TogglePattern == null)
				throw new Exception($"Pattern is not available for Element where Name=[{_element.Name}] - AutomationId=[{_element.AutomationId}] - ClassName=[{_element.ClassName}].");

			this.SetCurrentToggleState();
		}

		/// <summary>
		/// <para>Executes the Toggle Pattern's 'Toggle' action.</para>
		/// <para>A boolean is supplied to ensure the ToggleState of the element is what the user wants.</para>
		/// </summary>
		/// <param name="Check">True for ToggleState:On (1), False for ToggleState:Off (0)</param>
		public void Toggle(bool Check)
		{
			if (_element.IsEnabled)
			{
				bool IsSelected = CurrentToggleState == ToggleState.ToggleState_On;
				if (Check != IsSelected)
				{
					try { _TogglePattern.Toggle(); }
					catch (COMException e) { }
				}
			}
			this.Initialize(_element);
		}

		/// <summary>
		/// Sets the 'CurrentToggleState' Property of this class.
		/// </summary>
		private void SetCurrentToggleState()
		{
			this.CurrentToggleState = _TogglePattern.CurrentToggleState;
		}


	}
}
