using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Contains all the classes which sets the properties of the objects
/// </summary>
namespace BookStoreLibrary
{
    /// <summary>
    /// Creates items and their properties
    /// </summary>
    public class Item
    {
        public string bookName { get; set; }

        public string bookAuthor { get; set; }
        public string bookDescription { get; set; }
        public double price { get; set; }
        public bool isSold { get; set; }

        public int quantity { get; set; }
        public Seller owner { get; set; }
        /// <summary>
        ///Returns how items are displayed in store
        /// </summary>
        public string displayItemInStore
        {
            get
            {
                return string.Format("{0} - {1} -> {2}€; Quantity:{3}.", bookAuthor, bookName, price,quantity);
            }
        }
        /// <summary>
        /// Returns how items are displayed in cart
        /// </summary>
        public string displayItemInCart
        {
            get
            {
                return string.Format("{0}({1}) - {2}€.", bookName, bookAuthor, price);
            }
        }


    }
    /// <summary>
    /// Creates sellers and their properties
    /// </summary>
    public class Seller
    {
        public string sellerName { get; set; }
        public double sellerCommission { get; set; }
    }
    /// <summary>
    /// Creates store and its properites
    /// </summary>
    public class Store
    {
        public string storeName { get; set; }
        public List<Seller> sellers { get; set; }

        public List<Item> items { get; set; }
        /// <summary>
        /// Constructor of Store class
        /// </summary>
        public Store()
        {
            sellers = new List<Seller>();
            items = new List<Item>();
        }
    }
}
