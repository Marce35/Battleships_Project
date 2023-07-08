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
using System.Media;


namespace Battleships
{
    public partial class Form1 : Form
    {


        Random rand = new Random();
        public SoundPlayer SoundPlayer { get; set; }

        public Player Player { get; set; }
        public Player Opponent { get; set; }
        public bool ReadyToAttack { get; set; }

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            MaximizeBox = false;
            New_Game();
        }

        public void New_Game()
        {
            ReadyToAttack = true;
            Player = new Player(DeploymentMenu.BasePlayer.Name, 6);
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
                Opponent.FieldsList[i].Enabled = false;
                Opponent.FieldsList[i].BackgroundImage = null;
                Opponent.FieldsList[i].BackColor = Color.White;
            }

            btnStartBattle.Enabled = true;





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

            
            foreach(Control control in this.Controls)
            {
                if(control is Button button)
                {
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
            

            
        }

        private async void BattleTimerForOpponent_Tick(object sender, EventArgs e)
        {
            if(Player.RemainingShips > 0)
            {
                int attackIndex = rand.Next(Player.FieldsList.Count);

                if ((string)Player.FieldsList[attackIndex].Tag == "PlayerShip")
                {
                    UpdatePlayerButtons(Player.FieldsList[attackIndex], attackIndex);

                    Player.FieldsList[attackIndex].BackgroundImage = Properties.Resources.hit;

                    SoundPlayer = new SoundPlayer(Properties.Resources.hit0Sound);
                    SoundPlayer.Play();
                    
                    Player.FieldsList[attackIndex].BackColor = Color.Navy;
                    Player.FieldsList.RemoveAt(attackIndex);

                    Player.RemainingShips -= 1;

                    Opponent.Hits += 1;

                    
                    BattleTimerForOpponent.Stop();
                }
                else
                {
                    UpdatePlayerButtons(Player.FieldsList[attackIndex], attackIndex);

                    Player.FieldsList[attackIndex].BackgroundImage = Properties.Resources.splashImage;
                    SoundPlayer = new SoundPlayer(Properties.Resources.splash1Sound);
                    SoundPlayer.Play();


                    Player.FieldsList[attackIndex].BackColor = Color.Navy;
                    Player.FieldsList.RemoveAt(attackIndex);
                    
                    BattleTimerForOpponent.Stop();
                }
                await Task.Delay(1500);
                ReadyToAttack = true;

            }

            lblOpponentHits.Text = Opponent.Hits.ToString();

            if (Player.RemainingShips == 0)
            {
                MessageBox.Show("You have lost the game. Better luck next time", "Game Lost");
            }

            Invalidate();
        }

        

        private async void AttackButtons_Click(object sender, EventArgs e)
        {
            if (ReadyToAttack)
            {
                if (Opponent.RemainingShips > 0)
                {
                    Button attackFieldPositionBtn = (Button)sender;

                    int index = Opponent.FieldsList.FindIndex(z => z.Name == attackFieldPositionBtn.Name);

                    if (Opponent.FieldsList[index].Enabled)
                    {
                        if ((string)Opponent.FieldsList[index].Tag == "OpponentShip")
                        {
                            attackFieldPositionBtn.Tag = "OpponentShip";
                            attackFieldPositionBtn.Enabled = false;
                            attackFieldPositionBtn.BackgroundImage = Properties.Resources.hit;

                            SoundPlayer = new SoundPlayer(Properties.Resources.hit2Sound);
                            SoundPlayer.Play();
                            Opponent.RemainingShips -= 1;
                            Player.Hits += 1;
                        }
                        else
                        {
                            attackFieldPositionBtn.Tag = null;
                            attackFieldPositionBtn.Enabled = false;
                            attackFieldPositionBtn.BackgroundImage = Properties.Resources.splashImage;
                            SoundPlayer = new SoundPlayer(Properties.Resources.splash1Sound);
                            SoundPlayer.Play();
                        }
                    }
                    ReadyToAttack = false;
                    await Task.Delay(1500);

                    BattleTimerForOpponent.Start();
                }
            }
            
            
            

            lblPlayerHits.Text = Player.Hits.ToString();
            
            if(Opponent.RemainingShips == 0)
            {
                MessageBox.Show("Congratulations you have won the game!!!!", "Game Won");
            }
            Invalidate();
        }


        public void OpponentPositionPicker() 
        {
            List<int> availableShipLocations = Enumerable.Range(0, Opponent.FieldsList.Count).ToList();
            for(int i=0; i<Opponent.NumberOfShips; i++)
            {
                int shipLocationIndex = rand.Next(availableShipLocations.Count);
                int shipLocation = availableShipLocations[shipLocationIndex];

                Opponent.FieldsList[shipLocation].Tag = "OpponentShip";
                availableShipLocations.RemoveAt(shipLocationIndex);

                //Debug.WriteLine("Opponent Ship Position " + Opponent.FieldsList[shipLocation].Name);
            }
        }

        private void btnStartBattle_Click(object sender, EventArgs e)
        {
            btnStartBattle.Enabled = false;
            for (int i = 0; i < Opponent.FieldsList.Count; i++)
            {
                Opponent.FieldsList[i].Tag = null;
                Opponent.FieldsList[i].Enabled = true;
                Opponent.FieldsList[i].BackgroundImage = null;
                Opponent.FieldsList[i].BackColor = Color.White;
            }
            OpponentPositionPicker();
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UpdatePlayerButtons(Button sender, int indexToAttack)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button)
                {
                    if(button.Name.Equals(sender.Name))
                    {
                        if((string)button.Tag == "PlayerShip")
                        {
                            button.Enabled = false;
                            
                            button.BackgroundImage = Properties.Resources.hit;

                        }
                        else
                        {
                            button.Enabled = false;
                            
                            button.BackgroundImage = Properties.Resources.splashImage;
                        }
                    }
                    
                }
            }
            Invalidate();
        }




    }
}
