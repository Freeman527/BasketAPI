using basket_api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace basket_api.Controllers
{
    public class BasketController : Controller
    {
        [HttpGet("/basket/CheckBasket")]
        public string CheckBasketItems(int BasketId) 
        {
            return BasketManagerDb.ReadBasketItems(BasketId);
        }

        [HttpPost("/basket/createBasketItem")] 
        public BasketItem AddItemToBasket(int BasketId ,int ItemId, int Quantity) 
        {
            return BasketManagerDb.AddItemToBasket(BasketId ,ItemId, Quantity);
        }

        [HttpDelete("/basket/deleteItem")]
        public string DeleteItemFromBasket(int BasketId, int ItemId)
        {
            return BasketManagerDb.DeleteItemFromBasket(BasketId, ItemId);
        }

        [HttpDelete("/basket/purgeBasket")]
        public string PurgeBasket(int BasketId) 
        {
            return BasketManagerDb.PurgeBasket(BasketId);
        }

        [HttpPut("/basket/updateItem")]
        public bool UpdateBasket(int BasketId, int ItemId, int Quantity) 
        {
            return BasketManagerDb.UpdateItemFromBasket(BasketId, ItemId, Quantity);
        }

    }
}