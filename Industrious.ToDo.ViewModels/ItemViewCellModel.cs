using System;
using System.ComponentModel;
using System.Windows.Input;
using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class ItemViewCellModel : NotifyPropertyChanged, IDisposable
	{
		public ItemViewCellModel(AppState appState, ToDoItem toDoItem)
		{
			ToDoItem = toDoItem;
			ToDoItem.PropertyChanged += OnToDoItemPropertyChanged;
			OnToDoItemPropertyChanged(null, null);

			DeleteItemCommand = new Command(() =>
			{
				appState.DeleteItem(toDoItem);
			});

			ToggleCompleteCommand = new Command<Boolean>(value =>
			{
				toDoItem.IsComplete = value;
			});
		}


		private Boolean _isComplete;

		public Boolean IsComplete
		{
			get => _isComplete;
			set => SetAndRaiseIfChanged(ref _isComplete, value);
		}


		private String _title;

		public String Title
		{
			get => _title;
			private set
			{
				// Work around https://github.com/xamarin/Xamarin.Forms/issues/6118
				SetAndRaiseIfChanged(ref _title, !String.IsNullOrEmpty(value) ? value : " ");
			}
		}


		public ToDoItem ToDoItem { get; }


		public ICommand DeleteItemCommand { get; }


		public ICommand ToggleCompleteCommand { get; }


		public void Dispose()
		{
			// Called by ItemListViewModel TranslatingObservable
			ToDoItem.PropertyChanged -= OnToDoItemPropertyChanged;
		}


		private void OnToDoItemPropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			Title = ToDoItem.Title;
			IsComplete = ToDoItem.IsComplete;
		}
	}
}
