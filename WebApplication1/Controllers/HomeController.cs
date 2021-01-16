using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Index(login login)
        {
            string xUser = login.xUsername;
            string xPass = login.xPassword;

            if (xUser == "admin" && xPass == "1234")
            {
                TempData["mssg"] = "Success";
                ViewBag.mssg = TempData["mssg"] as string;
            }
            else
            {
                TempData["mssg"] = "Error";
                ViewBag.mssg = TempData["mssg"] as string;
            }




            return View();
        }
        public void login_(string uesr, string pass)
        {
            clsData cls = new clsData();
            StringBuilder strSql = new StringBuilder();
            DataTable dt = new DataTable();
            string dtm = DateTime.Now.ToString();
            strSql.AppendLine(" INSERT INTO public.user_auth(username, password,cwhen) VALUES ");
            strSql.AppendLine(" ('" + uesr + "',   '" + pass + "','" + dtm + "' ) RETURNING  uid ");
            dt = cls.sqlDataTable(strSql.ToString());
        }
        private void btnLogin_Click(string Username, string Password)
        {
            if (chkLogin(Username, Password) == true)
            {

                About();
            }

        }



        private bool chkLogin(string user, string pass)
        {
            bool statusLogin = false;
            clsData cls = new clsData();
            DataTable dt = new DataTable();
            StringBuilder sb = new StringBuilder();

            #region check user by database
            sb.AppendLine(" select * from user_auth where username ='" + user + "' and password = '" + pass + "'");
            dt = cls.sqlDataTable(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Xuser = dr["username"].ToString();
                    string Xpass = dr["password"].ToString();
                }
                statusLogin = true;
            }
            sb = null;
            dt = null;
            cls = null;
            #endregion
            return statusLogin;
        }

        public ActionResult onprintsave(string abc)
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult printT(print print)
        {


            return View();
        }
    }
}