using System;
using System.Runtime.InteropServices;
using UIAutomationClient;
using UIAutomationClientBase.Elements;

namespace UIAutomationClientBase.Patterns
{
	public class SelectionItemPattern
	{
		private Element _element { get; set; }
		private IUIAutomationSelectionItemPattern _SelectionItemPattern { get; set; }
		public bool IsSelected { get; private set; }

		public SelectionItemPattern(Element element)
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
				try { _SelectionItemPattern = (IUIAutomationSelectionItemPattern)_element.IUIElement.GetCurrentPattern(UIA_PatternIds.UIA_SelectionItemPatternId); }
				catch (COMException e) { }

			} while (_SelectionItemPattern == null && counter++ < 10);

			if (_SelectionItemPattern == null)
			{
				throw new Exception($"Pattern is not available for Element where Name=[{_element.Name}] - AutomationId=[{_element.AutomationId}] - ClassName=[{_element.ClassName}].");
			}

			this.SetProperties();
		}

		private void SetProperties()
		{
			this.IsSelected = _SelectionItemPattern.CurrentIsSelected == 1; // CurrentIsSelected returns int 0 or 1 for binary true/false
		}

		/// <summary>
		/// 
		/// </summary>
		public void Select()
		{
			if (!this.IsSelected)
				_SelectionItemPattern.Select();
			Initialize(this._element);
		}


	}
}
