using System;
using Xunit;

namespace Industrious.ToDo.Tests
{
	public class AppStateTests
	{
		private readonly ToDoItem[] TestItems =
		{
			new ToDoItem("Incomplete Item", false),
			new ToDoItem("Complete Item", true)
		};


		[Fact]
		public void Constructor_InitializesItems_WhenNoItemsProvided()
		{
			var sut = new AppState();
			Assert.Empty(sut.Items);
		}


		[Fact]
		public void Constructor_InitializesItems_WhenItemsProvided()
		{
			var sut = new AppState(TestItems);
			Assert.Equal(TestItems, sut.Items);
		}


		[Fact]
		public void AddNewItem_DoesAddItem()
		{
			var sut = new AppState();
			sut.AddNewItem();
			Assert.Single(sut.Items);
		}


		[Fact]
		public void DeleteItem_DoesDeleteItem()
		{
			var sut = new AppState(TestItems);
			sut.DeleteItem(TestItems[0]);
			Assert.DoesNotContain(TestItems[0], sut.Items);
		}


		[Fact]
		public void DeleteItem_ClearsSelectedItem_IfSelectedItemDeleted()
		{
			var sut = new AppState(TestItems);
			sut.SelectItem(TestItems[0]);
			sut.DeleteItem(TestItems[0]);
			Assert.Null(sut.SelectedItem);

		}


		[Fact]
		public void DeleteItem_DoesNotClearSelectedItem_IfSelectedItemNotDeleted()
		{
			var sut = new AppState(TestItems);
			sut.SelectItem(TestItems[0]);
			sut.DeleteItem(TestItems[1]);
			Assert.Equal(TestItems[0], sut.SelectedItem);
		}


		[Fact]
		public void SelectItem_RaisesSelectedItemChangedNotification()
		{
			var sut = new AppState(TestItems);

			String lastPropertyChanged = null;
			sut.PropertyChanged += (sender, e) => lastPropertyChanged = e.PropertyName;

			sut.SelectItem(TestItems[0]);

			Assert.Equal(nameof(AppState.SelectedItem), lastPropertyChanged);
		}
	}
}
