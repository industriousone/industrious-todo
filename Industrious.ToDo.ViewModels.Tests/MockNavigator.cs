using System;

namespace Industrious.ToDo.ViewModels.Tests
{
	public class MockNavigator : IAppNavigator
	{
		public Boolean IsEditorVisible;


		public void DismissEditor()
		{
			IsEditorVisible = false;
		}


		public void ShowEditor()
		{
			IsEditorVisible = true;
		}
	}
}
