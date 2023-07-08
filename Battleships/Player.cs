using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleships
{
    public class Player
    {
        public string Name { get; set; }
        public List<Button> FieldsList { get; set; }
        public int Points { get; set; }
        public int Hits { get; set; }
        public int Misses { get; set; }
        public int NumberOfShips { get; set; }
        public int DeployedShips { get; set; }
        public int RemainingShips { get; set; }

        public Player(string name, int numberOfShips)
        {
            Name = name;
            Points = 0;
            Hits = 0;
            Misses = 0;
            FieldsList = new List<Button>();
            NumberOfShips = numberOfShips;
            DeployedShips = 0;
            RemainingShips = numberOfShips;
        }

        //public Player() 
        //{
        //    NumberOfShips = 6;

        //}

    }
}
