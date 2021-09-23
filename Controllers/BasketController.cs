using basket_api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace basket_api.Controllers
{
    public class BasketController : Controller
    {
        [HttpGet("/basket/CheckBasket")]
        public string checkBasketItems(int BasketId) 
        {
            return BasketManagerDb.ReadBasketItems(BasketId);
        }

        [HttpPost("/basket/createBasketItem")] 
        public BasketItem addItemToBasket(int BasketId ,int ItemId, int Quantity) 
        {
            return BasketManagerDb.AddItemToBasket(BasketId ,ItemId, Quantity);
        }

        [HttpDelete("/basket/deleteItem")]
        public string deleteItemFromBasket(int BasketId, int ItemId)
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