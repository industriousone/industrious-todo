using System;
using System.ComponentModel;
using Xamarin.Forms;

using Industrious.ToDo.Forms.Pages;
using Industrious.ToDo.Forms.Views;

namespace Industrious.ToDo.Forms
{
	/// <summary>
	///  The application router is responsible for getting views on to and off of the
	///  screen in response to application and view model events.
	/// </summary>
	/// <remarks>
	///  This router is part of the Forms-specific code, so it is oriented toward the
	///  mobile device views.
	/// </remarks>
	public class FormsRouter
	{
		private readonly AppState _appState;
		private readonly IViewFactory _viewFactory;

		private NavigationPage _navigationPage;
		private RootPage _rootPage;
		private SplitView _splitView;
		private ItemEditorView _editorView;


		public FormsRouter(AppState appState, IViewFactory viewFactory)
		{
			_appState = appState;
			_viewFactory = viewFactory;

			appState.PropertyChanged += OnAppStatePropertyChanged;
		}


		/// <summary>
		///  Layout the main page and initial UI. Application state has not yet loaded
		///  at this point, so only "please wait" placeholder content is shown for now.
		/// </summary>
		public Page InitialPage()
		{
			_rootPage = _viewFactory.NewRootPage(IsSplitView());
			_rootPage.Content = _viewFactory.NewSpinnerView();

			_navigationPage = new NavigationPage(_rootPage);
			return (_navigationPage);
		}


		/// <summary>
		///  Decide if the device screen is large enough to support the two-column view.
		/// </summary>
		private Boolean IsSplitView()
		{
			switch (Device.Idiom)
			{
			case TargetIdiom.Tablet:
			case TargetIdiom.Desktop:
			case TargetIdiom.TV:
				return (true);

			default:
				return (false);
			}
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
			case RunState.Loaded:
				OnLoadComplete();
				break;
			}
		}


		/// <summary>
		///  The application state has finished loading; show the list and editor views.
		/// </summary>
		private void OnLoadComplete()
		{
			if (IsSplitView())
			{
				_splitView = _viewFactory.NewSplitView();
				_splitView.LeftContent = _viewFactory.NewItemListView();
				_splitView.RightContent = _viewFactory.NewNoItemSelectedView();
				_rootPage.Content = _splitView;
			}
			else
			{
				_rootPage.Content = _viewFactory.NewItemListView();
			}

			OnSelectedItemChanged();
		}


		private void OnSelectedItemChanged()
		{
			if (_appState.SelectedItem != null)
				OnItemSelected();
			else
				OnItemDeselected();
		}


		/// <summary>
		///  An item in the list was selected; show the item editor.
		/// </summary>
		private void OnItemSelected()
		{
			if (_editorView == null)
			{
				_editorView = _viewFactory.NewItemEditorView();
				if (IsSplitView())
				{
					_splitView.RightContent = _editorView;
				}
				else
				{
					var editorPage = _viewFactory.NewItemEditorPage();
					editorPage.Content = _editorView;
					_rootPage.Navigation.PushAsync(editorPage);
				}
			}
		}


		/// <summary>
		///  The item selection has been cleared; dismiss the editor if it is visible
		///  and return to a "no item selected" view.
		/// </summary>
		private void OnItemDeselected()
		{
			if (_editorView != null)
			{
				if (IsSplitView())
				{
					_splitView.RightContent = _viewFactory.NewNoItemSelectedView();
				}
				else
				{
					_rootPage.Navigation.PopAsync();
				}

				_editorView.Dispose();
				_editorView = null;
			}
		}
	}
}
