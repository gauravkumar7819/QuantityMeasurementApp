using System;
using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Model.DTO;
using QuantityMeasurement.Model.Units;

namespace QuantityMeasurement.Application
{
    public class Menu : IMenu
    {
        public void Start(IQuantityMeasurementService service)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\n--- Quantity Measurement System ---");
                    Console.WriteLine("1. Compare Quantities");
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

        private QuantityDTO ReadQuantity()
        {
            Console.Write("Enter value: ");
            double value = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Enter unit: ");
            string unit = Console.ReadLine()?.ToUpper() ?? "";

            return new QuantityDTO(value, unit);
        }

        private void HandleComparison(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Compare Quantities ---");
            var q1 = ReadQuantity();
            var q2 = ReadQuantity();

            bool result = service.Compare(q1, q2);
            Console.WriteLine($"Result: {(result ? "Equal" : "Not Equal")}");
        }

        private void HandleAddition(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Add Quantities ---");

            var q1 = ReadQuantity();
            var q2 = ReadQuantity();

            Console.Write("Enter target unit: ");
            string targetUnit = Console.ReadLine()?.ToUpper() ?? "";

            var result = service.Add(q1, q2, targetUnit);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private void HandleSubtraction(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Subtract Quantities ---");

            var q1 = ReadQuantity();
            var q2 = ReadQuantity();

            Console.Write("Enter target unit: ");
            string targetUnit = Console.ReadLine()?.ToUpper() ?? "";

            var result = service.Subtract(q1, q2, targetUnit);

            Console.WriteLine($"Result: {result.Value} {result.Unit}");
        }

        private void HandleDivision(IQuantityMeasurementService service)
        {
            Console.WriteLine("\n--- Divide Quantities ---");

            var q1 = ReadQuantity();
            var q2 = ReadQuantity();

            var result = service.Divide(q1, q2);

            Console.WriteLine($"Result: {result}");
        }

        private void HandleConversion(IQuantityMeasurementService service)
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