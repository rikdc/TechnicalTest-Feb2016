﻿using System.Data.Entity;

namespace PostcodeSearch.Models
{
    public class PostcodeSearchContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PostcodeSearchContext() : base("name=PostcodeSearchContext")
        {
        }

        public DbSet<Postcodes> Postcodes { get; set; }
    }
}
