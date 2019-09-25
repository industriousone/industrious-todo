using System;

namespace Industrious.ToDo.ViewModels.Tests
{
	public class MockNavigator : IAppNavigator
	{
		public Boolean IsEditorVisible;


		public void DismissEditorPage()
		{
			IsEditorVisible = false;
		}


		public void ShowEditorPage()
		{
			IsEditorVisible = true;
		}
	}
}
