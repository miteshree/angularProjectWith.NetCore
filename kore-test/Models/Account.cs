using System;
using System.Collections.Generic;

namespace kore_test.Models
{
    public partial class Account
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public int ContactId { get; set; }
        public string ContactName { get; set; }
    }
}
