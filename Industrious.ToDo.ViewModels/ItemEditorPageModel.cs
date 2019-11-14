using System;

namespace Industrious.ToDo.ViewModels
{
	public class ItemEditorPageModel : RootPageModel, IDisposable
	{
		private readonly AppState _appState;


		public ItemEditorPageModel(AppState appState)
			: base(appState)
		{
			_appState = appState;
		}


		/// <summary>
		///  Deselect the currently selected item when the page is dismissed.
		/// </summary>
		/// <remarks>
		///  Showing a ListView selection on phones doesn't really make sense. Unlike
		///  the other platforms only the list is shown, not the editor, so there is
		///  no context to make the selection meaningful.
		/// </remarks>
		public void Dispose()
		{
			_appState.SelectItem(null);
		}
	}
}
