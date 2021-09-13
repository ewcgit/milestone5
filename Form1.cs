using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace m2
{
    public partial class Form1 : Form
    {
        public Form1(){
            InitializeComponent();
        }

        public class item{ // Class for the vending machine's products.
            // Getters and setters here.
            public int itemId { get; set; }
            public string itemName { get; set; }
            public double itemPrice { get; set; }
            public string itemType { get; set; }
            public int itemQuantityRemaining { get; set; }
        }

        public static List<item> items = new List<item>() { // Default inventory.
            new item{itemId=0, itemName="Sprite", itemPrice=1.99, itemType="Soda", itemQuantityRemaining=10},
            new item{itemId=1, itemName="Coca-Cola", itemPrice=2.49, itemType="Soda", itemQuantityRemaining=10},
            new item{itemId=2, itemName="Doritos", itemPrice=3.99, itemType="Chips", itemQuantityRemaining=10},
            new item{itemId=3, itemName="Lays", itemPrice=4.49, itemType="Chips", itemQuantityRemaining=10},
            new item{itemId=4, itemName="Pringles", itemPrice=3.29, itemType="Chips", itemQuantityRemaining=10},
            new item{itemId=5, itemName="Hershey Bar", itemPrice=1.49, itemType="Candy", itemQuantityRemaining=10},
            new item{itemId=6, itemName="Twizzlers", itemPrice=2.99, itemType="Candy", itemQuantityRemaining=10},
            new item{itemId=7, itemName="Hubba Bubba", itemPrice=0.99, itemType="Candy", itemQuantityRemaining=10},
            new item{itemId=8, itemName="Strawberry Nutri-Grain", itemPrice=0.99, itemType="Bar", itemQuantityRemaining=10},
            new item{itemId=9, itemName="Blueberry Nutri-Grain", itemPrice=0.99, itemType="Bar", itemQuantityRemaining=10},
            new item{itemId=10, itemName="Apple Nutri-Grain", itemPrice=0.99, itemType="Bar", itemQuantityRemaining=10},
        };

        public List<item> items2 = new List<item>(){ // For storing the user's purchased items.
        };

        public double balance = 50; // Default user starting money.

        public class inventoryManager{ // From milestone 3 requirements.
            public static item addItem(int x, string y, double z, string a, int b){
                // Constructs a new item and adds it to the vending machine's inventory.
                return new item {itemId = x, itemName = y, itemPrice = z, itemType = a, itemQuantityRemaining = b};
            }
            public static int removeItem(int x){
                // For the user to fill an item index to remove.
                return x;
            }
            public static int stocker(int x){
                // For the user to fill an item index to stock.
                return x;
            }
            public static int displayAll(int x){
                // Displays all products in the vending machine via a C# message box.
                string message = "";
                string title = "Current Vending Machine Contents";
                for (int counter = 0; counter < x; counter++){
                    message += "Item ID: " + items[counter].itemId.ToString() +
                        " | Item Name: " + items[counter].itemName.ToString() +
                        " | Item Price: $" + items[counter].itemPrice.ToString() +
                        " | Item Type: " + items[counter].itemType.ToString() +
                        " | Item Quantity Remaining: " + items[counter].itemQuantityRemaining.ToString() +
                        "\n";
                }
                MessageBox.Show(message, title);
                return x;
            }
            public static string resultsName(int x, string str){
                // The product name portion of the search feature.
                string message = "";
                string title = "Inventory Search";
                if (items[x].itemName.Contains(str)){
                    message = str + " found at ID " + x;
                    MessageBox.Show(message, title);
                    return str;
                }
                else{
                    return str;
                }
            }
            public static string resultsType(int x, string str){
                // The product type portion of the search feature.
                string message = "";
                string title = "Inventory Search";
                if (items[x].itemType.Contains(str))
                {
                    message = str + " found at ID " + x;
                    MessageBox.Show(message, title);
                    return str;
                }
                else
                {
                    return str;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e){
            string message = ""; // For later message boxes.
            string title = "Title"; // For later message boxes.
            
            if(numericUpDown1.Value > items.Count - 1){
                label22.Visible = true; // Warns the user of an invalid item index.
            }
            else if (items[(int)numericUpDown1.Value].itemQuantityRemaining >= (int)numericUpDown2.Value){
                if(balance > (items[(int)numericUpDown1.Value].itemPrice * (int)numericUpDown2.Value)){
                    for (int counter = 0; counter < (int)numericUpDown2.Value; counter++){
                        items2.Add(items[(int)numericUpDown1.Value]);
                        balance -= items[(int)numericUpDown1.Value].itemPrice;
                    }
                    // Items are only added if they are stocked and the user has enough money for them.
                    items[(int)numericUpDown1.Value].itemQuantityRemaining -= (int)numericUpDown2.Value;
                    label22.Visible = false;

                    // Updates all quantity and balance information in real time now.
                    label3.Text = "Product name: " + items[(int)numericUpDown1.Value].itemName.ToString();
                    label4.Text = "Product price ($): " + items[(int)numericUpDown1.Value].itemPrice.ToString();
                    label5.Text = "Product type: " + items[(int)numericUpDown1.Value].itemType.ToString();
                    label6.Text = "Amount remaining: " + items[(int)numericUpDown1.Value].itemQuantityRemaining.ToString();
                    label7.Text = "Balance ($): " + balance.ToString("0.00");
                }
                else{
                    // Warns the user of why the purchase did not go through for their attempts.
                    message = "Balance is too low!";
                    title = "Warning";
                    MessageBox.Show(message, title);
                    message = "";
                    label22.Visible = false;
                }
            }
            else{
                // Warns the user of why the purchase did not go through for their attempts.
                message = "Stock is too low!";
                title = "Warning";
                MessageBox.Show(message, title);
                label22.Visible = false;
            }

            /* Uncomment this section to display the full vending machine inventory after each user purchase.
           for(int counter = 0; counter < items.Count; counter++){
                // Displays the new current stock for the vending machine used.
                message += "Item ID: " + items[counter].itemId.ToString() +
                    " | Item Name: " + items[counter].itemName.ToString() +
                    " | Item Price: $" + items[counter].itemPrice.ToString() +
                    " | Item Type: " + items[counter].itemType.ToString() +
                    " | Item Quantity Remaining: " + items[counter].itemQuantityRemaining.ToString() +
                    "\n";
            }
            title = "Customer Inventory";
            MessageBox.Show(message, title);
            label22.Visible = false;*/
        }

        private void button2_Click(object sender, EventArgs e){
            // Restocks all.
            for (int counter = 0; counter < items.Count; counter++)
            {
                items[counter].itemQuantityRemaining = 10;
            }
            string message = "The stock machine has been refilled!";
            string title = "Restock Prompt";
            label21.Visible = false;
            label22.Visible = false;
            MessageBox.Show(message, title);
        }

        private void button3_Click(object sender, EventArgs e){
            string message = ""; // For later.

            // Item display.
            for (int counter = 0; counter < items2.Count; counter++){
                message += "Item ID: " + items2[counter].itemId.ToString() +
                    " | Item Name: " + items2[counter].itemName.ToString() +
                    " | Item Price: $" + items2[counter].itemPrice.ToString() +
                    " | Item Type: " + items2[counter].itemType.ToString() +
                    "\n";
            }

            string title = "Title"; // For later.
            label22.Visible = false;
            MessageBox.Show(message, title);
        }

        private void button4_Click(object sender, EventArgs e){
            if (numericUpDown1.Value > items.Count - 1){ // Boundary checking.
                label22.Visible = true;
            }
            else{
                label3.Text = "Product name: " + items[(int)numericUpDown1.Value].itemName.ToString();
                label4.Text = "Product price ($): " + items[(int)numericUpDown1.Value].itemPrice.ToString();
                label5.Text = "Product type: " + items[(int)numericUpDown1.Value].itemType.ToString();
                label6.Text = "Amount remaining: " + items[(int)numericUpDown1.Value].itemQuantityRemaining.ToString();
                label7.Text = "Balance ($): " + balance.ToString("0.00");
                label22.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e){
            label22.Visible = false; 
            inventoryManager.displayAll(items.Count);
        }

        private void Form1_Load(object sender, EventArgs e){

        }

        private void label10_Click(object sender, EventArgs e){

        }

        private void button9_Click(object sender, EventArgs e){
            if (numericUpDown5.Value > items.Count - 1){
                label21.Visible = true;
            }
            else{
                items[inventoryManager.stocker((int)numericUpDown5.Value)].itemQuantityRemaining = 10;
                numericUpDown5.Value = 0;
                label21.Visible = false;
                string message = "Successfully restocked!";
                string title = "Product Restocking";
                MessageBox.Show(message, title);
            }            
        }

        private void button7_Click(object sender, EventArgs e){
            string message = "Successfully added!";
            string title = "Product Creation";
            items.Add(inventoryManager.addItem(items.Count + 1, textBox1.Text, (double)numericUpDown3.Value, textBox2.Text, 10));
            textBox1.Text = "";
            numericUpDown3.Value = 0;
            textBox2.Text = "";
            label21.Visible = false;
            MessageBox.Show(message, title);
        }

        private void button8_Click(object sender, EventArgs e){
            if (numericUpDown4.Value > items.Count - 1){
                label21.Visible = true;
            }
            else{
                items.RemoveAt(inventoryManager.removeItem((int)numericUpDown4.Value));
                numericUpDown4.Value = 0;
                label21.Visible = false;
                string message = "Successfully removed!";
                string title = "Product Removal";
                MessageBox.Show(message, title);
            }
        }

        private void button10_Click(object sender, EventArgs e){
            label21.Visible = false;
            for (int counter = 0; counter < items.Count; counter++) {
                inventoryManager.resultsName(counter, textBox3.Text);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label21.Visible = false;
            for (int counter = 0; counter < items.Count; counter++){
                inventoryManager.resultsType(counter, textBox4.Text);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Bonus feature.
            balance = 100;
            label7.Text = "Balance ($): 100.00";
        }
    }
}