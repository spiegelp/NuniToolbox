using NuniToolbox.Mvvm.ViewModel;

namespace NuniToolbox.Mvvm.Test.ViewModel;

public class SelectableViewModelTest
{
    public SelectableViewModelTest() { }

    [Fact]
    public void Test_PropertyChanged_Ok()
    {
        SelectableViewModel<int> selectableViewModel = new(2, false);

        Assert.Equal(2, selectableViewModel.Item);
        Assert.False(selectableViewModel.IsSelected);

        string propertyName = null;

        selectableViewModel.PropertyChanged += (sender, args) => propertyName = args.PropertyName;

        selectableViewModel.Item = 4;

        Assert.Equal(nameof(selectableViewModel.Item), propertyName);
        Assert.Equal(4, selectableViewModel.Item);

        selectableViewModel.IsSelected = true;

        Assert.Equal(nameof(selectableViewModel.IsSelected), propertyName);
        Assert.True(selectableViewModel.IsSelected);
    }
}
