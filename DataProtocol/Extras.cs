using System;

namespace DataProtocol
{
    public enum ExtraTaste
    {
        HotChocolate,
        Peanuts,
        Maple
    }
    public class Extra 
    {
        public Extra(ExtraTaste taste) {
            this.ExtraTaste = taste;
        }
        public ExtraTaste ExtraTaste { get; set; }
    }
}
