using NuniToolbox.Model;

namespace NuniToolbox.Test.Model;

public class ModelObjectTest
{
    public ModelObjectTest() { }

    [Fact]
    public void Test_PropertyChanged_Ok()
    {
        MyModel myModel = new();

        string propertyName = null;

        myModel.PropertyChanged += (sender, args) => propertyName = args.PropertyName;

        myModel.Attribute1 = 2;

        Assert.Equal(nameof(myModel.Attribute1), propertyName);

        myModel.Attribute2 = 2;

        Assert.Equal(nameof(myModel.Attribute2), propertyName);
    }

    public class MyModel : ModelObject
    {
        private int m_attribute1;
        private int m_attribute2;

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

        public int Attribute2
        {
            get
            {
                return m_attribute2;
            }

            set
            {
                m_attribute2 = value;

                OnPropertyChanged(nameof(Attribute2));
            }
        }

        public MyModel() : base() { }
    }
}
