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
            /*
            Comment by Raz:
            The ID should always be 0 when creating a new sale:
            The SQL server will a assign a uniqe ID to the new sale. 
            */
        
            
            CupType = CupType.Regular;
            date = "48.13.2053";
            Balls = new List<IceCreamBall>();
            //By Default we cannot have less then 0 balls.
            
            Balls.Add(new IceCreamBall(Taste.Cannabis));
            ExtrasOnBalls = new List<Extra>();
            TotalPrice = 0;

            Id = 0;
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
