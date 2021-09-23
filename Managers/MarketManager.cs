using basket_api.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace basket_api
{
    public static class MarketManager
    {
        //READ
        public static string ReadMarketItems()
        {
            var items = System.IO.File.ReadAllText("Data/market_items.json");
            return items;
        }

        //CREATE
        public static MarketItem PutItemToMarket(string itemName, string itemType, float itemPrice)
        {
            var marketitems = new MarketItem();
            //marketitems.itemId = itemId;
            marketitems.itemName = itemName;
            marketitems.itemType = itemType;
            marketitems.itemPrice = itemPrice;

            var items = System.IO.File.ReadAllText("Data/market_items.json");
            var exsistingItems = JsonConvert.DeserializeObject<List<MarketItem>>(items);
            marketitems.itemId = exsistingItems.Count + 1;
            exsistingItems.Add(marketitems);

            string jsondata = JsonConvert.SerializeObject(exsistingItems);
            System.IO.File.WriteAllText("Data/market_items.json", jsondata);

            return marketitems;
        }

        //DELETE
        public static string DeleteItemFromMarket(int itemId)
        {

            var marketItems = System.IO.File.ReadAllText("Data/market_items.json");
            var exsistingMarketItems = JsonConvert.DeserializeObject<List<MarketItem>>(marketItems);


            int itemToDeleteIndex = -1;
            for (int i = 0; i < exsistingMarketItems.Count; i++)
            {
                var currentMarketItem = exsistingMarketItems[i];
                if (currentMarketItem.itemId == itemId)
                {
                    itemToDeleteIndex = i;
                    break;
                }
            }

            exsistingMarketItems.RemoveAt(itemToDeleteIndex);

            string jsondata = JsonConvert.SerializeObject(exsistingMarketItems);
            System.IO.File.WriteAllText("Data/market_items.json", jsondata);

            return "item deleted sucessfully";
        }

        //UPDATE
        public static MarketItem UpdateItemFromMarket(int itemId, string itemName, string itemType, float itemPrice)
        {

            var marketItems = System.IO.File.ReadAllText("Data/market_items.json");
            var exsistingMarketItems = JsonConvert.DeserializeObject<List<MarketItem>>(marketItems);

            var marketItemToBeUpdated = new MarketItem();
            marketItemToBeUpdated.itemId = itemId;

            if (itemName == null)
            {
                marketItemToBeUpdated.itemName = exsistingMarketItems[itemId - 1].itemName;
            }
            else
            {
                marketItemToBeUpdated.itemName = itemName;
            }

            if (itemType == null)
            {
                marketItemToBeUpdated.itemType = exsistingMarketItems[itemId - 1].itemType;
            }
            else
            {
                marketItemToBeUpdated.itemType = itemType;
            }

            marketItemToBeUpdated.itemPrice = itemPrice;

            exsistingMarketItems[itemId - 1] = marketItemToBeUpdated;

            string jsondata = JsonConvert.SerializeObject(exsistingMarketItems);
            System.IO.File.WriteAllText("Data/market_items.json", jsondata);

            return marketItemToBeUpdated;
        }
    }
}