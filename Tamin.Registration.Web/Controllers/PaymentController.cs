using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Tamin.Registration.Utility.Payment.Pasargad;
using Tamin.Registration.Web.Models;
using System.Data.Entity.Validation;

namespace Tamin.Registration.Web.Controllers
{
    public class PaymentController : Controller
    {
        string merchantId = "5b9c52c6-b5a3-11e9-931d-000c295eb8fc";
        #region Saman Result

        string refrenceNumber = string.Empty;
        string reservationNumber = string.Empty;
        string transactionState = string.Empty;
        string traceNumber = string.Empty;
        bool isError = false;
        string errorMsg = "";
        string succeedMsg = "";

        //public ActionResult SepResult(string RefNum, string ResNum, string state, string TraceNo)
        //{
        //    try
        //    {
        //        if (RequestUnpack())
        //        {
        //            if (transactionState.Equals("OK"))
        //            {
        //                ///////////////////////////////////////////////////////////////////////////////////
        //                //   *** IMPORTANT  ****   ATTENTION
        //                // Here you should check refrenceNumber in your DataBase tp prevent double spending
        //                ///////////////////////////////////////////////////////////////////////////////////

        //                ///For Ignore SSL Error
        //                ServicePointManager.ServerCertificateValidationCallback =
        //                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

        //                ///WebService Instance
        //                var srv = new Naseri.Portal.Utility.Sep.RefrencePayment.PaymentIFBindingSoapClient();//.PaymentIFBinding();
        //                var result = srv.verifyTransaction(Request.Form["RefNum"], Request.Form["MID"]);

        //                if (result > 0)
        //                {
        //                    if (result == 1000) // چک کردن مبلغ بازگشتی از سرویس با مبلغ تراکنش
        //                    {
        //                        isError = false;
        //                        //  lblRewsult.Text = succeedMsg = "بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل گشت";
        //                        //  lblRewsult.Text += "<br/>" + " شماره رسید : " + refrenceNumber;
        //                    }
        //                    else
        //                    {
        //                        string userName = Request.Form["MID"];//نام کاربری همان ام آی دی است
        //                        string pass = ""; // رمز عبور برای شما ایمیل شده است
        //                        srv.reverseTransaction(Request.Form["RefNum"], Request.Form["MID"], userName, pass);
        //                    }
        //                }

        //                else
        //                {
        //                    //  lblRewsult.Text = TransactionChecking((int)result);
        //                }
        //            }
        //            else
        //            {
        //                isError = true;
        //                errorMsg = "متاسفانه بانک خريد شما را تاييد نکرده است";

        //                if (transactionState.Equals("Canceled By User") || transactionState.Equals(string.Empty))
        //                {
        //                    // Transaction was canceled by user
        //                    isError = true;
        //                    errorMsg = "تراكنش توسط خريدار كنسل شد";
        //                }
        //                else if (transactionState.Equals("Invalid Amount"))
        //                {
        //                    // Amount of revers teransaction is more than teransaction
        //                }
        //                else if (transactionState.Equals("Invalid Transaction"))
        //                {
        //                    // Can not find teransaction
        //                }
        //                else if (transactionState.Equals("Invalid Card Number"))
        //                {
        //                    // Card number is wrong
        //                }
        //                else if (transactionState.Equals("No Such Issuer"))
        //                {
        //                    // Issuer can not find
        //                }
        //                else if (transactionState.Equals("Expired Card Pick Up"))
        //                {
        //                    // The card is expired
        //                }
        //                else if (transactionState.Equals("Allowable PIN Tries Exceeded Pick Up"))
        //                {
        //                    // For third time user enter a wrong PIN so card become invalid
        //                }
        //                else if (transactionState.Equals("Incorrect PIN"))
        //                {
        //                    // Pin card is wrong
        //                }
        //                else if (transactionState.Equals("Exceeds Withdrawal Amount Limit"))
        //                {
        //                    // Exceeds withdrawal from amount limit
        //                }
        //                else if (transactionState.Equals("Transaction Cannot Be Completed"))
        //                {
        //                    // PIN and PAD are currect but Transaction Cannot Be Completed
        //                }
        //                else if (transactionState.Equals("Response Received Too Late"))
        //                {
        //                    // Timeout occur
        //                }
        //                else if (transactionState.Equals("Suspected Fraud Pick Up"))
        //                {
        //                    // User did not insert cvv2 & expiredate or they are wrong.
        //                }
        //                else if (transactionState.Equals("No Sufficient Funds"))
        //                {
        //                    // there are not suficient funds in the account
        //                }
        //                else if (transactionState.Equals("Issuer Down Slm"))
        //                {
        //                    // The bank server is down
        //                }
        //                else if (transactionState.Equals("TME Error"))
        //                {
        //                    // unknown error occur
        //                }

