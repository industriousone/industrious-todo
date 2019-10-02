using System;

using Industrious.Mvvm;

namespace Industrious.ToDo.ViewModels
{
	public class ItemEditorPageModel
	{
		private readonly AppState _appState;


		public ItemEditorPageModel(AppState appState)
		{
			_appState = appState;

			AddItemCommand = new Command(() =>
			{
				var item = appState.AddNewItem();
				appState.SelectItem(item);
			});

			DeleteItemCommand = new Command(() =>
			{
				appState.DeleteItem(appState.SelectedItem);
			});
		}


		public Command AddItemCommand { get; }


		public Command DeleteItemCommand { get; }


		public void OnViewDisappearing()
		{
			// When the user hits the Back navigation button on the item editor page,
			// they are indicating that they are no longer interested in working with
			// that item any longer.
			_appState.SelectItem(null);
		}
	}
}
