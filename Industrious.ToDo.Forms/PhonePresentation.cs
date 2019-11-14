using System;
using Xamarin.Forms;

using Industrious.ToDo.Forms.Pages;
using Industrious.ToDo.Forms.Views;
using Industrious.ToDo.ViewModels;

namespace Industrious.ToDo.Forms
{
	/// <summary>
	///  View builders for phone-sized devices (iPhone, Galaxy). Uses a separate page and
	///  push navigation for the editor when an item is selected.
	/// </summary>
	public class PhonePresentation : IUiPresentation
	{
		private readonly AppState _appState;
		private PhoneRootPage _rootPage;
		private ItemEditorView _editorView;


		public PhonePresentation(AppState appState)
		{
			_appState = appState;
		}


		public void OnAppLoadingStarted()
		{
			// show a loading page
			Application.Current.MainPage = new NavigationPage(new LoadingPage());
		}


		public void OnAppLoadingComplete()
		{
			// show the main screen
			_rootPage = new PhoneRootPage()
			{
				BindingContext = new RootPageModel(_appState),
				Content = new ItemListView()
				{
					BindingContext = new ItemListViewModel(_appState)
				}
			};

			Application.Current.MainPage = new NavigationPage(_rootPage);
		}


		public void OnItemSelected()
		{
			// navigate to the item editor, if I'm not there already
			if (_editorView == null)
			{
				_editorView = new ItemEditorView()
				{
					BindingContext = new ItemEditorViewModel(_appState)
				};

				var page = new ItemEditorPage()
				{
					BindingContext = new ItemEditorPageModel(_appState),
					Content = _editorView
				};

				_rootPage.Navigation.PushAsync(page);
			}
		}


		public void OnItemSelectionCleared()
		{
			// dismiss the item editor; return to the main screen
			if (_editorView != null)
			{
				// TODO: Find a better way; view should be able to clean up after itself
				((ItemEditorViewModel)_editorView.BindingContext).Dispose();

				_editorView = null;
				_rootPage.Navigation.PopAsync();
			}
		}
	}
}
