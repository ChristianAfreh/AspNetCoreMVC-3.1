﻿using System.Collections.Generic;

namespace BookStore.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Book> Book { get; set; }
    }
}