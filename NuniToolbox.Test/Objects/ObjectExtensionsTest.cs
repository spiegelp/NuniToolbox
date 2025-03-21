﻿using NuniToolbox.Objects;

namespace NuniToolbox.Test.Objects;

public class ObjectExtensionsTest
{
    public ObjectExtensionsTest() { }

    [Fact]
    public void Test_ShallowCopy_Ok()
    {
        Person person = new() { Name = "Boy", Age = 32, HasPets = true, Mother = new Person { Name = "Boy's mother", Age = 64, HasPets = null } };
        Person clonedPerson = person.ShallowCopy();

        Assert.False(person == clonedPerson);
        Assert.Equal(person.Name, clonedPerson.Name);
        Assert.Equal(person.Age, clonedPerson.Age);
        Assert.Equal(person.HasPets, clonedPerson.HasPets);
        Assert.True(person.Mother == clonedPerson.Mother);
    }

    [Fact]
    public void Test_ShallowCopy_ExcludedProperties_Ok()
    {
        Person person = new() { Name = "Boy", Age = 32, HasPets = true, Mother = new Person { Name = "Boy's mother", Age = 64, HasPets = null } };
        Person clonedPerson = person.ShallowCopy(new HashSet<string> { nameof(Person.Age) });

        Assert.False(person == clonedPerson);
        Assert.Equal(person.Name, clonedPerson.Name);
        Assert.NotEqual(person.Age, clonedPerson.Age);
        Assert.Equal(person.HasPets, clonedPerson.HasPets);
        Assert.True(person.Mother == clonedPerson.Mother);
    }

    [Fact]
    public void Test_DeepCopy_Ok()
    {
        Person person = new() { Name = "Boy", Age = 32, HasPets = true, Mother = new Person { Name = "Boy's mother", Age = 64, HasPets = null } };
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

    [Fact]
    public void Test_DeepCopy_ExcludedProperties_Ok()
    {
        Person person = new() { Name = "Boy", Age = 32, HasPets = true, Mother = new Person { Name = "Boy's mother", Age = 64, HasPets = null } };
        Person clonedPerson = person.DeepCopy(new Dictionary<string, ICollection<string>> { { typeof(Person).FullName, new HashSet<string> { nameof(Person.Age) } } });

        Assert.False(person == clonedPerson);
        Assert.Equal(person.Name, clonedPerson.Name);
        Assert.NotEqual(person.Age, clonedPerson.Age);
        Assert.Equal(person.HasPets, clonedPerson.HasPets);
        Assert.True(person.Mother != clonedPerson.Mother);
        Assert.Equal(person.Mother.Name, clonedPerson.Mother.Name);
        Assert.NotEqual(person.Mother.Age, clonedPerson.Mother.Age);
        Assert.Equal(person.Mother.HasPets, clonedPerson.Mother.HasPets);
    }

    public interface IPerson
    {
        string Name { get; set; }

        int Age { get; set; }

        IPerson Mother { get; set; }

        bool? HasPets { get; set; }
    }

    public class Person : IPerson
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public IPerson Mother { get; set; }

        public bool? HasPets { get; set; }

        public Person()
        {
            Name = null;
            Age = 0;
            HasPets = null;
        }
    }
}
