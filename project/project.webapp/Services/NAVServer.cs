using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using DocumentFormat.OpenXml.Spreadsheet;

namespace project.webapp.Services{
    public class NAVServer
    {
        public static WebManagement_PortClient mWebManagement;

        public static string NAVServiceURL, NAVServiceServerName, NAVServiceCompanyName,
            NAVServiceCodeunit, NAVServiceDomain, NAVServiceUserName, NAVServicePassword;

        public NAVServer()
        {
            // NAVServiceURL = configuration["DynamicNAV:NAVServiceURL"];
            // NAVServiceServerName = configuration["DynamicNAV:NAVServiceServerName"];
            // NAVServiceCompanyName = configuration["DynamicNAV:NAVServiceCompanyName"];
            // NAVServiceCodeunit = configuration["DynamicNAV:NAVServiceCodeunit"];
            // NAVServiceDomain = configuration["DynamicNAV:NAVServiceDomain"];
            // NAVServiceUserName = configuration["DynamicNAV:NAVServiceUserName"];
            // NAVServicePassword = configuration["DynamicNAV:NAVServicePassword"];

            // NAVServiceURL = string.Format(NAVServiceURL, NAVServiceServerName,
            //     NAVServiceCompanyName, NAVServiceCodeunit);

            // mWebManagement = new WebManagement_PortClient(new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly)
            // {
            //     Security =
            //     {
            //         Transport = { ClientCredentialType = HttpClientCredentialType.Basic }
            //     },
            //     MaxReceivedMessageSize = 20000000
            // }, new EndpointAddress(NAVServiceURL));

            // mWebManagement.ClientCredentials.UserName.UserName = string.Format("{0}\\{1}", NAVServiceDomain, NAVServiceUserName);
            // mWebManagement.ClientCredentials.UserName.Password = NAVServicePassword;
        }

        public string ServiceURL
        {
            get { return NAVServiceURL; }
        }

        // public bool InsertWebBasket(DateTime dateTime, string userID, string itemNo, decimal quantity, string salesDescription,
        //     ref string aException)
        // {
        //     try
        //     {
        //         mWebManagement.InsertWebBasket(userID, itemNo, quantity, salesDescription);
        //         return true;
        //     }
        //     catch (Exception exp)
        //     {
        //         aException = exp.Message;
        //         return false;
        //     }
        // }

        public static async Task<(bool success, string errorMessage)> InsertWebBasket(DateTime dateTime, string userID, string itemNo, decimal quantity, string salesDescription)
        {
            try
            {
                mWebManagement.InsertWebBasketAsync(userID, itemNo, quantity, salesDescription);
                return (true, null);
            }
            catch (Exception exp)
            {
                return (false, exp.Message);
            }
        }

        public async Task<(bool success, string errorMessage)> InsertSellout(string customerNo, string itemNo, decimal quantity, string name, string contactName, string address, string address2, 
            string phoneNo, DateTime invoiceDate, string serialNo, string taxAreaCode, string taxRegistrationNo, string city, string faxNo, string email, 
            string webSite, DateTime bilgitasInvoiceDate, string bilgitasInvoiceNo, string invoiceNo, DateTime setupDate, string description,
            bool governmentAgency)
        {
            try
            {
                mWebManagement.InsertSelloutAsync(customerNo, itemNo, quantity, name, contactName, address, address2, phoneNo, 
                    invoiceDate, serialNo, taxAreaCode, taxRegistrationNo, city, faxNo, email, webSite, bilgitasInvoiceDate, bilgitasInvoiceNo,
                    invoiceNo, setupDate, description, governmentAgency);
                return (true, null);
            }
            catch (Exception exp)
            {
                return (false, exp.Message);
            }
        }

        public async Task<(bool success, string errorMessage)> InsertCounter(
            string pServDevNo,
            int pCounterSB,
            int pCounterRK,
            DateTime pDate,
            string pExplanation,
            int pYear,
            int pMonth,
            int pFirstSB,
            int pFirstRK,
            string pBayiNo
            )
        {
            try
            {
                mWebManagement.EnterCounterAsync(pServDevNo, 
                    pCounterSB, 
                    pCounterRK, 
                    pDate, 
                    pExplanation,
                    pYear,
                    pMonth,
                    pFirstSB,
                    pFirstRK,
                    pBayiNo);
                return (true, null);
            }
            catch (Exception exp)
            {
                return (false, exp.Message);
            }
        }

