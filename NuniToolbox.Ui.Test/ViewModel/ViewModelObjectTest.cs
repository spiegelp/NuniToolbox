using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using NuniToolbox.Ui.ViewModel;

namespace NuniToolbox.Ui.Test.ViewModel
{
    public class ViewModelObjectTest
    {
        [Fact]
        public void Test_PropertyChanged_Ok()
        {
            MyViewModel myViewModel = new MyViewModel { Attribute1 = 2 };

            string propertyName = null;

            myViewModel.PropertyChanged += (sender, args) => propertyName = args.PropertyName;

            myViewModel.Attribute1 = 4;

            Assert.Equal(nameof(myViewModel.Attribute1), propertyName);
            Assert.Equal(4, myViewModel.Attribute1);
        }

        [Fact]
        public void Test_IsBusy_Ok()
        {
            MyViewModel myViewModel = new MyViewModel { Attribute1 = 2 };

            string propertyName = null;

            myViewModel.PropertyChanged += (sender, args) => propertyName = args.PropertyName;

            myViewModel.IsBusy = true;

            Assert.Equal(nameof(myViewModel.IsBusy), propertyName);
            Assert.True(myViewModel.IsBusy);

            myViewModel.IsBusy = false;

            Assert.Equal(nameof(myViewModel.IsBusy), propertyName);
            Assert.False(myViewModel.IsBusy);
        }

        public class MyViewModel : ViewModelObject
        {
            private int m_attribute1;

            public int Attribute1
            {
                get
                {
                    return m_attribute1;
                }

                set
                {
                    m_attribute1 = value;

                    OnPropertyChanged();
                }
            }

            public MyViewModel() : base() { }
        }
    }
}
