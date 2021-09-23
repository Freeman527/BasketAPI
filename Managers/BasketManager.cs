using basket_api.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace basket_api
{
    public static class BasketManager
    {
        public static string CheckBasketItems() 
        {
            string jsondata = System.IO.File.ReadAllText("Data/basket_items.json");
            return jsondata;
        }

        public static BasketItem AddItemToBasket(int itemId, int quantity)
        {
            var createdBasketItem = new BasketItem();
            createdBasketItem.ItemId = itemId;
            createdBasketItem.Quantity = quantity;
            
            var basketItems = System.IO.File.ReadAllText("Data/basket_items.json");
            var exsistingBasketItems = JsonConvert.DeserializeObject<List<BasketItem>>(basketItems);
            exsistingBasketItems.Add(createdBasketItem);

            string jsondata = JsonConvert.SerializeObject(exsistingBasketItems);
            System.IO.File.WriteAllText("Data/basket_items.json", jsondata);
            //basketItems.Add(createdBasketItem);
            

            return createdBasketItem;
        }

        public static BasketItem DeleteItemFromBasket(int itemId, int quantity)
        {
            var basketItemToBeDeleted = new BasketItem();
            basketItemToBeDeleted.ItemId = itemId;
            basketItemToBeDeleted.Quantity = quantity;
            
            var basketItems = System.IO.File.ReadAllText("Data/basket_items.json");
            var exsistingBasketItems = JsonConvert.DeserializeObject<List<BasketItem>>(basketItems);
            exsistingBasketItems.Remove(basketItemToBeDeleted);

            string jsondata = JsonConvert.SerializeObject(exsistingBasketItems);
            System.IO.File.WriteAllText("Data/basket_items.json", jsondata);

            return basketItemToBeDeleted;
        }

        //basketId parameter will be added on this function later
        public static string ClearAllBasketItems() 
        {
            System.IO.File.WriteAllText("Data/basket_items.json", "[]");
            return "all items are cleared";
        }

        public static string UpdateItem(int itemId, int quantity) 
        {
            return "item updated";
        }
    }
}