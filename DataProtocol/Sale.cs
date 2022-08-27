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
        static int IDCount = 0;
        public int Id { get; set; }
        public CupType CupType { get; set; }
        public string date { get; set; }
        public List<IceCreamBall> Balls { get; set; }
        public List<Extra> ExtrasOnBalls { get; set; }

        public float TotalPrice { get; set; }
        public Sale() {
            /**Setting up uniqe ID for each Sale*/
            Id = IDCount;
            IDCount++;
            
            CupType = CupType.Regular;
            date = "48.13.2053";
            Balls = new List<IceCreamBall>();
            ExtrasOnBalls = new List<Extra>();
            TotalPrice = 0;
        }
        public Sale(List<IceCreamBall> Balls, List<Extra> Extras, CupType cup, string date ) {
            /**Setting up uniqe ID for each Sale*/
            Id = IDCount;
            IDCount++;

            this.Balls = Balls;
            this.ExtrasOnBalls = Extras;
            this.CupType = cup;
            this.date = date;
            //Check if the sale is valid
            bool bValidSale = CheckForValidOrder();
            if (bValidSale)
            {
                //Calcilating Sale total price based on ingridients
                UpdateTotalPrice();
            }
            else 
            {
                throw new ArgumentException("Invaild Order");
            }

        }
        void AddIcecreamBall(IceCreamBall ball) 
        {
            Balls.Add(ball);
            bool bValidSale = CheckForValidOrder();
            if (bValidSale)
            {
                //Calcilating Sale total price based on ingridients
                UpdateTotalPrice();
            }
            else
            {
                throw new ArgumentException("Invaild Order");
            }

        }
        void AddExtra(Extra extra)
        {
            ExtrasOnBalls.Add(extra);
            bool bValidSale = CheckForValidOrder();
            if (bValidSale)
            {
                //Calcilating Sale total price based on ingridients
                UpdateTotalPrice();
            }
            else
            {
                throw new ArgumentException("Invaild Order");
            }

        }
        private bool CheckForValidOrder() 
        {
            return true;
        }
        private void UpdateTotalPrice() 
        {
            this.TotalPrice = 0;
        }
    }
}
