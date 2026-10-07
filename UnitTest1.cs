using System;
using Xunit;
using SmartphoneApp;

namespace SmartphoneApp.Tests
{
    public class SmartphoneTests
    {
        [Fact]
        public void DefaultConstructor_SetsExpectedDefaults()
        {
            var phone = new Smartphone();

            Assert.Equal("Generic", phone.Brand);
            Assert.Equal("Standard", phone.Model);
            Assert.Equal(64, phone.StorageCapacityGb);
            Assert.Equal(100, phone.BatteryLevel);
        }

        [Fact]
        public void CustomConstructor_ValidData_CreatesInstance()
        {
            var phone = new Smartphone("Apple", "iPhone 15", 256, 80);

            Assert.Equal("Apple", phone.Brand);
            Assert.Equal("iPhone 15", phone.Model);
            Assert.Equal(256, phone.StorageCapacityGb);
            Assert.Equal(80, phone.BatteryLevel);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Brand_InvalidValue_ThrowsArgumentException(string? invalidBrand)
        {
            Assert.Throws<ArgumentException>(() => new Smartphone(invalidBrand!, "Model", 128));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void Model_InvalidValue_ThrowsArgumentException(string? invalidModel)
        {
            Assert.Throws<ArgumentException>(() => new Smartphone("Brand", invalidModel!, 128));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-32)]
        public void StorageCapacity_ZeroOrNegative_ThrowsArgumentOutOfRangeException(int invalidStorage)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Smartphone("Brand", "Model", invalidStorage));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void BatteryLevel_OutsideRange_ThrowsArgumentOutOfRangeException(int invalidBattery)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Smartphone("Brand", "Model", 128, invalidBattery));
        }

        [Fact]
        public void Charge_AddsBatteryCorrectly()
        {
            var phone = new Smartphone("Samsung", "S24", 128, 50);

            phone.Charge(30);

            Assert.Equal(80, phone.BatteryLevel);
        }

        [Fact]
        public void Charge_CapsAt100Percent()
        {
            var phone = new Smartphone("Samsung", "S24", 128, 90);

            phone.Charge(30);

            Assert.Equal(100, phone.BatteryLevel);
        }

        [Fact]
        public void Charge_NegativeOrZero_ThrowsArgumentException()
        {
            var phone = new Smartphone();

            Assert.Throws<ArgumentException>(() => phone.Charge(0));
            Assert.Throws<ArgumentException>(() => phone.Charge(-10));
        }

        [Fact]
        public void UseBattery_DecreasesBatteryCorrectly()
        {
            var phone = new Smartphone("Google", "Pixel 8", 128, 50);

            phone.UseBattery(20);

            Assert.Equal(30, phone.BatteryLevel);
        }

        [Fact]
        public void UseBattery_FloorsAtZero()
        {
            var phone = new Smartphone("Google", "Pixel 8", 128, 20);

            phone.UseBattery(50);

            Assert.Equal(0, phone.BatteryLevel);
        }

        [Fact]
        public void UseBattery_NegativeOrZero_ThrowsArgumentException()
        {
            var phone = new Smartphone();

            Assert.Throws<ArgumentException>(() => phone.UseBattery(0));
            Assert.Throws<ArgumentException>(() => phone.UseBattery(-5));
        }

        [Fact]
        public void Deconstruct_ReturnsCorrectValues()
        {
            var phone = new Smartphone("Xiaomi", "13T", 256, 75);

            var (brand, model, storage, battery) = phone;

            Assert.Equal("Xiaomi", brand);
            Assert.Equal("13T", model);
            Assert.Equal(256, storage);
            Assert.Equal(75, battery);
        }

        [Fact]
        public void ToString_ReturnsFormattedString()
        {
            var phone = new Smartphone("Apple", "iPhone 13", 128, 90);

            string result = phone.ToString();

            Assert.Equal("Apple iPhone 13 (128GB) - Заряд: 90%", result);
        }

        [Fact]
        public void Equals_SameValues_ReturnsTrue()
        {
            var phone1 = new Smartphone("Apple", "iPhone 15", 128, 50);
            var phone2 = new Smartphone("apple", "iphone 15", 128, 90);

            Assert.True(phone1.Equals(phone2));
            Assert.True(phone1.Equals((object)phone2));
            Assert.Equal(phone1.GetHashCode(), phone2.GetHashCode());
        }

        [Fact]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            var phone1 = new Smartphone("Apple", "iPhone 15", 128);
            var phone2 = new Smartphone("Apple", "iPhone 15", 256);

            Assert.False(phone1.Equals(phone2));
            Assert.False(phone1.Equals(null));
        }
    }
}