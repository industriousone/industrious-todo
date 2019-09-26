using System;

using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class ItemListViewModel
	{
		public ItemListViewModel(IAppNavigator navigation, AppState appState)
		{
			// Translate the list of ToDoItem's to a list of item title strings to show in the view
			Items = new TranslatingObservable<ToDoItem, ItemViewCellModel>(appState.Items, item => new ItemViewCellModel(appState, item));

			SelectItemCommand = new Command<ItemViewCellModel>(itemViewCellModel =>
			{
				if (itemViewCellModel != null)
				{
					appState.SelectItem(itemViewCellModel.ToDoItem);
					navigation.ShowEditor();
				}
				else
				{
					appState.SelectItem(null);
				}
			});
		}


		public TranslatingObservable<ToDoItem, ItemViewCellModel> Items { get; }


		public Command<ItemViewCellModel> SelectItemCommand { get; }
	}
}
