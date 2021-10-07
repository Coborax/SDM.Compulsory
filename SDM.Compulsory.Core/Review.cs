using System;

namespace SDM.Compulsory.Core
{
    public class Review
    {
        public int Id {get; set;}
        public int Reviewer { get; set; }
        public int Movie { get; set; }
        public int Grade { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}