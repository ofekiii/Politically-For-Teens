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
using Android.Graphics;
using Android.Util;
using System.IO;

namespace PoliticallyForTeens
{
    class Helper
    {
        private static SQLiteConnection dbCommand;
        private static ISharedPreferences sp;
        private static ISharedPreferences spTrainer;
        private static string path;
        private static string dbName = "DBPolitic";

        public Helper()
        {

        }

        public static ISharedPreferences Sp()
        {
            return sp;
        }

        public static ISharedPreferences SpTrainer()
        {
            return spTrainer;
        }

        public static string Path()
        {
            path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Helper.dbName);
            return path;
        }


        public static string BitmapToBase64(Bitmap bitmap)
        {
            string str = "";
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                var bytes = stream.ToArray();
                str = Convert.ToBase64String(bytes);
            }
            return str;
        }

        public static Bitmap Base64ToBitmap(String base64String)
        {
            byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
            return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
        }

        public static bool Initialize(Context context)
        {
            path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), Helper.dbName);
            sp = context.GetSharedPreferences("details", FileCreationMode.Private);
            //spTrainer = context.GetSharedPreferences("detailsTrainer", FileCreationMode.Private);
            try
            {
                dbCommand = new SQLiteConnection(Path());
            }

            catch (Exception ex)
            {
                return false;
            }
            try
            {

                dbCommand.CreateTable<Users>();
                dbCommand.CreateTable<Comments>();
                dbCommand.CreateTable<Threads>();
            }

            catch (Exception ex)
            {
                return false;
            }


            //byte[] imageArray = File.ReadAllBytes(Resource.Drawable.picture.ToString());
            //Bitmap bitmap = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);

            //string stringManagerPicture = BitmapToBase64(bitmap);

            return true;

        }
    }
}