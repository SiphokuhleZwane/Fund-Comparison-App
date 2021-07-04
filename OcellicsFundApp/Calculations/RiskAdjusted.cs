using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OcellicsFundApp.Models;

namespace OcellicsFundApp.Calculations
{
    public class RiskAdjusted
    {
        private double riskFreeRate;

        private List<MonthlyReturn> monthlyReturns;

        private List<Fund> funds;

        public RiskAdjusted(List<MonthlyReturn> monthlyReturns, List<Fund> funds, double riskFreeRate = 0.05)
        {

            this.riskFreeRate = riskFreeRate;
            this.monthlyReturns = monthlyReturns;
            this.funds = funds;
          //  ThreeMonthCalc();
        //  SixMonthCalc();
            YearToDateCalc();
          //  OneYearCalc();
          //  TwoYearCalc();
          //  SinceInceptionCalc();
        }

        public double? SharpRatio(double? portfolioReturn, double? portfolioStdv, double riskFreeAdjuster = 1)
        {
            if (portfolioStdv == 0) return null;
            else
            return (double)(portfolioReturn - riskFreeRate) / portfolioStdv;
        }

        public void ThreeMonthCalc()
        {
            foreach (var fund in funds)
            {
                var monthlyReturn = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = monthlyReturn.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i + 3 <= count)
                    {
                        monthlyReturn[i].ThreeMonthRiskAdjusted = SharpRatio(monthlyReturn[i].ThreeMonth, monthlyReturn[i].ThreeMonthVolatility, 4);
                    }
                    else
                    {
                        monthlyReturn[i].ThreeMonthRiskAdjusted = null;
                    }
                }
            }
        }

        public void SixMonthCalc()
        {
            foreach (var fund in funds)
            {
                var monthlyReturn = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = monthlyReturn.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i + 6 <= count)
                    {
                        monthlyReturn[i].SixMonthRiskAdjusted = SharpRatio(monthlyReturn[i].SixMonth, monthlyReturn[i].SixMonthVolatility, 2);
                    }
                    else
                    {
                        monthlyReturn[i].SixMonthRiskAdjusted = null;
                    }
                }
            }
        }


        public void YearToDateCalc()
        {
            foreach (var fund in funds)
            {
                var monthlyReturn = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = monthlyReturn.Count();
                for (int i = 0; i < count; i++)
                {
                    int monthsInYear = monthlyReturn[i].EffectiveDate.Month;
                    if (i + monthsInYear <= count && monthsInYear != 1)
                    {
                        monthlyReturn[i].YearToDateRiskAdjusted = SharpRatio(monthlyReturn[i].YearToDate, monthlyReturn[i].YearToDateVolatility);
                    }
                    else
                    {
                        monthlyReturn[i].YearToDateRiskAdjusted = null;
                    }
                }
            }
        }


        public void OneYearCalc()
        {
            foreach (var fund in funds)
            {
                var monthlyReturn = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = monthlyReturn.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i + 12 <= count)
                    {
                        monthlyReturn[i].OneYearRiskAdjusted = SharpRatio(monthlyReturn[i].OneYear, monthlyReturn[i].OneYearVolatility);
                    }
                    else
                    {
                        monthlyReturn[i].OneYearRiskAdjusted = null;
                    }
                }
            }
        }


        public void TwoYearCalc()
        {
            foreach (var fund in funds)
            {
                var monthlyReturn = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = monthlyReturn.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i + 24 <= count)
                    {
                        monthlyReturn[i].TwoYearRiskAdjusted = SharpRatio(monthlyReturn[i].TwoYear, monthlyReturn[i].TwoYearVolatility);
                    }
                    else
                    {
                        monthlyReturn[i].TwoYearRiskAdjusted = null;
                    }
                }
            }
        }


        public void SinceInceptionCalc()
        {
            foreach (var fund in funds)
            {
                var monthlyReturn = monthlyReturns.Where(f => f.FundId == fund.Id).ToList();
                int count = monthlyReturn.Count();
                if (fund.StartDate == monthlyReturn[count - 1].EffectiveDate)
                {
                    for (int i = 0; i < count; i++)
                    {
                        int MonthsSinceInception = (monthlyReturn[i].EffectiveDate.Year - fund.StartDate.Year) * 12 - fund.StartDate.Month + monthlyReturn[i].EffectiveDate.Month + 1;
                        if (MonthsSinceInception > 1)
                        {
                            monthlyReturn[i].SinceInceptionRiskAdjusted = SharpRatio(monthlyReturn[i].SinceInception, monthlyReturn[i].SinceInceptionVolatility);
                        }
                        else
                        {
                            monthlyReturn[i].SinceInceptionRiskAdjusted = null;
                        }
                    }
                }
            }
        }
    }
}