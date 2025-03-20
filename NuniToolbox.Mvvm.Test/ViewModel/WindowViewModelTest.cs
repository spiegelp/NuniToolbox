using NuniToolbox.Mvvm.ViewModel;

namespace NuniToolbox.Mvvm.Test.ViewModel;

public class WindowViewModelTest
{
    public WindowViewModelTest() { }

    [Fact]
    public void Test_CurrentViewModel_Disposing_Ok()
    {
        MyViewModel oldViewModel = new();
        WindowViewModel windowViewModel = new(oldViewModel, true);

        Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

        MyViewModel newViewModel = new();
        windowViewModel.CurrentViewModel = newViewModel;

        Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
        Assert.True(oldViewModel.Disposed);
        Assert.False(newViewModel.Disposed);
    }

    [Fact]
    public void Test_CurrentViewModel_NotDisposing_Ok()
    {
        MyViewModel oldViewModel = new();
        WindowViewModel windowViewModel = new(oldViewModel, false); ;

        Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

        MyViewModel newViewModel = new();
        windowViewModel.CurrentViewModel = newViewModel;

        Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
        Assert.False(oldViewModel.Disposed);
        Assert.False(newViewModel.Disposed);
    }

    [Fact]
    public void Test_SetCurrentViewModel_Disposing_Ok()
    {
        MyViewModel oldViewModel = new();
        WindowViewModel windowViewModel = new() { CurrentViewModel = oldViewModel };

        Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

        MyViewModel newViewModel = new();
        windowViewModel.SetCurrentViewModel(newViewModel, true);

        Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
        Assert.True(oldViewModel.Disposed);
        Assert.False(newViewModel.Disposed);
    }

    [Fact]
    public void Test_SetCurrentViewModel_NotDisposing_Ok()
    {
        MyViewModel oldViewModel = new();
        WindowViewModel windowViewModel = new() { CurrentViewModel = oldViewModel };

        Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

        MyViewModel newViewModel = new();
        windowViewModel.SetCurrentViewModel(newViewModel, false);

        Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
        Assert.False(oldViewModel.Disposed);
        Assert.False(newViewModel.Disposed);
    }

    [Fact]
    public void Test_SetCurrentViewModel_NoChange_Ok()
    {
        MyViewModel oldViewModel = new();
        WindowViewModel windowViewModel = new() { CurrentViewModel = oldViewModel };

        string propertyName = null;

        windowViewModel.PropertyChanged += (sender, args) => propertyName = args.PropertyName;

        Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

        MyViewModel newViewModel = oldViewModel;
        windowViewModel.SetCurrentViewModel(newViewModel, false);

        Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
        Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);
        Assert.False(oldViewModel.Disposed);
        Assert.False(newViewModel.Disposed);
        Assert.Null(propertyName);
    }

    public class MyViewModel : ViewModelObject
    {
        public bool Disposed { get; private set; }

        public MyViewModel()
            : base()
        {
            Disposed = false;
        }

        public override void Dispose()
        {
            Disposed = true;

            base.Dispose();
        }
    }
}
