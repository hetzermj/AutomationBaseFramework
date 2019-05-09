using System;
using UIAutomationClient;

namespace UIAutomationClientBase.Elements
{
	public class UIA_Helpers
	{

	}

	public enum By
	{
		AutomationId,
		Name,
		ClassName,
		ControlType
	}
	public enum LocalizedControlType
	{
		Button,
		CheckBox,
		ComboBox,
		Custom,
		Dataitem,
		Dialog,
		Edit,
		ListBox,
		ListItem,
		MenuItem,
		Pane,
		RadioButton,
		SplitButton,
		StatusBar,
		Tab,
		TabItem,
		Text,
		Toolbar,
		Tree,
		TreeItem,
		TreeView,
		Window

	}

	public enum ElementRelative
	{
		Parent,
		FirstChild,
		NextSibling
	}

	public static class UIA_Enums
	{
		public static string GetControlTypeValue(this LocalizedControlType type)
		{
			string value = "";
			switch (type)
			{
				case LocalizedControlType.Button: value = "button"; break;
				case LocalizedControlType.CheckBox: value = "check box"; break;
				case LocalizedControlType.ComboBox: value = "combo box"; break;
				case LocalizedControlType.Custom: value = "custom"; break;
				case LocalizedControlType.Dataitem: value = "dataitem"; break;
				case LocalizedControlType.Dialog: value = "dialog"; break;
				case LocalizedControlType.Edit: value = "edit"; break;
				case LocalizedControlType.ListBox: value = "list"; break;
				case LocalizedControlType.ListItem: value = "list item"; break;
				case LocalizedControlType.MenuItem: value = "menu item"; break;
				case LocalizedControlType.Pane: value = "pane"; break;
				case LocalizedControlType.RadioButton: value = "radio button"; break;
				case LocalizedControlType.SplitButton: value = "split button"; break;
				case LocalizedControlType.StatusBar: value = "status bar"; break;
				case LocalizedControlType.Tab: value = "tab"; break;
				case LocalizedControlType.TabItem: value = "tab item"; break;
				case LocalizedControlType.Text: value = "text"; break;
				case LocalizedControlType.Toolbar: value = "tool bar"; break;
				case LocalizedControlType.Tree: value = "tree"; break;
				case LocalizedControlType.TreeItem: value = "tree item"; break;
				case LocalizedControlType.TreeView: value = "tree view"; break;
				case LocalizedControlType.Window: value = "window"; break;
				default: throw new Exception("The given enum is not present in the switch!!!");
			}
			return value;
		}
		public static int GetId(this By by)
		{
			int id = 0;
			switch (by)
			{
				case By.AutomationId: id = UIA_PropertyIds.UIA_AutomationIdPropertyId; break;
				case By.Name: id = UIA_PropertyIds.UIA_NamePropertyId; break;
				case By.ClassName: id = UIA_PropertyIds.UIA_ClassNamePropertyId; break;
				case By.ControlType: id = UIA_PropertyIds.UIA_LocalizedControlTypePropertyId; break;
				default: throw new Exception("The given enum is not present in the switch!!!");
			}
			return id;
		}
	}
}
