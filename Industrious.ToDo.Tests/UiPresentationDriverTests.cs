using System;
using System.Collections.Generic;
using Xunit;

namespace Industrious.ToDo.Tests
{
	public class UiPresentationDriverTests
	{
		private class MockUiPresentation : IUiPresentation
		{
			public List<String> StatesEncounted = new List<String>();

			public void OnAppLoadingComplete()
			{
				StatesEncounted.Add("AppLoadingComplete");
			}

			public void OnAppLoadingStarted()
			{
				StatesEncounted.Add("AppLoadingStarted");
			}

			public void OnItemSelected()
			{
				StatesEncounted.Add("ItemSelected");
			}

			public void OnItemSelectionCleared()
			{
				StatesEncounted.Add("ItemSelectionCleared");
			}
		}


		[Fact]
		public void OnAppLoadingStarted_IsCalled_WhenRunStateLoading()
		{
			var state = new AppState();
			var presentation = new MockUiPresentation();
			var sut = new UiPresentationDriver(state, presentation);
			state.RunState = RunState.Loading;
			Assert.Equal(new String[] { "AppLoadingStarted" }, presentation.StatesEncounted);
		}


		[Fact]
		public void OnAppLoadingComplete_IsCalled_WhenRunStateLoaded()
		{
			var state = new AppState();
			var presentation = new MockUiPresentation();
			var sut = new UiPresentationDriver(state, presentation);
			state.RunState = RunState.Loaded;
			Assert.Equal(new String[] { "AppLoadingComplete", "ItemSelectionCleared" }, presentation.StatesEncounted);
		}


		[Fact]
		public void OnItemSelected_IsCalled_WhenItemSelected()
		{
			var state = new AppState();
			var presentation = new MockUiPresentation();
			var sut = new UiPresentationDriver(state, presentation);
			state.SelectItem(new ToDoItem("Test"));
			Assert.Equal(new String[] { "ItemSelected" }, presentation.StatesEncounted);
		}


		[Fact]
		public void CallsOnItemSelectionCleared_OnSelectedItemCleared()
		{
			var state = new AppState();
			var presentation = new MockUiPresentation();
			var sut = new UiPresentationDriver(state, presentation);
			state.SelectItem(new ToDoItem("Test"));
			state.SelectItem(null);
			Assert.Equal(new String[] { "ItemSelected", "ItemSelectionCleared" }, presentation.StatesEncounted);
		}
	}
}
