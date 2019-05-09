using UIAutomationClient;

namespace UIAutomationClientBase.Elements
{
	public class SearchCondition
	{
		public IUIAutomationCondition UIAutomationCondition { get; set; }
		public By By { get; set; }
		public string ByValue { get; set; }
		public LocalizedControlType ControlType { get; set; }
		
		/// <summary>
		/// Creates a SearchCondition object that is used to locate elements.
		/// </summary>
		/// <param name="By">Enumeration for the Property by which to locate the element.</param>
		/// <param name="ByValue">The value of the corresponding Element property.</param>
		/// <param name="Type">Enumeration for specifying the Control Type of the Element.</param>
		public SearchCondition(By By, string ByValue, LocalizedControlType Type)
		{
			this.By = By;
			this.ByValue = ByValue;
			this.ControlType = Type;
			
			this.UIAutomationCondition = CreateSearchCondition(this.By, this.ByValue, this.ControlType);			
		}

		/// <summary>
		/// Internal method for creating the IUIAutomationCondition used to locate elements.
		/// </summary>
		/// <param name="By"></param>
		/// <param name="Value"></param>
		/// <param name="Type"></param>
		/// <returns></returns>
		private IUIAutomationCondition CreateSearchCondition(By By, string Value, LocalizedControlType Type)
		{
			IUIAutomationAndCondition cond = (IUIAutomationAndCondition)new CUIAutomationClass().CreateAndConditionFromArray(new IUIAutomationCondition[]
			{
				new CUIAutomationClass().CreatePropertyCondition(By.GetId(), Value),
				new CUIAutomationClass().CreatePropertyCondition(UIA_PropertyIds.UIA_LocalizedControlTypePropertyId, Type.GetControlTypeValue())
			});
			return cond as IUIAutomationCondition;
		}
		

	}
}
