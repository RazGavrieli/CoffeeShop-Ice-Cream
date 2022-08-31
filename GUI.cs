using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
//using WindowsFormsApplication1.BLL;

using DAL;
using DataProtocol;

class GUI { 
    businessLogic BL = new businessLogic();
    void salefunction(Object sender, EventArgs e) {
        Button newsaleButton = (Button)sender;
        Sale currSale = BL.newSale();

        Form saleForm = new Form();
        saleForm.Text = "Sale Box";
        saleForm.FormBorderStyle = FormBorderStyle.FixedDialog;
        saleForm.StartPosition = FormStartPosition.CenterParent;
        saleForm.Size = new Size(350, 200);
        TextBox cupBox = new TextBox();
        cupBox.Text = "Regular";
        cupBox.Location = new Point(10, 0);
        Button setCupButton = new Button();
        setCupButton.Text = "set cup type";
        setCupButton.Location = new Point (10, 30);
        setCupButton.AutoSize = true;
        setCupButton.Click += (sender, e) => setcuptype(sender, e, cupBox.Text, currSale);
        saleForm.Controls.Add(setCupButton);
        saleForm.Controls.Add(cupBox);

        TextBox ballBox = new TextBox();
        ballBox.Text = "Choose Ball";
        ballBox.Location = new Point(125, 0);
        Button addBall = new Button();
        addBall.Text = "add ball";
        addBall.Location = new Point (125, 30);
        addBall.AutoSize = true;
        addBall.Click += (sender, e) => addball(sender, e, ballBox.Text, currSale);
        saleForm.Controls.Add(addBall);
        saleForm.Controls.Add(ballBox);

        TextBox extraBox = new TextBox();
        extraBox.Text = "Choose Extra";
        extraBox.Location = new Point(250, 0);
        Button addExtra = new Button();
        addExtra.Text = "add extra";
        addExtra.Location = new Point (250, 30);
        addExtra.AutoSize = true;
        addExtra.Click += (sender, e) => addextra(sender, e, extraBox.Text, currSale);
        saleForm.Controls.Add(addExtra);
        saleForm.Controls.Add(extraBox);

        Button sumSaleButton = new Button();
        sumSaleButton.Text = "Finish Sale";
        sumSaleButton.Location = new Point (150, 100);
        sumSaleButton.AutoSize = true;
        //saleForm.CancelButton = sumSaleButton;
        sumSaleButton.Click += (sender, e) => sumsale(sender, e, currSale);
        saleForm.Controls.Add(sumSaleButton);

        Button seeSaleButton = new Button();
        seeSaleButton.Text = "See Sale";
        seeSaleButton.Location = new Point (150, 125);
        seeSaleButton.AutoSize = true;
        seeSaleButton.Click += (sender, e) => seesale(sender, e, currSale);
        saleForm.Controls.Add(seeSaleButton);

        saleForm.CancelButton = sumSaleButton;
        saleForm.ShowDialog();
    }



    void sumsale(Object sender, EventArgs e, Sale currSale) {
        BL.sumSale(currSale);
        MessageBox.Show(BL.getReceipt(currSale.Sid));
    }

    void seesale(Object sender, EventArgs e, Sale currSale) {
        string ans = "";
        foreach (var ball in currSale.Balls)
            ans += ball.Taste+", ";

        ans+="\n";
        foreach (var extra in currSale.ExtrasOnBalls)
            ans += extra+", ";
        MessageBox.Show(ans);
    }

    void setcuptype(Object sender, EventArgs e, string typestr, Sale currSale) {
        if (typestr == "Regular") {
            currSale.setCup(CupType.Regular);
        } else if (typestr == "Box") {
            currSale.setCup(CupType.Box);
        } else if (typestr == "Special") {
            currSale.setCup(CupType.Special);
        } else {
            MessageBox.Show("wrong cup type\n use 'Regular' \\ 'Box' \\ 'Special");
        }
    }

