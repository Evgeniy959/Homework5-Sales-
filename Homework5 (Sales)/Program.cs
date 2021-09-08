/*Задание.
Добавить в программу (https://github.com/itstep-shambala/Sales.git) возможность добавления данных о покупке после ввода 
всей необходимой информации. Т.е.нужно от пользователя получить данные для всех полей таблицы tab_orders и написать 
запрос на добавление в неё строки.*/

using MySql.Data.MySqlClient;
using System;

namespace Homework5__Sales_
{
    class Program
    {
        static void Main()
        {
            const string CONN_STR = "Server = mysql60.hostland.ru; Database = host1323541_sbd07; Uid = host1323541_itstep; Pwd = 269f43dc;";
            var data_base = new MySqlConnection(CONN_STR);
            data_base.Open();

            ShowInfo("Выберите продукт остатки котрого хотите посмотреть");
            ShowInfo("1. Phone");
            ShowInfo("2. Car");
            var select = Console.ReadLine();
            var sql = $"SELECT count FROM tab_products_stock JOIN tab_products ON tab_products_stock.product_id = tab_products.id WHERE product_id = {select}";
            /*var sql1 = $@"INSERT INTO tab_orders (buyer_id, seller_id, date, product_id, amount, total_price)
                      VALUE (1, 1, '2021-11-23', 1, 2, 10000);";*/
            /*var command = new MySqlCommand();
            command.CommandText = sql;
            command.Connection = data_base;*/
            var command = new MySqlCommand
            {
                CommandText = sql,
                Connection = data_base
            };
            /*var command1 = new MySqlCommand
            {
                CommandText = sql1,
                Connection = data_base
            };*/
            var res = command.ExecuteReader();
            //command1.ExecuteNonQuery();
            if (res.HasRows)
            {
                do
                {
                    res.Read();
                    var count = res.GetInt32("count");
                    ShowSuccess($"count = {count}");
                } while (res.NextResult());

            }
            else
            {
                ShowError("Вернулась пустая таблица");
            }
            data_base.Close();
        }
        /*static void AddNewOrder(uint buyer_id, uint seller_id, DateTime data, uint product_id, uint amount, uint total_price)
        {
            var sql = $@"INSERT INTO tab_orders (buyer_id, seller_id, date, product_id, amount, total_price)
                      VALUE (1, 1, '2021-11-23', 1, 2, 10000);";           
        }*/

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
        }

        static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[Success] {message}");
            Console.ResetColor();
        }
        static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[Info] {message}");
            Console.ResetColor();
        }
    }
}

