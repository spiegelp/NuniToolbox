# NuniToolbox (.NET Universal Toolbox)
<p align="center">
  <img src="https://raw.githubusercontent.com/spiegelp/NuniToolbox/master/icon/icon.png" alt="NuniToolbox icon" width="128px" />
</p>
<p align="center">
  NuniToolbox is a collection of common functions for the lazy programmer.
</p>
<p align="center">
  [![Build status](https://dev.azure.com/spiegelp/NuniToolbox/_apis/build/status/NuniToolbox-all)](https://dev.azure.com/spiegelp/NuniToolbox/_build/latest?definitionId=3)
  [![NuGet Status](https://img.shields.io/nuget/v/NuniToolbox.svg?style=flat&label=NuniToolbox&logo=nuget&color=blue)](https://www.nuget.org/packages/NuniToolbox/)
  [![NuGet Status](https://img.shields.io/nuget/v/NuniToolbox.Ui.svg?style=flat&label=NuniToolbox.Ui&logo=nuget&color=blue)](https://www.nuget.org/packages/NuniToolbox.Ui/)
</p>

## APIs

### NuniToolbox
* Collections
  * Extension methods on IEnumerable and ICollection for iterating, grouping, sorting, shuffling and adding items
  * `ExtendedObservableCollection` class for optimized adding of many items or replacing all items with only one changed event at the end of the operation
* Enum
  * Support for translating enum values into display strings
* Model
  * Base classes for model objects
* Objects
  * Extension methods for shallow and deep copy
* Time
  * Different classes for date and time only
  * Helper methods for weekdays, months and years

### NuniToolbox.Ui
* Commands
  * Implementation of a simple command taking delegates for the `Execute` and `CanExecute` logic
* Converters
  * Boolean algebra
  * `null` to `bool` or `Visibility`
  * Empty collection to `bool` or `Visibility`
  * Common conversions for displaying purposes
* ViewModel
  * Base class for view model objects
  * Ready to use view model for selections
  * Special view model for window content

## License
NuniToolbox is licensed under the [MIT license](https://github.com/spiegelp/NuniToolbox/blob/master/LICENSE).
