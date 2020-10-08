using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using BookStoreLibrary;


namespace ProiectIP
{
    /// <summary>
    /// Class which does the entire process of purchasing from the store.
    /// </summary>
    public partial class ShopForm : Form
    {
        private Store _myStore = new Store();
        private List<Item> _cartItems = new List<Item>();
        BindingSource itemsBinding = new BindingSource();
        BindingSource cartBinding = new BindingSource();
        
        /// <summary>
        /// Constructor of ShopForm class;
        /// </summary>
        public ShopForm()
        {
            InitializeComponent();
            
            SetUpData();

            itemsBinding.DataSource = _myStore.items;
            itemsListBox.DataSource = itemsBinding;

            itemsListBox.DisplayMember = "displayItemInStore";
            itemsListBox.ValueMember = "displayItemInStore";

            cartBinding.DataSource = _cartItems;
            shoppingCartListBox.DataSource = cartBinding;

            shoppingCartListBox.DisplayMember = "displayItemInCart";
            shoppingCartListBox.ValueMember = "displayItemInCart";
        }
        /// <summary>
        /// Adds informations about the items and puts them in the store.
        /// </summary>
        private void SetUpData()
        {
            Seller firstSeller = new Seller();
            firstSeller.sellerName = "Nemira";
            firstSeller.sellerCommission = .6;
            _myStore.sellers.Add(firstSeller);

            Seller secondSeller = new Seller();
            firstSeller.sellerName = "Polirom";
            firstSeller.sellerCommission = .5;
            _myStore.sellers.Add(firstSeller);

            Seller thirdSeller = new Seller();
            firstSeller.sellerName = "Libris";
            firstSeller.sellerCommission = .4;
            _myStore.sellers.Add(firstSeller);

            _myStore.items.Add(new Item
            {
                bookName = "Karamazov Brothers",
                bookAuthor="F.M. Dostoievski",
                bookDescription = "A book about magic.",
                price = 5.00,
                owner = _myStore.sellers[0],
                quantity=10
            });;
            _myStore.items.Add(new Item
            {
                bookName = "The Shining",
                bookAuthor = "Stephen King",
                bookDescription = "A horror book.",
                price = 8.00,
                owner = _myStore.sellers[1],
                quantity=15
            });;
            _myStore.items.Add(new Item
            {
                bookName = "Digital fortress",
                bookAuthor = "Dan Brown",
                bookDescription = "A thrillering book.",
                price = 7.50,
                owner = _myStore.sellers[2],
                quantity=8
            });
            _myStore.items.Add(new Item
            {
                bookName = "Ion",
                bookAuthor = "Liviu Rebreanu",
                bookDescription = "A book which describe the obsessive love for the land.",
                price = 3.75,
                owner = _myStore.sellers[0],
                quantity = 5
            });
            _myStore.items.Add(new Item
            {
                bookName = "The Da Vinci Code",
                bookAuthor = "Dan Brown",
                bookDescription = "Another thrillering book.",
                price = 7.50,
                owner = _myStore.sellers[2],
                quantity = 10
            });
            _myStore.items.Add(new Item
            {
                bookName = "Harry Potter",
                bookAuthor = "J.K. Rowling",
                bookDescription = "A book about magic.",
                price = 5,
                owner = _myStore.sellers[1],
                quantity = 12
            });
            _myStore.items.Add(new Item
            {
                bookName = "The A.B.C Murders",
                bookAuthor = "Agatha Christie",
                bookDescription = "A book about a mysterious murder.",
                price = 6.25,
                owner = _myStore.sellers[0],
                quantity = 5
            });


            _myStore.storeName = "The Book Deal";

        }

        /// <summary>
        /// Adds the items from the store in the cart.
        /// </summary>
        double _total = 0;
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (itemsListBox.Items.Count < 1)
            {
                MessageBox.Show("No items selected!");
            }
            Item selectedItem = (Item)itemsListBox.SelectedItem;
            if (selectedItem.quantity == 0)
            {
                MessageBox.Show("Sorry, " + selectedItem.bookName + " by " + selectedItem.bookAuthor + " is no longer available in our stock.");
            }
            else
            {
                _cartItems.Add(selectedItem);
                selectedItem.quantity--;
                _total += selectedItem.price;
            }
            totalTextBox.Text ="Total: "+ _total.ToString()+ "€." +Environment.NewLine  +"Click \"Buy\" to complete the order.";
            itemsBinding.ResetBindings(false);
            cartBinding.ResetBindings(false);
        }
        /// <summary>
        /// Does the purchase process of the selected items from the cart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count < 1)
            {
                MessageBox.Show("There are no items in your cart!");
                return;
            }
            else
            {
                foreach (Item item in _cartItems)
                {                  
                    if (item == null)
                    {
                        MessageBox.Show("There are no items in your cart!");
                        return;
                    }
                    item.isSold = true;

                }
                string message = CreateMessage(_cartItems);
                SendEmail(message, GlobalVariables.email);
                _cartItems.Clear();
                //itemsBinding.DataSource = _myStore.items.Where(x => x.sold == false).ToList();
                cartBinding.ResetBindings(false);
                itemsBinding.ResetBindings(false);
                MessageBox.Show("Product purchased! Thanks for buying from us!");
                _orderNum++;
                _total = 0;
                totalTextBox.Clear();
            }
            
        }

        int _orderNum = 1;
        /// <summary>
        /// Creates the message which is sent by email to the user who bought the items.
        /// </summary>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        public string CreateMessage(List<Item> cartItems)
        {
            int itemNum = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("Hi " + GlobalVariables.name + "," + Environment.NewLine + Environment.NewLine + "Thank you for shopping with us! Your order was successfully placed and your payment has been processed: " + Environment.NewLine);
            sb.AppendLine();
            sb.AppendLine("----------------------------YOUR ORDER NUMBER IS #" + _orderNum + "----------------------------" + Environment.NewLine);
            sb.AppendLine("Products list: " + Environment.NewLine);
            foreach (var Item in cartItems)
            {
                sb.AppendLine(itemNum + ". " + Item.bookName + "(" + Item.bookAuthor + ") - " + Item.price + "€.");
                itemNum++;
            }
            sb.AppendLine();
            sb.AppendLine("BILLING INFORMATION: " + GlobalVariables.adress + ".");
            sb.AppendLine();
            sb.AppendLine("Total amount of payment: " + _total + "€.");
            sb.AppendLine();
            sb.AppendLine("Kind regards, ");
            sb.AppendLine("The Book Deal");
            return sb.ToString();
        }
        /// <summary>
        /// Delete items from cart.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteFromCart_Click(object sender, EventArgs e)
        {
            Item selectedItem = (Item)shoppingCartListBox.SelectedItem;
            _cartItems.Remove(selectedItem);
            for(int i = 0; i < _myStore.items.Count; ++i)
            {
                if (_myStore.items[i] == selectedItem)
                {
                    _myStore.items[i].quantity++;
                    _total -= selectedItem.price;
                }
            }
            totalTextBox.Text ="Total: "+ _total.ToString() + "€." + Environment.NewLine + "Click \"Buy\" to complete the order.";
            itemsBinding.ResetBindings(false);
            cartBinding.ResetBindings(false);
        }

        /// <summary>
        /// Sends the confirmation mail to the user's email adress.
        /// </summary>
        /// <param name="confirmation">Continutul emeilului</param>
        /// <param name="email">Emailul destinatarului</param>
        public static void SendEmail(string confirmation, string email)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("TheBookDealLibrary@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Order information";             
                message.Body = confirmation;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("TheBookDealLibrary@gmail.com", "thebookdeal");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookStore_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
