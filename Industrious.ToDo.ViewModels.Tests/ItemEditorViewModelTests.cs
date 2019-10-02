using System;
using Xunit;

namespace Industrious.ToDo.ViewModels.Tests
{
	public class ItemEditorViewModelTests
	{
		private readonly ToDoItem[] TestItems =
		{
			new ToDoItem("Incomplete Item", false),
			new ToDoItem("Complete Item", true, "With Notes"),
			new ToDoItem("Another Item", false)
		};

		private readonly AppState _state;


		public ItemEditorViewModelTests()
		{
			_state = new AppState(TestItems);
		}


		[Fact]
		public void Constructor_InitializesIsComplete_ToSelectedItemValue()
		{
			_state.SelectItem(TestItems[1]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				Assert.Equal(TestItems[1].IsComplete, sut.IsComplete);
			}
		}


		[Fact]
		public void Constructor_InitializesNotes_ToSelectedItemValue()
		{
			_state.SelectItem(TestItems[1]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				Assert.Equal(TestItems[1].Notes, sut.Notes);
			}
		}


		[Fact]
		public void Constructor_InitializesTitle_ToSelectedItemValue()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				Assert.Equal(TestItems[0].Title, sut.Title);
			}
		}


		[Fact]
		public void Dispose_UnsubscribesFromAppStateUpdates()
		{
			_state.SelectItem(TestItems[0]);

			var sut = new ItemEditorViewModel(_state);
			sut.Dispose();

			_state.SelectItem(TestItems[1]);
			Assert.False(sut.IsComplete);
		}


		[Fact]
		public void Dispose_UnsubscribesFromToDoItemUpdates()
		{
			_state.SelectItem(TestItems[0]);

			var sut = new ItemEditorViewModel(_state);
			sut.Dispose();

			_state.Items[0].IsComplete = true;
			Assert.False(sut.IsComplete);
		}


		[Fact]
		public void IsComplete_RaisesPropertyChanged_WhenValueChanged()
		{
			using (var sut = new ItemEditorViewModel(_state))
			{
				String lastPropertyChanged = null;
				sut.PropertyChanged += (sender, e) => lastPropertyChanged = e.PropertyName;

				sut.IsComplete = !sut.IsComplete;
				Assert.Equal(nameof(ItemEditorViewModel.IsComplete), lastPropertyChanged);
			}
		}


		[Fact]
		public void IsComplete_Updates_WhenSelectedItemChanges()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				_state.SelectItem(TestItems[1]);
				Assert.Equal(TestItems[1].IsComplete, sut.IsComplete);
			}
		}


		[Fact]
		public void IsComplete_Updates_WhenSelectedItemIsCompleteChanges()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				_state.Items[0].IsComplete = true;
				Assert.True(sut.IsComplete);
			}
		}


		[Fact]
		public void Notes_RaisesPropertyChanged_WhenValueChanges()
		{
			using (var sut = new ItemEditorViewModel(_state))
			{
				String lastPropertyChanged = null;
				sut.PropertyChanged += (sender, e) => lastPropertyChanged = e.PropertyName;

				sut.Notes = "New notes";
				Assert.Equal(nameof(ItemEditorViewModel.Notes), lastPropertyChanged);
			}
		}


		[Fact]
		public void Notes_Updates_WhenSelectedItemChanges()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				_state.SelectItem(TestItems[1]);
				Assert.Equal(TestItems[1].Notes, sut.Notes);
			}
		}


		[Fact]
		public void Title_RaisesPropertyChanged_WhenValueChanges()
		{
			using (var sut = new ItemEditorViewModel(_state))
			{
				String lastPropertyChanged = null;
				sut.PropertyChanged += (sender, e) => lastPropertyChanged = e.PropertyName;

				sut.Title = "New title";
				Assert.Equal(nameof(ItemEditorViewModel.Title), lastPropertyChanged);
			}
		}


		[Fact]
		public void Title_Updates_WhenSelectedItemChanges()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				_state.SelectItem(TestItems[1]);
				Assert.Equal(TestItems[1].Title, sut.Title);
			}
		}


		[Fact]
		public void ChangeNotesCommand_UpdatesSelectedItemNotes()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				sut.ChangeNotesCommand.Execute("New notes");
				Assert.Equal("New notes", _state.SelectedItem.Notes);
			}
		}


		[Fact]
		public void ChangeTitleCommand_UpdatesSelectedItemTitle()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				sut.ChangeTitleCommand.Execute("New title");
				Assert.Equal("New title", _state.SelectedItem.Title);
			}
		}


		[Fact]
		public void ToggleCompleteCommand_UpdatesSelectedItemIsComplete()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				sut.ToggleCompleteCommand.Execute(true);
				Assert.True(_state.SelectedItem.IsComplete);
			}
		}


		[Fact]
		public void UnsubscribesFromItemUpdates_OnItemChanged()
		{
			_state.SelectItem(TestItems[0]);
			using (var sut = new ItemEditorViewModel(_state))
			{
				_state.SelectItem(TestItems[2]);
				_state.Items[0].IsComplete = true;
				Assert.False(sut.IsComplete);
			}
		}
	}
}
