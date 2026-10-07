using System;

namespace SmartphoneApp
{
    public class Smartphone : IEquatable<Smartphone>
    {
        private string brand = string.Empty;
        private string model = string.Empty;
        private int storageCapacityGb;
        private int batteryLevel;

        public string Brand
        {
            get => brand;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Бренд не може бути порожнім.");
                brand = value.Trim();
            }
        }

        public string Model
        {
            get => model;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Модель не може бути порожньою.");
                model = value.Trim();
            }
        }

        public int StorageCapacityGb
        {
            get => storageCapacityGb;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Обсяг пам'яті має бути більшим за 0 ГБ.");
                storageCapacityGb = value;
            }
        }

        public int BatteryLevel
        {
            get => batteryLevel;
            private set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), "Рівень заряду має бути в межах від 0 до 100%.");
                batteryLevel = value;
            }
        }

        public Smartphone()
        {
            Brand = "Generic";
            Model = "Standard";
            StorageCapacityGb = 64;
            BatteryLevel = 100;
        }

        public Smartphone(string brand, string model, int storageCapacityGb, int initialBatteryLevel = 100)
        {
            Brand = brand;
            Model = model;
            StorageCapacityGb = storageCapacityGb;
            BatteryLevel = initialBatteryLevel;
        }

        public void Charge(int percentage)
        {
            if (percentage <= 0)
                throw new ArgumentException("Значення заряджання має бути додатним.");

            BatteryLevel = Math.Min(100, BatteryLevel + percentage);
        }

        public void UseBattery(int percentage)
        {
            if (percentage <= 0)
                throw new ArgumentException("Рівень розряджання має бути додатним.");

            BatteryLevel = Math.Max(0, BatteryLevel - percentage);
        }

        public void Deconstruct(out string brand, out string model, out int storageCapacityGb, out int batteryLevel)
        {
            brand = Brand;
            model = Model;
            storageCapacityGb = StorageCapacityGb;
            batteryLevel = BatteryLevel;
        }

        public override string ToString()
        {
            return $"{Brand} {Model} ({StorageCapacityGb}GB) - Заряд: {BatteryLevel}%";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Smartphone);
        }

        public bool Equals(Smartphone? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return string.Equals(Brand, other.Brand, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(Model, other.Model, StringComparison.OrdinalIgnoreCase) &&
                   StorageCapacityGb == other.StorageCapacityGb;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Brand.ToUpperInvariant(),
                Model.ToUpperInvariant(),
                StorageCapacityGb
            );
        }
    }
}