using System;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms;

using Industrious.ToDo.ViewModels;

namespace Industrious.ToDo.Forms
{
	public partial class App : Application, IViewFactory
	{
		private const String SERIALIZATION_KEY = "AppState";

		private readonly AppState _appState;


		public App()
		{
			InitializeComponent();

			_appState = new AppState();

			var router = new FormsRouter(_appState, this);
			MainPage = router.InitialPage();

			_appState.RunState = RunState.Loading;
			RestoreState();
			_appState.RunState = RunState.Loaded;

			_appState.RunState = RunState.Running;
		}


		public ItemEditorPage NewItemEditorPage()
		{
			return (new ItemEditorPage()
			{
				BindingContext = new ItemEditorPageModel(_appState),
			});
		}


		public ItemEditorView NewItemEditorView()
		{
			return (new ItemEditorView()
			{
				BindingContext = new ItemEditorViewModel(_appState)
			});
		}


		public ItemListView NewItemListView()
		{
			return (new ItemListView()
			{
				BindingContext = new ItemListViewModel(_appState)
			});
		}


		public NoItemSelectedView NewNoItemSelectedView()
		{
			return (new NoItemSelectedView());
		}


		public RootPage NewRootPage(Boolean isSplitView)
		{
			return (new RootPage(isSplitView)
			{
				BindingContext = new RootPageModel(_appState)
			});
		}


		public SpinnerView NewSpinnerView()
		{
			return (new SpinnerView());
		}


		public SplitView NewSplitView()
		{
			return (new SplitView());
		}


		protected override void OnSleep()
		{
			SaveState();
			base.OnSleep();
		}


		private void RestoreState()
		{
			if (Properties.ContainsKey(SERIALIZATION_KEY))
			{
				var serializedString = (String)Properties[SERIALIZATION_KEY];

				XmlSerializer xmlSerializer = new XmlSerializer(typeof(AppState.Serialized));
				using (StringReader textReader = new StringReader(serializedString))
				{
					var serialized = (AppState.Serialized)xmlSerializer.Deserialize(textReader);
					_appState.Deserialize(serialized);
				}
			}
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
