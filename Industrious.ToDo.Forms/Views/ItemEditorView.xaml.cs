using System;
using Xamarin.Forms;

using Industrious.ToDo.ViewModels;

namespace Industrious.ToDo.Forms.Views
{
	public partial class ItemEditorView : ContentView
	{
		public ItemEditorView()
		{
			InitializeComponent();
		}


		public ItemEditorViewModel ViewModel => (ItemEditorViewModel)BindingContext;


		public void OnIsCompleteToggled(Object sender, ToggledEventArgs e)
		{
			ViewModel.ToggleCompleteCommand.Execute(e.Value);
		}


		public void OnNotesChanged(Object sender, TextChangedEventArgs eventArgs)
		{
			ViewModel.ChangeNotesCommand.Execute(eventArgs.NewTextValue);
		}


		public void OnTitleChanged(Object sender, TextChangedEventArgs e)
		{
			ViewModel.ChangeTitleCommand.Execute(e.NewTextValue);
		}
	}
}
