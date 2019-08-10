using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

using NuniToolbox.Collections;

namespace NuniToolbox.Test.Collections
{
    public class IEnumerableExtensionsTest
    {
        [Fact]
        public void Test_Foreach_Ok()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 4);

            MyModel myModel = new MyModel();

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

            int[] sorted = numbers.Sorted(i => i).ToArray();

            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.Equal(i + 1, sorted[i]);
            }
        }

        [Fact]
        public void Test_Sorted_Descending_Ok()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 4).OrderBy(i => i);

            int[] sorted = numbers.Sorted(i => i, true).ToArray();

            for (int i = 0; i < sorted.Length; i++)
            {
                Assert.Equal(4 - i, sorted[i]);
            }
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
}
