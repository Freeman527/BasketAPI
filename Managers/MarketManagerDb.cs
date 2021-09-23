using basket_api.Entities;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace basket_api
{
    public class MarketManagerDb
    {
        private static string db_serveradress = "127.0.0.1";
        private static string db_serverport = "5432";
        private static string db_userid = "postgres";
        private static string db_password = "adamadam41";


        //READ
        public static string ReadMarketItem() 
        {
            string sqlcommand = "SELECT item_id as itemId,item_name as itemName,item_type as itemType,item_price as itemPrice FROM market_items";

            var dbconnection = new NpgsqlConnection($"Server={db_serveradress};Port={db_serverport};Database=postgres;User Id={db_userid};Password={db_password};");
            List<MarketItem> items = dbconnection.Query<MarketItem>(sqlcommand).ToList();

            var jsondata = JsonConvert.SerializeObject(items).ToString(); // Data acces shouldn't serialize any output data

            return jsondata;
        }

        //CREATE
        public static bool PutItemToMarket(int itemId, string itemName, string itemType, float itemPrice)  
        {
            string sqlcommand = $"INSERT INTO market_items VALUES({itemId},'{itemName}','{itemType}',{itemPrice});";

            var dbconnection = new NpgsqlConnection($"Server={db_serveradress};Port={db_serverport};Database=postgres;User Id={db_userid};Password={db_password};");    

            dbconnection.Execute(sqlcommand);

            return true;
        }

        //DELETE

        public static bool DeleteItemFromMarket(int itemId) 
        {
            string sqlcommand = $"DELETE FROM market_items WHERE item_id = {itemId}";

            var dbconnection = new NpgsqlConnection($"Server={db_serveradress};Port={db_serverport};Database=postgres;User Id={db_userid};Password={db_password};"); 

            dbconnection.Execute(sqlcommand);

            return true; 
        }

        //UPDATE    check here later.

         public static bool UpdateItemFromMarket(int itemId, string itemName, string itemType, float itemPrice) 
         {
            string sqlcommand = $"UPDATE market_items SET item_name = '{itemName}' , item_type = '{itemType}' , item_price = '{itemPrice}' WHERE item_id = {itemId}";

            var dbconnection = new NpgsqlConnection($"Server={db_serveradress};Port={db_serverport};Database=postgres;User Id={db_userid};Password={db_password};");

            MarketItem marketitems = new MarketItem();
            marketitems.itemId = itemId;
            
            if(itemName == null) 
            {
                marketitems.itemType = dbconnection.Query($"SELECT item_name FROM market_items WHERE item_id = {itemId}").ToString();
            } else 
            {
                marketitems.itemName = itemName;
            }

            if(itemType != null) 
            {
                marketitems.itemType = dbconnection.Query($"SELECT item_type FROM market_items WHERE item_id = {itemId}").ToString();
            } else 
            {
                marketitems.itemType = itemType;
            }
            
            marketitems.itemPrice = itemPrice;

            dbconnection.Execute(sqlcommand);

            return true;
         }
    }
}