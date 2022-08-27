using System;
using System.Collections;
using DataProtocol;

namespace DataProtocol {
    public enum CupType
    {
        Regular,
        Special,
        Box
    }
    public class Sale {
        
        public int Id { get; set; }
        public CupType CupType { get; set; }
        public int date { get; set; }
        public List<IceCreamBall> Balls { get; set; }
        List<Extra> ExtrasOnBalls { get; set; }
        Sale() {
            Balls = new List<IceCreamBall>();
            ExtrasOnBalls = new List<Extra>();
        }
    }
}
