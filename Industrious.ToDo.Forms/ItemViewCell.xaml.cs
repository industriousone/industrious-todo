using System;
using Xamarin.Forms;

using Industrious.ToDo.ViewModels;

namespace Industrious.ToDo.Forms
{
	public partial class ItemViewCell : ViewCell
	{
		public ItemViewCell()
		{
			InitializeComponent();
		}


		public ItemViewCellModel ViewModel => (ItemViewCellModel)BindingContext;


		public void OnIsCompleteToggled(Object sender, ToggledEventArgs e)
		{
			ViewModel?.ToggleCompleteCommand.Execute(e.Value);
		}
	}
}

