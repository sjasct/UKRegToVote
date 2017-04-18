using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace UkRegVoteBot
{
    class Program
    {
        static void Main(string[] args)
        {

            try {
                auth authDetails = new auth();
                Auth.SetUserCredentials(authDetails.consumerKey, authDetails.consumerSecret, authDetails.accessToken, authDetails.accessSecret);
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log("Connect failed!", "error");
                Log(ex.ToString(), "error");
            }
            finally
            {
                Log("Connect success!", "success");
            }
            sendTweet();
            Console.ReadLine();
        }

        static void sendTweet()
        {

            string regUrl = " https://www.gov.uk/register-to-vote ";
            string hash = " #GeneralElection ";
            string foot = regUrl + hash;

            string[] tweetsReg = new string[]
            {
                "Remember to register to vote before it's too late!" + foot,
                "Have you changed your address since the last election you voted in? You need to re-register!" + foot,
                "Registering to vote usually only takes 5 minutes of your time. Do it before you forget!" + foot,
                "Just turned 18? Is the 2017" + hash + "the first election you'll vote in? Remember to register!" + regUrl,
                "If you're having exams on the day of the election, you can sign up for a postal vote!" + foot,
                "Student? You can register to vote at home and your Uni accommodation" + foot,
                "Outside of the UK on the 8th of June? You can still register for a postal vote!" + foot,
            };

            Random random = new Random();

            string chosenTweet = null;

            while (true) {
                chosenTweet = tweetsReg[random.Next(tweetsReg.Length)];

                // try to tweet
                try
                {
                    Tweet.PublishTweet(chosenTweet);
                }
                // catch error
                catch (Exception ex)
                {
                    Log("Tweet failed!", "error");
                    Log(ex.ToString(), "error");
                }
                // detail success + tweet posted
                finally
                {
                    Log("Tweet success!", "success");
                    Log(chosenTweet, "");

                }

                // repeat every 20 sec
                System.Threading.Thread.Sleep(3600000);

            }

            
        }

        static void Log(string msg, string type)
        {
            if(type.ToLower() == "error")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if(type.ToLower() == "success")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine(DateTime.Now + " - " + msg);
        }
    }
}
