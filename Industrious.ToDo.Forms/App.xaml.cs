using System;
using Xamarin.Forms;

using Industrious.ToDo.ViewModels;
using System.Xml.Serialization;
using System.IO;

namespace Industrious.ToDo.Forms
{
	public partial class App : Application, IAppNavigator
	{
		private const String SERIALIZATION_KEY = "AppState";

		private readonly AppState _appState;


		public App()
		{
			InitializeComponent();

			_appState = RestoreState() ?? new AppState();

			MainPage = new NavigationPage(CreateMainPage());
		}


		public ContentPage CurrentPage => (ContentPage)((NavigationPage)MainPage).CurrentPage;


		public void DismissEditor()
		{
			if (IsSplitScreen)
			{
				var splitView = (SplitView)CurrentPage.Content;
				splitView.RightContent = new NoItemSelectedView();
			}
			else if (CurrentPage is ItemEditorPage)
			{
				MainPage.Navigation.PopAsync();
			}
		}


		public void ShowEditor()
		{
			if (IsSplitScreen)
			{
				var splitView = (SplitView)CurrentPage.Content;
				if (!(splitView.RightContent is ItemEditorView))
				{
					splitView.RightContent = new ItemEditorView()
					{
						BindingContext = new ItemEditorViewModel(this, _appState)
					};
				}
			}
			else if (!(CurrentPage is ItemEditorPage))
			{
				MainPage.Navigation.PushAsync(new ItemEditorPage()
				{
					BindingContext = new ItemEditorViewModel(this, _appState)
				});
			}
		}


		protected override void OnSleep()
		{
			SaveState();
			base.OnSleep();
		}


		private Page CreateMainPage()
		{
			return (IsSplitScreen)
				? CreateMainTwoColumnPage()
				: CreateMainOneColumnPage();
		}


		private Page CreateMainOneColumnPage()
		{
			return (new ItemListPage()
			{
				BindingContext = new MainPageViewModel(this, _appState),
				Content = new ItemListView()
				{
					BindingContext = new ItemListViewModel(this, _appState)
				}
			});
		}


		private Page CreateMainTwoColumnPage()
		{
			return (new SplitViewPage()
			{
				BindingContext = new MainPageViewModel(this, _appState),
				LeftContent = new ItemListView()
				{
					BindingContext = new ItemListViewModel(this, _appState)
				},
				RightContent = new NoItemSelectedView()
			});
		}


		/// <summary>
		///  Decide if the device screen is large enough to support the two-column view.
		/// </summary>
		private Boolean IsSplitScreen
		{
			get
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


		private AppState RestoreState()
		{
			if (Properties.ContainsKey(SERIALIZATION_KEY))
			{
				var serializedString = (String)Properties[SERIALIZATION_KEY];

				XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppState.Serialized));
				using (StringReader textReader = new StringReader(serializedString))
				{
					var serialized = (AppState.Serialized)xmlSerializer.Deserialize(textReader);
					return (AppState.Deserialize(serialized));
				}
			}

			return (null);
		}


		private void SaveState()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppState.Serialized));
			using (StringWriter textWriter = new StringWriter())
			{
				xmlSerializer.Serialize(textWriter, _appState.Serialize());
				Properties[SERIALIZATION_KEY] = textWriter.ToString();
			}
		}
	}
}
