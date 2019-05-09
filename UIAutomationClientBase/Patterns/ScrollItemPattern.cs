using System;
using System.Runtime.InteropServices;
using UIAutomationClient;
using UIAutomationClientBase.Elements;

namespace UIAutomationClientBase.Patterns
{
	public class ScrollItemPattern
	{
		private Element _element { get; set; }
		private IUIAutomationScrollItemPattern _ScrollItemPattern { get; set; }

		public ScrollItemPattern(Element element)
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
				try { _ScrollItemPattern = (IUIAutomationScrollItemPattern)_element.IUIElement.GetCurrentPattern(UIA_PatternIds.UIA_ScrollItemPatternId); }
				catch (COMException e) { }

			} while (_ScrollItemPattern == null && counter++ < 10);

			if (_ScrollItemPattern == null)
			{
				throw new Exception($"Pattern is not available for Element where Name=[{_element.Name}] - AutomationId=[{_element.AutomationId}] - ClassName=[{_element.ClassName}].");
			}
		}

		/// <summary>
		/// Attempts to Scroll the Element into view on the Window or Container.
		/// </summary>
		public void ScrollIntoView()
		{
			_ScrollItemPattern.ScrollIntoView();
		}
	}
}
