using ClassLibrary1;
using System;

namespace HomeWork2
{
    class SubsidyCalculator : ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;

        public void CalcException(Exception ex = null)
        {
            OnException?.Invoke(this, new ("Ошибка!", ex ?? new Exception()));
            throw ex ?? new Exception();
        }

        public Charge CalculateSubsidy(Volume volume, Tariff tariff)
        {
            if (OnNotify != null)
            {
                OnNotify(this, $"Расчёт начат в {DateTime.Now}");
            }

            var charge = new Charge();
            charge.Value = volume.Value * tariff.Value;
            charge.HouseId = volume.HouseId;
            charge.ServiceId = volume.ServiceId;
            charge.Month = volume.Month;
            if (OnNotify != null)
            {
                OnNotify(this, $"Расчёт успешно завершён в {DateTime.Now}");
            }
           
            return charge;
        }

        public void CheckSubsidyCorrection(Volume volume, Tariff tariff)
        {
            try
            {
                try
                {
                    if (volume.ServiceId != tariff.ServiceId)
                    {
                        CalcException(new Exception($"Неправльный service объем{volume.ServiceId}, тариф {tariff.ServiceId}"));
                    }

                    if (volume.HouseId != tariff.HouseId)
                    {
                        CalcException(new Exception($"Неправльный house объем{volume.HouseId}, тариф {tariff.HouseId}"));
                    }

                    if (volume.Month < tariff.PeriodBegin || volume.Month > tariff.PeriodEnd)
                    {
                        CalcException(new Exception($"Месяц объёма {volume.Month} не входит в период действия тарифа"));
                    }

                    if (tariff.Value <= 0)
                    {
                        CalcException(new Exception($"Неправльный тариф {tariff.Value}"));
                    }

                    if (volume.Value <= 0)
                    {
                        CalcException(new Exception($"Неправльный объем{volume.Value}"));
                    }
                }
                catch
                {
                    CalcException();
                    throw;
                }
                
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }          
        }
    }
}
