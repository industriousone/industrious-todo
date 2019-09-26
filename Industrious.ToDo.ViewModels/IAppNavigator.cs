using System;

namespace Industrious.ToDo.ViewModels
{
	public interface IAppNavigator
	{
		void DismissEditor();

		void ShowEditor();
	}
}
