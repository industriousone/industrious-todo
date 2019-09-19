using System;
using Xamarin.Forms;

using Industrious.ToDo.ViewModels;

namespace Industrious.ToDo.Forms
{
	public partial class ItemListView : ContentView
	{
		public ItemListView()
		{
			InitializeComponent();
		}


		public ItemListViewModel ViewModel => (ItemListViewModel)BindingContext;


		public void OnItemSelected(Object sender, SelectedItemChangedEventArgs e)
		{
			ViewModel.SelectItemCommand.Execute(null);
		}
	}
}

