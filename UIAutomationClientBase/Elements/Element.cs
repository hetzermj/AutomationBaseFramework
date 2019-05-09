using System;
using System.Runtime.InteropServices;
using System.Threading;
using UIAutomationClient;

namespace UIAutomationClientBase.Elements
{

	public class Element
	{
		public IUIAutomationElement IUIElement = null;
		public string Name = null;
		public string AutomationId = null;
		public string ClassName = null;
		public string LocalizedControlType = null;
		public int ProcessId = 0;
		public bool IsNull = false;
		private bool _IsEnabled = false;
		public bool IsEnabled { get { return this.IUIElement.CurrentIsEnabled == 1; } }
		public tagRECT BoundingRectangle { get; set; }
		private int counter = 0;

		public Element(IUIAutomationElement element)
		{
			if (element != null)
			{
				this.IsNull = false;
				this.IUIElement = element;
				while (counter++ < 30)
				{
					try
					{
						this.Name = element.CurrentName;
						this.AutomationId = element.CurrentAutomationId;
						this.ClassName = element.CurrentClassName;
						this.LocalizedControlType = element.CurrentLocalizedControlType;
						this.ProcessId = element.CurrentProcessId;
						//this.IsEnabled = element.CurrentIsEnabled == 1;
						this.BoundingRectangle = element.CurrentBoundingRectangle;
						break;
					}
					catch (COMException com)
					{

					}
				}
			}
			else
			{
				this.IsNull = true;
			}
		}

		/// <summary>
		/// Focuses the Element.
		/// </summary>
		public Element SetFocus()
		{
			try
			{
				IUIElement.SetFocus();
			}
			catch (COMException e) { }
			Thread.Sleep(100);
			return this;
		}

		public Element[] GetAllElements(TreeScope Scope)
		{
			IUIAutomationElementArray elementsToFind = null;
			elementsToFind = IUIElement.FindAll(Scope, new CUIAutomationClass().CreateTrueCondition());
			int size = elementsToFind.Length;
			Element[] arr = new Element[size];
			for (int i = 0; i < size; i++)
			{
				arr[i] = new Element(elementsToFind.GetElement(i));
			}
			return arr;
		}

