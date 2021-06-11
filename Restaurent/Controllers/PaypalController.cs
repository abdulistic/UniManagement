using PayPal.Api;
using Restaurant.ClassLibrary;
using libOrder = Restaurant.ClassLibrary.PakClassified;
using Restaurant.ClassLibrary.UsersMgt;
using Restaurent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.ClassLibrary.PakClassified;

namespace Restaurent.Controllers
{
    public class PaypalController : Controller
    {
        // GET: Paypal
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FailureView()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SuccessView()
        {
            return View();
        }


        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Paypal/PaymentWithPayPal?";

                    //here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {

                    // This function exectues after receving all parameters for the payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (PayPal.ConfigException ex)
            {
                Console.WriteLine(ex.StackTrace);
                return View("FailureView");
            }

            //on successful payment, show success page to user.
            return RedirectToAction("confirmorder", "home");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            User cUser = (User)Session[WebUtil.CurrentUser];

            libOrder.Order o = new AdvertisementsHandler().GetUserOrder(cUser.Id);
            if (o.Price > 0)
            {
                string pPrice = Convert.ToString( Math.Round((o.Price/124),2));
                //create itemlist and add item objects to it
                var itemList = new ItemList() { items = new List<Item>() };

                //Adding Item Details like name, currency, price etc
                itemList.items.Add(new Item()
                {
                    name = "Gustavo's Food",
                    currency = "USD",
                    price = pPrice,
                    quantity = "1",
                    sku = "sku"
                });

                var payer = new Payer() { payment_method = "paypal" };

                // Configure Redirect Urls here with RedirectUrls object
                var redirUrls = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&Cancel=true",
                    return_url = redirectUrl
                };

                // Adding Tax, shipping and Subtotal details
                var details = new Details()
                {
                    tax = "0",
                    shipping = "0",
                    subtotal = pPrice
                };

                //Final amount with details
                var amount = new Amount()
                {
                    currency = "USD",
                    total = pPrice, // Total must be equal to sum of tax, shipping and subtotal.
                    details = details
                };

                var transactionList = new List<Transaction>();
                // Adding description about the transaction
                transactionList.Add(new Transaction()
                {
                    description = "Transaction description",
                    invoice_number = Convert.ToString(o.Id), //Generate an Invoice No
                    amount = amount,
                    item_list = itemList
                });


                this.payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrls
                };
            }
            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }


    }
}