using System;
using Xamarin.Forms;

using Industrious.ToDo.ViewModels;


namespace Industrious.ToDo.Forms
{
	public partial class ItemEditorPage : ContentPage
	{
		public ItemEditorPage()
		{
			InitializeComponent();
		}


		protected override void OnDisappearing()
		{
			((ItemEditorPageModel)BindingContext).OnViewDisappearing();
		}
	}
}
