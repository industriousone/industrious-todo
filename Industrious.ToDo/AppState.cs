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
			RunState = RunState.Starting;
			Items = new ObservableCollection<ToDoItem>();
		}


		public AppState(IEnumerable<ToDoItem> items)
		{
			RunState = RunState.Starting;
			Items = new ObservableCollection<ToDoItem>(items);
		}


		private ObservableCollection<ToDoItem> _items;

		public ObservableCollection<ToDoItem> Items
		{
			get => _items;
			private set => SetAndRaiseIfChanged(ref _items, value);
		}


		private RunState _runState;

		public RunState RunState
		{
			get => _runState;
			set => SetAndRaiseIfChanged(ref _runState, value);
		}


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


		public void SetItems(IEnumerable<ToDoItem> items)
		{
			Items = new ObservableCollection<ToDoItem>(items);
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


		public void Deserialize(Serialized serialized)
		{
			var items = serialized.Items.Select(ToDoItem.Deserialize);
			SetItems(items);

			SelectedItem = (serialized.SelectedItemIndex > -1)
				? Items[serialized.SelectedItemIndex]
				: null;
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
