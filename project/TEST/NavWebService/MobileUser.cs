// using System;
// using System.Collections.Generic;
// using System.Web;
// using System.Data;
// using System.Data.SqlClient;
// using System.Collections;

// namespace NavWebService
// {    
//     public class MobileUser
//     {
//         private string _UserID = "", _Password = "", _Company = "", _CustomerNo = "", _Name = "";
//         private bool _Valid = false, _PasswordChangeRequired = false;
//         private int _WebVisibility = 0;
//         private string[] _Emails = new string[] { "", "", "", "", "", ""};
//         private DataTable _smtpmailtable;
//         private DataTable _lightusertable;

//         public MobileUser()
//         {
                    
//         }

//         public bool ValidateUser(string aCompany, string aUserID, string aPassword)
//         {
//             _UserID = ""; _Password = ""; _Company = ""; _CustomerNo = ""; _Name = "";  _WebVisibility = 0;

//             string _Exception = "";
//             try
//             {                
//                 IsValid = (new NAVServer()).ValidateUser(aCompany, aUserID, aPassword, 
//                     ref _PasswordChangeRequired, ref _CustomerNo, ref _WebVisibility, ref _Emails, ref _Name, ref _Exception);

//                 if (IsValid)
//                 {
//                     Company = aCompany;
//                     UserID = aUserID;
//                     Password = aPassword;
//                     DataStore _datastore = new DataStore();
//                     _lightusertable = _datastore.GetLightUserTable(this);
//                     _smtpmailtable = _datastore.GetSMTPMailSetupTable(this);
//                     return true;
//                 }
//                 else
//                     return false;
//             }
//             catch
//             {
//                 IsValid = false;
//                 return false;
//             }
//         }

//         public bool ChangePassword(string companyp, string userIDp, string passwordp, string newPasswordp)
//         {
//             string _Exception = "";
//             try
//             {
//                 return (new NAVServer()).ChangePassword(companyp, userIDp, passwordp, newPasswordp, ref _Exception);
//             }
//             catch
//             {
//                 return false;
//             }
//         }

//         public bool IsValid
//         {
//             get { return _Valid; }
//             set { _Valid = value; }
//         }

//         public string UserID
//         {
//             get { return _UserID; }
//             set { _UserID = value.ToUpper(); }
//         }

//         public string Company
//         {
//             get { return _Company; }
//             set { _Company = value; }
//         }

//         public string CustomerNo
//         {
//             get { return _CustomerNo; }
//             set { _CustomerNo = value.ToUpper(); }
//         }
        
//         public string Name
//         {
//             get { return _Name; }
//             set { _Name = value; }
//         }

//         public string Password
//         {
//             get { return _Password; }
//             set { _Password = value.ToUpper(); }
//         }

//         public bool PasswordChangeRequired
//         {
//             get { return _PasswordChangeRequired; }
//             set { _PasswordChangeRequired = value; }
//         }

//         public int WebVisibility
//         {
//             get { return _WebVisibility; }
//             set { _WebVisibility = value; }
//         }

//         public string EMail
//         {
//             get { return _Emails[0]; }
//         }

//         public string ConfirmEMail1
//         {
//             get { return _Emails[1]; }
//         }

//         public string ConfirmEMail2
//         {
//             get { return _Emails[2]; }
//         }

//         public string ConfirmEMail3
//         {
//             get { return _Emails[3]; }
//         }

//         public string ConfirmEMail4
//         {
//             get { return _Emails[4]; }
//         }

//         public string ConfirmEMail5
//         {
//             get { return _Emails[5]; }
//         }

//         public string SMTP_Server
//         {
//             get { return _smtpmailtable.Rows[0]["SMTP Server"].BlankIfFails(); }
//         }

//         public string SMTP_User_ID
//         {
//             get { return _smtpmailtable.Rows[0]["User ID"].BlankIfFails(); }
//         }

//         public string SMTP_Password
//         {
//             get { return _smtpmailtable.Rows[0]["Password"].BlankIfFails(); }
//         }

//         public string SMTP_Sender_Name
//         {
//             get { return _smtpmailtable.Rows[0]["Sender Name"].BlankIfFails(); }
//         }

//         public string SMTP_Sender_Address
//         {
//             get { return _smtpmailtable.Rows[0]["Sender Address"].BlankIfFails(); }
//         }

//         public int SMTP_Server_Port
//         {
//             get { return _smtpmailtable.Rows[0]["SMTP Server Port"].IntZeroIfFails(); }
//         }

//         public bool SMTP_Secure_Connection
//         {
//             get { return _smtpmailtable.Rows[0]["Secure Connection"].IntZeroIfFails().FalseIfFails(); }
//         }

//         public bool IsAllowed(string pMenuName)
//         {
//             return _lightusertable.Rows[0][pMenuName].IntZeroIfFails().FalseIfFails();
//         }
//     }
// }