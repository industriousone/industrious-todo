using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
	}
}
