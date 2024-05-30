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
    [Table("CreateThreads")]
    public class CreateThreads
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id { get; set; }

        [Column("userEmail")]
        public string userEmail { get; set; }

        [Column("threadId")]
        public string threadId { get; set; }


        public CreateThreads()
        {
        }

        public CreateThreads(int id, string userEmail, string threadId)
        {
            this.id = id;
            this.userEmail = userEmail;
            this.threadId = threadId;
        }


    }
}