using System;

using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class MainViewModel
	{
		public MainViewModel(IAppNavigator navigation)
		{
			AddItemCommand = new Command(() =>
			{
				navigation.ShowEditorPage();
			});

			DeleteItemCommand = new Command(
				() => { },
				() => false
			);
		}


		public Command AddItemCommand { get; }


		public Command DeleteItemCommand { get; }
	}
}
