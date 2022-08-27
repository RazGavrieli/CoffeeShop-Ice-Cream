using System;

namespace DataProtocol
{
    public enum ExtraTaste
    {
        HotChocolate,
        Peanuts,
        Maple
    }
    class Extra : Ingredient
    {
        public ExtraTaste ExtraTaste { get; set; }
    }
}
