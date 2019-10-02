using System;
using Xamarin.Forms;

using Industrious.ToDo.ViewModels;

namespace Industrious.ToDo.Forms.Views
{
	public partial class ItemListView : ContentView
	{
		public ItemListView()
		{
			InitializeComponent();
		}


		public ItemListViewModel ViewModel => (ItemListViewModel)BindingContext;


		public void OnItemTapped(Object sender, ItemTappedEventArgs e)
		{
			var selectedItem = (ItemViewCellModel)e.Item;
			ViewModel.SelectItemCommand.Execute(selectedItem);
		}
	}
}