    void addball(Object sender, EventArgs e, string typestr, Sale currSale) {
        /*
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
        */
        if (typestr == "Chocolate") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.Chocolate));
        } else if (typestr == "Vanille") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.Vanille));
        } else if (typestr == "GummyBear") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.GummyBear));
        } else if (typestr == "Meat") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.Meat));
        } else if (typestr == "Pizza") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.Pizza));
        } else if (typestr == "Mekupelet") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.Mekupelet));
        } else if (typestr == "Cannabis") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.Cannabis));
        } else if (typestr == "BrokenDreams") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.BrokenDreams));
        } else if (typestr == "SimpleRickWafers") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.SimpleRickWafers));
        } else if (typestr == "GiveMe100") {
            currSale.AddIcecreamBall(new IceCreamBall(Taste.GiveMe100));
        } else {
            MessageBox.Show("wrong ball taste \n use Chocolate\\Vanille\\GummyBear\\Meat\\Pizza\\Mekupelet\\Cannabis\\BrokenDreams\\SimpleRickWafers\\GiveMe100");
        }
    }

    void addextra(Object sender, EventArgs e, string typestr, Sale currSale) {
        /*
        HotChocolate, // Iid = 11
        Peanuts,// Iid = 12
        Maple// Iid = 13
        */
        if (typestr == "HotChocolate") {
            currSale.AddExtra(new Extra(ExtraTaste.HotChocolate));
        } else if (typestr == "Peanuts") {
            currSale.AddExtra(new Extra(ExtraTaste.Peanuts));
        } else if (typestr == "Maple") {
            currSale.AddExtra(new Extra(ExtraTaste.Maple));
        } else {
            MessageBox.Show("wrong ball taste \n use HotChocolate\\Peanuts\\Maple");
        }
    }

    void adminfunction(Object sender, EventArgs e)
    {
        Button adminButton = (Button)sender;
        TextBox dateBox = new TextBox();
        dateBox.Text = "XX/XX/XXXX";
        dateBox.Location = new Point(0, 1);
        TextBox SidBox = new TextBox();
        SidBox.Text = "Sale ID";
        SidBox.Location = new Point(0, 30);
        Form adminForm = new Form();
        adminForm.Text = "Admin Box";
        Button getDaySumButton = new Button();
        getDaySumButton.Text = "Get Day Sum";
        getDaySumButton.Location = new Point (100, 1);
        getDaySumButton.AutoSize = true;
        Button getReceiptButton = new Button();
        getReceiptButton.Text = "Get Receipt";
        getReceiptButton.Location = new Point (100, 30);
        getReceiptButton.AutoSize = true;
        Button getUnfinishedButton = new Button();
        getUnfinishedButton.Text = "Get Unfinished";
        getUnfinishedButton.Location = new Point (100, 60);
        getUnfinishedButton.AutoSize = true;
        Button deleteUnfinishedButton = new Button();
        deleteUnfinishedButton.Text = "Get and Delete Unfnished";
        deleteUnfinishedButton.Location = new Point (100, 90);
        deleteUnfinishedButton.AutoSize = true;
        Button getBestSellersButton = new Button();
        getBestSellersButton.Text = "Best Sellers";
        getBestSellersButton.Location = new Point (100, 120);
        getBestSellersButton.AutoSize = true;

        Button toggleSQLButton = new Button();
        toggleSQLButton.Text = "SQL";
        toggleSQLButton.Location = new Point (100, 150);
        toggleSQLButton.AutoSize = true;

        adminForm.Controls.Add(SidBox);
        adminForm.Controls.Add(dateBox);
        adminForm.Controls.Add(getDaySumButton);
        adminForm.Controls.Add(getReceiptButton);
        adminForm.Controls.Add(getUnfinishedButton);
        adminForm.Controls.Add(deleteUnfinishedButton);
        adminForm.Controls.Add(getBestSellersButton);
        adminForm.Controls.Add(toggleSQLButton);
        adminForm.AutoSize = true;
        getDaySumButton.Click += (sender, e) => getdaysum(sender, e, dateBox.Text);
        getReceiptButton.Click += (sender, e) => getreceipt(sender, e, SidBox.Text);
        getUnfinishedButton.Click += (sender, e) => getunfinished(sender, e, false);
        deleteUnfinishedButton.Click += (sender, e) => getunfinished(sender, e, true);
        getBestSellersButton.Click += (sender, e) => getbestsellers(sender, e);
        toggleSQLButton.Click += (sender, e) => toggleSQLButtonF(sender, e);

        adminForm.ShowDialog();
        

        //Console.WriteLine("button clicked "+test+" THIS IS IT");
    }

    void toggleSQLButtonF(Object sender, EventArgs e) {
        Button toggleButton = (Button)sender;
        BL.noSQL = !BL.noSQL;
        if (BL.noSQL) {
            toggleButton.Text = "MongoDB";
        } else {
            toggleButton.Text = "SQL";
        }
    }
    void getdaysum(Object sender, EventArgs e, string date) {
        string ans = BL.getDaySum(date);
        MessageBox.Show(ans);
    }
    void getreceipt(Object sender, EventArgs e, string sid) {
        string ans = BL.getReceipt(Int32.Parse(sid));
        MessageBox.Show(ans);
    }
    void getunfinished(Object sender, EventArgs e, Boolean delete) {
        string ans = BL.unfinshedSales(delete);
        MessageBox.Show(ans);
    }
    void getbestsellers(Object sender, EventArgs e) {
        string ans = BL.getBestSellers();
        MessageBox.Show(ans);
    }
    


    void Go() {
        //BL.initializeDatabase(); // THIS LINE IS NEEDED IF THE SQL SERVER IS NOT SET ON YOUR MACHINE
    
        //Console.Write(BL.getDaySum("30/08/2022"));
        // Create a new instance of the form1.
        Form form1 = new Form();
        // Create two buttons to use as the accept and cancel buttons.
        Button button1 = new Button();
        Button button2 = new Button();
        
        // Set the text of button1 to "OK".
        button1.Text = "New Sale";
        button1.Click += new EventHandler(salefunction);
        // Set the position of the button on the form.
        button1.Location = new Point (10, 10);
        // Set the text of button2 to "Cancel".
        button2.Text = "Admin";
        button2.Click += new EventHandler(adminfunction);
        // Set the position of the button based on the location of button1.
        button2.Location
            = new Point (button1.Left, button1.Height + button1.Top + 10);
        // Set the caption bar text of the form.   
        form1.Text = "Ice Cream Shop";
        // Display a help button on the form.
        //form1.HelpButton = true;

        
        // Define the border style of the form to a dialog box.
        form1.FormBorderStyle = FormBorderStyle.FixedDialog;
        form1.StartPosition = FormStartPosition.CenterScreen;
        
        // Add button1 to the form.
        form1.Controls.Add(button1);
        // Add button2 to the form.
        form1.Controls.Add(button2);
        
        // Display the form as a modal dialog box.
        form1.ShowDialog();
    }
    static void Main(string[] args)
    {
    GUI g = new GUI();
    g.Go();
    }

}