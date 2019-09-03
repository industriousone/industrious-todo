using System;

using Foundation;
using UIKit;

namespace Industrious.ToDo.iOS
{
	[Register("AppDelegate")]
	public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
		{
			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new Industrious.ToDo.Forms.App());

			return base.FinishedLaunching(uiApplication, launchOptions);
		}
	}
}
