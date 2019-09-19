using System;

using Xamarin.Forms;

namespace Industrious.ToDo.Forms
{
	public partial class MainOneColumnPage : ContentPage
	{
		public MainOneColumnPage()
		{
			InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, "");
		}
	}
}
