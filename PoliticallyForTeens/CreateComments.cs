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
    [Table("CreateComments")]
    public class CreateComments
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int id { get; set; }

        [Column("userEmail")]
        public string userEmail { get; set; }
        [Column("commentId")]
        public int commentId { get; set; }



        public CreateComments()
        {
        }

        public CreateComments(string userEmail, int commentId, int id)
        {
            this.userEmail = userEmail;
            this.commentId = commentId;
            this.id = id;
        }


    }
}