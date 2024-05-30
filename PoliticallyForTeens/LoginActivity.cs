
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using SQLite;
using Android.Views;
using Android.Content;
using System;
using System.Linq;

namespace PoliticallyForTeens
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        EditText email_ET, password_ET, fgpwEmail_ET;
        TextView fgpwTV, Register_TV;
        CheckBox rmbrmeCB, showPasswordCheckBox;
        Button LoginBTN, fgpwConfirmBtn;
        Dialog dialog;
        SQLiteConnection dbCommand;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);

            email_ET = FindViewById<EditText>(Resource.Id.email_ET);
            password_ET = FindViewById<EditText>(Resource.Id.password_ET);
            fgpwEmail_ET = FindViewById<EditText>(Resource.Id.fgpwEmail_ET);
            rmbrmeCB = FindViewById<CheckBox>(Resource.Id.rmbrmeCB);
            showPasswordCheckBox = FindViewById<CheckBox>(Resource.Id.showPasswordCheckBox);
            LoginBTN = FindViewById<Button>(Resource.Id.LoginBTN);
            fgpwConfirmBtn = FindViewById<Button>(Resource.Id.fgpwConfirmBtn);
            fgpwTV = FindViewById<TextView>(Resource.Id.fgpwTV);
            Register_TV = FindViewById<TextView>(Resource.Id.Register_TV);







            dbCommand = new SQLiteConnection(Helper.Path());

            showPasswordCheckBox.CheckedChange += ShowPasswordCheckBox_CheckedChange;
            fgpwTV.Click += FgpwTV_Click;
            Register_TV.Click += Register_TV_Click;
        }

        private void Register_TV_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegistrationActivity));
            StartActivity(intent);
        }

        private void FgpwTV_Click(object sender, System.EventArgs e)
        {
            createForgotPasswordDialog();
        }

        private void ShowPasswordCheckBox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            bool isChecked = showPasswordCheckBox.Checked;
            if (isChecked)
                password_ET.TransformationMethod = null;
            else
                password_ET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;
        }

        

        private void LoginBTN_Click()
        {
            var editor = Helper.Sp().Edit();
            try
            {
                //Looks for the email and password combination in the database
                var allData = dbCommand.Query<Users>("SELECT * FROM UsersTBL WHERE Email= '" + email_ET.Text + "' AND Password= '" + password_ET.Text + "'");

                //If user is found saves all user information in shared preferences
                if (allData.Count() != 0)
                {
                    editor.PutString("email", email_ET.Text);
                    editor.PutString("password", password_ET.Text);
                    editor.PutString("fName", ((Users)allData[0]).fName);
                    editor.PutString("lName", ((Users)allData[0]).lName);
                    editor.PutString("age", ((Users)allData[0]).age);
                    editor.PutString("picture", ((Users)allData[0]).picture);

                    editor.PutBoolean("rmbrmeCB", rmbrmeCB.Checked);
                    editor.Commit();

                    Intent intent = new Intent(this, typeof(HomePageActivity));
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, "Incorrect Email / Password", ToastLength.Long).Show();
                }
            }

            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
        }

        public void createForgotPasswordDialog()
        {

            dialog = new Dialog(this);
            dialog.SetContentView(Resource.Layout.ForgotPasswordDialogLayout);
            dialog.SetTitle("Reset");
            dialog.SetCancelable(true);
            fgpwEmail_ET = dialog.FindViewById<EditText>(Resource.Id.fgpwEmail_ET);
            fgpwConfirmBtn = dialog.FindViewById<Button>(Resource.Id.fgpwConfirmBtn);
            fgpwConfirmBtn.Click += FgpwConfirmBtn_Click;
            dialog.Show();
        }

        private void FgpwConfirmBtn_Click(object sender, System.EventArgs e)
        {
            try
            {
                var customer1 = dbCommand.Query<Users>("SELECT * FROM UsersTBL WHERE Email = '" + fgpwEmail_ET.Text + "'");
                var customerPassword = ((Users)customer1[0]).password;
                Toast.MakeText(this, "Your Password is: " + customerPassword.ToString(), ToastLength.Long).Show(); // Change to send SMS / Email to change password
                dialog.Hide();
            }
            catch
            {
                Toast.MakeText(this, "User Not Found", ToastLength.Long).Show();
            }
        }
    }
}