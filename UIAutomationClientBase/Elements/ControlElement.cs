using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using TestStack.White.InputDevices;
using UIAutomationClient;
using UIAutomationClientBase.Patterns;

namespace UIAutomationClientBase.Elements
{


	public class ControlElement : Element
	{
		public ControlElement(IUIAutomationElement element) : base(element)
		{
			
		}
		

		public TogglePattern Pattern_Toggle()
		{			
			return new TogglePattern(this);
		}
		public ScrollItemPattern Pattern_ScrollItem()
		{
			return new ScrollItemPattern(this);
		}
		public LegacyIAccessiblePattern Pattern_LegacyIAccessible()
		{
			return new LegacyIAccessiblePattern(this);
		}
		public ExpandCollapsePattern Pattern_ExpandCollapsePattern()
		{
			return new ExpandCollapsePattern(this);
		}
		
		

		/// <summary>
		/// Sends Mouse Click to the X,Y Point of the center of the Element's bounding rectangle.
		/// </summary>
		public void Click()
		{
			try { IUIElement.SetFocus(); } catch (COMException e) { }
			var rectangle = IUIElement.CurrentBoundingRectangle;
			Point center = new Point();
			center.X = rectangle.left + ((rectangle.right - rectangle.left) / 2);
			center.Y = rectangle.top + ((rectangle.bottom - rectangle.top) / 2);			
			Mouse.Instance.Location = center;
			Thread.Sleep(50);

			Mouse.Instance.Click(MouseButton.Left, center);
		}

		

		//public void SelectFromComboBox(string Option)
		//{
		//	var childrenOfCombobox = GetAllChildren();
		//	ControlElement listItemToSelect = null;

		//	bool IsInsideTable = GetRelative(ElementRelative.Parent).IUIElement.CurrentLocalizedControlType == "dataitem";

		//	//	Seeing if the intended Option to Select even exists
		//	bool found = false;
		//	for (int i = 0; i < childrenOfCombobox.Length; i++)
		//	{
		//		if (childrenOfCombobox.GetElement(i).CurrentName == Option)
		//		{
		//			found = true;
		//			listItemToSelect = (IUIAElement)childrenOfCombobox.GetElement(i);
		//			break;
		//		}
		//	}
		//	if (!found)
		//	{
		//		Console.WriteLine($"List Item is not an Option!!! ComboBox Name: [{Name}] - ComboBox ID: [{AutomationId}] - List Item Selection: [{Option}] .");
		//		throw new Exception($"List Item is not an Option!!! ComboBox Name: [{Name}] - ComboBox ID: [{AutomationId}] - List Item Selection: [{Option}] .");
		//	}

		//	//	If intended list item is already selected - just skip everything - no need
		//	if (listItemToSelect.IsItemSelected())
		//		return;

		//	//	Seeing if a blank option exists
		//	bool blankOptionExists = false;
		//	for (int i = 0; i < childrenOfCombobox.Length; i++)
		//	{
		//		if (childrenOfCombobox.GetElement(i).CurrentName == " ")
		//		{
		//			blankOptionExists = true;
		//			break;
		//		}
		//	}

		//	// Block to Complete selection if the combo box is inside a table row
		//	if (IsInsideTable)
		//	{
		//		try { element.ScrollIntoView(); } catch (Exception e) { }
		//		Thread.Sleep(50);
		//		element.Focus();
		//		Thread.Sleep(50);
		//		element.ClickCenterOfBounds();

		//		for (int i = 0; i < (childrenOfCombobox.Length + 2); i++)
		//		{
		//			Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.UP);
		//		}
		//		for (int i = 0; i < (childrenOfCombobox.Length + 2); i++)
		//		{
		//			if (listItemToSelect.IsItemSelected())
		//				break;
		//			Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.DOWN);
		//		}
		//		goto czCheckpoint;
		//	}


		//	//	Block to complete the ComboBox Selection
		//	if (blankOptionExists)
		//	{
		//		Thread.Sleep(50);
		//		element.SetFocus();
		//		Thread.Sleep(50);
		//		Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.SPACE);
		//		element.Collapse();

		//		for (int i = 0; i < childrenOfCombobox.Length + 2; i++)
		//		{
		//			Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.DOWN);
		//			if (listItemToSelect.IsItemSelected())
		//				break;
		//		}
		//	}
		//	else if (!blankOptionExists)
		//	{
		//		Thread.Sleep(50);
		//		element.SetFocus();
		//		Thread.Sleep(50);

		//		element.SetFocus();
		//		//Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
		//		//Keyboard.Instance.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.SHIFT);
		//		//Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
		//		//Keyboard.Instance.LeaveAllKeys();

		//		for (int i = 0; i < (childrenOfCombobox.Length + 2); i++)
		//		{
		//			Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.UP);
		//		}
		//		for (int i = 0; i < (childrenOfCombobox.Length + 2); i++)
		//		{
		//			if (listItemToSelect.IsItemSelected())
		//				break;
		//			Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.DOWN);
		//		}
		//	}


		//	czCheckpoint:

		//	if (!listItemToSelect.IsItemSelected())
		//	{
		//		Console.WriteLine($"ComboBox list item was not properly selected!!! Element Name: [{element.CurrentName}] - Element ID: [{element.CurrentAutomationId}] - List Item Selection: [{Option}] .");
		//		throw new Exception($"ComboBox list item was not properly selected!!! Element Name: [{element.CurrentName}] - Element ID: [{element.CurrentAutomationId}] - List Item Selection: [{Option}] .");
		//	}

		//	Keyboard.Instance.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
		//	Thread.Sleep(100);


		//}

		
	}
}
