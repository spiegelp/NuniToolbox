using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using NuniToolbox.Ui.ViewModel;

namespace NuniToolbox.Ui.Test.ViewModel
{
    public class SelectableViewModelTest
    {
        [Fact]
        public void Test_PropertyChanged_Ok()
        {
            SelectableViewModel<int> selectableViewModel = new SelectableViewModel<int>(2, false);

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
}
