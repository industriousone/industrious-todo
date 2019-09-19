using System;
using Xamarin.Forms;

using Industrious.ToDo.ViewModels;

namespace Industrious.ToDo.Forms
{
	public partial class App : Application, IAppNavigator
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(CreateMainPage());
		}


		public Page CurrentPage => ((NavigationPage)MainPage).CurrentPage;


		public void ShowEditorPage()
		{
			if (!ShouldSplitScreen() && !(CurrentPage is ItemEditorPage))
			{
				MainPage.Navigation.PushAsync(new ItemEditorPage()
				{
					BindingContext = new ItemEditorViewModel()
				});
			}
		}


		private Page CreateMainPage()
		{
			return (ShouldSplitScreen())
				? CreateMainTwoColumnPage()
				: CreateMainOneColumnPage();
		}


		private Page CreateMainOneColumnPage()
		{
			return (new MainOneColumnPage()
			{
				BindingContext = new MainViewModel(this),
				Content = new ItemListView()
				{
					BindingContext = new ItemListViewModel(this)
				}
			});
		}


		private Page CreateMainTwoColumnPage()
		{
			return (new MainTwoColumnPage()
			{
				BindingContext = new MainViewModel(this),
				LeftContent = new ItemListView()
				{
					BindingContext = new ItemListViewModel(this)
				},
				RightContent = new ItemEditorView()
				{
					BindingContext = new ItemEditorViewModel()
				}
			});
		}


		/// <summary>
		///  Decide if the device screen is large enough to support the two-column view.
		/// </summary>
		private Boolean ShouldSplitScreen()
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
	}
}
