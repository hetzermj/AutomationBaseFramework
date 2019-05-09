using UIAutomationClient;
using UIAutomationClientBase.Patterns;

namespace UIAutomationClientBase.Elements
{
	public class WindowElement : Element
	{		
		public SearchCondition Locator { get; set; }
		private IUIAutomationWindowPattern _WindowPattern { get; set; }

		public WindowElement(IUIAutomationElement element) : base (element)
		{
							
		}

		public WindowPattern Pattern_Window()
		{
			return new WindowPattern(this);
		}








	}
}
