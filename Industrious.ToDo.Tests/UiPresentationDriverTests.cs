using System;
using Xunit;

namespace Industrious.ToDo.Tests
{
	public class UiPresentationDriverTests
	{
		private class MockUiPresentation : IUiPresentation
		{
			public String LastState;

			public void OnAppLoadingComplete()
			{
				LastState = "AppLoadingComplete";
			}

			public void OnAppLoadingStarted()
			{
				LastState = "AppLoadingStarted";
			}

			public void OnItemSelected()
			{
				LastState = "ItemSelected";
			}

			public void OnItemSelectionCleared()
			{
				LastState = "ItemSelectionCleared";
			}
		}


		[Fact]
		public void CallsOnLoadingStarting_OnRunStateLoading()
		{
			var state = new AppState();
			var presentation = new MockUiPresentation();
			var sut = new UiPresentationDriver(state, presentation);

			state.RunState = RunState.Loading;
			Assert.Equal("AppLoadingStarted", presentation.LastState);
		}


		[Fact]
		public void CallsOnLoadingComplete_OnRunStateLoaded()
		{
			var state = new AppState();
			var presentation = new MockUiPresentation();
			var sut = new UiPresentationDriver(state, presentation);

			state.RunState = RunState.Loaded;
			Assert.Equal("AppLoadingComplete", presentation.LastState);
		}


		[Fact]
		public void CallsOnItemSelected_OnSelectedItemSet()
		{
			var state = new AppState();
			var presentation = new MockUiPresentation();
			var sut = new UiPresentationDriver(state, presentation);

			state.SelectItem(new ToDoItem("Test"));

			Assert.Equal("ItemSelected", presentation.LastState);
		}


		[Fact]
		public void CallsOnItemSelectionCleared_OnSelectedItemCleared()
		{
			var state = new AppState();
			var presentation = new MockUiPresentation();
			var sut = new UiPresentationDriver(state, presentation);

			state.SelectItem(new ToDoItem("Test"));
			state.SelectItem(null);

			Assert.Equal("ItemSelectionCleared", presentation.LastState);
		}
	}
}
