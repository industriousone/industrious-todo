using System;

using Xamarin.Forms;

namespace Industrious.ToDo.Forms
{
	public partial class ItemListPage : ContentPage
	{
		public ItemListPage()
		{
			InitializeComponent();
			NavigationPage.SetBackButtonTitle(this, "");
		}
	}
}
