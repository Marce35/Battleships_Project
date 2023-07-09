using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Battleships
{
    public partial class DeploymentMenu : Form
    {
        public static BasePlayer BasePlayer { get; set; }
        public int FleetSizeCount { get; set; }

        //reference to the game window
        public Form1 GameWindow { get; set; }
        
        //false = easyMode true = hardMode
        public static bool GameDificulty { get; set; }
        public DeploymentMenu()
        {
            InitializeComponent();
            CenterToScreen();
            MaximizeBox = false;
            PrepareGame();
            
        }

        public void PrepareGame()
        {
            //radio buttons customization
            rbtnEasyMode.Appearance = Appearance.Button;
            rbtnHardMode.Appearance = Appearance.Button;
            rbtnEasyMode.FlatStyle = FlatStyle.Flat;
            rbtnHardMode.FlatStyle = FlatStyle.Flat;
           
            rbtnHardMode.FlatAppearance.BorderColor = Color.Red;
            rbtnEasyMode.FlatAppearance.BorderColor = Color.Green;

            rbtnEasyMode.BackColor = Color.Transparent;
            rbtnHardMode.BackColor = Color.Transparent;


            GameDificulty = false;
            FleetSizeCount = 6;
            lblUndeployedShips.Text = FleetSizeCount.ToString();
            btnStartGame.Enabled = false;
            btnStartGame.ForeColor = Control.DefaultForeColor;
            btnStartGame.BackColor = Control.DefaultBackColor;

            BasePlayer = new BasePlayer();
            BasePlayer.FleetPositionsList.Clear();
            BasePlayer.TotalShips = FleetSizeCount;

            BasePlayer.FleetPositionsList = new List<Button> { a1,a2, a3, a4, a5, a6, b1, b2, b3, b4, b5, b6,
                                                     c1, c2, c3, c4, c5, c6, d1, d2, d3, d4, d5, d6,
                                                     e1, e2, e3, e4, e5, e6, f1, f2, f3, f4, f5, f6};

            for(int i=0; i<BasePlayer.FleetPositionsList.Count; i++)
            {
                BasePlayer.FleetPositionsList[i].Enabled = true;
                BasePlayer.FleetPositionsList[i].Tag = null;
                BasePlayer.FleetPositionsList[i].BackColor = Color.White;
                BasePlayer.FleetPositionsList[i].BackgroundImage = null;
            }
            



        }

        private void DeploymentMenu_Load1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SelectShipPositions_Click_Event(object sender, EventArgs e)
        {
            if(FleetSizeCount > 0)
            {

                Button SelectedPosition = (Button)sender;
                var index = BasePlayer.FleetPositionsList.FindIndex(z => z.Name == SelectedPosition.Name);
                if(index != -1)
                {
                    BasePlayer.FleetPositionsList[index].Tag = "PlayerShip";
                }
                SelectedPosition.Enabled = false;
                SelectedPosition.Tag = "PlayerShip";
                SelectedPosition.BackColor = Color.Navy;
                FleetSizeCount--;
            }

            if(FleetSizeCount == 0)
            {
                btnStartGame.Enabled = true;
                btnStartGame.ForeColor = Color.White;
                btnStartGame.BackColor = Color.Red;
                lblUndeployedShips.Text = "0";
            }
            else
            {
                lblUndeployedShips.Text = FleetSizeCount.ToString();
            }

            
        }

        private void tbPlayerName_Validating(object sender, CancelEventArgs e)
        {
            emptyFieldsError = new ErrorProvider();
            if (tbPlayerName.Text.Trim().Length == 0)
            {
                emptyFieldsError.SetError(tbPlayerName, "Enter a player name");
                
                e.Cancel = true;
            }
            else
            {
                if(tbPlayerName.Text.Trim().Length < 1 || tbPlayerName.Text.Trim().Length > 15)
                {
                    emptyFieldsError.SetError(tbPlayerName, "Name must be between 1 and 15 characters");
                    e.Cancel = true;
                }
                else
                {
                    emptyFieldsError.SetError(tbPlayerName, null);
                }
            }
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (ValidateChildren() && FleetSizeCount == 0)
            {
                BasePlayer.Name = tbPlayerName.Text;


                GameWindow = new Form1();

                GameWindow.Location = Location;
                //hide the main menu form
                this.Hide();


                GameWindow.FormClosed += GameWindow_FormClosed;
                GameWindow.Show();
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                
            }
        }

        public void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            PrepareGame();
            this.Show();
        }

        private void rbtnGameModeEasy_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (button.Checked)
            {
                string difficulty = button.Text;
                if(difficulty == "Easy Mode")
                {
                    
                    rbtnEasyMode.FlatAppearance.CheckedBackColor = Color.Black;
                    rbtnHardMode.FlatAppearance.CheckedBackColor = Color.Transparent;
                    GameDificulty = false;
                }
                else
                {
                    
                    rbtnHardMode.FlatAppearance.CheckedBackColor = Color.Black;
                    rbtnEasyMode.FlatAppearance.CheckedBackColor = Color.Transparent;
                    GameDificulty = true;
                }
            }
            
        }
    }
}
