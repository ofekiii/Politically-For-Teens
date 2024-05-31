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

        [Table("Comments")]
         public  class Comments
        {
            [PrimaryKey, AutoIncrement, Column("id")]
            public int id { get; set; }

            [Column("description")]
            public string description { get; set; }

            [Column("userEmail")]
            public string userEmail { get; set; }

            [Column("dateP")]
            public string dateP { get; set; }

           [Column("pro")]
           public bool pro { get; set; }



            public Comments()
            {

            }

            public Comments(string description, string userEmail, string dateP, bool pro)
            {
                this.description = description;
                this.userEmail = userEmail;
                this.dateP = dateP;
                this.pro = pro;

            }

        }
    
}
