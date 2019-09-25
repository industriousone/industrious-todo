using System;
using Xunit;

namespace Industrious.ToDo.ViewModels.Tests
{
	public class ItemViewCellModelTests
	{
		private readonly ToDoItem[] TestItems =
		{
			new ToDoItem("Title", true)
		};

		private readonly AppState _state;
		private readonly ItemViewCellModel _sut;


		public ItemViewCellModelTests()
		{
			_state = new AppState(TestItems);
			_sut = new ItemViewCellModel(_state, TestItems[0]);
		}


		[Fact]
		public void Constructor_InitializesTitle_FromItemValue()
		{
			Assert.Equal("Title", _sut.Title);
		}


		[Fact]
		public void Constructor_InitializesIsComplete_FromItemValue()
		{
			Assert.True(_sut.IsComplete);
		}


		[Fact]
		public void DeleteItemCommand_DoesDeleteItem()
		{
			_sut.DeleteItemCommand.Execute(null);
			Assert.Empty(_state.Items);
		}


		[Fact]
		public void IsComplete_UpdatesWhenItemChanges()
		{
			TestItems[0].IsComplete = false;
			Assert.False(_sut.IsComplete);
		}


		[Fact]
		public void Title_UpdatesWhenItemChanges()
		{
			TestItems[0].Title = "New title";
			Assert.Equal("New title", _sut.Title);
		}
	}
}
