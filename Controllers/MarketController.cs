using basket_api.Entities;
using basket_api;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace basket_api.Controllers
{
    public class MarketController : Controller
    {   
        [HttpGet("market/ReadMarketItems")]
        public string ReadMarketItem()
        {
            return MarketManagerDb.ReadMarketItem();
        }

        [HttpPost("market/PutItemToMarket")]
        public bool PutItemToMarket(int itemId ,string itemName, string itemType, float itemPrice) 
        {
            return MarketManagerDb.PutItemToMarket(itemId, itemName, itemType, itemPrice);
        }

        [HttpDelete("/market/DeleteItem")]
        public bool DeleteItemFromMarket(int itemId)
        {
            return MarketManagerDb.DeleteItemFromMarket(itemId);
        }

        [HttpPut("/market/UpdateItem")]
        public bool UpdateItemFromMarket(int itemId, string itemName, string itemType, float itemPrice)
        {
            return MarketManagerDb.UpdateItemFromMarket(itemId, itemName, itemType, itemPrice);
        }
    }
}