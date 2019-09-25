using System;
using System.ComponentModel;
using System.Windows.Input;

using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class ItemEditorViewModel : NotifyPropertyChanged, IDisposable
	{
		private readonly AppState _appState;


		public ItemEditorViewModel(IAppNavigator appNavigator, AppState appState)
		{
			_appState = appState;
			_appState.PropertyChanged += OnAppStatePropertyChanged;

			OnSelectedItemChanged(_appState.SelectedItem);

			AddItemCommand = new Command(() =>
			{
				var item = appState.AddNewItem();
				appState.SelectItem(item);
				appNavigator.ShowEditorPage();
			});

			ChangeNotesCommand = new Command<String>(value =>
			{
				if (SelectedItem != null)
					SelectedItem.Notes = value;
			});

			ChangeTitleCommand = new Command<String>(value =>
			{
				if (SelectedItem != null)
					SelectedItem.Title = value;
			});

			DeleteItemCommand = new Command(() =>
			{
				appState.DeleteItem(SelectedItem);
				appNavigator.DismissEditorPage();
			});

			ToggleCompleteCommand = new Command<Boolean>(value =>
			{
				if (SelectedItem != null)
					SelectedItem.IsComplete = value;
			});
		}


		private Boolean _isComplete;

		public Boolean IsComplete
		{
			get => _isComplete;
			set => SetAndRaiseIfChanged(ref _isComplete, value);
		}


		private String _notes;

		public String Notes
		{
			get => _notes;
			set => SetAndRaiseIfChanged(ref _notes, value);
		}


		private ToDoItem _selectedItem;

		public ToDoItem SelectedItem
		{
			get => _selectedItem;
			private set => SetAndRaiseIfChanged(ref _selectedItem, value);
		}


		private String _title;

		public String Title
		{
			get => _title;
			set => SetAndRaiseIfChanged(ref _title, value);
		}


		public ICommand AddItemCommand { get; }


		public ICommand ChangeNotesCommand { get; }


		public ICommand ChangeTitleCommand { get; }


		public ICommand DeleteItemCommand { get; }


		public ICommand ToggleCompleteCommand { get; }


		public void Dispose()
		{
			// Called by ItemEditorPage.OnDisappearing()
			if (_appState != null)
				_appState.PropertyChanged -= OnAppStatePropertyChanged;

			if (_selectedItem != null)
				_selectedItem.PropertyChanged -= OnToDoItemPropertyChanged;
		}


		private void OnAppStatePropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
			case nameof(AppState.SelectedItem):
				OnSelectedItemChanged(_appState.SelectedItem);
				break;
			}
		}


		private void OnSelectedItemChanged(ToDoItem newSelectedItem)
		{
			if (SelectedItem != null)
				SelectedItem.PropertyChanged -= OnToDoItemPropertyChanged;

			SelectedItem = newSelectedItem;

			if (SelectedItem != null)
				SelectedItem.PropertyChanged += OnToDoItemPropertyChanged;


			OnToDoItemPropertyChanged(null, null);
		}


		private void OnToDoItemPropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			if (SelectedItem != null)
			{
				Title = SelectedItem.Title ?? String.Empty;
				Notes = SelectedItem.Notes ?? String.Empty;
				IsComplete = SelectedItem.IsComplete;
			}
			else
			{
				Title = String.Empty;
				Notes = String.Empty;
				IsComplete = false;
			}
		}
	}
}
