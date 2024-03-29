﻿using System.Collections.Generic;
using System.Linq;

namespace API.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; } = new();


        public void AddItem( Product product, int quantity )
        {
            if ( Items.All( items => items.ProductId != product.Id ) )
            {
                Items.Add( new CartItem { Product = product, Quantity = quantity } );
            }

            var existingItem = Items.FirstOrDefault( x => x.ProductId == product.Id );
            if ( existingItem != null ) existingItem.Quantity += quantity;
        }


        public void RemoveItem( int productId, int quantity )
        {
            var item = Items.FirstOrDefault( item => item.ProductId == productId );
            if ( item == null ) return;
            item.Quantity -= quantity;
            if ( item.Quantity == 0 ) Items.Remove( item );
        }
    }
}
