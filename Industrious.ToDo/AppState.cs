using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Industrious.Mvvm;

namespace Industrious.ToDo
{
	public class AppState : NotifyPropertyChanged
	{
		public AppState()
		{
			Items = new ObservableCollection<ToDoItem>();
		}


		public AppState(IEnumerable<ToDoItem> items)
		{
			Items = new ObservableCollection<ToDoItem>(items);
		}


		public ObservableCollection<ToDoItem> Items { get; }


		private ToDoItem _selectedItem;

		public ToDoItem SelectedItem
		{
			get => _selectedItem;
			private set => SetAndRaiseIfChanged(ref _selectedItem, value);
		}


		public ToDoItem AddNewItem()
		{
			var newItem = new ToDoItem(String.Empty);
			Items.Insert(0, newItem);
			return (newItem);
		}


		public void DeleteItem(ToDoItem item)
		{
			Items.Remove(item);
			if (item == SelectedItem)
				SelectedItem = null;
		}


		public void SelectItem(ToDoItem item)
		{
			SelectedItem = item;
		}


		#region Serialization

		/// <summary>
		///  A simple data-only version for serialization.
		/// </summary>
		public class Serialized
		{
			public Int32 Version;
			public ToDoItem.Serialized[] Items;
			public Int32 SelectedItemIndex;
		}


		public static AppState Deserialize(Serialized serialized)
		{
			var items = serialized.Items.Select(ToDoItem.Deserialize);
			var state = new AppState(items);

			if (serialized.SelectedItemIndex > -1)
				state.SelectedItem = state.Items[serialized.SelectedItemIndex];

			return (state);
		}


		public Serialized Serialize()
		{
			return new Serialized
			{
				Version = 0,
				Items = Items.Select(item => item.Serialize()).ToArray(),
				SelectedItemIndex = (SelectedItem != null) ? Items.IndexOf(SelectedItem) : -1
			};
		}

		#endregion
	}
}
