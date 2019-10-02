using System;
using Xamarin.Forms;

namespace Industrious.ToDo.Forms.Pages
{
	public partial class RootPage : ContentPage
	{
		public RootPage(Boolean isSplitView)
		{
			InitializeComponent();

			if (!isSplitView)
				ToolbarItems.Remove(DeleteItem);
		}
	}
}
