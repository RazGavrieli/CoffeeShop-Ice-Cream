using System;
using System.Collections;
using DataProtocol;
using System.Globalization;


namespace DataProtocol {
    public enum CupType
    {
        Regular, // Iid = 14
        Special,// Iid = 15
        Box// Iid = 16
    }
    public class Sale {
        static int IDCount = 0;
        public int Id { get; set; }
        public CupType CupType { get; set; }
        public string date { get; set; }
        public List<IceCreamBall> Balls { get; set; }
        public List<Extra> ExtrasOnBalls { get; set; }
        public Boolean boolClosedSale { get; set; }

        public float TotalPrice { get; set; }
        public Sale(CupType type=CupType.Regular) {   
            Id = 0;
            CupType = type;
            DateTime localDate = DateTime.Now;
            date = localDate.ToString();
            Balls = new List<IceCreamBall>(); //By Default we cannot have less then 0 balls.
            ExtrasOnBalls = new List<Extra>();
            TotalPrice = 0;

            boolClosedSale=false;
        }
        public Sale(List<IceCreamBall> Balls, List<Extra> Extras, CupType cup, string date ) {
            /**Setting up uniqe ID for each Sale*/
            Id = 0;

            this.Balls = Balls;
            this.ExtrasOnBalls = Extras;
            this.CupType = cup;
            this.date = date;
            //Check if the sale is valid
            bool bValidSale = CheckForValidOrder();
            boolClosedSale=false;

            if (bValidSale)
            {
                //Calculating Sale total price based on ingridients
                UpdateTotalPrice();

            }
            else 
            {
                throw new ArgumentException("Invaild Order");
            }

        }

        public void setCup(CupType type) {
            if (Balls.Count > 0 || ExtrasOnBalls.Count > 0) {
                throw new ArgumentException("Cannot change cuptype once a ball or a taste were added");
            }
            CupType = type;
        }
        public void AddIcecreamBall(IceCreamBall ball) 
        {
            if(boolClosedSale)
            {
                throw new ArgumentException("Cannot add new ingridients, Sale is closed");
            }
            Balls.Add(ball);
            bool bValidSale = CheckForValidOrder();
            if (bValidSale)
            {
                //Calculating Sale total price based on ingridients
                UpdateTotalPrice();
            }
            else
            {
                throw new ArgumentException("Invaild Order");
            }

        }
        public void AddExtra(Extra extra)
        {
            if(boolClosedSale)
            {
                throw new ArgumentException("Cannot add new ingridients, Sale is closed");
            }
            ExtrasOnBalls.Add(extra);
            bool bValidSale = CheckForValidOrder();
            if (bValidSale)
            {
                //Calculating Sale total price based on ingridients
                UpdateTotalPrice();
            }
            else
            {
                throw new ArgumentException("Invaild Order");
            }

        }
        public bool CheckForValidOrder() 
        {
            //Cannot have 0 balls in order.
            if (Balls.Count == 0) 
            {
                return false;
            }
            switch (this.CupType){
                /**
                 * Case 1: regular cup:
                 * 1.amount of icecream balls: 1-3
                 * 2.if there is Extras-> we must have 2 balls or more
                 * 3.chocolate balls taste->hot chocolate extra invalid
                 * 4.mekupelet balls taste->hot chocolate extra invalid
                 * 5.vanille balls taste->maple extra invalid
                 */
                case CupType.Regular:
                    if (Balls.Count > 3) 
                    {
                        return false;
                    }
                    if (Balls.Count < 2 && ExtrasOnBalls.Count != 0) 
                    {
                        return false;
                    }
                    for (int i = 0; i < Balls.Count; i++) 
                    {
                        if (Balls[i].Taste == Taste.Chocolate || Balls[i].Taste == Taste.Mekupelet) 
                        {
                            for (int k = 0; k < ExtrasOnBalls.Count; k++) 
                            {
                                if (ExtrasOnBalls[k].ExtraTaste == ExtraTaste.HotChocolate) 
                                {
                                    return false;
                                }
                            }
                        }

                        if (Balls[i].Taste == Taste.Vanille) 
                        {
                            for (int k = 0; k < ExtrasOnBalls.Count; k++)
                            {
                                if (ExtrasOnBalls[k].ExtraTaste == ExtraTaste.Maple)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    break;

                    /**
                     * Case 2: Speciel cup type
                     * 1.Balls count: 1-3
                     */
                case CupType.Special:
                    if (Balls.Count > 3)
                    {
                        return false;
                    }
                    break;

                /**
                 * Case 3: Box cup type
                 * 
                 */
                case CupType.Box:
                    return true;
            }
            return true;
        }
        private void UpdateTotalPrice() 
        {

            /**
             * if we have 1 ball price is 7$,
             * else price is 6$ per ball.
             * for Speciel cup add 2$,
             * for Box cup add 5$.
             * Extras price not writen correct, my guees is 2$ per extra.
             **/

            TotalPrice = 0;
            if (Balls.Count == 1)
            {
                TotalPrice = 7;
            } else if (Balls.Count == 2) {
                TotalPrice = 12;
            }
            else 
            {
                TotalPrice = Balls.Count * 6;
            }

            if (this.CupType == CupType.Special) 
            {
                TotalPrice += 2;
            }
            else if (this.CupType == CupType.Box)
            {
                TotalPrice += 5;
            }
            TotalPrice += (ExtrasOnBalls.Count * 2);

        }
    }
}
