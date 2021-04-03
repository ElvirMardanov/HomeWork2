using ClassLibrary1;
using System;

namespace HomeWork2
{
    class Program
    {
        static void Main(string[] args)
        {
            var subsidyCalculator = new SubsidyCalculator();
            subsidyCalculator.OnNotify += SubsidyCalculator_OnNotify;
            var volume1 = new Volume()
            {
                HouseId = 1,
                ServiceId = 1,
                Month = new DateTime(2021,1,1),
                Value = 10
            };

            var tariff1 = new Tariff()
            {
                HouseId = 1,
                ServiceId = 1,
                PeriodBegin = new DateTime(2021, 1, 1),
                PeriodEnd = new DateTime(2021, 3, 31),
                Value = 2
            };
            subsidyCalculator.CheckSubsidyCorrection(volume1, tariff1);
            subsidyCalculator.CalculateSubsidy(volume1, tariff1);
        }

        private static void SubsidyCalculator_OnNotify(object sender, string e)
        {
            Console.WriteLine(e);
        }
    }
}
