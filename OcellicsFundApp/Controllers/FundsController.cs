using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OcellicsFundApp.Models;
using OcellicsFundApp.ViewModels;
using OcellicsFundApp.Calculations;

namespace OcellicsFundApp.Controllers
{
    public class FundsController : Controller
    {
        private ApplicationDbContext _context;

        public FundsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Funds
        public ActionResult Index()
        {
          //  AnnualisedPerformanceCalculations();
         //   AnnualisedVolatilityCalculations();
            RiskAdjustedyCalculations();
            var fund = _context.Funds.Include(c => c.Category).ToList();

            return View(fund);
        }

        public ActionResult Details(int Id)
        {

            var viewModel = new FundDetailsViewModel
            {
                MonthlyReturns = _context.MonthlyReturns.Include(f => f.Fund).ToList().FindAll(f => f.FundId == Id),
                Fund = _context.Funds.Include(c => c.Category).ToList().SingleOrDefault(f => f.Id == Id)
            };

            return View(viewModel);
        }

        [Route("funds/details/{id}/AnnualisedPerformance")]
        public ActionResult AnnualisedPerformance(int Id)
        {
            var viewModel = new FundDetailsViewModel
            {
                MonthlyReturns = _context.MonthlyReturns.Include(f => f.Fund).ToList().FindAll(f => f.FundId == Id),
                Fund = _context.Funds.Include(c => c.Category).ToList().SingleOrDefault(f => f.Id == Id)
            };

            return View(viewModel);
        }

        [Route("funds/details/{id}/AnnualisedVolatility")]
        public ActionResult AnnualisedVolatility(int Id)
        {
            var viewModel = new FundDetailsViewModel
            {
                MonthlyReturns = _context.MonthlyReturns.Include(f => f.Fund).ToList().FindAll(f => f.FundId == Id),
                Fund = _context.Funds.Include(c => c.Category).ToList().SingleOrDefault(f => f.Id == Id)
            };

            return View(viewModel);
        }

        [Route("funds/details/{id}/RiskAdjusted")]
        public ActionResult RiskAdjusted(int Id)
        {
            var viewModel = new FundDetailsViewModel
            {
                MonthlyReturns = _context.MonthlyReturns.Include(f => f.Fund).ToList().FindAll(f => f.FundId == Id),
                Fund = _context.Funds.Include(c => c.Category).ToList().SingleOrDefault(f => f.Id == Id)
            };

            return View(viewModel);
        }


        public void AnnualisedPerformanceCalculations()
        {
            var monthlyReturns = _context.MonthlyReturns.ToList();
            var funds = _context.Funds.ToList();
            new AnnualisedPerformance(monthlyReturns, funds);
            _context.SaveChanges();
        }

        public void AnnualisedVolatilityCalculations()
        {
            var monthlyReturns = _context.MonthlyReturns.ToList();
            var funds = _context.Funds.ToList();
            new AnnualisedVolatility(monthlyReturns, funds);
            _context.SaveChanges();
        }

        public void RiskAdjustedyCalculations()
        {
            var monthlyReturns = _context.MonthlyReturns.ToList();
            var funds = _context.Funds.ToList();
            new RiskAdjusted(monthlyReturns, funds);
            _context.SaveChanges();
        }
    }
}
