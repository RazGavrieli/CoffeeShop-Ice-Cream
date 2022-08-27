using System;

namespace DataProtocol
{
    public enum ExtraTaste
    {
        HotChocolate,
        Peanuts,
        Maple
    }
    public class Extra : Ingredient
    {
        public ExtraTaste ExtraTaste { get; set; }
    }
}
