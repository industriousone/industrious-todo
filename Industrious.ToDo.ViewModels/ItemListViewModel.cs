using System;

using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class ItemListViewModel : NotifyPropertyChanged
	{
		public ItemListViewModel(AppState appState)
		{
			// Translate the list of ToDoItem's to a list of item title strings to show in the view
			Items = new TranslatingObservable<ToDoItem, ItemViewCellModel>(appState.Items, item => new ItemViewCellModel(appState, item));
			SelectedItem = Items.GetTranslatedValueOrNull(appState.SelectedItem);

			SelectItemCommand = new Command<ItemViewCellModel>(itemViewCellModel =>
			{
				var selectedItem = itemViewCellModel?.ToDoItem;
				appState.SelectItem(selectedItem);
			});
		}


		public TranslatingObservable<ToDoItem, ItemViewCellModel> Items { get; }


		private ItemViewCellModel _selectedItem;

		public ItemViewCellModel SelectedItem
		{
			get => _selectedItem;
			set => SetAndRaiseIfChanged(ref _selectedItem, value);
		}


		public Command<ItemViewCellModel> SelectItemCommand { get; }
	}
}
