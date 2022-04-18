using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZoomIn.Models;

namespace ZoomIn.Controllers
{
    public class BalancesController : Controller
    {
        private readonly ModelContext _context;

        public BalancesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Balances
        public IActionResult Index()
        {

            var dataStr = JsonConvert.SerializeObject(GetDailyRevenues(), Formatting.None);
            ViewBag.dRevenuesArray = new HtmlString(dataStr);

            dataStr = JsonConvert.SerializeObject(GetDailyPayments(), Formatting.None);
            ViewBag.dPaymentsArray = new HtmlString(dataStr);

            dataStr = JsonConvert.SerializeObject(GetMonthlyRevenues(), Formatting.None);
            ViewBag.mRevenuesArray = new HtmlString(dataStr);

            dataStr = JsonConvert.SerializeObject(GetMonthlyPayments(), Formatting.None);
            ViewBag.mPaymentsArray = new HtmlString(dataStr);


            ViewBag.Balance = _context.Balances.Where(c => c.BalanceLock == "X").SingleOrDefault().BalanceValue;
            ViewBag.ProfitRate = _context.Balances.Where(c => c.BalanceLock == "X").SingleOrDefault().Profitrate * 100;

            dynamic mymodel = new ExpandoObject();
            mymodel.Purchases = _context.Userpurchaseitems
                .Include(p => p.Balancetransaction)
                .Include(p => p.Usertransaction);
            mymodel.bTrans = _context.Balancetransactions
                .Include(b => b.Purchase);
            mymodel.uTrans = _context.Usertransactions
                .Include(u => u.Purchase);
            return View(mymodel);

            //return View(await _context.Balances.ToListAsync());
        }

        private int[] GetDailyRevenues()
        {
            int[] temp = new int[31];
            for (int i = 0; i < 31; i++)
            {
                temp[i] = _context.Balancetransactions
                    .Where(c => c.Btransactiondate.Value.Day == i + 1 
                    && c.Btransactiondate.Value.Month == DateTime.Now.Month)
                    .Sum(x => (int)x.Btransactionvalue);
            }
            return temp;
        }
        private int[] GetMonthlyRevenues()
        {
            int[] temp = new int[12];
            for (int i = 0; i < 12; i++)
            {
                temp[i] = _context.Balancetransactions
                    .Where(c => c.Btransactiondate.Value.Month == i + 1)
                    .Sum(x => (int)x.Btransactionvalue);
            }
            return temp;
        }
        private int[] GetDailyPayments()
        {
            int[] temp = new int[31];
            for (int i = 0; i < 31; i++)
            {
                temp[i] = _context.Userpurchaseitems.Where(c => c.Purchasedate.Value.Day == i + 1
                    && c.Purchasedate.Value.Month == DateTime.Now.Month)
                    .Sum(x => (int)x.Paymenttotal);
            }
            return temp;
        }
        private int[] GetMonthlyPayments()
        {
            int[] temp = new int[12];
            for (int i = 0; i < 12; i++)
            {
                temp[i] = _context.Userpurchaseitems.Where(c => c.Purchasedate.Value.Month == i + 1)
                    .Sum(x => (int)x.Paymenttotal);
            }
            return temp;
        }

        // GET: Balances/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var balance = await _context.Balances.FindAsync(id);
            if (balance == null)
            {
                return NotFound();
            }
            return View(balance);
        }

        // POST: Balances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BalanceLock,Profitrate,BalanceValue")] Balance balance)
        {
            if (id != balance.BalanceLock)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(balance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BalanceExists(balance.BalanceLock))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(balance);
        }


        // GET: Purchases
        public async Task<IActionResult> LoadPurchases()
        {
            return View(await _context.Userpurchaseitems.ToListAsync());
        }
        // GET: Balance Transactions
        public async Task<IActionResult> LoadBTransactions()
        {
            return View(await _context.Balancetransactions.ToListAsync());
        }
        // GET: User Transactions
        public async Task<IActionResult> LoadUTransactions()
        {
            return View(await _context.Usertransactions.ToListAsync());
        }


        // GET: Creditcards/Create
        public IActionResult CreatePurchase()
        {
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemId");
            ViewData["BuyerId"] = new SelectList(_context.Endusers, "UserId", "UserId");
            return View();
        }

        // POST: Creditcards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePurchase([Bind("PurchaseId,Purchasedate,Paymenttotal,Relatedsiterate,BuyerId,ItemId")] Userpurchaseitem purchase)
        {
            if (ModelState.IsValid)
            {
                purchase.Purchasedate = DateTime.Now;
                purchase.Paymenttotal = _context.Items.Where(i => i.ItemId == purchase.ItemId).SingleOrDefault().Price;
                purchase.Relatedsiterate = _context.Balances.Where(b => b.BalanceLock == "X").SingleOrDefault().Profitrate;
                purchase.BuyerId = _context.Endusers.OrderByDescending(r => r.Registerdate).FirstOrDefault().UserId;

                _context.Add(purchase);
                await _context.SaveChangesAsync();

                var bTrans = new Balancetransaction();
                bTrans.Btransactiondate = DateTime.Now;
                bTrans.Paymenttotal = purchase.Paymenttotal;
                bTrans.Relatedsiterate = purchase.Relatedsiterate;
                bTrans.Btransactionvalue = bTrans.Paymenttotal * bTrans.Relatedsiterate;
                bTrans.Btdescription = $"User: [{purchase.BuyerId}] have purchased: [{purchase.ItemId}] at the value of [{purchase.Paymenttotal}].";
                bTrans.PurchaseId = purchase.PurchaseId;
                bTrans.FromId = purchase.BuyerId;
                bTrans.Balancelock = _context.Balances.FirstOrDefault().BalanceLock;

                _context.Add(bTrans);
                await _context.SaveChangesAsync();

                var uTrans = new Usertransaction();
                uTrans.Utransactiondate = DateTime.Now;
                uTrans.Paymenttotal = purchase.Paymenttotal;
                uTrans.Relatedsiterate = purchase.Relatedsiterate;
                uTrans.Utransactionvalue = bTrans.Paymenttotal * (1 - bTrans.Relatedsiterate);
                uTrans.PurchaseId = purchase.PurchaseId;
                uTrans.FromId = purchase.BuyerId;
                uTrans.ToId = _context.Items.Where(i => i.ItemId == purchase.ItemId).SingleOrDefault().OwnerId;
                uTrans.Utdescription = $"User: [{uTrans.FromId}] have purchased: [{purchase.ItemId}] from user: [{uTrans.ToId}], at the value of [{purchase.Paymenttotal}].";

                _context.Add(uTrans);
                await _context.SaveChangesAsync();

                _context.Creditcards
                    .Where(c => c.CardId ==
                    (_context.Endusers.Where(u => u.UserId == uTrans.FromId).FirstOrDefault().CreditcId))
                    .FirstOrDefault()
                    .Balance -= purchase.Paymenttotal;

                _context.Balances
                    .FirstOrDefault()
                    .BalanceValue += bTrans.Btransactionvalue;

                _context.Creditcards
                    .Where(c => c.CardId ==
                    (_context.Endusers.Where(u => u.UserId == uTrans.ToId).FirstOrDefault().CreditcId))
                    .FirstOrDefault()
                    .Balance += uTrans.Utransactionvalue;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchase);
        }

        private bool BalanceExists(string id)
        {
            return _context.Balances.Any(e => e.BalanceLock == id);
        }
    }
}
