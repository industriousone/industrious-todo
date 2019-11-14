using System;

namespace Industrious.ToDo
{
	/// <summary>
	///  A platform-agnostic interface to the application's major UI interactions. This
	///  allows me to provide different implementations for phone- and table-sized devices,
	///  for example, and drive them all from application state transitions.
	/// </summary>
	/// <seealso cref="Industrious.ToDo.UiPresentationDriver"/>
	public interface IUiPresentation
	{
		/// <summary>
		///  The application has bootstrapped and is about to begin asynchronously loading
		///  its state. Show the user a "hold please" layout while the load completes.
		/// </summary>
		void OnAppLoadingStarted();


		/// <summary>
		///  The application's state is now loaded and everything is ready to go.
		/// </summary>
		void OnAppLoadingComplete();


		/// <summary>
		///  A to-do item has just been selected or added; show the item editor so the user
		///  can view the item's details and make changes.
		/// </summary>
		void OnItemSelected();


		/// <summary>
		///  The previous item selection has been cleared; dismiss the item editor.
		/// </summary>
		void OnItemSelectionCleared();
	}
}
