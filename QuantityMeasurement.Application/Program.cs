using System;
using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Business.Impl;
using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.DTO;

namespace QuantityMeasurement.Application
{
    class Program
    {
        static void Main()
        {
            IQuantityMeasurementService service = new QuantityMeasurementServiceImpl();

            while (true)
            {
                try
                {
                    Console.WriteLine("\n--- Quantity Measurement System ---");
                    Console.WriteLine("1. Compare Quantities (Length/Weight/Volume/Temp)");
                    Console.WriteLine("2. Add Quantities");
                    Console.WriteLine("3. Subtract Quantities");
                    Console.WriteLine("4. Divide Quantities");
                    Console.WriteLine("5. Convert Units");
                    Console.WriteLine("6. Exit");

                    Console.Write("\nChoose an option: ");
                    if (!int.TryParse(Console.ReadLine(), out int option)) continue;

                    switch (option)
                    {
                        case 1: HandleComparison(service); break;
                        case 2: HandleAddition(service); break;
                        case 3: HandleSubtraction(service); break;
                        case 4: HandleDivision(service); break;
                        case 5: HandleConversion(service); break;
                        case 6: return;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static QuantityDTO ReadQuantity()
        {
            Console.Write("Enter value: ");
            double value = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter unit (e.g., FEET, INCHES, KILOGRAM, LITRE, CELSIUS): ");
            string unit = Console.ReadLine()?.ToUpper() ?? "";

            return new QuantityDTO(value, unit);
        }

        static void HandleComparison(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Compare Quantities ---");
            var q1 = ReadQuantity();
            var q2 = ReadQuantity();

            bool result = false;
            if (Enum.TryParse<LengthUnit>(q1.Unit, out var l1) && Enum.TryParse<LengthUnit>(q2.Unit, out var l2))
                result = service.AreLengthsEqual(q1.Value, l1, q2.Value, l2);
            else if (Enum.TryParse<WeightUnit>(q1.Unit, out var w1) && Enum.TryParse<WeightUnit>(q2.Unit, out var w2))
                result = service.AreWeightsEqual(q1.Value, w1, q2.Value, w2);
            else if (Enum.TryParse<VolumeUnit>(q1.Unit, out var v1) && Enum.TryParse<VolumeUnit>(q2.Unit, out var v2))
                result = service.AreVolumesEqual(q1.Value, v1, q2.Value, v2);
            else if (Enum.TryParse<TemperatureUnit>(q1.Unit, out var t1) && Enum.TryParse<TemperatureUnit>(q2.Unit, out var t2))
                result = service.AreTemperaturesEqual(q1.Value, t1, q2.Value, t2);
            else
                throw new ArgumentException("Units must be of the same type for comparison.");

            Console.WriteLine($"Result: {(result ? "Equal" : "Not Equal")}");
        }

        static void HandleAddition(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Add Quantities ---");
            var q1 = ReadQuantity();
            var q2 = ReadQuantity();
            Console.Write("Enter target unit for result: ");
            string targetUnit = Console.ReadLine()?.ToUpper() ?? "";

            var result = service.Add(q1, q2, targetUnit);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        static void HandleSubtraction(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Subtract Quantities ---");
            var q1 = ReadQuantity();
            var q2 = ReadQuantity();
            Console.Write("Enter target unit for result: ");
            string targetUnit = Console.ReadLine()?.ToUpper() ?? "";

            var result = service.Subtract(q1, q2, targetUnit);
            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        static void HandleDivision(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Divide Quantities ---");
            var q1 = ReadQuantity();
            var q2 = ReadQuantity();

            var result = service.Divide(q1, q2);
            Console.WriteLine($"Result: {result}");
        }

        static void HandleConversion(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Convert Units ---");
            var q = ReadQuantity();
            Console.Write("Enter target unit: ");
            string targetUnit = Console.ReadLine()?.ToUpper() ?? "";

            var result = service.Convert(q, targetUnit);
            Console.WriteLine($"Converted: {result.Value} {result.Unit}");
        }
    }
}
