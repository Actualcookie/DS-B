using System;

namespace DS8
{
    //Tim Schut - 4255410
    class Program
    {
        static string[] input;
        static Skiplist list;

        static void Main(string[] args)
        {
            list = new Skiplist();

            while (true)
            {
                //Get the input
                string playerinp = Console.ReadLine();
                //Check if the input is null
                if (playerinp == null)
                    break;

                input = playerinp.Split();

                switch (input[0])
                {
                    case "T":
                        AddPlayer();
                        break;
                    case "G":
                        break;
                    case "R":
                        break;
                }
            }
        }


    }
}