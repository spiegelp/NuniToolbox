using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xunit;

using NuniToolbox.Collections;

namespace NuniToolbox.Test.Collections
{
    public class ExtendedObservableCollectionTest
    {
        [Fact]
        public void Test_ReplaceWith_Ok()
        {
            IEnumerable<int> initialNumbers = Enumerable.Range(1, 4);
            IEnumerable<int> newNumbers = Enumerable.Range(16, 32);

            ExtendedObservableCollection<int> observableCollection = new ExtendedObservableCollection<int>(initialNumbers);

            observableCollection.ReplaceWith(newNumbers);

            Assert.Equal(newNumbers.Count(), observableCollection.Count);

            foreach (int number in newNumbers)
            {
                Assert.Contains(observableCollection, numberItem => numberItem == number);
            }
        }

        [Fact]
        public void Test_AddRange_Ok()
        {
            int eventCount = 0;

            IEnumerable<int> initialNumbers = Enumerable.Range(1, 4);
            IEnumerable<int> newNumbers = Enumerable.Range(16, 32);

            ExtendedObservableCollection<int> observableCollection = new ExtendedObservableCollection<int>(initialNumbers);

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
        public void Test_Add_Ok()
        {
            int eventCount = 0;

            IEnumerable<int> initialNumbers = Enumerable.Range(1, 4);
            IEnumerable<int> newNumbers = Enumerable.Range(16, 32);

            ExtendedObservableCollection<int> observableCollection = new ExtendedObservableCollection<int>(initialNumbers);

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
}
