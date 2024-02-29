using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

class Program
{
    static void Main()
    {

        string input;
        bool isValidInput = false;

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
            }
        }
    }
}
