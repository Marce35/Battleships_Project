using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;

namespace Battleships
{
    public partial class Form1 : Form
    {


        Random rand = new Random();

        public Player Player { get; set; }
        public Player Opponent { get; set; }

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            MaximizeBox = false;
            New_Game();
        }

        public void New_Game()
        {
            Player = new Player("Marko", 6);
            Opponent = new Player("Computer", 6);
            Player.FieldsList = new List<Button>() { a1,a2, a3, a4, a5, a6, b1, b2, b3, b4, b5, b6,
                                                     c1, c2, c3, c4, c5, c6, d1, d2, d3, d4, d5, d6,
                                                     e1, e2, e3, e4, e5, e6, f1, f2, f3, f4, f5, f6};
            Opponent.FieldsList = new List<Button>() { u1, u2, u3, u4, u5, u6, v1, v2, v3, v4, v5, v6,
                                                       w1, w2, w3, w4, w5, w6, x1, x2, x3, x4, x5, x6,
                                                       y1, y2, y3, y4, y5, y6, z1, z2, z3, z4, z5, z6};

            
            for (int i=0; i< Opponent.FieldsList.Count; i++)
            {
                Opponent.FieldsList[i].Tag = null;
                Opponent.FieldsList[i].Enabled = true;
                Opponent.FieldsList[i].BackgroundImage = null;
                Opponent.FieldsList[i].BackColor = Color.White;
            }

            btnStartBattle.Enabled = false;





            Player.FieldsList = DeploymentMenu.BasePlayer.FleetPositionsList;

           
            for(int i=0; i< Player.FieldsList.Count; i++)
            {
                if ((string)Player.FieldsList[i].Tag == "PlayerShip")
                {
                    Player.FieldsList[i].Enabled = false;
                    Player.FieldsList[i].BackColor = Color.Navy;
                    

                }
                else
                {
                    Player.FieldsList[i].Tag = null;
                    Player.FieldsList[i].Enabled = false;
                    Player.FieldsList[i].BackgroundImage = null;
                    Player.FieldsList[i].BackColor = Color.White;
                }
                //Debug.WriteLine("Player name:" + Player.FieldsList[i].Name + " Player tag: " + (string)Player.FieldsList[i].Tag);
                //Debug.WriteLine(String.Format("Opponent Name: %s\t Tag: %s\t", (string)Opponent.FieldsList[i].Name, (string)Player.FieldsList[i].Tag));
            }

            int counter = 0;
            foreach(Control control in this.Controls)
            {
                if(control is Button button)
                {
                    if (counter >= Player.FieldsList.Count)
                    {
                        break;
                    }
                    for(int i=0; i < Player.FieldsList.Count; i++)
                    {
                        if (button.Name.ToLower() == Player.FieldsList[i].Name.ToLower())
                        {
                            if ((string)Player.FieldsList[i].Tag == "PlayerShip")
                            {
                                button.Enabled = false;
                                button.Tag = "PlayerShip";
                                button.BackColor = Color.Navy;
                            }
                            else
                            {
                                button.Enabled = false;
                                button.Tag = null;
                                button.BackColor = Color.White;
                            }

                            //Debug.WriteLine("Button: " + button.Name + "Player Name: " + Player.FieldsList[i].Name);
                        }
                    }

                }
            }

            Invalidate();
            OpponentPositionPicker();

            
        }

        private void BattleTimerForOpponent_Tick(object sender, EventArgs e)
        {

        }

        private void PlayerFieldSelectButtons_Click(object sender, EventArgs e)
        {
            if(Player.DeployedShips < Player.NumberOfShips)
            {
                Button pickedPosition = (Button)sender;

                pickedPosition.Enabled = false;
                pickedPosition.Tag = "playerShip";
                pickedPosition.BackColor = Color.Navy;
                Player.DeployedShips += 1;
            }
            if(Player.DeployedShips == Player.NumberOfShips)
            {
                btnStartBattle.Enabled = true;
                btnStartBattle.ForeColor = Color.White;
                btnStartBattle.BackColor = Color.Red;

                lblMainText.Text = "Press the \"Start Battle\" button";
            }
        }

        private void AttackButtons_Click(object sender, EventArgs e)
        {

        }


        public void OpponentPositionPicker() 
        {
            for(int i = 0; i <= Opponent.NumberOfShips; i++)
            {
                int shipLocation = rand.Next(Opponent.FieldsList.Count);

                if ((string)Opponent.FieldsList[i].Tag == null && Opponent.FieldsList[i].Enabled == true)
                {
                    Opponent.FieldsList[shipLocation].Tag = "OpponentShip";

                    Debug.WriteLine("Opponent Ship Position " + Opponent.FieldsList[shipLocation].Name);
                }
            }
        }

        private void btnStartBattle_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
