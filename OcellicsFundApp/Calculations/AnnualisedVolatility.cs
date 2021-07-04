using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OcellicsFundApp.Models;

namespace OcellicsFundApp.Calculations
{
    public class AnnualisedVolatility
    {
        private List<MonthlyReturn> monthlyReturns;
        private List<Fund> funds;

        public AnnualisedVolatility(List<MonthlyReturn> monthlyReturns, List<Fund> funds)
        {
            this.monthlyReturns = monthlyReturns;
            this.funds = funds;
            ThreeMonthCalc();
            SixMonthCalc();
            YearToDateCalc();
            OneYearCalc();
            TwoYearCalc();
            SinceInceptionCalc();
        }

        public double StandardDeviation(List<double> numbers)
        {

            var meanOfNumbers = numbers.Average();


            var squaredDifferences = new List<double>(numbers.Count);
            foreach (var number in numbers)
            {
                var difference = number - meanOfNumbers;
                var squaredDifference = Math.Pow(difference, 2);
                squaredDifferences.Add(squaredDifference);
            }


            var meanOfSquaredDifferences = squaredDifferences.Sum() / (numbers.Count - 1);


            var standardDeviation = Math.Sqrt(meanOfSquaredDifferences);

            return standardDeviation;
        }
        public void ThreeMonthCalc()
        {
            foreach (var fund in funds)
            {
                var fundReturns = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = fundReturns.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i + 3 <= count)
                    {
                        List<double> numbers = new List<double>();
                        for (int j = i; j < 3 + i; j++)
                        {
                            numbers.Add(fundReturns[j].MonthToDate);
                        }
                        fundReturns[i].ThreeMonthVolatility = Math.Round(StandardDeviation(numbers), 8, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        fundReturns[i].ThreeMonthVolatility = null;
                    }
                }

            }
        }


        public void SixMonthCalc()
        {
            foreach (var fund in funds)
            {
                var fundReturns = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = fundReturns.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i + 6 <= count)
                    {
                        List<double> numbers = new List<double>();
                        for (int j = i; j < 6 + i; j++)
                        {
                            numbers.Add(fundReturns[j].MonthToDate);
                        }
                        fundReturns[i].SixMonthVolatility = Math.Round(StandardDeviation(numbers), 8, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        fundReturns[i].SixMonthVolatility = null;
                    }
                }
            }
        }


        public void YearToDateCalc()
        {
            foreach (var fund in funds)
            {

                var fundReturns = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = fundReturns.Count();
                for (int i = 0; i < count; i++)
                {
                    int monthsInYear = fundReturns[i].EffectiveDate.Month;
                    if (i + monthsInYear <= count && monthsInYear != 1)
                    {
                        List<double> numbers = new List<double>();
                        for (int j = i; j < monthsInYear + i; j++)
                        {
                            numbers.Add(fundReturns[j].MonthToDate);
                        }
                        fundReturns[i].YearToDateVolatility = Math.Round(StandardDeviation(numbers), 8, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        fundReturns[i].YearToDateVolatility = null;
                    }
                }
            }

        }


        public void OneYearCalc()
        {
            foreach (var fund in funds)
            {

                var fundReturns = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = fundReturns.Count();
                for (int i = 0; i < count; i++)
                {
                    List<double> numbers = new List<double>();
                    if (i + 12 <= count)
                    {
                        for (int j = i; j < 12 + i; j++)
                        {
                            numbers.Add(fundReturns[j].MonthToDate);
                        }
                        fundReturns[i].OneYearVolatility = Math.Round(StandardDeviation(numbers), 8, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        fundReturns[i].OneYearVolatility = null;
                    }
                }
            }

        }


        public void TwoYearCalc()
        {
            foreach (var fund in funds)
            {

                var fundReturns = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = fundReturns.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i + 24 <= count)
                    {
                        List<double> numbers = new List<double>();
                        for (int j = i; j < 24 + i; j++)
                        {
                            numbers.Add(fundReturns[j].MonthToDate);
                        }

                        fundReturns[i].TwoYearVolatility = Math.Round((StandardDeviation(numbers) * Math.Sqrt(12)), 8, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        fundReturns[i].TwoYearVolatility = null;
                    }
                }
            }

        }

        public void SinceInceptionCalc()
        {

            foreach (var fund in funds)
            {
                var fundReturns = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = fundReturns.Count();
                if (fund.StartDate == fundReturns[count - 1].EffectiveDate)
                {
                    for (int i = 0; i < count; i++)
                    {
                        int MonthsSinceInception = (fundReturns[i].EffectiveDate.Year - fund.StartDate.Year) * 12 - fund.StartDate.Month + fundReturns[i].EffectiveDate.Month + 1;
                        if (MonthsSinceInception >= 12)
                        {
                            List<double> numbers = new List<double>();
                            for (int j = i; j < MonthsSinceInception + i; j++)
                            {
                                numbers.Add(fundReturns[j].MonthToDate);
                            }

                            fundReturns[i].SinceInceptionVolatility = Math.Round((StandardDeviation(numbers) * Math.Sqrt(12)), 8, MidpointRounding.AwayFromZero);
                        }
                        if (MonthsSinceInception != 1)
                        {

                            List<double> numbers = new List<double>();
                            for (int j = i; j < MonthsSinceInception + i; j++)
                            {
                                numbers.Add(fundReturns[j].MonthToDate);
                            }
                            fundReturns[i].SinceInceptionVolatility = Math.Round(StandardDeviation(numbers), 8, MidpointRounding.AwayFromZero);

                        }
                        else
                        {
                            fundReturns[i].SinceInceptionVolatility = null;
                        }
                    }

                }
            }
        }
    }
}