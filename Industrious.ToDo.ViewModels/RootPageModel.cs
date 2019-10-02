using System;
using System.ComponentModel;
using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class RootPageModel
	{
		private readonly AppState _appState;

		public RootPageModel(AppState appState)
		{
			_appState = appState;
			_appState.PropertyChanged += OnAppStatePropertyChanged;

			AddItemCommand = new Command(() =>
			{
				var item = appState.AddNewItem();
				appState.SelectItem(item);
			});

			DeleteItemCommand = new Command(
				() => appState.DeleteItem(appState.SelectedItem),
				() => (appState.SelectedItem != null)
			);
		}


		public Command AddItemCommand { get; }


		public Command DeleteItemCommand { get; }


		private void OnAppStatePropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
			case nameof(AppState.SelectedItem):
				DeleteItemCommand.RaiseCanExecuteChanged();
				break;
			}
		}
	}
}
