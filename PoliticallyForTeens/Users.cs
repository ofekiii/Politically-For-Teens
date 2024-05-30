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
    class Users
    {
          [Table("Users")]
        class Clients
        {
            [PrimaryKey, Column("email")]
            public string email { get; set; }
            [Column("fName")]
            public string fName { get; set; }
            [Column("lName")]
            public string lName { get; set; }

            [Column("password")]
            public string password { get; set; }

            [Column("age")]
            public string age { get; set; }

            [Column("gender")]
            public int gender { get; set; }

            [Column("picture")]
            public string picture { get; set; }


            public Clients()
            {
            }

            public Clients(string email, string fName, string lName, string password, string picture, string age, int gender)
            {
                this.email = email;
                this.fName = fName;
                this.lName = lName;
                this.password = password;
                this.picture = picture;
                this.age = age;
                this.gender = gender;
            }

        }
    }