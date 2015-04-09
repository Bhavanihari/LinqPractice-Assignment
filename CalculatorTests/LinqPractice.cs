using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.CheckBook;
using System.Linq;
using System.Collections.ObjectModel;

namespace CalculatorTests
{
    [TestClass]
    public class LinqPractice
    {
        //==========================================================================================================================
        //                                    1. Test for Average transaction amount for each tag.
        //==========================================================================================================================
        
        [TestMethod]
        public void AvgAmntTag()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var food = ob.Transactions.Where(t => t.Tag == "Food");
            var auto = ob.Transactions.Where(t => t.Tag == "Auto");

            var total1 = food.Average(t => t.Amount);
            var total2 = auto.Average(t => t.Amount);

            Assert.AreEqual(32.625, total1);
            Assert.AreEqual(75, total2);
        }


        //==========================================================================================================================
        //                                    2. Test for Pay to Each Payee.
        //==========================================================================================================================

        [TestMethod]
        public void PaytoeachPayee()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var PayToMoshe = ob.Transactions.Where(t => t.Payee == "Moshe");
            var Amount1 = PayToMoshe.Sum(t => t.Amount);
            Assert.AreEqual(130, Amount1);

            var PayToBracha = ob.Transactions.Where(t => t.Payee == "Bracha");
            var Amount2 = PayToBracha.Sum(t=> t.Amount);
            Assert.AreEqual(131, Amount2);

            var PayToTim = ob.Transactions.Where(t => t.Payee == "Tim");
            var Amount3 = PayToTim.Sum(t => t.Amount);
            Assert.AreEqual(300, Amount3);
        }


        //==========================================================================================================================
        //                                    3. Test for Pay to Each Payee for Food.
        //==========================================================================================================================

        [TestMethod]
        public void PaytoEachPayeeForFood()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var PayToMoshe = ob.Transactions.Where(t => t.Payee == "Moshe" && t.Tag == "Food");
            var Amount1 = PayToMoshe.Sum(t => t.Amount);
            Assert.AreEqual(130, Amount1);
                               
            var PayToBracha = ob.Transactions.Where(t => t.Payee == "Bracha" && t.Tag == "Food");
            var Amount2 = PayToBracha.Sum(t => t.Amount);
            Assert.AreEqual(131, Amount2);

            var PayToTim = ob.Transactions.Where(t => t.Payee == "Tim" && t.Tag == "Food");
            var Amount3 = PayToTim.Sum(t => t.Amount);
            Assert.AreEqual(0, Amount3);

        }


        //==========================================================================================================================
        //                                    4. Test for Transaction between April 5th and 7th (Inclusive).
        //==========================================================================================================================

        [TestMethod]
        public void TrnsBtwn5n7()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var NoOfTrns = ob.Transactions.Where(t => t.Date >= DateTime.Parse("4/5/2015") && t.Date < DateTime.Parse("4/8/2015")).Count();
            Assert.AreEqual(6, NoOfTrns);
        }


        //==========================================================================================================================
        //                                    5. Test for Dates on Which Each Account was used.
        //==========================================================================================================================

        [TestMethod]
        public void DatesOfAccountsUsed()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var AccountUsed1 = ob.Transactions.Where(t => t.Account == "Checking");
            var Account1 = AccountUsed1.OrderBy(t => t.Date).Count();
            Assert.AreEqual(6, Account1);

            var AccountUsed2 = ob.Transactions.Where(t => t.Account == "Credit");
            var Account2 = AccountUsed2.OrderBy(t => t.Date).Count();
            Assert.AreEqual(6, Account2);
        }


        //==========================================================================================================================
        //                                    6. Test for Account which was used most (amount of money) on auto expenses.
        //==========================================================================================================================
        
        [TestMethod]
        public void MoreAmtOnAuto()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var CheckingAccount = ob.Transactions.Where(t => t.Tag == "Auto" && t.Account == "Checking");
            var Amount1 = CheckingAccount.Sum(t => t.Amount);

            var CreditAccount = ob.Transactions.Where(t => t.Tag == "Auto" && t.Account == "Credit");
            var Amount2 = CreditAccount.Sum(t => t.Amount);

            var Final = Amount1.CompareTo(Amount2);

            Assert.AreEqual(Amount1, Amount2);
        }


        //==========================================================================================================================
        //                                    7. Test for number of transactions from each account between April 5th and 7th.
        //==========================================================================================================================

        [TestMethod]
        public void TransBwn5n7ForEachAccount()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var CheckingAccount = ob.Transactions.Where(d => d.Date >= DateTime.Parse("4/5/2015") && d.Date < DateTime.Parse("4/8/2015") && d.Account == "Checking");
            var Count1 = CheckingAccount.Count();
            Assert.AreEqual(3, Count1);

            var CreditAccount = ob.Transactions.Where(d => d.Date >= DateTime.Parse("4/5/2015") && d.Date < DateTime.Parse("4/8/2015") && d.Account == "Credit");
            var Count2 = CreditAccount.Count();
            Assert.AreEqual(3, Count2);
        }
    }
}
