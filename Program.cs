using System;

namespace DS8
{
    //Tim Schut - 4255410
    class Program
    {
        static string[] input;
        static Spelerboom boom;

        static void Main(string[] args)
        {
            boom = new Spelerboom();

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

        static void AddPlayer()
        {
            boom.AddPlayer(new Player(int.Parse(input[1]), int.Parse(input[2])));
        }
    }

    public class Player
    {
        int number, score;

        public Player(int number, int score)
        {
            this.number = number;
            this.score = score;
        }

        public override string ToString()
        {
            return "Nr: " + number + ", score: " + score;
        }

        public int Number
        { get { return number; } }
        public int Score
        { get { return score; } }
    }

    public class BoomOnderdelen<T>
    {
        public T Value;
        public BoomOnderdelen<T> Parent;
        BoomOnderdelen<T> left, right;
        bool red = true;
        bool black = false;

        public BoomOnderdelen(T value)
        {
            this.Value = value;
        }

        public BoomOnderdelen<T> Left
        {
            get { return left; }
            set
            {
                left = value;
                if (left != null)
                    left.Parent = this;
            }
        }
        public BoomOnderdelen<T> Right
        {
            get { return right; }
            set
            {
                right = value;
                if (right != null)
                    right.Parent = this;
            }
        }
        public bool Black
        {
            get { return black; }
            set
            {
                black = value;
                red = !value;
            }
        }
        public bool Red
        {
            get { return red; }
            set
            {
                black = !value;
                red = value;
            }
        }
    }

    public class Spelerboom
    {
        BoomOnderdelen<Player> root;

        public void AddPlayer(Player p)
        {
            BoomOnderdelen<Player> newPlayer = new BoomOnderdelen<Player>(p);

            //Check if the tree is empty
            if (root == null)
            {
                //Save the new player as root
                root = newPlayer;
                root.Black = true;
                return;
            }

            //Insert
            BoomOnderdelen<Player> y = null;
            BoomOnderdelen<Player> x = root;
            while(x != null)
            {
                y = x;
                //Compare the scores
                if (newPlayer.Value.Score <= x.Value.Score)
                    x = x.Left;
                else
                    x = x.Right;
            }
            //Save the new player in the tree
            if (newPlayer.Value.Score <= y.Value.Score)
                y.Left = newPlayer;
            else
                y.Right = newPlayer;

            //Fix the tree
            FixBoom(newPlayer);
        }
        void FixBoom(BoomOnderdelen<Player> element)
        {
            while (element.Red)
            {
                if (element.Parent.Parent != null && element.Parent.Parent.Left != null && element.Parent == element.Parent.Parent.Left)
                {
                    BoomOnderdelen<Player> y = element.Parent.Parent.Right;
                    //Case 1
                    if (y != null && y.Red)
                    {
                        element.Parent.Black = true;
                        y.Black = true;
                        element.Parent.Parent.Red = true;
                        element = element.Parent.Parent;
                    }
                    //Case 2
                    else if (element.Parent.Right != null && element == element.Parent.Right)
                    {
                        element = element.Parent;
                        RoteerLinks(element);
                    }
                    //Case 3
                    element.Parent.Black = true;
                    element.Parent.Parent.Red = true;
                    RoteerRechts(element.Parent.Parent);
                }
                //else: same code with left and right switched
                else
                {
                    BoomOnderdelen<Player> x = element.Parent.Parent.Left;
                    //Case 4
                    if (x != null && x.Red)
                    {
                        element.Parent.Black = true;
                        x.Black = true;
                        element.Parent.Parent.Red = true;
                        element = element.Parent.Parent;
                    }
                    //Case 5
                    else if (element.Parent.Left != null && element == element.Parent.Left)
                    {
                        element = element.Parent;
                        RoteerLinks(element);
                    }
                    //Case 6
                    element.Parent.Black = true;
                    element.Parent.Parent.Red = true;
                    RoteerRechts(element.Parent.Parent);

                }
            }
        }

        void RoteerLinks(BoomOnderdelen<Player> element)
        {
             BoomOnderdelen<Player> Y = element.Right; // set Y
            element.Right = Y.Left;//turn Y's left subtree into X's right subtree
            if (Y.Left != null)
            {
                Y.Left.Parent = element;
            }
            if (Y != null)
            {
                Y.Parent = element.Parent;//link X's parent to Y
            }
            if (element.Parent == null)
            {
                root = Y;
            }
            if (element == element.Parent.Left)
            {
                element.Parent.Left = Y;
            }
            else
            {
                element.Parent.Right = Y;
            }
            Y.Left = element; //put element on Y's left
            if (element != null)
            {
                element.Parent = Y;
            }
        }
        void RoteerRechts(BoomOnderdelen<Player> element)
        {
            BoomOnderdelen<Player> Y = element.Left;
            element.Right = Y.Left;
            if (Y.Right != null)
            {
                Y.Right.Parent = element;
            }
            if (Y != null)
            {
                Y.Parent = element.Parent;
            }
            if (element.Parent == null)
            {
                root = Y;
            }
            if (element == element.Parent.Right)
            {
                element.Parent.Right = Y;
            }
            else
            {
                element.Parent.Left = Y;
            }
            Y.Right = element; 
            if (element != null)
            {
                element.Parent = Y;
            }
        }
    }
}
