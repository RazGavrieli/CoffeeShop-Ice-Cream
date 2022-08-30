using System;

namespace DataProtocol
{
    public enum ExtraTaste
    {
        HotChocolate, // Iid = 11
        Peanuts,// Iid = 12
        Maple// Iid = 13
    }
    public class Extra 
    {
        public Extra(ExtraTaste taste) {
            this.ExtraTaste = taste;
        }
        public ExtraTaste ExtraTaste { get; set; }
    }
}