        // public bool ValidateUser(string companyp, string userIDp, string passwordp, 
        //     ref bool resetPasswordp, ref string customerNop, ref int webVisibilityp, ref string[] eMails, ref string namep, ref string aException)
        // {
        //     try
        //     {
        //         return mWebManagement.ValidateUser(companyp, userIDp, passwordp, ref resetPasswordp, ref customerNop, ref webVisibilityp, ref eMails, ref namep);
        //     }
        //     catch (Exception exc)
        //     {
        //         aException = exc.Message;
        //         return false;
        //     }
        // }

        // public async Task<(bool success, string errorMessage)> ChangePassword(string companyp, string userIDp, string passwordp, string newPasswordp,
        //     ref string aException)
        // {
        //     try
        //     {
        //         return mWebManagement.ChangePasswordAsync(companyp, userIDp, passwordp, newPasswordp);
        //     }
        //     catch (Exception exp)
        //     {
        //         return (false, exp.Message);
        //     }
        // }

        public static async Task<(bool success, string errorMessage)> ConfirmWebBasket(string pUserId)
        {
            try
            {
                await mWebManagement.ConfirmWebBasketAsync(pUserId);
                return (true, null);
            }

            catch (Exception exc)
            {
                return (false, exc.Message);
            }
        }

        // public async Task<int> FindItemSerial(string pUserID, string pSerialNo, string pDealerNo)
        // {
        //     try
        //     {
        //         var result = await mWebManagement.FindItemSerialAsync(pSerialNo, pDealerNo, pUserID);
                
        //         return 1;
        //     }

        //     catch (Exception exc)
        //     {
        //         // Exceptionp = exc.Message;
        //         Console.WriteLine(exc.Message);
        //         return 0;
        //     }
        // }

        // public bool UpdateWarReqEntry(int pEntryNo, string pSpareItemNo, string pSpareOldSerialNo, string pSpareNewSerialNo,
        //             DateTime pRepairDate, int pNumaratoratProblem, int pNumaratoratRepair, string pProblemCode, string pProblemDescription,
        //             string pUserID,
        //     ref string pReferenceNo,
        //     ref string Exceptionp)
        // {
        //     try
        //     {
        //         mWebManagement.UpdateWarReqEntry(pEntryNo, pSpareItemNo, pSpareOldSerialNo, pSpareNewSerialNo,
        //             pRepairDate, pNumaratoratProblem, pNumaratoratRepair, pProblemCode, pProblemDescription, pUserID, 
        //             ref pReferenceNo);
        //         return true;
        //     }

        //     catch (Exception exc)
        //     {
        //         Exceptionp = exc.Message;
        //         return false;
        //     }
        // }

        // public bool CreateReport_50018(string customerNo, DateTime startDate, DateTime endDate, string reportType,
        //     ref string fileContent,
        //     ref string exception)
        // {
        //     try
        //     {
        //         mWebManagement.CreateReport_50018(customerNo, startDate, endDate, reportType, ref fileContent);
        //         return true;
        //     }
        //     catch (Exception exc)
        //     {
        //         exception = exc.Message;
        //         return false;
        //     }
        // }
        public static async Task<(bool success, string errorMessage, string fileContent)> CreateReport_50018(string pCustomerNo, DateTime pStartDate, DateTime pEndDate, string pReportType)
        {
            // PDF
            // Excel
            try
            {
                CreateReport_50018 createReport_ = new CreateReport_50018(){
                    customerNo = pCustomerNo,
                    startDate = pStartDate,
                    endDate = pEndDate,
                    reportType = pReportType,
                    fileContent = ""
                };
                var result = await mWebManagement.CreateReport_50018Async(createReport_);

                return (true, null, result.fileContent);
            }
            catch (Exception exc)
            {
                return (false, exc.Message, null);
            }
        }

        // public bool CreateReport_50162(string customerNo, DateTime startDate, DateTime endDate, string reportType,
        //     ref string fileContent,
        //     ref string exception)
        // {
        //     try
        //     {
        //         mWebManagement.CreateReport_50162(customerNo, startDate, endDate, reportType, ref fileContent);
        //         return true;
        //     }
        //     catch (Exception exc)
        //     {
        //         exception = exc.Message;
        //         return false;
        //     }
        // }
    }
}
