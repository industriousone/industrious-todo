using System;
using Xunit;

namespace Industrious.ToDo.Tests
{
	public class ToDoItemTests
	{
		[Fact]
		public void Constructor_InitializesAllFields()
		{
			var sut = new ToDoItem("My title", true, "Some notes");
			Assert.Equal("My title", sut.Title);
			Assert.True(sut.IsComplete);
			Assert.Equal("Some notes", sut.Notes);
		}


		[Fact]
		public void Equals_IsTrue_OnAllFieldsEqual()
		{
			var sut1 = new ToDoItem("Title1", false, null);
			var sut2 = new ToDoItem(sut1);
			Assert.Equal(sut1, sut2);
		}


		[Fact]
		public void Equals_IsFalse_WhenIdsDiffer()
		{
			var sut1 = new ToDoItem("Title1", false, null);
			var sut2 = new ToDoItem("Title1", false, null);
			Assert.NotEqual(sut1, sut2);
		}


		[Fact]
		public void Equals_IsFalse_WhenTitlesDiffer()
		{
			var sut1 = new ToDoItem("Title1", false, null);
			var sut2 = new ToDoItem(sut1)
			{
				Title = "Title2"
			};
			Assert.NotEqual(sut1, sut2);
		}


		[Fact]
		public void Equals_IsFalse_WhenCompletionDiffers()
		{
			var sut1 = new ToDoItem("Title1", false, null);
			var sut2 = new ToDoItem(sut1)
			{
				IsComplete = true
			};
			Assert.NotEqual(sut1, sut2);
		}


		[Fact]
		public void Equals_IsFalse_WhenNotesDiffer()
		{
			var sut1 = new ToDoItem("Title1", false, null);
			var sut2 = new ToDoItem(sut1)
			{
				Notes = "Other notes"
			};
			Assert.NotEqual(sut1, sut2);
		}
	}
}

