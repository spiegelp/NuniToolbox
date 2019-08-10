using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using NuniToolbox.Collections;

namespace NuniToolbox.Test.Collections
{
    public class ICollectionExtensionsTest
    {
        [Fact]
        public void Test_AddAll_OnList_Ok()
        {
            ICollection<int> collection = new List<int> { 0 };
            collection.AddAll(new List<int> { 1, 2 });

            Assert.Equal(3, collection.Count);
            Assert.Contains(collection, number => number == 0);
            Assert.Contains(collection, number => number == 1);
            Assert.Contains(collection, number => number == 2);
        }
        [Fact]
        public void Test_AddAll_OnHashSet_Ok()
        {
            ICollection<int> collection = new HashSet<int> { 0 };
            collection.AddAll(new List<int> { 1, 2 });

            Assert.Equal(3, collection.Count);
            Assert.Contains(collection, number => number == 0);
            Assert.Contains(collection, number => number == 1);
            Assert.Contains(collection, number => number == 2);
        }

        [Fact]
        public void Test_AddAll_ExpectNullReferenceException()
        {
            ICollection<int> collection = new List<int> { 0 };

            Assert.Throws<NullReferenceException>(() => collection.AddAll(null));
        }
    }
}
