using System;
using System.Collections.ObjectModel;

using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class ItemListViewModel
	{
		public ItemListViewModel(IAppNavigator navigation)
		{
			Items = new ObservableCollection<string>(new String[]
			{
				"Cereal",
				"Juice",
				"Lettuce",
				"Ice cream"
			});

			SelectItemCommand = new Command(() =>
			{
				navigation.ShowEditorPage();
			});
		}


		public ObservableCollection<String> Items { get; }


		public Command SelectItemCommand { get; }
	}
}
