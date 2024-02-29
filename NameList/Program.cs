using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {

        string input;
        bool isValidInput = false;

        // Connection string to SQL
        string connectionString = "server=192.168.88.8;Database=ERPTest;User Id=report_reader;Password=R3p0rt1@3;";

        while (!isValidInput)
        {

            // Instruction for user to input the list of names.
            Console.WriteLine("Please enter a list of names, separated by commas.");
            input = Console.ReadLine();


            // Check if the input have any special character.
            if (Regex.IsMatch(input, @"[^\w\s,]"))
            {
                Console.WriteLine("Error: Input contains special characters.");
            }
            else
            {
                string[] nameList = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //Convert strings into lower case
                for (int i = 0; i < nameList.Length; i++)
                {
                    nameList[i] = nameList[i].ToLower();
                }

                Dictionary<string, int> wordCount = new Dictionary<string, int>();


                //Split all names into words and then counts the appearances of each word using dictionary.
                foreach (string name in nameList)
                {
                    string[] words = name.Trim().Split(' ');
                    foreach (string word in words)
                    {
                        if (wordCount.ContainsKey(word))
                        {
                            wordCount[word]++;
                        }
                        else
                        {
                            wordCount[word] = 1;
                        }
                    }
                }

                var sortedWordCount = wordCount.OrderByDescending(pair => pair.Value);

                //Print each word and its count
                foreach (var pair in sortedWordCount)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value}");
                }

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Insert each word and its count into a database table
                        foreach (var pair in sortedWordCount)
                        {
                            string sqlInsert = "INSERT INTO WordCounts (Word, Count) VALUES (@Word, @Count)";
                            using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                            {
                                command.Parameters.AddWithValue("@Word", pair.Key);
                                command.Parameters.AddWithValue("@Count", pair.Value);
                                command.ExecuteNonQuery();
                            }
                        }
                        Console.WriteLine("Data inserted into the database successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inserting data into the database: {ex.Message}");
                }
            }

        }  

    }
}
