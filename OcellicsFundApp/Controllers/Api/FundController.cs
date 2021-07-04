using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OcellicsFundApp.Models;

namespace OcellicsFundApp.Controllers.Api
{
    public class FundController : ApiController
    {
        private ApplicationDbContext _context;

        public FundController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/fund/id
        public IEnumerable<MonthlyReturn> GetMonthlyReturns(int Id)
        {
            return _context.MonthlyReturns.ToList().FindAll(f => f.FundId == Id);
        }
    }
}
