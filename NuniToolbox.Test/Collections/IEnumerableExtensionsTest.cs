using NuniToolbox.Collections;

namespace NuniToolbox.Test.Collections;

public class IEnumerableExtensionsTest
{
    public IEnumerableExtensionsTest() { }

    [Fact]
    public void Test_Foreach_Ok()
    {
        IEnumerable<int> numbers = Enumerable.Range(1, 4);

        MyModel myModel = new();

        numbers.Foreach(number => myModel.Sum += number);

        Assert.Equal(10, myModel.Sum);
    }

    [Fact]
    public void Test_Foreach_ExpectNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => Enumerable.Range(1, 4).Foreach(null));
    }

    [Fact]
    public void Test_GroupByToDictionary_Ok()
    {
        string key1 = "key1";
        string key2 = "key2";

        IEnumerable<MyModel> objects = new List<MyModel> {
            new MyModel { Key = key2 }, new MyModel { Key = key1 }, new MyModel { Key = key2 }, new MyModel { Key = key1 }, new MyModel { Key = key2 }
        };

        Dictionary<string, List<MyModel>> objectsDictionary = objects.GroupByToDictionary(obj => obj.Key);

        Assert.Equal(2, objectsDictionary.Keys.Count);
        Assert.Equal(2, objectsDictionary[key1].Count);
        Assert.DoesNotContain(objectsDictionary[key1], obj => obj.Key != key1);
        Assert.Equal(3, objectsDictionary[key2].Count);
        Assert.DoesNotContain(objectsDictionary[key2], obj => obj.Key != key2);
    }

    [Fact]
    public void Test_ToSet_Ok()
    {
        IEnumerable<int> numbers = Enumerable.Range(1, 4);

        HashSet<int> numbersSet = numbers.ToSet();

        Assert.Equal(numbers.Count(), numbersSet.Count);

        foreach(int number in numbers)
        {
            Assert.Contains(numbersSet, numberItem => numberItem == number);
        }
    }

    [Fact]
    public void Test_ToSet_WithElementSelector_Ok()
    {
        IEnumerable<int> numbers = Enumerable.Range(1, 4);

        HashSet<double> numbersSet = numbers.ToSet(number => number + 0.5);

        Assert.Equal(numbers.Count(), numbersSet.Count);

        foreach (int number in numbers)
        {
            Assert.Contains(numbersSet, numberItem => numberItem == (number + 0.5));
        }
    }

    [Fact]
    public void Test_Sorted_Ascending_Ok()
    {
        IEnumerable<int> numbers = Enumerable.Range(1, 4).OrderByDescending(i => i);

        int[] sorted = [.. numbers.Sorted(i => i)];

        for (int i = 0; i < sorted.Length; i++)
        {
            Assert.Equal(i + 1, sorted[i]);
        }
    }

    [Fact]
    public void Test_Sorted_Descending_Ok()
    {
        IEnumerable<int> numbers = Enumerable.Range(1, 4).OrderBy(i => i);

        int[] sorted = [.. numbers.Sorted(i => i, true)];

        for (int i = 0; i < sorted.Length; i++)
        {
            Assert.Equal(4 - i, sorted[i]);
        }
    }

    [Theory]
    [InlineData(1024, new int[] { 5, 8, 7, 3, 1, 4, 2, 6 })]
    [InlineData(323232, new int[] { 2, 8, 5, 4, 6, 7, 1, 3 })]
    [InlineData(666666, new int[] { 3, 7, 5, 6, 4, 2, 1, 8 })]
    public void Test_Shuffle_Ok(int seed, int[] expected)
    {
        IEnumerable<int> numbers = Enumerable.Range(1, 8);

        // use Random with a seed to get an expected list of random numbers
        List<int> numbersSorted = numbers.Shuffle(new Random(seed));

        Assert.Equal(expected[0], numbersSorted[0]);
        Assert.Equal(expected[1], numbersSorted[1]);
        Assert.Equal(expected[2], numbersSorted[2]);
        Assert.Equal(expected[3], numbersSorted[3]);
        Assert.Equal(expected[4], numbersSorted[4]);
        Assert.Equal(expected[5], numbersSorted[5]);
        Assert.Equal(expected[6], numbersSorted[6]);
        Assert.Equal(expected[7], numbersSorted[7]);
    }

    public class MyModel
    {
        public int Sum { get; set; }

        public string Key { get; set; }

        public MyModel()
        {
            Sum = 0;
            Key = null;
        }
    }
}
