using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

using Xunit;

using NuniToolbox.Ui.Commands;

namespace NuniToolbox.Ui.Test.Commands
{
    public class DelegateCommandTest
    {
        [Fact]
        public void Test_Constructor_Ok()
        {
            Assert.NotNull(new DelegateCommand(() => { }));
            Assert.NotNull(new DelegateCommand(() => { }, () => true));
            Assert.NotNull(new DelegateCommand<object>(o => { }));
            Assert.NotNull(new DelegateCommand<object>(o => { }, o => true));
            Assert.NotNull(new DelegateCommand<int>(i => { }));
            Assert.NotNull(new DelegateCommand<int>(i => { }, i => true));
            Assert.NotNull(new DelegateCommand<int?>(i => { }));
            Assert.NotNull(new DelegateCommand<int?>(i => { }, i => true));
        }

        [Fact]
        public void Test_Constructor_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(null, () => true));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand(() => { }, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<object>(null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<object>(null, o => true));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<object>(o => { }, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<int>(null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<int>(null, i => true));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<int>(i => { }, null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<int?>(null));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<int?>(null, i => true));
            Assert.Throws<ArgumentNullException>(() => new DelegateCommand<int?>(i => { }, null));
        }

        [Fact]
        public void Test_CanExecute_Ok()
        {
            Assert.True(new DelegateCommand(() => { }, () => true).CanExecute(null));
            Assert.False(new DelegateCommand(() => { }, () => false).CanExecute(null));

            ICommand command = new DelegateCommand<int>(i => { }, i => i > 0);

            Assert.True(command.CanExecute(2));
            Assert.False(command.CanExecute(-4));
        }

        [Fact]
        public void Test_Execute_Ok()
        {
            MyModel myModel = new MyModel { Integer = 2 };
            ICommand command = new DelegateCommand(() => { myModel.Integer += 2; }, () => false);
            command.Execute(null);

            Assert.Equal(2, myModel.Integer);

            myModel = new MyModel { Integer = 2 };
            command = new DelegateCommand(() => { myModel.Integer += 2; }, () => true);
            command.Execute(null);

            Assert.Equal(4, myModel.Integer);

            myModel = new MyModel { Integer = 2 };
            command = new DelegateCommand<int>(i => { myModel.Integer += i; }, i => false);
            command.Execute(2);

            Assert.Equal(2, myModel.Integer);

            myModel = new MyModel { Integer = 2 };
            command = new DelegateCommand<int>(i => { myModel.Integer += i; }, i => true);
            command.Execute(2);

            Assert.Equal(4, myModel.Integer);
        }

        [Fact]
        public void Test_Execute_ThrowsInvalidCastException()
        {
            ICommand command = new DelegateCommand<int>(i => { }, i => true);

            Assert.Throws<InvalidCastException>(() => command.Execute(DateTime.Now));
        }

        public class MyModel
        {
            public int Integer { get; set; }

            public MyModel()
            {
                Integer = default;
            }
        }
    }
}
