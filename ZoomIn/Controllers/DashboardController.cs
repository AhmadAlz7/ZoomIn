using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZoomIn.Models;

namespace ZoomIn.Controllers
{
    public class DashboardController : Controller
    {

        private readonly ModelContext _context;

        public DashboardController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //ViewBag.prodCount = _context.Products.Select(c => c.ProductId).ToList().Count;
            ViewBag.Balance = _context.Balances.Where(c => c.BalanceLock == "X").SingleOrDefault().BalanceValue;
            ViewBag.totalNoPurchases = _context.Userpurchaseitems.Count();
            ViewBag.totalNoUsers = _context.Endusers.Count();
            ViewBag.totalNoItems = _context.Items.Count();
            ViewBag.totalNoVisits = _context.Accesslogers.Count();
            ViewBag.CashFlow = _context.Userpurchaseitems.Sum(x => (int)x.Paymenttotal);

            string dataStr = JsonConvert.SerializeObject(GetMonthlyVisits(), Formatting.None);
            ViewBag.visitsArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlyAccounts(), Formatting.None);
            ViewBag.accountsArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlySessions(), Formatting.None);
            ViewBag.sessionsArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlyPurchases(), Formatting.None);
            ViewBag.purchasesArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlyUploads(), Formatting.None);
            ViewBag.uploadsArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlyInteractions(), Formatting.None);
            ViewBag.interactionsArray = new HtmlString(dataStr);

            return View();
        }

        public IActionResult Reports()
        {
            //ViewBag.prodCount = _context.Products.Select(c => c.ProductId).ToList().Count;
            ViewBag.Balance = _context.Balances.Where(c => c.BalanceLock == "X").SingleOrDefault().BalanceValue;
            ViewBag.totalNoPurchases = _context.Userpurchaseitems.Count();
            ViewBag.totalNoUsers = _context.Endusers.Count();
            ViewBag.totalNoItems = _context.Items.Count();
            ViewBag.totalNoVisits = _context.Accesslogers.Count();
            ViewBag.CashFlow = _context.Userpurchaseitems.Sum(x => (int)x.Paymenttotal);

            string dataStr = JsonConvert.SerializeObject(GetDailyVisits(), Formatting.None);
            ViewBag.visitsDArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetDailyAccounts(), Formatting.None);
            ViewBag.accountsDArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetDailySessions(), Formatting.None);
            ViewBag.sessionsDArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetDailyPurchases(), Formatting.None);
            ViewBag.purchasesDArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetDailyUploads(), Formatting.None);
            ViewBag.uploadsDArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetDailyInteractions(), Formatting.None);
            ViewBag.interactionsDArray = new HtmlString(dataStr);

            dataStr = JsonConvert.SerializeObject(GetMonthlyVisits(), Formatting.None);
            ViewBag.visitsMArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlyAccounts(), Formatting.None);
            ViewBag.accountsMArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlySessions(), Formatting.None);
            ViewBag.sessionsMArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlyPurchases(), Formatting.None);
            ViewBag.purchasesMArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlyUploads(), Formatting.None);
            ViewBag.uploadsMArray = new HtmlString(dataStr);
            dataStr = JsonConvert.SerializeObject(GetMonthlyInteractions(), Formatting.None);
            ViewBag.interactionsMArray = new HtmlString(dataStr);

            return View();
        }

        private int[] GetMonthlyVisits()
        {
            int[] temp = new int[12];
            for (int i = 0; i < 12; i++)
            {
                temp[i] = _context.Accesslogers.Where(c => c.AccessDate.Value.Month == i + 1
                && c.AccessDate.Value.Year == DateTime.Now.Year).Count() / 10;
            }
            return temp;
        }
        private int[] GetMonthlyAccounts()
        {
            int[] temp = new int[12];
            for (int i = 0; i < 12; i++)
            {
                temp[i] = _context.Endusers.Where(c => c.Registerdate.Value.Month == i + 1
                && c.Registerdate.Value.Year == DateTime.Now.Year).Count();
            }
            return temp;
        }
        private int[] GetMonthlySessions()
        {
            int[] temp = new int[12];
            for (int i = 0; i < 12; i++)
            {
                temp[i] = _context.Loginlogers.Where(c => c.LoginDate.Value.Month == i + 1
                && c.LoginDate.Value.Year == DateTime.Now.Year).Count();
            }
            return temp;
        }
        private int[] GetMonthlyPurchases()
        {
            int[] temp = new int[12];
            for (int i = 0; i < 12; i++)
            {
                temp[i] = _context.Userpurchaseitems.Where(c => c.Purchasedate.Value.Month == i + 1
                && c.Purchasedate.Value.Year == DateTime.Now.Year).Count();
            }
            return temp;
        }
        private int[] GetMonthlyUploads()
        {
            int[] temp = new int[12];
            for (int i = 0; i < 12; i++)
            {
                temp[i] = _context.Items.Where(c => c.Uploaddate.Value.Month == i + 1
                && c.Uploaddate.Value.Year == DateTime.Now.Year).Count();
            }
            return temp;
        }
        private int[] GetMonthlyInteractions()
        {
            int[] temp = new int[12];
            for (int i = 0; i < 12; i++)
            {
                temp[i] = _context.Userlikeitems.Where(c => c.Likedate.Value.Month == i + 1
                && c.Likedate.Value.Year == DateTime.Now.Year).Count();
            }
            return temp;
        }



        private int[] GetDailyVisits()
        {
            int[] temp = new int[31];
            for (int i = 0; i < 31; i++)
            {
                temp[i] = _context.Accesslogers.Where(c => c.AccessDate.Value.Day == i + 1
                && c.AccessDate.Value.Month == DateTime.Now.Month).Count() / 10;
            }
            return temp;
        }
        private int[] GetDailyAccounts()
        {
            int[] temp = new int[31];
            for (int i = 0; i < 31; i++)
            {
                temp[i] = _context.Endusers.Where(c => c.Registerdate.Value.Day == i + 1
                && c.Registerdate.Value.Month == DateTime.Now.Month).Count();
            }
            return temp;
        }
        private int[] GetDailySessions()
        {
            int[] temp = new int[31];
            for (int i = 0; i < 31; i++)
            {
                temp[i] = _context.Loginlogers.Where(c => c.LoginDate.Value.Day == i + 1
                && c.LoginDate.Value.Month == DateTime.Now.Month).Count();
            }
            return temp;
        }
        private int[] GetDailyPurchases()
        {
            int[] temp = new int[31];
            for (int i = 0; i < 31; i++)
            {
                temp[i] = _context.Userpurchaseitems.Where(c => c.Purchasedate.Value.Day == i + 1
                && c.Purchasedate.Value.Month == DateTime.Now.Month).Count();
            }
            return temp;
        }
        private int[] GetDailyUploads()
        {
            int[] temp = new int[31];
            for (int i = 0; i < 31; i++)
            {
                temp[i] = _context.Items.Where(c => c.Uploaddate.Value.Day == i + 1
                && c.Uploaddate.Value.Month == DateTime.Now.Month).Count();
            }
            return temp;
        }
        private int[] GetDailyInteractions()
        {
            int[] temp = new int[31];
            for (int i = 0; i < 31; i++)
            {
                temp[i] = _context.Userlikeitems.Where(c => c.Likedate.Value.Day == i + 1
                && c.Likedate.Value.Month == DateTime.Now.Month).Count();
            }
            return temp;
        }

    }
}
