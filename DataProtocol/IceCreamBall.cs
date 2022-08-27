using System;



namespace DataProtocol {
    public enum Taste
    {
        Chocolate,
        Vanille,
        GummyBear,
        Meat,
        Pizza,
        Mekupelet,
        Cannabis,
        BrokenDreams,
        SimpleRickWafers,
        GiveMe100
    }

    public class IceCreamBall
    {
        public IceCreamBall(Taste taste)
        {
            this.Taste = taste;
        }
        public Taste Taste { get; set; }


    }
}