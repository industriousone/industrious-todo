using System;

namespace Industrious.ToDo.Forms
{
	/// <summary>
	///  The view factory is responsible for instantiating all of the various application
	///  views, and supplying them with any required dependencies.
	/// </summary>
	/// <remarks>
	///  I usually implement this interface with the main Application or AppDelegate class,
	///  since it often is already holding references to any dependencies required by the
	///  views.
	/// </remarks>
	public interface IViewFactory
	{
		ItemEditorPage NewItemEditorPage();

		ItemEditorView NewItemEditorView();

		ItemListView NewItemListView();

		NoItemSelectedView NewNoItemSelectedView();

		RootPage NewRootPage(Boolean isSplitView);

		SpinnerView NewSpinnerView();

		SplitView NewSplitView();
	}
}
