using System;



namespace DataProtocol {
    public enum Taste
    {
        Chocolate, // Iid = 1
        Vanille,// Iid = 2
        GummyBear,// Iid = 3
        Meat,// Iid = 4
        Pizza,// Iid = 5
        Mekupelet,// Iid = 6
        Cannabis,// Iid = 7
        BrokenDreams,// Iid = 8
        SimpleRickWafers,// Iid = 9
        GiveMe100// Iid = 10
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