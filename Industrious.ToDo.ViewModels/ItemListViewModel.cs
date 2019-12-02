using System;
using System.ComponentModel;
using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class ItemListViewModel : NotifyPropertyChanged
	{
		private readonly AppState _appState;


		public ItemListViewModel(AppState appState)
		{
			_appState = appState;

			// Translate the AppState's ObservableCollection<ToDoItem> into an
			// ObservableCollection<ItemViewCellModel> which can then be used to populate a ListView
			Items = new TranslatingObservable<ToDoItem, ItemViewCellModel>(appState.Items, item => new ItemViewCellModel(appState, item));

			// Convert the AppState's SelectedItem from a ToDoItem to the corresponding ItemViewCellModel
			// so it can be bound to a ListView's SelectedItem property (the value needs to match the one
			// in the ObservableCollection<ItemViewCellModel> which is backing the ListView).
			SelectedItem = Items.GetTranslatedValueOrNull(appState.SelectedItem);

			// When the user selects an item in the list, update AppState accordingly
			SelectItemCommand = new Command<ItemViewCellModel>(itemViewCellModel =>
			{
				var selectedItem = itemViewCellModel?.ToDoItem;
				appState.SelectItem(selectedItem);
			});

			// When the AppState's SelectedItem changes, update the ListView selection accordingly
			appState.PropertyChanged += OnAppStatePropertyChanged;
		}


		public TranslatingObservable<ToDoItem, ItemViewCellModel> Items { get; }


		private ItemViewCellModel _selectedItem;

		public ItemViewCellModel SelectedItem
		{
			get => _selectedItem;
			set => SetAndRaiseIfChanged(ref _selectedItem, value);
		}


		public Command<ItemViewCellModel> SelectItemCommand { get; }


		private void OnAppStatePropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(AppState.SelectedItem))
				SelectedItem = Items.GetTranslatedValueOrNull(_appState.SelectedItem);
		}
	}
}
