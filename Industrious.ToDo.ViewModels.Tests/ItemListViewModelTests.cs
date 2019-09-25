using System;
using Xunit;

namespace Industrious.ToDo.ViewModels.Tests
{
	public class ItemListViewModelTests
	{
		private static readonly ToDoItem[] TestItems =
		{
			new ToDoItem("Incomplete Item", false),
			new ToDoItem("Complete Item", true)
		};

		private readonly AppState _state = new AppState(TestItems);


		[Fact]
		public void Constructor_InitializesItems_ToAppStateValue()
		{
			var sut = new ItemListViewModel(new MockNavigator(), _state);
			Assert.Equal(TestItems.Length, sut.Items.Count);
		}


		[Fact]
		public void SelectItemCommand_DoesSelectItem()
		{
			var sut = new ItemListViewModel(new MockNavigator(), _state);
			using (var itemViewModel = new ItemViewCellModel(_state, TestItems[0]))
			{
				sut.SelectItemCommand.Execute(itemViewModel);
				Assert.Equal(TestItems[0], _state.SelectedItem);
			}
		}


		[Fact]
		public void SelectItemCommand_NavigatesToEditor()
		{
			var navigator = new MockNavigator()
			{
				IsEditorVisible = false
			};

			var sut = new ItemListViewModel(navigator, _state);
			using (var itemViewModel = new ItemViewCellModel(_state, TestItems[0]))
			{
				sut.SelectItemCommand.Execute(itemViewModel);
				Assert.True(navigator.IsEditorVisible);
			}
		}
	}
}
