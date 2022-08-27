using System;
using System.Collections;

namespace DataProtocol {
    public enum CupType
    {
        Regular,
        Special,
        Box
    }
    class Sale {
        
        public int Id { get; set; }
        public CupType CupType { get; set; }
        public int date { get; set; }
        public List<IceCreamBall> Balls { get; set; }
        List<Extras> ExtrasOnBalls { get; set; }
        Sale() {
            Balls = new List<IceCreamBall>();
            ExtrasOnBalls = new List<Extras>();
        }
    }
}
