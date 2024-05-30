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
    [Table("Threads")]
    public class Threads
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id { get; set; }
        [Column("title")]
        public string title { get; set; }
        [Column("description")]
        public string description { get; set; }

        [Column("userEmail")]
        public string userEmail { get; set; }

        [Column("dateP")]
        public string dateP { get; set; }

        [Column("commentsCount")]
        public int commentsCount { get; set; }



        public Threads()
        {
        }

        public Threads(int id, string description, string userEmail, string dateP, int commentsCount)
        {
            this.id = id;
            this.description = description;
            this.userEmail = userEmail;
            this.dateP = dateP;
            this.commentsCount = commentsCount;
        }


    }
}