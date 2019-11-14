using System;
using Xunit;

namespace Industrious.ToDo.ViewModels.Tests
{
	public class ItemEditorPageModelTests
	{
		private readonly ToDoItem[] TestItems =
		{
			new ToDoItem("Incomplete Item", false),
			new ToDoItem("Complete Item", true, "With Notes"),
			new ToDoItem("Another Item", false)
		};

		private readonly AppState _state;


		public ItemEditorPageModelTests()
		{
			_state = new AppState(TestItems);
		}


		[Fact]
		public void AddItemCommand_DoesAddItem()
		{
			var sut = new ItemEditorPageModel(_state);
			sut.AddItemCommand.Execute(null);
			Assert.Equal(4, _state.Items.Count);
		}


		[Fact]
		public void DeleteItemCommand_DoesDeleteItem()
		{
			_state.SelectItem(TestItems[0]);

			var sut = new ItemEditorPageModel(_state);
			sut.DeleteItemCommand.Execute(null);
			Assert.DoesNotContain(TestItems[0], _state.Items);
		}


		[Fact]
		public void Dispose_DeselectsItem()
		{
			_state.SelectItem(TestItems[0]);

			var sut = new ItemEditorPageModel(_state);
			sut.Dispose();
			Assert.Null(_state.SelectedItem);
		}
	}
}
