using basket_api.Entities;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace basket_api
{
    public class MarketManagerDb
    {
    
        //READ
        public static string ReadMarketItem() 
        {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            string sqlcommand = "SELECT item_id as ItemId,item_name as ItemName,item_type as ItemType,item_price as ItemPrice FROM market_items";

            List<MarketItem> items = dbconnection.Query<MarketItem>(sqlcommand).ToList();

            var jsondata = JsonConvert.SerializeObject(items).ToString(); // Data acces shouldn't serialize any output data

            return jsondata;
        }

        //CREATE
        public static bool PutItemToMarket(int itemId, string itemName, string itemType, float itemPrice)  
        {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            string sqlcommand = $"INSERT INTO market_items VALUES({itemId},'{itemName}','{itemType}',{itemPrice});";

            dbconnection.Execute(sqlcommand);

            return true;
        }

        //DELETE

        public static bool DeleteItemFromMarket(int itemId) 
        {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            string sqlcommand = $"DELETE FROM market_items WHERE item_id = {itemId}";

            dbconnection.Execute(sqlcommand);

            return true; 
        }

        //UPDATE    check here later.

         public static bool UpdateItemFromMarket(int itemId, string itemName, string itemType, float itemPrice) 
         {
            SqlConnection dbconnection = new("Server=freecmsDB.mssql.somee.com;Database=freecmsDB;User Id=freeman527_SQLLogin_1;Password=o7es1v2wzs;");

            string sqlcommand = $"UPDATE market_items SET item_name = '{itemName}' , item_type = '{itemType}' , item_price = '{itemPrice}' WHERE item_id = {itemId}";

            MarketItem marketitems = new();
            marketitems.ItemId = itemId;
            
            if(itemName == null) 
            {
                marketitems.ItemType = dbconnection.Query($"SELECT item_name FROM market_items WHERE item_id = {itemId}").ToString();
            } else 
            {
                marketitems.ItemName = itemName;
            }

            if(itemType != null) 
            {
                marketitems.ItemType = dbconnection.Query($"SELECT item_type FROM market_items WHERE item_id = {itemId}").ToString();
            } else 
            {
                marketitems.ItemType = itemType;
            }
            
            marketitems.ItemPrice = itemPrice;

            dbconnection.Execute(sqlcommand);

            return true;
         }
    }
}