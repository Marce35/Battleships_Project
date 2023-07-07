using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleships
{
    public class BasePlayer
    {
        public string Name { get; set; }
        public int TotalShips { get; set; }
        public List<Button> FleetPositionsList { get; set; }

        public BasePlayer()
        {
            FleetPositionsList = new List<Button>();
        }
    }
}
