using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xunit;

using NuniToolbox.Enum;

namespace NuniToolbox.Test.Enum
{
    public class EnumTranslatorTest
    {
        [Theory]
        [InlineData(VehicleType.Bicycle, "de", EnumTranslationType.Default, "Fahrrad")]
        [InlineData(VehicleType.Bicycle, "de", EnumTranslationType.Plural, "Fahrräder")]
        [InlineData(VehicleType.Car, "de", EnumTranslationType.Default, "Auto")]
        [InlineData(VehicleType.Car, "de", EnumTranslationType.Plural, "Autos")]
        [InlineData(VehicleType.Motorbike, "de", EnumTranslationType.Default, "Motorrad")]
        [InlineData(VehicleType.Motorbike, "de", EnumTranslationType.Plural, "Motorräder")]
        [InlineData(VehicleType.Scooter, "de", EnumTranslationType.Default, "Roller")]
        [InlineData(VehicleType.Scooter, "de", EnumTranslationType.Plural, "Roller")]
        [InlineData(VehicleType.Bicycle, "en", EnumTranslationType.Default, "bicycle")]
        [InlineData(VehicleType.Bicycle, "en", EnumTranslationType.Plural, "bicycles")]
        [InlineData(VehicleType.Car, "en", EnumTranslationType.Default, "car")]
        [InlineData(VehicleType.Car, "en", EnumTranslationType.Plural, "cars")]
        [InlineData(VehicleType.Motorbike, "en", EnumTranslationType.Default, "motorbike")]
        [InlineData(VehicleType.Motorbike, "en", EnumTranslationType.Plural, "motorbikes")]
        [InlineData(VehicleType.Scooter, "en", EnumTranslationType.Default, "scooter")]
        [InlineData(VehicleType.Scooter, "en", EnumTranslationType.Plural, "scooters")]
        public void Test_GetStringForValue_Ok(VehicleType enumValue, string languageName, EnumTranslationType translationType, string expected)
        {
            EnumTranslator enumTranslator = InitTranslations();

            string translation = enumTranslator.GetStringForValue(enumValue, new CultureInfo(languageName), translationType);

            Assert.Equal(expected, translation);
        }

        [Fact]
        public void Test_GetStringForValue_IllegalGenericArgument_ExpectArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new EnumTranslator().GetStringForValue(2));
        }

        [Fact]
        public void Test_GetStringForValue_IllegalLanguage_ExpectArgumentException()
        {
            EnumTranslator enumTranslator = InitTranslations();

            Assert.Throws<ArgumentException>(() => enumTranslator.GetStringForValue(VehicleType.Bicycle, new CultureInfo("es")));
        }

        [Fact]
        public void Test_GetValuesSortedByTranslation_Ok()
        {
            EnumTranslator enumTranslator = InitTranslations();

            VehicleType[] enumValues = enumTranslator.GetValuesSortedByTranslation<VehicleType>(new CultureInfo("de"), EnumTranslationType.Default);

            Assert.NotNull(enumValues);
            Assert.Equal(4, enumValues.Length);
            Assert.Equal(VehicleType.Car, enumValues[0]);
            Assert.Equal(VehicleType.Bicycle, enumValues[1]);
            Assert.Equal(VehicleType.Motorbike, enumValues[2]);
            Assert.Equal(VehicleType.Scooter, enumValues[3]);
        }

        private EnumTranslator InitTranslations()
        {
            DictionaryEnumTranslationSource<VehicleType> germanTranslations = new DictionaryEnumTranslationSource<VehicleType>(new CultureInfo("de"));

            germanTranslations.DefaultTranslations = new Dictionary<VehicleType, string>
            {
                { VehicleType.Bicycle, "Fahrrad" },
                { VehicleType.Car, "Auto" },
                { VehicleType.Motorbike, "Motorrad" },
                { VehicleType.Scooter, "Roller" }
            };

            germanTranslations.PluralTranslations = new Dictionary<VehicleType, string>
            {
                { VehicleType.Bicycle, "Fahrräder" },
                { VehicleType.Car, "Autos" },
                { VehicleType.Motorbike, "Motorräder" },
                { VehicleType.Scooter, "Roller" }
            };

            DictionaryEnumTranslationSource<VehicleType> englishTranslations = new DictionaryEnumTranslationSource<VehicleType>(new CultureInfo("en"));

            englishTranslations.DefaultTranslations = new Dictionary<VehicleType, string>
            {
                { VehicleType.Bicycle, "bicycle" },
                { VehicleType.Car, "car" },
                { VehicleType.Motorbike, "motorbike" },
                { VehicleType.Scooter, "scooter" }
            };

            englishTranslations.PluralTranslations = new Dictionary<VehicleType, string>
            {
                { VehicleType.Bicycle, "bicycles" },
                { VehicleType.Car, "cars" },
                { VehicleType.Motorbike, "motorbikes" },
                { VehicleType.Scooter, "scooters" }
            };

            EnumTranslator enumTranslator = new EnumTranslator();
            enumTranslator.RegisterTranslationSource(germanTranslations);
            enumTranslator.RegisterTranslationSource(englishTranslations);

            return enumTranslator;
        }

        public enum VehicleType
        {
            Bicycle,
            Car,
            Motorbike,
            Scooter
        }
    }
}
