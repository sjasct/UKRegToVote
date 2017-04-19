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

            int min = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;
            int date = DateTime.Now.Day;
            int month = DateTime.Now.Month;

            // Reg
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
                "You only have until the 22nd of May to register!" + foot,
                "If you're 16-17, you can still register even though you can't yet vote!" + foot,

            };

            //vote day
            string hrsLeft = ((22 - 7) - hour).ToString();
            string hrsLeftStatement = hrsLeft + " hours left!";
            string footVote = hrsLeftStatement + hash;

            string[] tweetsVote = new string[]
            {
                "Remember to go out and vote to make your voice heard!" + foot,
                "Remember to vote if you haven't already! " + foot,
                "Vote!" + foot,
                "Vote vote vote!" + regUrl,
            };

            while (true) {

                // if before reg deadline
                if(date < 22 && month < 5)
                {
                    SendTweet(tweetsReg, 60);
                }
                // if between 7pm-10pm on vote day
                else if(date == 8 && month == 6 && hour >= 7 && hour <= 22)
                {
                    SendTweet(tweetsVote, 30);
                }

            }

        }

        static void SendTweet(string[] tweetArray, int interval)
        {

            int min = DateTime.Now.Minute;


            Random random = new Random();

            // if
            // specified tweet every 60 mins and it's minute 0
            // or
            // specified tweet every 30 mins and it's miniute 0 or 40
            if ((interval == 60 && min == 0) || (interval == 30 && (min == 0 || min == 30)) )
            {

                string chosenTweet = tweetArray[random.Next(tweetArray.Length)];

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
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(30000);
                }

            }

            System.Threading.Thread.Sleep(30000);

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

            Console.WriteLine(DateTime.Now + "  |  " + msg);
        }

        
    }
}
