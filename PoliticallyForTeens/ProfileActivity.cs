using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android;
using Plugin.Media;
using Android.Graphics;
using SQLite;

namespace PoliticallyForTeens
{
    [Activity(Label = "PoliticallyForTeens")]
    public class PersonalInfoActivity : AppCompatActivity
    {
        EditText firstName_ET, lastName_ET, oldPw_ET, newPw_ET, confirmPw_ET;
        Button confirmChangesBtn, takePhotoBtn, uploadGalleryBtn, profileImageConfirmBtn, changePwBtn, changePwConfirmBtn;
        CheckBox showPasswordCheckBox;
        ImageView profileImageView, dialogProfileImageView;
        Dialog dialog;
        Bitmap profileImage;
        string profileImageStr;
        string oldPassword = Helper.Sp().GetString("password", "");
        SQLiteConnection dbCommand = new SQLiteConnection(Helper.Path());

        readonly string[] permissionGroup =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Camera
        };


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.ProfileLayout);

            firstName_ET = FindViewById<EditText>(Resource.Id.firstName_ET);
            lastName_ET = FindViewById<EditText>(Resource.Id.lastName_ET);
            oldPw_ET = FindViewById<EditText>(Resource.Id.oldPw_ET);
            newPw_ET = FindViewById<EditText>(Resource.Id.newPw_ET);
            confirmPw_ET = FindViewById<EditText>(Resource.Id.confirmPw_ET);
            confirmChangesBtn = FindViewById<Button>(Resource.Id.confirmChangesBtn);
            takePhotoBtn = FindViewById<Button>(Resource.Id.takePhotoBtn);
            uploadGalleryBtn = FindViewById<Button>(Resource.Id.uploadGalleryBtn);
            profileImageConfirmBtn = FindViewById<Button>(Resource.Id.profileImageConfirmBtn);
            changePwBtn = FindViewById<Button>(Resource.Id.changePwBtn);
            changePwConfirmBtn = FindViewById<Button>(Resource.Id.changePwConfirmBtn);
            profileImageView = FindViewById<ImageView>(Resource.Id.profileImageView);
            dialogProfileImageView = FindViewById<ImageView>(Resource.Id.dialogProfileImageView);
            showPasswordCheckBox = FindViewById<CheckBox>(Resource.Id.showPasswordCheckBox);

            oldPw_ET = new EditText(this);
            newPw_ET = new EditText(this);
            confirmPw_ET = new EditText(this);
            changePwConfirmBtn = new Button(this);
            showPasswordCheckBox = new CheckBox(this);


            firstName_ET.Text = Helper.Sp().GetString("fName", "");
            lastName_ET.Text = Helper.Sp().GetString("lName", "");


            profileImageStr = Helper.Sp().GetString("picture", "");
            //Check if user already has a profile image, if not, keeps it as default, if yes, shows it.
            if (profileImageStr != "")
            {
                profileImageView.SetImageBitmap(Helper.Base64ToBitmap(profileImageStr));
            }
            else
            {

            }



            changePwBtn.Click += ChangePwBtn_Click;
            profileImageView.Click += ProfileImageView_Click;
            confirmChangesBtn.Click += ConfirmChangesButton_Click;

        }

        private void ChangePwBtn_Click(object sender, EventArgs e)
        {
            CreateChangePwDialog();
        }

        public void CreateChangePwDialog()
        {

            dialog = new Dialog(this);
            dialog.SetContentView(Resource.Layout.ChangePasswordDialogLayout);
            dialog.SetTitle("Change Password");
            dialog.SetCancelable(true);
            oldPw_ET = FindViewById<EditText>(Resource.Id.oldPw_ET);
            newPw_ET = FindViewById<EditText>(Resource.Id.newPw_ET);
            confirmPw_ET = FindViewById<EditText>(Resource.Id.confirmPw_ET);
            changePwConfirmBtn = FindViewById<Button>(Resource.Id.changePwConfirmBtn);
            showPasswordCheckBox = FindViewById<CheckBox>(Resource.Id.showPasswordCheckBox);

            //Hides all password fields by default
            oldPw_ET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;
            newPw_ET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;
            confirmPw_ET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;

            showPasswordCheckBox.CheckedChange += ShowPasswordCheckBox_CheckedChange;
            changePwConfirmBtn.Click += ChangePwConfirmBtn_Click;
            dialog.Show();
        }

        private void ChangePwConfirmBtn_Click(object sender, EventArgs e)
        {
            //Fields Validation
            bool valid = true;

            if (oldPw_ET.Text == oldPassword)
            {
                if (Validations.CheckPass(newPw_ET.Text) == false)
                {
                    valid = false;
                    Toast.MakeText(this, "Password Must be within 2-10 Characters", ToastLength.Long).Show();
                }
                if (confirmPw_ET.Text != newPw_ET.Text)
                {
                    valid = false;
                    Toast.MakeText(this, "Confirmation Field Must be Identical to New Field", ToastLength.Long).Show();
                }
            }
            else
                Toast.MakeText(this, "Old Password Is Incorrect", ToastLength.Long).Show();

            if (valid == true)
            {
                //Update password and disable dialog
                var editor = Helper.Sp().Edit();

                editor.PutString("password", newPw_ET.Text);
                editor.Commit();
                dialog.Hide();
                Toast.MakeText(this, "Password Updated Successfully", ToastLength.Long).Show();
            }
        }

        //If "show password" checkbox is checked, shows text
        private void ShowPasswordCheckBox_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            bool isChecked = showPasswordCheckBox.Checked;
            if (isChecked)
            {
                oldPw_ET.TransformationMethod = null;
                newPw_ET.TransformationMethod = null;
                confirmPw_ET.TransformationMethod = null;
            }
            else
            {
                oldPw_ET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;
                newPw_ET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;
                confirmPw_ET.TransformationMethod = Android.Text.Method.PasswordTransformationMethod.Instance;
            }
        }

        private void ProfileImageView_Click(object sender, EventArgs e)
        {
            CreateProfileImageDialog();
        }

        private void ConfirmChangesButton_Click(object sender, EventArgs e)
        {
            bool valid = true;
            if (Validations.CheckFName(firstName_ET.Text) == false)
            {
                valid = false;
                Toast.MakeText(this, "First Name Must be only letters and up to one space", ToastLength.Long).Show();
            }
            if (Validations.CheckLName(lastName_ET.Text) == false)
            {
                valid = false;
                Toast.MakeText(this, "Last Name Must be only letters and up to one space", ToastLength.Long).Show();
            }


            var editor = Helper.Sp().Edit();

            string userEmail = Helper.Sp().GetString("email", null);

            editor.PutString("fName", firstName_ET.Text);
            editor.PutString("lName", lastName_ET.Text);
            editor.PutString("picture", profileImageStr);

            dbCommand.Execute("UPDATE Users SET fName = '" + firstName_ET.Text + "', lName = '" + lastName_ET.Text + "', picture = '" + profileImageStr + "' WHERE email= '" + userEmail + "'");

            editor.Commit();



            Toast.MakeText(this, "Changes Confirmed", ToastLength.Long).Show();
        }

        public void CreateProfileImageDialog()
        {

            dialog = new Dialog(this);
            dialog.SetContentView(Resource.Layout.ProfileImageDialogLayout);
            dialog.SetTitle("Upload Image");
            dialog.SetCancelable(true);
            dialogProfileImageView = dialog.FindViewById<ImageView>(Resource.Id.dialogProfileImageView);
            takePhotoBtn = dialog.FindViewById<Button>(Resource.Id.takePhotoBtn);
            uploadGalleryBtn = dialog.FindViewById<Button>(Resource.Id.uploadGalleryBtn);
            profileImageConfirmBtn = dialog.FindViewById<Button>(Resource.Id.profileImageConfirmBtn);

            dialogProfileImageView.SetImageBitmap(profileImage);

            takePhotoBtn.Click += TakePhotoBtn_Click;
            uploadGalleryBtn.Click += UploadGalleryBtn_Click;
            profileImageConfirmBtn.Click += ProfileImageConfirmBtn_Click;
            dialog.Show();
        }

        private void UploadGalleryBtn_Click(object sender, EventArgs e)
        {

            RequestPermissions(permissionGroup, 0);
            UploadPhoto();
        }

        private void TakePhotoBtn_Click(object sender, EventArgs e)
        {
            //Requests access permissions necessary to take a photo
            RequestPermissions(permissionGroup, 0);
            TakePhoto();
        }

        private void ProfileImageConfirmBtn_Click(object sender, EventArgs e)
        {
            profileImageView.SetImageBitmap(profileImage);
            dialog.Hide();
        }

        async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40,
                Name = "profileImage.png",
                Directory = "picture"

            });

            if (file == null)
            {
                profileImageStr = null;
            }

            //Convert file to byte array and set the resulting bitmap to imageview
            try
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                dialogProfileImageView.SetImageBitmap(bitmap);
                profileImage = bitmap;
                profileImageStr = Helper.BitmapToBase64(profileImage);

            }
            catch
            {

            }

        }

        public async void UploadPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Toast.MakeText(this, "Upload not supported on this device", ToastLength.Short).Show();
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 40

            });

            //Convert file to byte array and set the resulting bitmap to imageview
            try
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
                Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                dialogProfileImageView.SetImageBitmap(bitmap);
                profileImage = bitmap;
            }
            catch
            {

            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



    }
}