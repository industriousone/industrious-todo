<p align="center">
  <a href="https://premake.github.io/" target="blank"><img src="Assets/icons8-checkmark.svg?raw=true" height="200" width="200" alt="Industrious.ToDo" /></a>
</p>

<p align="center">
	<a href="https://opensource.org/licenses/MIT" target="_blank">
        <img src="https://img.shields.io/github/license/industriousone/industrious-todo" alt="MIT" />
    </a>
    <a href="https://twitter.com/industrious" target="_blank">
        <img src="https://img.shields.io/twitter/follow/industrious.svg?style=social&label=Follow">
    </a>
</p>

# Industrious.ToDo (GottaDo)

An Industrious One take on the canonical to-do list example application.

I use this one to demonstrate how to use the various Industrious packages together in something resembling a real world application. It also makes a nice sandbox for experimenting with new approaches and ideas; you'll catch me playing here from time to time. I've tried to architect it as I would any "real" application to make it a meaningful testbed.

This current iteration is a "plain" MVVM approach using mutable model objects. Once I have everything working to my liking I'll introduce a [Redux](https://redux.js.org)-style unidirectional data flow with immutable objects. (I'm taking this approach to verify that the support libraries work property with both mutable and immutable objects.) I'm also experiementing with different approaches to state-based navigation controllers.

The Industrious packages in use so far (I have a few more on the way) include:

- [Industrious.Forms](https://github.com/industriousone/Industrious.Forms)
- [Industrious.Mvvm](https://github.com/industriousone/Industrious.Mvvm)

**Industrious.ToDo** is developed using [Xamarin.Forms](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/), and currently includes targets for iOS and Android.

<p align="center">
  <a href="Assets/ios-phone-list.png"><img src="Assets/ios-phone-list.png?raw=true" width="180"></a>
  <a href="Assets/ios-phone-edit.png"><img src="Assets/ios-phone-edit.png?raw=true" width="180"></a>
  <a href="Assets/android-phone-list.png"><img src="Assets/android-phone-list.png?raw=true" width="180"></a>
  <a href="Assets/android-phone-edit.png"><img src="Assets/android-phone-edit.png?raw=true" width="180"></a>
  <a href="Assets/ios-tablet.png"><img src="Assets/ios-tablet.png?raw=true" width="480"></a>
</p>

## Stay in touch

- Website - https://industriousone.com/
- Twitter - [@industrious](https://twitter.com/industrious)

## License

[MIT](https://opensource.org/licenses/MIT)

[Cloud Checkbox icon](https://icons8.com/icon/110574/checked-checkbox) and in-app icons by [Icons8](https://icons8.com).

## Side Notes

- `ItemViewCellModel.Title` setter has a workaround for [Xamarin.Forms issue #6118](https://github.com/xamarin/xamarin-macios/issues/6118)
