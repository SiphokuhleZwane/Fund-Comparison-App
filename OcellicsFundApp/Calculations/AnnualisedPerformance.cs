using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OcellicsFundApp.Models;

namespace OcellicsFundApp.Calculations
{
    public class AnnualisedPerformance
    {
        private List<MonthlyReturn> monthlyReturns;
        private List<Fund> funds;

        public AnnualisedPerformance(List<MonthlyReturn> monthlyReturns, List<Fund> funds)
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
                        double constant = 1;
                        for (int j = i; j < 3 + i; j++)
                        {
                            constant *= (1 + monthlyReturn[j].MonthToDate);
                        }

                        monthlyReturn[i].ThreeMonth = constant - 1;
                    }
                    else
                    {
                        monthlyReturn[i].ThreeMonth = null;
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
                        double constant = 1;
                        for (int j = i; j < 6 + i; j++)
                        {
                            constant *= (1 + monthlyReturn[j].MonthToDate);
                        }
                        monthlyReturn[i].SixMonth = constant - 1;
                    }
                    else
                    {
                        monthlyReturn[i].SixMonth = null;
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
                    if (i + monthsInYear <= count)
                    {
                        double constant = 1;
                        for (int j = i; j < monthsInYear + i; j++)
                        {
                            constant *= (1 + monthlyReturn[j].MonthToDate);
                        }
                        monthlyReturn[i].YearToDate = constant - 1;
                    }
                    else
                    {
                        monthlyReturn[i].YearToDate = null;
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
                        double constant = 1;
                        for (int j = i; j < 12 + i; j++)
                        {
                            constant *= (1 + monthlyReturn[j].MonthToDate);
                        }
                        monthlyReturn[i].OneYear = constant - 1;
                    }
                    else
                    {
                        monthlyReturn[i].OneYear = null;
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
                        double constant = 1;
                        for (int j = i; j < 24 + i; j++)
                        {
                            constant *= (1 + monthlyReturn[j].MonthToDate);
                        }

                        monthlyReturn[i].TwoYear = Math.Pow(constant, 1.0 / 2) - 1;
                    }
                    else
                    {
                        monthlyReturn[i].TwoYear = null;
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
                        if (MonthsSinceInception >= 12)
                        {
                            double SinceInceptionReturn = 1;
                            for (int j = i; j < MonthsSinceInception + i; j++)
                            {
                                SinceInceptionReturn *= (1 + monthlyReturn[j].MonthToDate);
                            }
                            double power = (double)(12 / MonthsSinceInception);
                            monthlyReturn[i].SinceInception = Math.Pow(SinceInceptionReturn, 12.0 / MonthsSinceInception) - 1;
                        }
                        else
                        {
                            double SinceInceptionReturn = 1;
                            for (int j = i; j < MonthsSinceInception + i; j++)
                            {
                                SinceInceptionReturn *= (1 + monthlyReturn[j].MonthToDate);
                            }
                            monthlyReturn[i].SinceInception = SinceInceptionReturn - 1;

                        }
                    }

                }
            }
        }
    }
}