using NuniToolbox.Collections;

namespace NuniToolbox.Test.Collections;

public class ExtendedObservableCollectionTest
{
    public ExtendedObservableCollectionTest() { }

    [Fact]
    public void Test_ReplaceWith_Ok()
    {
        int eventCount = 0;

        IEnumerable<int> initialNumbers = Enumerable.Range(1, 4);
        IEnumerable<int> newNumbers = Enumerable.Range(16, 32);

        ExtendedObservableCollection<int> observableCollection = [.. initialNumbers];

        observableCollection.CollectionChanged += (sender, args) => eventCount++;

        observableCollection.ReplaceWith(newNumbers);

        Assert.Equal(newNumbers.Count(), observableCollection.Count);

        foreach (int number in newNumbers)
        {
            Assert.Contains(observableCollection, numberItem => numberItem == number);
        }

        Assert.Equal(1, eventCount);
    }
    [Fact]
    public void Test_ReplaceWith_EmptyItemsArgument_Ok()
    {
        int eventCount = 0;

        ExtendedObservableCollection<int> observableCollection = [];

        observableCollection.CollectionChanged += (sender, args) => eventCount++;

        observableCollection.ReplaceWith(null);
        observableCollection.ReplaceWith(new List<int>());
        observableCollection.ReplaceWith(new int[0]);

        Assert.Empty(observableCollection);
        Assert.Equal(0, eventCount);
    }

    [Fact]
    public void Test_AddRange_Ok()
    {
        int eventCount = 0;

        IEnumerable<int> initialNumbers = Enumerable.Range(1, 4);
        IEnumerable<int> newNumbers = Enumerable.Range(16, 32);

        ExtendedObservableCollection<int> observableCollection = [.. initialNumbers];

        observableCollection.CollectionChanged += (sender, args) => eventCount++;

        observableCollection.AddRange(newNumbers);

        Assert.Equal(initialNumbers.Count() + newNumbers.Count(), observableCollection.Count);

        foreach (int number in initialNumbers)
        {
            Assert.Contains(observableCollection, numberItem => numberItem == number);
        }

        foreach (int number in newNumbers)
        {
            Assert.Contains(observableCollection, numberItem => numberItem == number);
        }

        Assert.Equal(1, eventCount);
    }

    [Fact]
    public void Test_AddRange_EmptyItemsArgument_Ok()
    {
        int eventCount = 0;

        IEnumerable<int> newNumbers = Enumerable.Range(16, 32);

        ExtendedObservableCollection<int> observableCollection = [];

        observableCollection.CollectionChanged += (sender, args) => eventCount++;

        observableCollection.AddRange(null);
        observableCollection.AddRange(new List<int>());
        observableCollection.AddRange(new int[0]);

        Assert.Empty(observableCollection);
        Assert.Equal(0, eventCount);
    }

    [Fact]
    public void Test_Add_Ok()
    {
        int eventCount = 0;

        IEnumerable<int> initialNumbers = Enumerable.Range(1, 4);
        IEnumerable<int> newNumbers = Enumerable.Range(16, 32);

        ExtendedObservableCollection<int> observableCollection = [.. initialNumbers];

        observableCollection.CollectionChanged += (sender, args) => eventCount++;

        foreach (int number in newNumbers)
        {
            observableCollection.Add(number);
        }

        Assert.Equal(initialNumbers.Count() + newNumbers.Count(), observableCollection.Count);

        foreach (int number in initialNumbers)
        {
            Assert.Contains(observableCollection, numberItem => numberItem == number);
        }

        foreach (int number in newNumbers)
        {
            Assert.Contains(observableCollection, numberItem => numberItem == number);
        }

        Assert.Equal(newNumbers.Count(), eventCount);
    }
}