        //            }

        //        }

        //    }

        //    catch (Exception ex)
        //    {

        //    }

        //    return View();
        //}


        public ActionResult SepResult()
        {
            var r = new PaymentResult();
            try
            {
                if (RequestUnpack())
                {
                    if (transactionState.Equals("OK"))
                    {
                        ///////////////////////////////////////////////////////////////////////////////////
                        //   *** IMPORTANT  ****   ATTENTION
                        // Here you should check refrenceNumber in your DataBase tp prevent double spending
                        ///////////////////////////////////////////////////////////////////////////////////

                        using (var db = new DataLayer.RegistrationDbContext())
                        {
                            var orders = db.RegisterForms.Where(o => o.ReferenceNumber == refrenceNumber).ToList();
                            if (orders != null && orders.Count() > 0)
                            {
                                r.Result = false;
                                r.Message = " تراکنش معتبر نیست! ReferenceNumber تکراری ارسال شده است. ";
                                return View("Result", r);
                            }
                        }
                        ///For Ignore SSL Error
                        ServicePointManager.ServerCertificateValidationCallback =
                            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                        ///WebService Instance
                        var srv = new Utility.Sep.Payment.PaymentIFBindingSoapClient();// PaymentIFBinding();//.PaymentIFBinding();
                        var result = srv.verifyTransaction(Request.Form["RefNum"], Request.Form["MID"]);

                        if (result > 0)
                        {
                            using (var db = new DataLayer.RegistrationDbContext())
                            {
                                int resNum = 0;
                                if (!int.TryParse(reservationNumber, out resNum))
                                {
                                    r.Result = false;
                                    r.Message = " شماره فاکتور اشتباه ارسال شده است! ";
                                    return View("Result", r);
                                }
                                var order = db.RegisterForms.Find(resNum);
                                if (result == order.Total)
                                {
                                    order.IsPaied = true;
                                    order.PaiedOn = DateTime.Now;

                                    order.TraceNumber = traceNumber;
                                    order.ReferenceNumber = refrenceNumber;
                                    order.TransactionReferenceID = transactionState;
                                    order.TransactionDate = DateTime.Now.ToString();

                                    db.SaveChanges();

                                    var viewResult = new PaymentResult()
                                    {
                                        invoiceNumber = order.Id.ToString(),
                                        Message = "پرداخت شما با موفقیت انجام شده است.",
                                        PayOn = order.TransactionDate,
                                        referenceNumber = order.ReferenceNumber,
                                        Result = true,
                                        traceNumber = order.TraceNumber,
                                        transactionReferenceID = order.TransactionReferenceID
                                    };

                                    return View(viewResult);
                                }
                                else
                                {
                                    string userName = Request.Form["MID"];//نام کاربری همان ام آی دی است
                                    string pass = "6047156"; // رمز عبور برای شما ایمیل شده است
                                    srv.reverseTransaction(Request.Form["RefNum"], Request.Form["MID"], userName, pass);
                                }
                            }


                        }

                        else
                        {
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = TransactionChecking((int)result)
                            };

                            return View(viewResult);
                            //  lblRewsult.Text = TransactionChecking((int)result);
                        }
                    }
                    else
                    {
                        isError = true;
                        errorMsg = "متاسفانه بانک خريد شما را تاييد نکرده است";

                        if (transactionState.Equals("Canceled By User") || transactionState.Equals(string.Empty))
                        {
                            // Transaction was canceled by user
                            isError = true;
                            errorMsg = "تراكنش توسط خريدار كنسل شد";
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "تراكنش توسط خريدار كنسل شد"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Invalid Amount"))
                        {
                            // Amount of revers teransaction is more than teransaction
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "Amount of revers teransaction is more than teransaction"
                            };

                            return View(viewResult);

                        }
                        else if (transactionState.Equals("Invalid Transaction"))
                        {
                            // Can not find teransaction
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "Can not find teransaction"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Invalid Card Number"))
                        {
                            // Card number is wrong

                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "Card number is wrong"
                            };

                            return View(viewResult);

                        }
                        else if (transactionState.Equals("No Such Issuer"))
                        {
                            // Issuer can not find

                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "Issuer can not find"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Expired Card Pick Up"))
                        {
                            // The card is expired
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = " The card is expired"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Allowable PIN Tries Exceeded Pick Up"))
                        {
                            // For third time user enter a wrong PIN so card become invalid
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = " For third time user enter a wrong PIN so card become invalid"
                            };

                            return View(viewResult);

                        }
                        else if (transactionState.Equals("Incorrect PIN"))
                        {
                            // Pin card is wrong
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "  Pin card is wrong"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Exceeds Withdrawal Amount Limit"))
                        {
                            // Exceeds withdrawal from amount limit
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "Exceeds withdrawal from amount limit"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Transaction Cannot Be Completed"))
                        {
                            // PIN and PAD are currect but Transaction Cannot Be Completed
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "PIN and PAD are currect but Transaction Cannot Be Completed"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Response Received Too Late"))
                        {
                            // Timeout occur
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "Timeout occur"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Suspected Fraud Pick Up"))
                        {
                            // User did not insert cvv2 & expiredate or they are wrong.
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "User did not insert cvv2 & expiredate or they are wrong."
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("No Sufficient Funds"))
                        {
                            // there are not suficient funds in the account
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "there are not suficient funds in the account"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("Issuer Down Slm"))
                        {
                            // The bank server is down
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "The bank server is down"
                            };

                            return View(viewResult);
                        }
                        else if (transactionState.Equals("TME Error"))
                        {
                            // unknown error occur
                            var viewResult = new PaymentResult()
                            {
                                Result = false,
                                Message = "unknown error occur"
                            };

                            return View(viewResult);
                        }

                    }

                }

            }

            catch (Exception ex)
            {
                var viewResultx = new PaymentResult()
                {
                    Result = false,
                    Message = ex.Message
                };

                return View(viewResultx);
            }

            var viewResultt = new PaymentResult()
            {
                Result = false,
                Message = "unknown error occur"
            };

            return View(viewResultt);
        }

        private bool RequestUnpack()
        {
            if (RequestFeildIsEmpty()) return false;

            refrenceNumber = Request.Form["RefNum"].ToString();
            reservationNumber = Request.Form["ResNum"].ToString();
            transactionState = Request.Form["state"].ToString();
            traceNumber = Request.Form["TraceNo"].ToString();

            return true;
        }

        private bool RequestFeildIsEmpty()
        {
            if (Request.Form["state"].ToString().Equals(string.Empty))
            {
                isError = true;
                errorMsg = "خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت! مشکلي در فرايند رزرو خريد شما پيش آمده است";
                return true;
            }

            if (Request.Form["RefNum"].ToString().Equals(string.Empty) && Request.Form["state"].ToString().Equals(string.Empty))
            {
                isError = true;
                errorMsg = "فرايند انتقال وجه با موفقيت انجام شده است اما فرايند تاييد رسيد ديجيتالي با خطا مواجه گشت";
                return true;
            }

            if (Request.Form["ResNum"].ToString().Equals(string.Empty) && Request.Form["state"].ToString().Equals(string.Empty))
            {
                isError = true;
                errorMsg = "خطا در برقرار ارتباط با بانک";
                return true;
            }
            return false;
        }

        private string TransactionChecking(int i)
        {
            bool isError = false;
            string errorMsg = "";
            switch (i)
            {

                case -1:		//TP_ERROR
                    isError = true;
                    errorMsg = "بروز خطا درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-1";
                    break;
                case -2:		//ACCOUNTS_DONT_MATCH
                    isError = true;
                    errorMsg = "بروز خطا در هنگام تاييد رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-2";
                    break;
                case -3:		//BAD_INPUT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-3";
                    break;
                case -4:		//INVALID_PASSWORD_OR_ACCOUNT
                    isError = true;
                    errorMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-4";
                    break;
                case -5:		//DATABASE_EXCEPTION
                    isError = true;
                    errorMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-5";
                    break;
                case -7:		//ERROR_STR_NULL
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-7";
                    break;
                case -8:		//ERROR_STR_TOO_LONG
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-8";
                    break;
                case -9:		//ERROR_STR_NOT_AL_NUM
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-9";
                    break;
                case -10:	//ERROR_STR_NOT_BASE64
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-10";
                    break;
                case -11:	//ERROR_STR_TOO_SHORT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-11";
                    break;
                case -12:		//ERROR_STR_NULL
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-12";
                    break;
                case -13:		//ERROR IN AMOUNT OF REVERS TRANSACTION AMOUNT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-13";
                    break;
                case -14:	//INVALID TRANSACTION
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-14";
                    break;
                case -15:	//RETERNED AMOUNT IS WRONG
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-15";
                    break;
                case -16:	//INTERNAL ERROR
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-16";
                    break;
                case -17:	// REVERS TRANSACTIN FROM OTHER BANK
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-17";
                    break;
                case -18:	//INVALID IP
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-18";
                    break;

            }
            return errorMsg;
        }

        #endregion
        public ActionResult Result()
        {
            //اطلاعات زیر جهت ارجاع فاکتور از بانک می باشد
            string invoiceNumber = Request.QueryString["iN"]; // شماره فاکتور
            string invoiceDate = Request.QueryString["iD"]; // تاریخ فاکتور
            string TransactionReferenceID = Request.QueryString["tref"]; // شماره مرجع

            string strXML;

            try
            {

                strXML = ReadPaymentResult();

                if (strXML == null)
                {
                    return Content("read xml failed");
                }
            }
            catch
            {
                return Content("read xml failed");
            }
            var iNumber = invoiceNumber;
            var iDate = invoiceDate;
            var trf = TransactionReferenceID;

            ///نگهداری اطلاعات برای انجام مرحله دوم
            Session.Add("InvoiceNumber", invoiceNumber);
            Session.Add("InvoiceDate", invoiceDate);

            ///

            //در صورتی که تراکنشی انجام نشده باشد فایلی از بانک برگشت داده نمی شود  
            //دستور شزطی زیر جهت اعلام نتیجه به کاربر است
            if (strXML == "")
            {
                Response.Write("تراکنش  انجام نشد ");
            }
            else
            {

                XmlDocument oXml = new XmlDocument();
                try
                {
                    oXml.LoadXml(strXML);
                }
                catch
                {
                    return Content(strXML);
                    //return Content("first xml failed");
                }
                XmlElement oElResult;
                try
                {
                    oElResult = (XmlElement)oXml.SelectSingleNode("//result"); //نتیجه تراکنش

                }
                catch (Exception)
                {

                    return Content("oElResult");
                }
                XmlElement _oElTraceNumber; try
                {
                    _oElTraceNumber = (XmlElement)oXml.SelectSingleNode("//traceNumber"); //شماره پیگیری
                }
                catch
                {
                    return Content("_oElTraceNumber");
                }
                XmlElement _TXNreferenceNumber; try
                {
                    _TXNreferenceNumber = (XmlElement)oXml.SelectSingleNode("//referenceNumber"); //شماره ارجاع
                }
                catch
                {
                    return Content("_TXNreferenceNumber");
                }

                XmlElement _invoiceNumber; try
                {
                    _invoiceNumber = (XmlElement)oXml.SelectSingleNode("//invoiceNumber"); //نتیجه تراکنش
                }
                catch
                {
                    return Content("_invoiceNumber");
                }
                XmlElement _transactionReferenceID; try
                {
                    _transactionReferenceID = (XmlElement)oXml.SelectSingleNode("//transactionReferenceID"); //نتیجه تراکنش
                }
                catch
                { return Content("_transactionReferenceID"); }


                XmlElement _transactionDate;
                try
                {
                    _transactionDate = (XmlElement)oXml.SelectSingleNode("//transactionDate"); //نتیجه تراکنش
                }
                catch
                {
                    return Content("_transactionDate");
                }

                XmlElement _amount;
                try
                {
                    _amount = (XmlElement)oXml.SelectSingleNode("//amount"); //نتیجه تراکنش
                }
                catch
                {
                    return Content("_amount");
                }

                if (bool.Parse(oElResult.InnerText))
                {
                    if (iNumber == _invoiceNumber.InnerText)
                    {
                        using (var db = new DataLayer.RegistrationDbContext())
                        {
                            var pay = db.RegisterForms.Find(int.Parse(iNumber));
                            if (pay.Total == int.Parse(_amount.InnerText))
                            {

                                #region send Feedback
                                /*
                                string merchantCode = "4273469";//appRead.GetValue("MerchantCode", typeof(string)).ToString();
                                string terminalCode = "1441976";// appRead.GetValue("TerminalCode", typeof(string)).ToString();
                                var amount = pay.Total;  //  ﻣﺒﻠﻎ ﻓﺎﮐﺘﻮر 
                                var xinvoiceNumber = pay.Id; //  ﺷﻤﺎره ﻓﺎﮐﺘﻮر  
                                var xinvoiceDate = pay.CreateOn.ToString("yyyy/MM/dd HH:mm:ss"); ; //  ﺗﺎرﯾﺦ ﻓﺎﮐﺘﻮر 
                                var timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                                string PrivateKey = "<RSAKeyValue><Modulus>tKNv7j6rzfnLoOevYtnOLGPP9FFcveWGQfw57LktAvARRNCPycOJPJVtJQSkPhd4DAmC6EDYra+m27uUgV4n/cMNRgO+AlTm/hTCIkO1yHR8oURK0VbVV2iLvX5xaINRIlhlQhU4l7UGFMPEqS+JFzyn+gbfDueCNsU/IhBrodc=</Modulus><Exponent>AQAB</Exponent><P>9JcHVtBI1lhgZXbjPjpB+jFBULIMbHOWNBDVvhiyqqBLPb7lom0kYJATJbVMkuzVoB4HcSgergd3aU/eJ2ZUKw==</P><Q>vRCt0f2C7jdsP1KVGy7vzXsZsB78JZZ6GjMmbN8RdKvtDn3t7Pk4YpDcnjsyj7vo/bPD/EMyvX9B41WRTOR3BQ==</Q><DP>U+LcjITBBmqnHQWKaJQ4fqOYACIgqq117YN8m0cDOAvj4lhvj6aZHFkth/hHO/joR5MlAEU/SHadGMxgp+irow==</DP><DQ>Tc6+aiw1pQnED69R09UWNW9S3At0Y5ew+nVQe/+1dFmI/qzOrPbHwLCzSp88KLEBqt8/aeLRz/C+UeuWF5nybQ==</DQ><InverseQ>gkjT4WVPybiQzpFgPGRI6ZyV4h5JCL1u49lc6SC6HMtWr0GgNRaJawurbclDU9S9ADury6Vs0FGVoMC1bGKpqA==</InverseQ><D>mU3r/MJrhM/vJYv7qWU4OSwsnSlAEFnva6jocCNoNAoSXdizb4Tnv+cBTl12FsxMMcsQspP2UNG2Rt4X1wNV4/PPwvRM0cIGfgqNe4dBoM7hpAA4J0/9RUbkhwqVPhM0qH3RApvIJAaPdqs4IMf0zsFe/QV22OWUNpRXvOIMbKE=</D></RSAKeyValue>";
                                rsa.FromXmlString(PrivateKey);
                                string data = "#" + merchantCode + "#" + terminalCode + "#" + xinvoiceNumber + "#" + xinvoiceDate + "#" + amount + "#" + timeStamp + "#";
                                byte[] signMain = rsa.SignData(Encoding.UTF8.GetBytes(data), new SHA1CryptoServiceProvider());
                                var sign = Convert.ToBase64String(signMain);
                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pep.shaparak.ir/VerifyPayment.aspx");
                                string text = " InvoiceNumber =" + xinvoiceNumber + "& InvoiceDate=" + xinvoiceDate + "&MerchantCode=" + merchantCode + "&TerminalCode=" + terminalCode + "& Amount=" + amount + "& TimeStamp=" + timeStamp + "&Sign=" + sign;
                                byte[] textArray = Encoding.UTF8.GetBytes(text);
                                request.Method = "POST";
                                request.ContentType = "application/x-www-form-urlencoded";
                                request.ContentLength = textArray.Length;
                                request.GetRequestStream().Write(textArray, 0, textArray.Length);
                                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                StreamReader reader = new StreamReader(response.GetResponseStream());
                                string result = reader.ReadToEnd();

                            //    return Content(result);
                                oXml.LoadXml(result);
                                try
                                {

                                    XmlElement xmlresult = (XmlElement)oXml.SelectSingleNode("//result"); //نتیجه تراکنش
                                    if (!bool.Parse(xmlresult.InnerText))
                                    {
                                        return Content(result);
                                    }
                                }
                                catch
                                {
                                    return Content("xml 2 failed");

                                }


    */
                                #endregion


                                #region self
                                AppSettingsReader appRead = new AppSettingsReader();

                                //دریافت تنظیمات از web.config
                                string merchantCode = "4450022";//appRead.GetValue("MerchantCode", typeof(string)).ToString();
                                string terminalCode = "1628035";// appRead.GetValue("TerminalCode", typeof(string)).ToString();
                                                                // string redirectAddress = "http://azmoon.kartamin.ir/Payment/Result";//appRead.GetValue("RedirectAddress", typeof(string)).ToString();
                                string PrivateKey = " <RSAKeyValue><Modulus>vu1JWqa+weHOQbCCG17+F+oCLCj4VPciUyoFpweBY+oe2DpU4LcwHeZiWcFW3NsrIfi7vm2T9IcLwux3q8IUO5twJ6QM2SiYC/5quZT0Fmf+lxucdcKZJC/B21DA2JHZ4ldgUhjrtozyP6a3xCeZx7DGMkWn8MqQA1uE4cEbTkU=</Modulus><Exponent>AQAB</Exponent><P>9fs3BHFSAXKdiEODw5nh4t+0Rs3uAvVIqmRkV1l5EaUQwCFjj3/4uYdLJWF8u9YhdzmkKXnbNCMu2xwDACy87Q==</P><Q>xrQIf61qoG/y34iDbuvK67oLry5FooQP2mkbjPg/dF1qKqSzIfzyTa+Qnej3B56md6qKV+ZmWcF22eXVwtADuQ==</Q><DP>VMuE68MkwdsA8zhS89rYQ51aSA41Pk/P/O0eqf3t/mconxLjf1ReKZa6EOjKVvY6Ex+Lt8CKEC8Qt/ewER9bAQ==</DP><DQ>OBA10ahhTFEpyq4ev14iC+6bO1sn5Jm0S2CamGS2qqNswAlmTXGsAAVIHXXMtUarG1pv3Csyt6JhYUt6y5ObaQ==</DQ><InverseQ>Z85qWekGz3EVeS4eoceXe+H725a/oCu4Og8Sm20eO843r0FEU9326hz9f8KlRHBpYcbbS0s/JoY7m8w9cHHqLg==</InverseQ><D>kMxW8Ig7bcE58vnRgr6lSC+yHBmqVI3lG1toVAfOKp95axW6H37u4A5Ekrudi/wwFyCUClUCe9YbpmY+UCXtvwHgHWtx43KKmsNdcf5SrIWoQ78Gbn22CnmJvvhawtwYLhjgU6MbFQFEX/owgmKg6NL6/U5T62PAt1K8WAqZ02E=</D></RSAKeyValue>";


                                string amount = pay.Total.ToString();
                                string timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                                invoiceDate = pay.RegisterOn.ToString("yyyy/MM/dd HH:mm:ss");// txtInvoiceDate.Text;
                                invoiceNumber = pay.Id.ToString();




                                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                                rsa.FromXmlString(PrivateKey);

                                string data = "#" + merchantCode + "#" + terminalCode + "#" + invoiceNumber +
                              "#" + invoiceDate + "#" + amount + "#" + timeStamp + "#";

                                byte[] signedData = rsa.SignData(Encoding.UTF8.GetBytes(data), new
                                SHA1CryptoServiceProvider());

                                string signedString = Convert.ToBase64String(signedData);

                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pep.shaparak.ir/VerifyPayment.aspx");
                                string text = "InvoiceNumber=" + invoiceNumber + "&InvoiceDate=" +
                                            invoiceDate + "&MerchantCode=" + merchantCode + "&TerminalCode=" +
                                            terminalCode + "&Amount=" + amount + "&TimeStamp=" + timeStamp + "&Sign=" + signedString;
                                byte[] textArray = Encoding.UTF8.GetBytes(text);
                                request.Method = "POST";
                                request.ContentType = "application/x-www-form-urlencoded";
                                request.ContentLength = textArray.Length;
                                request.GetRequestStream().Write(textArray, 0, textArray.Length);
                                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                StreamReader reader = new StreamReader(response.GetResponseStream());
                                string result = reader.ReadToEnd();
                                #endregion

                                try
                                {
                                    oXml.LoadXml(result);
                                    XmlElement xmlresult = (XmlElement)oXml.SelectSingleNode("//result"); //نتیجه تراکنش

                                    // return Content(xmlresult.InnerText);
                                    if (xmlresult.InnerText.Trim() == "True")
                                    {
                                        // true
                                        var lastEntity = db.RegisterForms.Where(o => o.IsPaied == true && o.Gender == pay.Gender).OrderByDescending(o => o.Id).FirstOrDefault();
                                        if (lastEntity == null)
                                        {
                                            if (pay.Gender)
                                            {
                                                pay.SeatNumber = "2000";
                                            }
                                            else
                                            {
                                                pay.SeatNumber = "1000";
                                            }
                                        }
                                        else
                                        {
                                            pay.SeatNumber = (int.Parse(lastEntity.SeatNumber) + 1).ToString();
                                        }
                                        pay.IsPaied = true;
                                        pay.PaiedOn = DateTime.Now;

                                        pay.TraceNumber = _oElTraceNumber.InnerText;
                                        pay.ReferenceNumber = _TXNreferenceNumber.InnerText;
                                        pay.TransactionReferenceID = _transactionReferenceID.InnerText;
                                        pay.TransactionDate = _transactionDate.InnerText;

                                        try
                                        {

                                            db.SaveChanges();
                                        }

                                        catch (DbEntityValidationException e)
                                        {
                                            var content = "";
                                            foreach (var eve in e.EntityValidationErrors)
                                            {
                                                content += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                                    eve.Entry.Entity.GetType().Name, eve.Entry.State + "<br />");
                                                foreach (var ve in eve.ValidationErrors)
                                                {
                                                    content += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                                        ve.PropertyName, ve.ErrorMessage + "<br />");
                                                }
                                            }
                                            return Content(content);
                                        }

                                        var viewResult = new PaymentResult()
                                        {
                                            invoiceNumber = pay.Id.ToString(),
                                            Message = "پرداخت شما با موفقیت انجام شده است.",
                                            PayOn = pay.TransactionDate,
                                            referenceNumber = pay.ReferenceNumber,
                                            Result = true,
                                            traceNumber = pay.TraceNumber,
                                            transactionReferenceID = pay.TransactionReferenceID
                                        };

                                        return View(viewResult);

                                    }

                                    return View(new PaymentResult()
                                    {
                                        Result = false,
                                        Message = "نتیجه بازگشتی صحیح نیست"
                                    });

                                }
                                catch (Exception ex)
                                {
                                    return View(new PaymentResult()
                                    {
                                        Result = false,
                                        Message = ex.Message
                                    });
                                }
                            }
                            else
                            {
                                return View(new PaymentResult()
                                {
                                    Result = false,
                                    Message = "مقادیر فاکتور برابر نیست"

                                });
                            }
                        }
                    }
                    else
                    {
                        return View(new PaymentResult()
                        {
                            Result = false,
                            Message = "شماره فاکتور ها صحیح نیست"
                        });
                    }
                }
                else
                {
                    return View(new PaymentResult()
                    {
                        Result = false,
                        Message = "بانک صحیح نمی داند"
                    });
                }

            }

            return View(new PaymentResult()
            {
                Result = false,
                Message = "هر خطای دیگری"
            });
        }
        private string ReadPaymentResult()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://pep.shaparak.ir/CheckTransactionResult.aspx");
                string text = "invoiceUID=" + Request.QueryString["tref"];
                byte[] textArray = Encoding.UTF8.GetBytes(text);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = textArray.Length;
                request.GetRequestStream().Write(textArray, 0, textArray.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public ActionResult ZarinResult(string Authority, string Status)

        {
            if (Status.ToLower() == "ok")
            {

                Utility.ZarinServiceReference.PaymentGatewayImplementationServicePortTypeClient request = new Utility.ZarinServiceReference.PaymentGatewayImplementationServicePortTypeClient();

                using (var db = new Tamin.Registration.DataLayer.RegistrationDbContext())
                {
                    var order = db.RegisterForms.Where(o => o.TraceNumber == Authority).FirstOrDefault();
                    long refId;
                    var result = request.PaymentVerification(merchantId, Authority, order.Total, out refId);

                    order.TransactionReferenceID = refId.ToString();
                    order.IsPaied = true;
                    order.PaiedOn = DateTime.Now;
                    db.SaveChanges();

                }
                return Content("پرداخت با موفقیت انجام شد.");
            }
            else
            {
                return Content("پرداخت با موفقیت انجام نشده است!");
            }
        }

        [HttpPost]
        public ActionResult Pay(int id)
        {
            string amount, invoiceDate, invoiceNumber;
            using (var db = new DataLayer.RegistrationDbContext())
            {
                var order = db.RegisterForms.Find(id);

                //  amount = order.Total.ToString();
                //  invoiceDate = order.RegisterOn.ToString("yyyy/MM/dd HH:mm:ss");
                //  invoiceNumber = order.Id.ToString();

                #region Pasargad
                //  AppSettingsReader appRead = new AppSettingsReader();


                //  //دریافت تنظیمات از web.config
                //  string merchantCode = "0000";//appRead.GetValue("MerchantCode", typeof(string)).ToString();
                //  string terminalCode = "0000";// appRead.GetValue("TerminalCode", typeof(string)).ToString();
                //  string redirectAddress = "http://karamozesh.ir/Payment/Result";//appRead.GetValue("RedirectAddress", typeof(string)).ToString();
                //  string PrivateKey = "<RSAKeyValue><Modulus>0u1JWqa+weHOQbCCG17+F+oCLCj4VPciUyoFpweBY+oe2DpU4LcwHeZiWcFW3NsrIfi7vm2T9IcLwux3q8IUO5twJ6QM2SiYC/5quZT0Fmf+lxucdcKZJC/B21DA2JHZ4ldgUhjrtozyP6a3xCeZx7DGMkWn8MqQA1uE4cEbTkU=</Modulus><Exponent>AQAB</Exponent><P>9fs3BHFSAXKdiEODw5nh4t+0Rs3uAvVIqmRkV1l5EaUQwCFjj3/4uYdLJWF8u9YhdzmkKXnbNCMu2xwDACy87Q==</P><Q>xrQIf61qoG/y34iDbuvK67oLry5FooQP2mkbjPg/dF1qKqSzIfzyTa+Qnej3B56md6qKV+ZmWcF22eXVwtADuQ==</Q><DP>VMuE68MkwdsA8zhS89rYQ51aSA41Pk/P/O0eqf3t/mconxLjf1ReKZa6EOjKVvY6Ex+Lt8CKEC8Qt/ewER9bAQ==</DP><DQ>OBA10ahhTFEpyq4ev14iC+6bO1sn5Jm0S2CamGS2qqNswAlmTXGsAAVIHXXMtUarG1pv3Csyt6JhYUt6y5ObaQ==</DQ><InverseQ>Z85qWekGz3EVeS4eoceXe+H725a/oCu4Og8Sm20eO843r0FEU9326hz9f8KlRHBpYcbbS0s/JoY7m8w9cHHqLg==</InverseQ><D>kMxW8Ig7bcE58vnRgr6lSC+yHBmqVI3lG1toVAfOKp95axW6H37u4A5Ekrudi/wwFyCUClUCe9YbpmY+UCXtvwHgHWtx43KKmsNdcf5SrIWoQ78Gbn22CnmJvvhawtwYLhjgU6MbFQFEX/owgmKg6NL6/U5T62PAt1K8WAqZ02E=</D></RSAKeyValue>";




                //  //تاریخ فاکتور و زمان اجرای عملیات از سیستم گرفته می شود
                //  //شما می توانید تاریخ فاکتور را به صورت دستی وارد نمایید 
                //  string timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");


                //  string ActionIs = "1003";




                //  RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                //  rsa.FromXmlString(PrivateKey);

                //  string data = "#" + merchantCode + "#" + terminalCode + "#" + invoiceNumber +
                //"#" + invoiceDate + "#" + amount + "#" + redirectAddress + "#" + ActionIs + "#" + timeStamp + "#";

                //  byte[] signedData = rsa.SignData(System.Text.Encoding.UTF8.GetBytes(data), new
                //  SHA1CryptoServiceProvider());

                //  string signedString = Convert.ToBase64String(signedData);
                //  DataPost dp = new DataPost();
                //  dp.Url = "https://pep.shaparak.ir/gateway.aspx";
                //  dp.FormName = "form1";
                //  dp.Method = "post";
                //  dp.AddKey("merchantCode", merchantCode);
                //  dp.AddKey("terminalCode", terminalCode);
                //  dp.AddKey("amount", amount);
                //  dp.AddKey("redirectAddress", redirectAddress);
                //  dp.AddKey("invoiceNumber", invoiceNumber);
                //  dp.AddKey("invoiceDate", invoiceDate);
                //  dp.AddKey("action", ActionIs);
                //  dp.AddKey("sign", signedString);
                //  dp.AddKey("timeStamp", timeStamp);
                //  dp.Post();
                //  return View();
                #endregion

                #region Zarinpal
                Utility.ZarinServiceReference.PaymentGatewayImplementationServicePortTypeClient request = new Utility.ZarinServiceReference.PaymentGatewayImplementationServicePortTypeClient();
                string autohority = string.Empty;

                var result = request.PaymentRequest(
                     merchantId, 728907, "هزینه ثبت نام", order.Email, order.Mobile, "http://karamozesh.ir/payment/zarinresult", out autohority
                     );
                if (result > 0)
                {
                    order.TraceNumber = autohority;
                    db.SaveChanges();
                    var url = "https://www.zarinpal.com/pg/StartPay/" + long.Parse(autohority);
                    return Redirect(url);
                }
                else
                {
                    return Content("Error: " + result);
                }

                #endregion


            }
            return View();
        }

    }

    public class PhonenumberResults
    {
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
    }
}