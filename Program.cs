using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;



namespace HelloLeet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("After Analyzing.. \n");    // start

            string text = System.IO.File.ReadAllText(@"./asset/The Count of Monte Cristo Modified.txt");

            // Q1
            var count = Regex.Matches(text, "Chapter").Count;           
            Console.WriteLine($"There are {count} Chapters so far");


            // Q2
            File.WriteAllText(@"./asset/The Count of Monte Cristo.txt", Translate(text)); 
            

            // Q3
            ValidPhone(text);


            // Q4
            Console.WriteLine($"\nKey Information: {DateToSend(text)}" );

            
        }



        public static string Translate(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var charMap = new Dictionary<char, char>
            {
                {'4', 'a'}, {'3', 'e'}, {'6', 'g'}, {'1', 'i'}, 
                {'0', 'o'}, {'5', 's'}, {'7', 't'}
            };

            StringBuilder result = new StringBuilder();

            for (var i = 0; i < input.Length; i++)
            {

                if (
                    (
                        (i > 0 && char.IsLetter(result[i - 1])) ||
                        (i < input.Length - 1 && char.IsLetter(input[i + 1]))
                    ) && charMap.ContainsKey(input[i]) && (!(input.Substring(i, 3).Contains("th")))
                    )
                {
                    result.Append(charMap[input[i]]);

                    var prevIndex = i - 1;

                    while (prevIndex >= 0 && charMap.ContainsKey(result[prevIndex]))
                    {
                        result[prevIndex] = charMap[result[prevIndex]];
                        prevIndex--;
                    }
                }

                else
                {
                    result.Append(input[i]);
                }
            }

            return result.ToString();
        }



        public static string ValidPhone(string input)
        {
            var regex = new Regex(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})?(?: *x(\d+))?\s");
            var match = regex.Match(input);

            Console.WriteLine("\nWithin Phone Numbers: ");

            while (match.Success)
            {
                string phoneNumber = match.Groups[0].Value;
                match = match.NextMatch();
                Console.WriteLine(phoneNumber);              
            }
            return "Done the Phone";
  
        }  

        


        public static string DateToSend(string input)  
        {
            var regex = new Regex("[^.!?;]*(Notre-Dame de la Garde)[^.!?;]*");
            var match = regex.Matches(input);
            var result = Enumerable.Range(0, match.Count).Select(index => match[index].Value).ToList();

            return string.Join(" ", result);
        }
     

    }
}




