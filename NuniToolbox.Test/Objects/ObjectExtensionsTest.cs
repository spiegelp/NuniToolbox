using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using NuniToolbox.Objects;

namespace NuniToolbox.Test.Objects
{
    public class ObjectExtensionsTest
    {
        [Fact]
        public void Test_ShallowCopy_Ok()
        {
            Person person = new Person { Name = "Boy", Age = 32, HasPets = true, Mother = new Person { Name = "Boy's mother", Age = 64, HasPets = null } };
            Person clonedPerson = person.ShallowCopy();

            Assert.False(person == clonedPerson);
            Assert.Equal(person.Name, clonedPerson.Name);
            Assert.Equal(person.Age, clonedPerson.Age);
            Assert.Equal(person.HasPets, clonedPerson.HasPets);
            Assert.True(person.Mother == clonedPerson.Mother);
        }

        [Fact]
        public void Test_DeepCopy_Ok()
        {
            Person person = new Person { Name = "Boy", Age = 32, HasPets = true, Mother = new Person { Name = "Boy's mother", Age = 64, HasPets = null } };
            Person clonedPerson = person.DeepCopy();

            Assert.False(person == clonedPerson);
            Assert.Equal(person.Name, clonedPerson.Name);
            Assert.Equal(person.Age, clonedPerson.Age);
            Assert.Equal(person.HasPets, clonedPerson.HasPets);
            Assert.True(person.Mother != clonedPerson.Mother);
            Assert.Equal(person.Mother.Name, clonedPerson.Mother.Name);
            Assert.Equal(person.Mother.Age, clonedPerson.Mother.Age);
            Assert.Equal(person.Mother.HasPets, clonedPerson.Mother.HasPets);
        }

        public class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public Person Mother { get; set; }

            public bool? HasPets { get; set; }

            public Person()
            {
                Name = null;
                Age = 0;
                HasPets = null;
            }
        }
    }
}
