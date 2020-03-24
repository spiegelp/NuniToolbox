# Release notes
## NuniToolbox
### v1.0.0 (upcoming release)
#### Features
* Base classes for model objects
* Extension methods for `IEnumerable`
  * `Foreach`
  * `GroupByToDictionary`
  * `Sorted`
  * `ToSet`
* Extension methods for `ICollection`
  * `AddAll`
* Optimized version of `ObservableCollection` for bulk operations
* Translation of `enum` into display strings
* Extension methods for shallow and deep copy
* Different classes for date and time only
* Helper methods for weekdays, months and years
## NuniToolbox.Ui
### v1.0.0 (upcoming release)
#### Features
* Implementation of a simple command taking delegates for the `Execute` and `CanExecute` logic
* Base class for view model objects
* Converters
  * `BoolAndConverter`
  * `BoolAndToVisibilityConverter`
  * `BoolNotConverter`
  * `BoolOrConverter`
  * `BoolOrToVisibilityConverter`
  * `BoolToVisibilityConverter`
  * `ByteArrayToBitmapImageConverter`
  * `DateTimeOffsetToDateTimeConverter`
  * `EmptyCollectionToVisibilityConverter`
  * `EmptyEnumerableToBoolConverter`
  * `EmptyEnumerableToVisibilityConverter`
  * `IndexPlusOneConverter`
  * `NullToBoolConverter`
  * `NullToVisibilityConverter`
  * `UpperCaseConverter`
