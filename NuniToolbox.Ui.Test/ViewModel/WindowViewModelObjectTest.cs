using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using NuniToolbox.Ui.ViewModel;

namespace NuniToolbox.Ui.Test.ViewModel
{
    public class WindowViewModelObjectTest
    {
        [Fact]
        public void Test_CurrentViewModel_Disposing_Ok()
        {
            MyViewModel oldViewModel = new MyViewModel();
            WindowViewModel windowViewModel = new WindowViewModel(oldViewModel, true);

            Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

            MyViewModel newViewModel = new MyViewModel();
            windowViewModel.CurrentViewModel = newViewModel;

            Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
            Assert.True(oldViewModel.Disposed);
            Assert.False(newViewModel.Disposed);
        }

        [Fact]
        public void Test_CurrentViewModel_NotDisposing_Ok()
        {
            MyViewModel oldViewModel = new MyViewModel();
            WindowViewModel windowViewModel = new WindowViewModel(oldViewModel, false); ;

            Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

            MyViewModel newViewModel = new MyViewModel();
            windowViewModel.CurrentViewModel = newViewModel;

            Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
            Assert.False(oldViewModel.Disposed);
            Assert.False(newViewModel.Disposed);
        }

        [Fact]
        public void Test_SetCurrentViewModel_Disposing_Ok()
        {
            MyViewModel oldViewModel = new MyViewModel();
            WindowViewModel windowViewModel = new WindowViewModel { CurrentViewModel = oldViewModel };

            Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

            MyViewModel newViewModel = new MyViewModel();
            windowViewModel.SetCurrentViewModel(newViewModel, true);

            Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
            Assert.True(oldViewModel.Disposed);
            Assert.False(newViewModel.Disposed);
        }

        [Fact]
        public void Test_SetCurrentViewModel_NotDisposing_Ok()
        {
            MyViewModel oldViewModel = new MyViewModel();
            WindowViewModel windowViewModel = new WindowViewModel { CurrentViewModel = oldViewModel };

            Assert.Equal(oldViewModel, windowViewModel.CurrentViewModel);

            MyViewModel newViewModel = new MyViewModel();
            windowViewModel.SetCurrentViewModel(newViewModel, false);

            Assert.Equal(newViewModel, windowViewModel.CurrentViewModel);
            Assert.False(oldViewModel.Disposed);
            Assert.False(newViewModel.Disposed);
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
}