		/// <summary>
		/// Attempts to match an Element's current properties to a SearchCondition.
		/// </summary>
		/// <param name="element"></param>
		/// <param name="Condition"></param>
		/// <returns>True if the element's properties match, else False.</returns>
		public bool MatchElementCondition(IUIAutomationElement element, SearchCondition Condition)
		{
			bool found = false;

			for (int i = 0; i < 10; i++)
			{
				try
				{
					switch (Condition.By)
					{
						case By.AutomationId:
							if (element.CurrentAutomationId == Condition.ByValue && element.CurrentLocalizedControlType == Condition.ControlType.GetControlTypeValue())
								found = true;
							break;
						case By.Name:
							if (element.CurrentName == Condition.ByValue && element.CurrentLocalizedControlType == Condition.ControlType.GetControlTypeValue())
								found = true;
							break;
						case By.ClassName:
							if (element.CurrentClassName == Condition.ByValue && element.CurrentLocalizedControlType == Condition.ControlType.GetControlTypeValue())
								found = true;
							break;
					}
				}
				catch (COMException e) { }
			}

			return found;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="RelativeType"></param>
		/// <param name="Condition"></param>
		/// <returns></returns>
		public Element GetNextRelative(ElementRelative RelativeType)
		{
			IUIAutomationElement relative = null;

			IUIAutomationTreeWalker walker = new CUIAutomationClass().RawViewWalker;
			switch (RelativeType)
			{
				case ElementRelative.Parent:
					for (int i = 0; i < 10; i++)
					{
						try
						{
							relative = walker.GetParentElement(this.IUIElement);
							break;
						}
						catch (COMException e) { }
					}
					break;
				case ElementRelative.FirstChild:
					for (int i = 0; i < 10; i++)
					{
						try
						{
							relative = walker.GetFirstChildElement(this.IUIElement);
							break;
						}
						catch (COMException e) { }
					}
					break;
				case ElementRelative.NextSibling:
					for (int i = 0; i < 10; i++)
					{
						try
						{
							relative = walker.GetNextSiblingElement(this.IUIElement);
							break;
						}
						catch (COMException e) { }
					}
					break;
				default: throw new Exception("Given enum was not present in the switch!!!");
			}
			if (relative == null)
				return null;
			else
				return new Element(relative);
		}

		/// <summary>
		/// <para>This method will continually get the same subsequent relatives until a match is found or no more elements exist (null element returned).</para>
		/// <para>Useful for stepping down a large tree until the expected element is found.</para>
		/// <para>Example: Recursively finds the first child of the next element until a condition is hit.</para>
		/// </summary>
		/// <param name="RelativeType"></param>
		/// <param name="Condition"></param>
		/// <returns></returns>
		public Element GetRelativeElementUntilFound(ElementRelative RelativeType, SearchCondition Condition, int NumberOfRelatives = 1000)
		{
			Console.WriteLine($"--- Start of GetRelativeElementUntilFound Method ---");
			//Console.WriteLine($"--- Iterating through Relative Type : {RelativeType.ToString()} ---");
			Console.WriteLine($"--- Looking for Condition - By: [{Condition.By}] - ByValue: [{Condition.ByValue}] - LocalizedControlType: [{Condition.ControlType}] ---");
			IUIAutomationElement relative = this.IUIElement;

			IUIAutomationTreeWalker walker = new CUIAutomationClass().RawViewWalker;
			switch (RelativeType)
			{
				case ElementRelative.Parent:
					for (int i = 0; i < NumberOfRelatives; i++)
					{
						try
						{
							relative = walker.GetParentElement(relative);
							//Console.WriteLine($"-- Current Relative Properties - Name: [{relative.Name}] - AutomationId: [{relative.AutomationId}] - LocalizedControlType: [{relative.LocalizedControlType}]");
						}
						catch (COMException e) { }

						if (relative == null)
							break;
						else if (MatchElementCondition(relative, Condition))
							break;
					}
					break;
				case ElementRelative.FirstChild:
					for (int i = 0; i < NumberOfRelatives; i++)
					{
						try
						{
							relative = walker.GetFirstChildElement(relative);
							//Console.WriteLine($"-- Current Relative Properties - Name: [{relative.CurrentName}] - AutomationId: [{relative.CurrentAutomationId}] - LocalizedControlType: [{relative.CurrentLocalizedControlType}]");
						}
						catch (COMException e) { }

						if (relative == null)
							break;
						else if (MatchElementCondition(relative, Condition))
							break;
					}
					break;
				case ElementRelative.NextSibling:
					for (int i = 0; i < NumberOfRelatives; i++)
					{
						try
						{
							relative = walker.GetNextSiblingElement(relative);
							//Console.WriteLine($"-- Current Relative Properties - Name: [{relative.Name}] - AutomationId: [{relative.AutomationId}] - LocalizedControlType: [{relative.LocalizedControlType}]");
						}
						catch (COMException e) { }

						if (relative == null)
							break;
						else if (MatchElementCondition(relative, Condition))
							break;
					}
					break;
				default: throw new Exception("Given enum was not present in the switch!!!");
			}
			if (relative == null)
			{
				//Console.WriteLine($"--- End of GetRelativeElementUntilFound Method - Returning Null ---");
				return null;
			}
			else
			{
				//Console.WriteLine($"--- End of GetRelativeElementUntilFound Method - Returning Element ---");
				return new Element(relative);
			}
		}

		/// <summary>
		/// Get an element from the current element that matches the specified conditon.
		/// </summary>
		/// <param name="Scope"></param>
		/// <param name="Condition"></param>
		/// <returns></returns>
		public ControlElement GetControl(TreeScope Scope, SearchCondition Condition)
		{
			IUIAutomationElement elementToFind = null;
			//Console.WriteLine($"GetControl Method - Getting Control from Element where Name: [{this.Name}] - AutomationId: [{this.AutomationId}] - LocalizedControlType: [{this.LocalizedControlType}] .");
			for (int i = 0; i < 100; i++)
			{
				try
				{
					elementToFind = this.IUIElement.FindFirst(Scope, Condition.UIAutomationCondition);
				}
				catch (COMException e) { }
				catch (UnauthorizedAccessException ex) { }

				if (elementToFind != null)
					break;
				else
					Thread.Sleep(100);
			}
			if (elementToFind == null)
			{
				Console.WriteLine($"The Element was not Found!!! Search Criteria - By: [{Condition.By}] - By Value: [{Condition.ByValue}] - Localized Control Type: [{Condition.ControlType.GetControlTypeValue()}] .");
				throw new Exception($"The Element was not Found!!! Search Criteria - By: [{Condition.By}] - By Value: [{Condition.ByValue}] - Localized Control Type: [{Condition.ControlType.GetControlTypeValue()}] .");
			}

			return new ControlElement(elementToFind);
		}

		/// <summary>
		/// Try to get an element from the current element that matches the specified conditon.
		/// </summary>
		/// <param name="Scope"></param>
		/// <param name="Condition"></param>
		/// <returns>
		/// if found return ControlElement
		/// if not found return null element
		/// </returns>
		public ControlElement TryGetControl(TreeScope Scope, SearchCondition Condition)
		{
			IUIAutomationElement elementToFind = null;

			for (int i = 0; i < 10; i++)
			{
				try
				{
					elementToFind = this.IUIElement.FindFirst(Scope, Condition.UIAutomationCondition);
				}
				catch (COMException e) { }
				catch (UnauthorizedAccessException ex) { }

				if (elementToFind != null)
					break;
				else
					Thread.Sleep(100);
			}

			return new ControlElement(elementToFind);
		}
		/// <summary>
		/// Get an element from the current element that matches the specified conditon.
		/// </summary>
		/// <param name="Scope"></param>
		/// <param name="Condition"></param>
		/// <returns></returns>
		public ControlElement[] GetControls(TreeScope Scope, SearchCondition Condition)
		{
			IUIAutomationElementArray elementsToFind = null;
			ControlElement[] arr = null;

			for (int i = 0; i < 100; i++)
			{
				try
				{
					elementsToFind = this.IUIElement.FindAll(Scope, Condition.UIAutomationCondition);
				}
				catch (COMException e) { }
				catch (UnauthorizedAccessException ex) { }

				if (elementsToFind != null)
				{
					int size = elementsToFind.Length;
					arr = new ControlElement[size];
					for (int j = 0; j < size; j++)
					{
						arr[j] = new ControlElement(elementsToFind.GetElement(j));
					}
					break;
				}
				else
					Thread.Sleep(100);
			}

			if (elementsToFind == null)
				return null;
			else
				return arr;
		}

		/// <summary>
		/// Get all elements from the current element that match the specified control type.
		/// </summary>
		/// <param name="Scope"></param>
		/// <param name="ControlType"></param>
		/// <returns></returns>
		public ControlElement[] GetControlsByControlType(TreeScope Scope, LocalizedControlType ControlType)
		{
			IUIAutomationElementArray elementsToFind = null;
			IUIAutomationCondition cond = new CUIAutomationClass().CreatePropertyCondition(By.ControlType.GetId(), ControlType.GetControlTypeValue());
			elementsToFind = this.IUIElement.FindAll(Scope, cond);
			int size = elementsToFind.Length;
			ControlElement[] arr = new ControlElement[size];
			for (int i = 0; i < size; i++)
			{
				arr[i] = new ControlElement(elementsToFind.GetElement(i));
			}
			return arr;
		}

		public ControlElement ControlElement()
		{
			return new ControlElement(this.IUIElement);
		}
		public WindowElement WindowElement()
		{
			return new WindowElement(this.IUIElement);
		}
	}
}
