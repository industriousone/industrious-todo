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


		public ItemEditorViewModel ViewModel => (ItemEditorViewModel)BindingContext;


		protected override void OnDisappearing()
		{
			ViewModel.Dispose();
		}
	}
}
