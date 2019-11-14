using System;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace Industrious.ToDo.Forms
{
	public partial class App : Application
	{
		private const String SERIALIZATION_KEY = "AppState";

		private readonly AppState _appState;
		private readonly IUiPresentation _uiPresentation;
		private readonly UiPresentationDriver _presentationDriver;


		public App()
		{
			InitializeComponent();

			_appState = new AppState();
			_uiPresentation = ChoosePresentation();
			_presentationDriver = new UiPresentationDriver(_appState, _uiPresentation);

			// TODO: This should run async so loading screen isn't blocked
			_appState.RunState = RunState.Loading;
			RestoreState();
			_appState.RunState = RunState.Loaded;
		}


		private IUiPresentation ChoosePresentation()
		{
			switch (Device.Idiom)
			{
			case TargetIdiom.Tablet:
				return (new TabletPresentation(_appState));

			case TargetIdiom.Phone:
			default:
				return (new PhonePresentation(_appState));
			}
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
