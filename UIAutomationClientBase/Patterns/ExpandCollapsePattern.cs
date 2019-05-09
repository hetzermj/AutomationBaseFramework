using System;
using System.Runtime.InteropServices;
using UIAutomationClient;
using UIAutomationClientBase.Elements;

namespace UIAutomationClientBase.Patterns
{
	public class ExpandCollapsePattern
	{
		private Element _element { get; set; }
		private IUIAutomationExpandCollapsePattern _ExpandCollapsePattern { get; set; }
		public ExpandCollapseState CurrentState { get; private set; }
		public bool IsExpanded { get; private set; }
		public bool IsCollapsed { get; private set; }
		public bool IsLeafNode { get; private set; }

		public ExpandCollapsePattern(Element element)
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
				try { _ExpandCollapsePattern = (IUIAutomationExpandCollapsePattern)_element.IUIElement.GetCurrentPattern(UIA_PatternIds.UIA_ExpandCollapsePatternId); }
				catch (COMException e) { }

			} while (_ExpandCollapsePattern == null && counter++ < 10);

			if (_ExpandCollapsePattern == null)
			{
				throw new Exception($"Pattern is not available for Element where Name=[{_element.Name}] - AutomationId=[{_element.AutomationId}] - ClassName=[{_element.ClassName}].");
			}

			this.SetProperties();
		}

		private void SetProperties()
		{
			CurrentState = _ExpandCollapsePattern.CurrentExpandCollapseState;
			if (CurrentState.HasFlag(ExpandCollapseState.ExpandCollapseState_Collapsed))
				IsCollapsed = true;
			if (CurrentState.HasFlag(ExpandCollapseState.ExpandCollapseState_Expanded))
				IsExpanded = true;
			if (CurrentState.HasFlag(ExpandCollapseState.ExpandCollapseState_LeafNode))
				IsLeafNode = true;
		}

		public void Expand()
		{
			_ExpandCollapsePattern.Expand();
			Initialize(this._element);
		}

		public void Collapse()
		{
			_ExpandCollapsePattern.Collapse();
			Initialize(this._element);
		}
	}
}
