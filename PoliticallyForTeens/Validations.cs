using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PoliticallyForTeens
{
    class Validations
    {

        public static bool CheckFName(string etFName)
        {
            if (etFName.Length > 10 || etFName.Length < 2)
                return false;
            else
                return true;
        }

        public static bool CheckLName(string etLName)
        {
            if (etLName.Length > 10 || etLName.Length < 2)
                return false;
            else
                return true;
        }

        public static bool CheckEmail(string etEmail)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(etEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return false;
            return true;
        }

        public static bool CheckPass(string etPassword)
        {
            if (etPassword.Length > 10 || etPassword.Length < 3)
                return false;
            if (!System.Text.RegularExpressions.Regex.IsMatch(etPassword, "(?=.*[a-zA-Z])(?=.*[0-9])"))
                return false;
            return true;
        }

        public static bool CheckCpass(string etCPassword, string etPassword)
        {
            if (etCPassword != etPassword)
                return false;
            return true;
        }

        public static bool IsNumeric(string input)
        {
            return !Regex.IsMatch(input, @"[^0-9]");
        }

    }
}