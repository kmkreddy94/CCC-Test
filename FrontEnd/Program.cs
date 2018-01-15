using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Exam;

namespace FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("============================================");
            Console.WriteLine("1. Enter State Name or State Abbreviation");
            Console.WriteLine("2. Enter 1 to exit from program");
            Console.WriteLine("============================================");

            while (true)
            {
                var key = Console.ReadLine();

                if (key.ToString() != "1")
                {
                    RunAsync(key.ToString().ToUpper()).GetAwaiter().GetResult();
                }

                if (key.ToString() == "")
                {
                    Console.WriteLine("Please enter State Name or State Abbreviation to search");
                }

                if (key.ToString() == "1")
                {
                    Environment.Exit(0);
                }
            }

        }

        static async Task RunAsync(string query)
        {
            try
            {
                Exam.Country country = new Exam.Country();
                // Get the state //Later we can move it to the config file
                string path = "http://services.groupkt.com/state/get/USA/all";
                var state =  await country.GetStateDetailsAsync(path, query);
                ShowState(state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        static void ShowState(State state)
        {
            if (state == null)
            {
                Console.WriteLine($"No Record Found ");
            }
            else
            {
                Console.WriteLine($"Capital: {state.capital} ");
                Console.WriteLine($"Largest City: {state.largest_city} ");
            }
        }

    }
}
