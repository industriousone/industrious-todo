using System;

namespace Industrious.ToDo.ViewModels
{
	public class ItemEditorViewModel
	{
		public ItemEditorViewModel()
		{
			IsItemSelected = true;
		}


		public Boolean IsItemSelected { get; }
	}
}
