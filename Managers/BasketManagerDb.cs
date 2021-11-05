using basket_api.Entities;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace basket_api
{
    public class BasketManagerDb
    {

        //READ

        public static string ReadBasketItems(int BasketId)  
        {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            List<BasketItem> items = dbconnection.Query<BasketItem>($"SELECT basket_id as BasketId, item_id as ItemId, quantity as Quantity FROM basket_items WHERE basket_id = {BasketId}").ToList();

            var jsondata = JsonConvert.SerializeObject(items).ToString(); // serialization should be handled in the upper classes
            // serializing data is not bussiness of data access

            return jsondata;
        }

        //CREATE
        public static BasketItem AddItemToBasket(int BasketId, int ItemId, int Quantity) // Add item doesn't necessarly needs to return added value
        {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            string sqlcommand = $"SELECT basket_id as BasketId, item_id as ItemId, quantity as Quantity FROM basket_items WHERE basket_id = {BasketId} AND item_id = {ItemId};";

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
            BasketItem basketitems = new()
            {
                BasketId = BasketId,
                ItemId = ItemId,
                Quantity = Quantity
            };

            return basketitems;
        }

        //DELETEALL
        public static string PurgeBasket(int BasketId) // incosisten naming sceme it should be CREATE,GET,DELETE,UPDATE or Add,Remove,Update,Get etc
        {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            dbconnection.Execute($"DELETE FROM basket_items WHERE basket_id = {BasketId}");

            return $"Basket '{BasketId}' purged.";
        }

        //DELETE
        public static string DeleteItemFromBasket(int BasketId, int ItemId) 
        {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            dbconnection.Execute($"DELETE FROM basket_items WHERE basket_id = {BasketId} AND item_id = {ItemId}");

            return $"item '{ItemId}' removed.";
        }

        //UPDATE
        public static bool UpdateItemFromBasket(int BasketId, int ItemId, int Quantity) 
        {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            dbconnection.Execute($"UPDATE basket_items SET basket_id = {BasketId}, item_id = {ItemId}, quantity = {Quantity} WHERE basket_id = {BasketId} AND item_id = {ItemId}");

            return true;
        }
    }
}