﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; // MySqlData -t telepíteni kell

namespace SQLTermekek_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string kapcsolatLeiro = "datasource=127.0.0.1;port=3306;database=hardver;username=root;password=;";

            MySqlConnection SQLkapcsolat = new MySqlConnection(kapcsolatLeiro);

            try
            {
                SQLkapcsolat.Open();
            }
            catch (MySqlException hiba)
            {
                Console.WriteLine(hiba.Message);
                Environment.Exit(1);
            }

            //Melyik a legdrágább alaplap? Kérem a nevét, gyártóját és árát!
            string SQLselect = "SELECT Név, Gyártó, Ár " +
                "FROM termékek " +
                "WHERE Ár = " +
                "(SELECT MAX(Ár) FROM Termékek GROUP BY Kategória HAVING Kategória = 'Alaplapok')";

            MySqlCommand SQLparancs = new MySqlCommand(SQLselect, SQLkapcsolat);
            MySqlDataReader eredmenyOlvaso = SQLparancs.ExecuteReader();
            while (eredmenyOlvaso.Read())
            {
                Console.Write($"\n\nTermék:{eredmenyOlvaso.GetString("Név")}, amit " +
                    $"{eredmenyOlvaso.GetString("Gyártó")} gyárt, ára : {eredmenyOlvaso.GetInt32("Ár")}\n\n\n");
            }
            eredmenyOlvaso.Close();
            SQLkapcsolat.Close();

        }
    }
}
