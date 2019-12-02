using System;
using System.ComponentModel;

namespace Industrious.ToDo
{
	/// <summary>
	///  Monitors changes to the application's state and reports major "intents" to the user
	///  interface implementation, which responds by updating the on-screen views to match.
	/// </summary>
	/// <remarks>
	///  Traditionally I'd let the viewmodels drive the UI transitions, but that scatters
	///  navigation logic across multiple classes. Having a go with this application state
	///  driver approach to see if it improves maintainability. On the pro side: centralizes
	///  navigation, simplies and increases reusablity of viewmodels, persists UI layout with
	///  application state, enables UI automation. On the con side: could become unwieldy as
	///  application grows.
	/// </remarks>
	public class UiPresentationDriver
	{
		private readonly AppState _appState;
		private readonly IUiPresentation _uiPresentation;


		public UiPresentationDriver(AppState appState, IUiPresentation uiPresentation)
		{
			_appState = appState;
			_uiPresentation = uiPresentation;

			_appState.PropertyChanged += OnAppStatePropertyChanged;
		}


		private void OnAppStatePropertyChanged(Object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
			case nameof(AppState.RunState):
				OnRunStateChanged();
				break;

			case nameof(AppState.SelectedItem):
				if (_appState.RunState != RunState.Loading)
					OnSelectedItemChanged();
				break;
			}
		}


		private void OnRunStateChanged()
		{
			switch (_appState.RunState)
			{
			case RunState.Loading:
				// show a loading screen
				_uiPresentation.OnAppLoadingStarted();
				break;

			case RunState.Loaded:
				// show the main application interface
				_uiPresentation.OnAppLoadingComplete();
				OnSelectedItemChanged();
				break;
			}
		}


		private void OnSelectedItemChanged()
		{
			if (_appState.SelectedItem != null)
			{
				// show the item editor, loaded w/the selected item
				_uiPresentation.OnItemSelected();
			}
			else
			{
				// dismiss the editor, show "no item selected"
				_uiPresentation.OnItemSelectionCleared();
			}
		}
	}
}
