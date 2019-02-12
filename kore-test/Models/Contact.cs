using System;
using System.Collections.Generic;

namespace kore_test.Models
{
    public partial class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Title { get; set; }
    }
}
