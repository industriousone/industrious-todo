using System;
using Xunit;

namespace Industrious.ToDo.ViewModels.Tests
{
	public class MainViewModelTests
	{
		private readonly ToDoItem[] TestItems =
		{
			new ToDoItem("Incomplete Item", false),
			new ToDoItem("Complete Item", true, "With Notes"),
			new ToDoItem("Another Item", false)
		};

		private readonly AppState _state;


		public MainViewModelTests()
		{
			_state = new AppState(TestItems);
		}


		[Fact]
		public void AddItemCommand_DoesAddItem()
		{
			var sut = new MainViewModel(new MockNavigator(), _state);
			sut.AddItemCommand.Execute(null);
			Assert.Equal(4, _state.Items.Count);
		}


		[Fact]
		public void AddItemCommand_NavigatesToEditorPage()
		{
			var navigator = new MockNavigator();
			var sut = new MainViewModel(navigator, _state);
			sut.AddItemCommand.Execute(null);
			Assert.True(navigator.IsEditorVisible);
		}


		[Fact]
		public void DeleteItemCommand_DoesDeleteItem()
		{
			_state.SelectItem(TestItems[0]);

			var sut = new MainViewModel(new MockNavigator(), _state);
			sut.DeleteItemCommand.Execute(null);
			Assert.DoesNotContain(TestItems[0], _state.Items);
		}


		[Fact]
		public void DeleteItemCommand_DoesDisable_WhenNoItemSelected()
		{
			_state.SelectItem(TestItems[0]);
			var sut = new MainViewModel(new MockNavigator(), _state);

			_state.SelectItem(null);
			Assert.False(sut.DeleteItemCommand.CanExecute(null));
		}


		[Fact]
		public void DeleteItemCommand_DoesEnable_WhenNoItemSelected()
		{
			var sut = new MainViewModel(new MockNavigator(), _state);
			_state.SelectItem(TestItems[0]);
			Assert.True(sut.DeleteItemCommand.CanExecute(null));
		}
	}
}
