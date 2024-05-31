using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace PoliticallyForTeens
{
    [Activity(Label = "RegistrationActivity")]
    public class RegistrationActivity : Activity
    {
        EditText fName;
        EditText lName;
        EditText age;
        RadioButton male;
        RadioButton female;
        EditText email;
        EditText password;
        EditText mPassword;
        Button btnRegister;
        TextView warnFName;
        TextView warnLName;
        TextView warnAge;
        TextView warnGender;
        TextView warnPassword;
        TextView warnMPassword;
        TextView warnEmail;
        SQLiteConnection dbCommand;

        private MenuFunctions menuFunctions;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegistrationLayout);

            fName = FindViewById<EditText>(Resource.Id.fName);
            lName = FindViewById<EditText>(Resource.Id.lName);
            age = FindViewById<EditText>(Resource.Id.age);
            male = FindViewById<RadioButton>(Resource.Id.Male);
            female = FindViewById<RadioButton>(Resource.Id.Female);
            email = FindViewById<EditText>(Resource.Id.email);
            password = FindViewById<EditText>(Resource.Id.Password);
            mPassword = FindViewById<EditText>(Resource.Id.mPassword);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            warnFName = FindViewById<TextView>(Resource.Id.warnFName);
            warnLName = FindViewById<TextView>(Resource.Id.warnLName);
            warnAge = FindViewById<TextView>(Resource.Id.warnAge);
            warnGender = FindViewById<TextView>(Resource.Id.warnGender);
            warnPassword = FindViewById<TextView>(Resource.Id.warnPassword);
            warnMPassword = FindViewById<TextView>(Resource.Id.warnMPassword);
            warnEmail = FindViewById<TextView>(Resource.Id.warnEmail);
            btnRegister.Click += BtnRegister_Click;

            menuFunctions = new MenuFunctions(this);

            dbCommand = new SQLiteConnection(Helper.Path());
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            bool valid = true;

            if (Validations.CheckEmail(email.Text))
            {
                warnEmail.Visibility = ViewStates.Visible;
                valid = false;
            }

            if (Validations.CheckFName(fName.Text) == false)
            {
                warnFName.Visibility = ViewStates.Visible;

                valid = false;
            }

            if (Validations.CheckLName(lName.Text) == false)
            {
                warnLName.Visibility = ViewStates.Visible;
                valid = false;
            }

            if (Validations.CheckPass(password.Text) == false)
            {
                warnPassword.Visibility = ViewStates.Visible;

                valid = false;
            }

            if (Validations.CheckCpass(mPassword.Text, password.Text) == false)
            {
                warnMPassword.Visibility = ViewStates.Visible;

                valid = false;
            }

            if (Validations.IsNumeric(age.Text) == false)
            {
                warnAge.Visibility = ViewStates.Visible;
                valid = false;

            }
            if (male.Checked == false && female.Checked == false)
            {
                warnGender.Visibility = ViewStates.Visible;
                valid = false;

            }

            bool dbValid = true;
            if (valid == true)
            {
                try
                {
                    Users user1 = dbCommand.Get<Users>(email.Text);

                    Toast.MakeText(this, "User Already Exists" + "", ToastLength.Long).Show();
                    dbValid = false;
                }
                catch
                {
                    int gender;
                    if(male.Checked==true)
                    {
                        gender = 1;
                    }

                       gender = 0;
                    

                    //Initiates a new user with the inputted information;                                                       **currently initiates user with admin perms for development
                    Users user = new Users(email.Text, fName.Text, lName.Text, password.Text, "", age.Text, gender);

                    try
                    {
                        var change = dbCommand.Insert(user);
                        if (change > 0)
                        {
                            Toast.MakeText(this, "Action Successful", ToastLength.Long).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "Action Unsuccessful", ToastLength.Long).Show();
                            dbValid = false;

                        }
                    }
                    catch
                    { }
                }
                if (dbValid)
                {
                    Intent intent = new Intent(this, typeof(LoginActivity));
                    StartActivity(intent);
                }
            }
            else
            {
                Toast.MakeText(this, "Validation Error", ToastLength.Long).Show();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return menuFunctions.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return menuFunctions.OnCreateOptionsMenu(menu);
        }

    }
}