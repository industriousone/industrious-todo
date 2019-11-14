using System;
using Xamarin.Forms;

using Industrious.ToDo.Forms.Pages;
using Industrious.ToDo.Forms.Views;
using Industrious.ToDo.ViewModels;

namespace Industrious.ToDo.Forms
{
	/// <summary>
	///  View builders for tablet devices (iPad, Tab). Uses a two-column split view
	///  to display the list of items and the editor at the same time.
	/// </summary>
	public class TabletPresentation : IUiPresentation
	{
		private readonly AppState _appState;
		private SplitView _splitView;
		private ItemEditorView _editorView;


		public TabletPresentation(AppState appState)
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
			var page = new TabletRootPage()
			{
				BindingContext = new RootPageModel(_appState),
				Content = new SplitView()
				{
					LeftContent = new ItemListView()
					{
						BindingContext = new ItemListViewModel(_appState)
					},
					RightContent = new NoItemSelectedView()
				}
			};

			_splitView = (SplitView)page.Content;

			Application.Current.MainPage = new NavigationPage(page);
		}


		public void OnItemSelected()
		{
			// display item editor in right pane, if it isn't already
			if (_editorView == null)
			{
				_editorView = new ItemEditorView()
				{
					BindingContext = new ItemEditorViewModel(_appState)
				};

				_splitView.RightContent = _editorView;
			}
		}


		public void OnItemSelectionCleared()
		{
			// dismiss item editor; display "no item selected" in right pane
			if (_editorView != null)
			{
				// TODO: Find a better way; view should be able to clean up after itself
				((ItemEditorViewModel)_editorView.BindingContext).Dispose();

				_editorView = null;
				_splitView.RightContent = new NoItemSelectedView();
			}
		}
	}
}
