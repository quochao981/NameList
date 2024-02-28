using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {

        string input;
        bool isValidInput = false;

        while (!isValidInput)
        {

            // Instruction for user to input the list of names.
            Console.WriteLine("Please enter a list of names, and then press Enter");
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

                List<string> mostCommonWords = new List<string>(); // Store the most common word(s) in the input names.
                int maxCount = 0; // Store the maximum count of occurrences of any word in the input names.

                // Loop through the word count dictionary.
                foreach (var pair in wordCount)
                {
                    if (pair.Value > maxCount)
                    {
                        mostCommonWords.Clear(); // Clear previous most common words
                        maxCount = pair.Value;
                        mostCommonWords.Add(pair.Key);
                    }
                    else if (pair.Value == maxCount)
                    {
                        mostCommonWords.Add(pair.Key);
                    }
                }


                //Print out most common word(s) that appeared in the list.
                Console.Write("Most common word(s): ");
                for (int i = 0; i < mostCommonWords.Count; i++)
                {
                    Console.Write(mostCommonWords[i]);
                    if (i < mostCommonWords.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine($" appeared {maxCount} times in {nameList.Length} names");
            }
        }
    }
}
