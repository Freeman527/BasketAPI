using basket_api.Entities;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace basket_api
{
    public class BasketManagerDb
    {
        // advice: const would be more adequite instead of static       Updated.
        // naming should be improved        Names updated.
        private const string DbServerAdress = "127.0.0.1";
        private const string DbServerPort = "5432";
        private const string DbUserId = "postgres";
        private const string DbPassword = "adamadam41";
    
        

        //READ

        public static string ReadBasketItems(int BasketId)  
        {
            string sqlcommand = $"SELECT  basket_id as BasketId, item_id as ItemId, quantity as Quantity FROM basket_items WHERE basket_id = {BasketId}";

            var dbconnection = new NpgsqlConnection($"Server={DbServerAdress};Port={DbServerPort};Database=postgres;User Id={DbUserId};Password={DbPassword};");
            List<BasketItem> items = dbconnection.Query<BasketItem>(sqlcommand).ToList(); // use var instead of expilict definition

            var jsondata = JsonConvert.SerializeObject(items).ToString(); // serialization should be handled in the upper classes
            // serializing data is not bussiness of data access

            return jsondata;
        }

        //CREATE
        public static BasketItem AddItemToBasket(int BasketId, int ItemId, int Quantity) // Add item doesn't necessarly needs to return added value
        {
            string sqlcommand = $"SELECT basket_id as BasketId, item_id as ItemId, quantity as Quantity FROM basket_items WHERE basket_id = {BasketId} AND item_id = {ItemId};";

            var dbconnection = new NpgsqlConnection($"Server={DbServerAdress};Port={DbServerPort};Database=postgres;User Id={DbUserId};Password={DbPassword};");
            List<BasketItem> items = dbconnection.Query<BasketItem>(sqlcommand).ToList();

            
            if(items.Count == 0) // it would be more elegant with .Any()        Doesn't work with .Any(), giving error that it can't be null.
            {
                dbconnection.Execute($"INSERT INTO basket_items VALUES({BasketId}, {ItemId}, {Quantity});");
            } else 
            {
                Quantity += items.First().Quantity; // No need for direct access needed better option is FirstOrDefault()       Doesn't work like '.Any()' 
                dbconnection.Execute($"UPDATE basket_items SET quantity = {Quantity} WHERE basket_id = {BasketId};");
            }

            // No need
            BasketItem basketitems = new BasketItem();
            basketitems.BasketId = BasketId;
            basketitems.ItemId = ItemId;
            basketitems.Quantity = Quantity;

            return basketitems;
        }

        //DELETEALL
        public static string PurgeBasket(int BasketId) // incosisten naming sceme it should be CREATE,GET,DELETE,UPDATE or Add,Remove,Update,Get etc
        {
            var dbconnection = new NpgsqlConnection($"Server={DbServerAdress};Port={DbServerPort};Database=postgres;User Id={DbUserId};Password={DbPassword};");
            dbconnection.Execute($"DELETE FROM basket_items WHERE basket_id = {BasketId}");

            return $"Basket '{BasketId}' purged.";
        }

        //DELETE
        public static string DeleteItemFromBasket(int BasketId, int ItemId) 
        {
            var dbconnection = new NpgsqlConnection($"Server={DbServerAdress};Port={DbServerPort};Database=postgres;User Id={DbUserId};Password={DbPassword};");
            dbconnection.Execute($"DELETE FROM basket_items WHERE basket_id = {BasketId} AND item_id = {ItemId}");

            return $"item '{ItemId}' removed.";
        }

        //UPDATE
        public static bool UpdateItemFromBasket(int BasketId, int ItemId, int Quantity) 
        {
            var dbconnection = new NpgsqlConnection($"Server={DbServerAdress};Port={DbServerPort};Database=postgres;User Id={DbUserId};Password={DbPassword};");
            dbconnection.Execute($"UPDATE basket_items SET basket_id = {BasketId}, item_id = {ItemId}, quantity = {Quantity} WHERE basket_id = {BasketId} AND item_id = {ItemId}");

            return true;
        }
    }
}