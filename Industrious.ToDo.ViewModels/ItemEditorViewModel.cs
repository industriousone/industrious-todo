using System;
using System.ComponentModel;

using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class ItemEditorViewModel : NotifyPropertyChanged, IDisposable
	{
		private readonly AppState _appState;


		public ItemEditorViewModel(AppState appState)
		{
			_appState = appState;

			_appState.PropertyChanged += OnAppStatePropertyChanged;

			OnSelectedItemChanged(_appState.SelectedItem);

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


		private Boolean _shouldFocusTitle;

		public Boolean ShouldFocusTitle
		{
			get => _shouldFocusTitle;
			set => SetAndRaiseIfChanged(ref _shouldFocusTitle, value);
		}


		private String _title;

		public String Title
		{
			get => _title;
			set => SetAndRaiseIfChanged(ref _title, value);
		}


		public Command<String> ChangeNotesCommand { get; }


		public Command<String> ChangeTitleCommand { get; }


		public Command<Boolean> ToggleCompleteCommand { get; }


		public void Dispose()
		{
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
			OnToDoItemPropertyChanged(null, null);

			if (SelectedItem != null)
			{
				SelectedItem.PropertyChanged += OnToDoItemPropertyChanged;

				// TODO: Autofocus is causing page transitions to hitch on iOS 13; backing out for now (#13)
				//ShouldFocusTitle = (Title.Length == 0);
			}
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
