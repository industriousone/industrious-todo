using System;
using System.Collections.Generic;

using Industrious.Mvvm;

namespace Industrious.ToDo
{
	public class ToDoItem : NotifyPropertyChanged, IEquatable<ToDoItem>
	{
		private String _title;
		private String _notes;
		private Boolean _isComplete;


		public ToDoItem(String title, Boolean isComplete = false, String notes = null)
			: this(Guid.NewGuid(), title, isComplete, notes)
		{ }


		public ToDoItem(ToDoItem itemToCopy)
			: this(itemToCopy.ID, itemToCopy.Title, itemToCopy.IsComplete, itemToCopy.Notes)
		{ }


		private ToDoItem(Guid id, String title, Boolean isComplete, String notes)
		{
			ID = id;
			_title = title;
			_notes = notes;
			_isComplete = isComplete;
		}


		public Guid ID { get; }


		public String Title
		{
			get => _title;
			set => SetAndRaiseIfChanged(ref _title, value);
		}


		public String Notes
		{
			get => _notes;
			set => SetAndRaiseIfChanged(ref _notes, value);
		}


		public Boolean IsComplete
		{
			get => _isComplete;
			set => SetAndRaiseIfChanged(ref _isComplete, value);
		}


		#region IEquatable

		public bool Equals(ToDoItem other)
		{
			if (other is null)
				return (false);

			return (
				ID == other.ID &&
				Title == other.Title &&
				Notes == other.Notes &&
				IsComplete == other.IsComplete
			);
		}


		public override bool Equals(Object obj)
		{
			return this.Equals(obj as ToDoItem);
		}


		public override int GetHashCode()
		{
			var hashCode = -612858477;
			hashCode = hashCode * -1521134295 + ID.GetHashCode();
			hashCode = hashCode * -1521134295 + Title.GetHashCode();
			hashCode = hashCode * -1521134295 + Notes.GetHashCode();
			hashCode = hashCode * -1521134295 + IsComplete.GetHashCode();
			return hashCode;
		}


		public static bool operator ==(ToDoItem item1, ToDoItem item2)
		{
			return EqualityComparer<ToDoItem>.Default.Equals(item1, item2);
		}


		public static bool operator !=(ToDoItem item1, ToDoItem item2)
		{
			return !(item1 == item2);
		}

		#endregion

		#region Serialization

		/// <summary>
		///  A simple data-only version for serialization.
		/// </summary>
		public class Serialized
		{
			public Int32 Version;
			public Guid ID;
			public String Title;
			public String Notes;
			public Boolean IsComplete;
		}


		public static ToDoItem Deserialize(Serialized serialized)
		{
			var item = new ToDoItem(serialized.ID, serialized.Title, serialized.IsComplete, serialized.Notes);
			return (item);
		}


		public Serialized Serialize()
		{
			return new Serialized
			{
				Version = 0,
				ID = ID,
				Title = Title,
				Notes = Notes,
				IsComplete = IsComplete
			};
		}

		#endregion
	}
}
