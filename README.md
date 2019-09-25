<p align="center">
  <a href="https://premake.github.io/" target="blank"><img src="https://github.com/industriousone/industrious-todo/blob/master/Assets/icons8-checkmark.svg" height="200" width="200" alt="Premake" /></a>
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

Industrious One's take on the canonical to-do list example application.

Demonstrates how to use the various Industrious packages together in something resembling a real-world application, as well as my ability to create a canonical to-do list example. The packages in use so far include:

- [Industrious.Forms](https://github.com/industriousone/Industrious.Forms)
- [Industrious.Mvvm](https://github.com/industriousone/Industrious.Mvvm)

This current iteration is a simple MVVM-based approach using mutable model objects. Once I have everything working to my liking I'll introduce a [Redux](https://redux.js.org)-style unidirectional data flow with immutable objects. I'm taking this approach to verify that the support libraries work property with both mutable and immutable objects.

**Industrious.ToDo** is developed using [Xamarin.Forms](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/), and currently includes targets for iOS and Android.

## Stay in touch

- Website - https://industriousone.com
- Twitter - [@industrious](https://twitter.com/industrious)

## License

[MIT](https://opensource.org/licenses/MIT)

[Cloud Checkbox icon](https://icons8.com/icon/110574/checked-checkbox) and in-app icons by [Icons8](https://icons8.com).

## Side Notes

- `ItemViewCellModel.Title` setter has a workaround for [Xamarin.Forms issue #6118](https://github.com/xamarin/xamarin-macios/issues/6118)
