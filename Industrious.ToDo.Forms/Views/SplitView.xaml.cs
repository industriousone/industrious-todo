using System;
using Xamarin.Forms;

namespace Industrious.ToDo.Forms.Views
{
	public partial class SplitView : ContentView
	{
		public static BindableProperty LeftContentProperty = BindableProperty.Create(
			propertyName: nameof(LeftContent),
			returnType: typeof(View),
			declaringType: typeof(SplitView),
			defaultValue: default(View),
			defaultBindingMode: BindingMode.OneWay,
			propertyChanged: OnLeftContentPropertyChanged);

		public static BindableProperty RightContentProperty = BindableProperty.Create(
			propertyName: nameof(RightContent),
			returnType: typeof(View),
			declaringType: typeof(SplitView),
			defaultValue: default(View),
			defaultBindingMode: BindingMode.OneWay,
			propertyChanged: OnRightContentPropertyChanged);


		public SplitView()
		{
			InitializeComponent();
		}


		public View LeftContent
		{
			get => (View)base.GetValue(LeftContentProperty);
			set => base.SetValue(LeftContentProperty, value);
		}


		public View RightContent
		{
			get => (View)base.GetValue(RightContentProperty);
			set => base.SetValue(RightContentProperty, value);
		}


		static void OnLeftContentPropertyChanged(BindableObject bindable, Object oldValue, Object newValue)
		{
			((SplitView)bindable).LeftContentView.Content = (View)newValue;
		}


		static void OnRightContentPropertyChanged(BindableObject bindable, Object oldValue, Object newValue)
		{
			((SplitView)bindable).RightContentView.Content = (View)newValue;
		}
	}
}
