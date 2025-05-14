using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
using System.Threading;
using System.Xml.Linq;
//using NAudio.SoundFont;
//using System.Speech.Synthesis;
using System.Net.Http.Headers;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace ChatbotApp
{
    class Program
    {
        static string userName = "";
        static string userInterest = "";

        static Dictionary<string, string> keywordResponses = new Dictionary<string, string>()
        {
            {"password", "Make sure to use strong, unique passwords for each account. Avoid using personal details." },
            {"scam", "Beware of online scams. Don't click on suspicious links and always verify sources." },
            {"privacy", "Review your privacy settings regularly. Limit what you share publicly." }
        };
        static void Main(string[] args)
        {
            Console.OutputEncoding =System.Text.Encoding.UTF8;
            Console.Clear();
            DisplayAsciiLogo();

            System.Threading.Thread.Sleep(1000);

            
            // Play the voice greeting
            PlayVoiceGreeting();


            // Greet the user and ask for their name


            Console.Write("What is your name? ");
            string userName = Console.ReadLine();
            if (string.IsNullOrEmpty(userName))
            {
                Console.WriteLine("You didn't enter a name. Please try again.");
                return;
            }
            Console.WriteLine($"Hello, {userName}! Welcome to the Cybersecurity Awareness Bot!");

            // Simulate typing effect and ask user about how they need help
            TypeText("I am here to help you stay safe online by providing some cybersecurity tips...");

            // Respond to basic user queries
            RespondToQueries();

            // Example input validation handling
            InputValidation();
        }

        // Method to play the voice greeting when the chatbot starts
        static void PlayVoiceGreeting()
        {
            try 
            {
                string filePath = @"C:\Users\RC_Student_lab\source\repos\ChatBotApp2\greeting\greeting.wav";

                //Check if the file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Error: Audio file not found at " + filePath);
                    return;
                }

                SoundPlayer myPlayer = new SoundPlayer(filePath);
                myPlayer.PlaySync();

                Console.WriteLine("Welcome to the Cybersecurity Awareness chatbot!");
            }
            catch (Exception ex)  
            { 
                Console.WriteLine("Error playing the voice greeting: " + ex.Message);
            }
        }

        // Method to display the ASCII logo or art of the chatbot
        static void DisplayAsciiLogo()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
  ____        _     _                 _       
 / ___| _   _| |__ (_)_ __ ___   __ _| |_ ___ 
 \___ \| | | | '_ \| | '_ ` _ \ / _` | __/ _ \
  ___) | |_| | |_) | | | | | | | (_| | ||  __/
 |____/ \__,_|_.__/|_|_| |_| |_|\__,_|\__\___|
   CYBERSECURITY  CHATBOT  

  ");
            Console.ResetColor();
        }

        // Method to simulate typing effect
        static void TypeText(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);




                Thread.Sleep(50); // Simulate typing delay
            }
            Console.WriteLine();
        }
        
        // Method to respond to user queries about the chatbot's purpose
        static void RespondToQueries()
        {
            Console.WriteLine("Ask me anything about cybersecurity. Type 'exit' to quit.");
            string input;
            while ((input = Console.ReadLine().ToLower()) != "exit")
            {
                if (input == "how are you?")
                {
                    Console.WriteLine("I'm just a bot, but I'm here to help!");
                }
                else if (input == "what's your purpose?")
                {
                    Console.WriteLine("My purpose is to help you stay safe online by providing cybersecurity tips and answering your questions.");
                }
                else if (input == "what is phishing?")
                {
                    Console.WriteLine("Phishing is a scam where attckers trick you into revealing sensitive informaton. Alaways verify links before clicking!");
                }
                else if (input == "how to create a strong password?")
                {
                    Console.WriteLine("Use a strong password with a mix of letters, numbers, and symbols. Enable two-factor authentication when possible.");
                }
                else if (input == "what is safe browsing?")
                {
                    Console.WriteLine("Safe browsing is when you avoid suspicious links, use secure website (HTTPS), and update your browser regularly.");
                }
                else if (input.Contains("phishing"))
                {
                    RandomPhishingTip();
                }
                else if (input.Contains("interested in"))
                {
                    int index = input.IndexOf("interested in") + "interested in".Length;
                    userInterest = input.Substring(index).Trim();
                    Console.WriteLine($"Great! I'll remeber that you're interested in {userInterest}. It's a crucail part of staying safe online.");
                }
                else if (input.Contains("remind me"))
                {
                    if (!string.IsNullOrEmpty(userInterest))
                    {
                        Console.WriteLine($"As someone interested in {userInterest}, you might want to check your account settings regularly.");
                    }
                    else
                    {
                        Console.WriteLine("You haven't told me your interest yet.");
                    }
                }
                else if (input.Contains("worried") || input.Contains("scared"))
                {
                    Console.WriteLine("It's completely understandable to feel that way. Scammers can be very convincng. Let me share some tips to help you stay safe.");
                }
                else if (input.Contains("frustrated"))
                {
                    Console.WriteLine("Don't worry, you're not alone. Let me try to explain things more clearly");
                }
                else if (input.Contains("curious"))
                {
                    Console.WriteLine("Curiosity is great! Let me share some interesting facts about staying safe online.");
                }
                else
                {
                    bool foundKeyword = false;
                    foreach (var keyword in keywordResponses.Keys)
                    {
                        if (input.Contains(keyword))
                        {
                            Console.WriteLine(keywordResponses[keyword]);
                            foundKeyword = true;
                            break;
                        }
                    }

                    if (!foundKeyword)
                    {
                        Console.WriteLine("I didn't quite understand that. Could you rephrase?");
                    }
                }
                
            }

        }

        static void RandomPhishingTip()
        {
            string[] tips = {
                "Never click on suspicious email links.",
                "Scammers may impersonate trusted companies-always verify the sender.",
                "Hover over links to preview the URL before clicking."
            };

            Random rand = new Random();
            Console.WriteLine(tips[rand.Next(tips.Length)]);
        }
        // Method to handle input validation and guide the user if no valid input is entered
        static void InputValidation()
        {
            Console.Write("Please type a question: ");
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("You didn't enter anything. Please type a valid question.");
            }
            else
            {
                Console.WriteLine("Thank you for your input! Now, how else can I help you?");
            }
        }
    }
}