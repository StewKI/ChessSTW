
using System.Configuration;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using ChessLibrary;
using NetworkLibrary;
using NetworkLibrary.Connections;

namespace ChessSTW_Desktop
{
    public partial class Form1 : Form
    {
        private Chess game;
        private Button[,] btns;
        private bool rotateTable;
        private bool done = false;

        public Form1()
        {
            InitializeComponent();
            btns = GetButtons();
            game = new Chess(PromotePiece);
            rotateTable = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string? rotate = ConfigurationManager.AppSettings.Get("Rotate");
            if (rotate is null)
            {
                rotate = "False";
                ConfigurationManager.AppSettings.Set("Rotate", "False");
            }
            rotateTable = bool.Parse(rotate);
            UpdateButtons(game);
            foreach (Control c in table.Controls)
            {
                c.MouseClick += new MouseEventHandler(ClickOnTableLayoutPanel!);
            }
        }

        public void ClickOnTableLayoutPanel(object sender, MouseEventArgs e)
        {

            int y = table.GetRow((Control)sender);
            int x = table.GetColumn((Control)sender);
            if (rotateTable)
            {
                y = 8 - y - 1;
                x = 8 - x - 1;
            }
            game.Click(y, x);
            UpdateButtons(game);
        }


        private Button[,] GetButtons()
        {
            Button[,] btns = new Button[8, 8];
            btns[0, 0] = button1;
            btns[0, 1] = button2;
            btns[0, 2] = button3;
            btns[0, 3] = button4;
            btns[0, 4] = button5;
            btns[0, 5] = button6;
            btns[0, 6] = button7;
            btns[0, 7] = button8;

            btns[1, 0] = button9;
            btns[1, 1] = button10;
            btns[1, 2] = button11;
            btns[1, 3] = button12;
            btns[1, 4] = button13;
            btns[1, 5] = button14;
            btns[1, 6] = button15;
            btns[1, 7] = button16;

            btns[2, 0] = button17;
            btns[2, 1] = button18;
            btns[2, 2] = button19;
            btns[2, 3] = button20;
            btns[2, 4] = button21;
            btns[2, 5] = button22;
            btns[2, 6] = button23;
            btns[2, 7] = button24;

            btns[3, 0] = button25;
            btns[3, 1] = button26;
            btns[3, 2] = button27;
            btns[3, 3] = button28;
            btns[3, 4] = button29;
            btns[3, 5] = button30;
            btns[3, 6] = button31;
            btns[3, 7] = button32;

            btns[4, 0] = button33;
            btns[4, 1] = button34;
            btns[4, 2] = button35;
            btns[4, 3] = button36;
            btns[4, 4] = button37;
            btns[4, 5] = button38;
            btns[4, 6] = button39;
            btns[4, 7] = button40;

            btns[5, 0] = button41;
            btns[5, 1] = button42;
            btns[5, 2] = button43;
            btns[5, 3] = button44;
            btns[5, 4] = button45;
            btns[5, 5] = button46;
            btns[5, 6] = button47;
            btns[5, 7] = button48;

            btns[6, 0] = button49;
            btns[6, 1] = button50;
            btns[6, 2] = button51;
            btns[6, 3] = button52;
            btns[6, 4] = button53;
            btns[6, 5] = button54;
            btns[6, 6] = button55;
            btns[6, 7] = button56;

            btns[7, 0] = button57;
            btns[7, 1] = button58;
            btns[7, 2] = button59;
            btns[7, 3] = button60;
            btns[7, 4] = button61;
            btns[7, 5] = button62;
            btns[7, 6] = button63;
            btns[7, 7] = button64;

            return btns;
        }


        private int PromotePiece()
        {
            int r = 0;
            using (PromoteDialogue pd = new PromoteDialogue())
            {
                pd.StartPosition = FormStartPosition.CenterParent;

                if (pd.ShowDialog() == DialogResult.OK)
                {
                    r = pd.ReturnValue;
                }
                else
                {
                    MessageBox.Show("Error while chosing! Queen chosed by default!", "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return r;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Close ChessSTW?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (game is ChessOnline)
                {
                    _ = ((ChessOnline)game).Disconnect();
                }
            }
        }

        private void UpdateButtons(Chess game)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int y = i, x = j;
                    if (rotateTable)
                    {
                        y = 8 - i - 1;
                        x = 8 - j - 1;
                    }

                    SetButtonText(btns[y, x], Chess.PieceToString(game.pieces[i, j]));

                    if (i == game.selectedPos.y && j == game.selectedPos.x)
                    {
                        SetButtonColor(btns[y, x], Color.LightSteelBlue);
                    }
                    else if (game.validFields[i, j])
                    {
                        if (game.pieces[i, j] == Piece.Empty)
                        {
                            SetButtonColor(btns[y, x], Color.LightBlue);
                        }
                        else
                        {
                            SetButtonColor(btns[y, x], Color.OrangeRed);
                        }
                    }
                    else
                    {
                        if ((i + j) % 2 == 1)
                        {
                            SetButtonColor(btns[y, x], Color.DarkGray);
                        }
                        else
                        {
                            SetButtonColor(btns[y, x], Color.White);
                        }
                    }
                }
            }
        }

        delegate void SetTextCallback(string text);

        private void SetInfoText(string text)
        {
            if (infoLabel.InvokeRequired)
            {
                SetTextCallback d = new(SetInfoText);
                Invoke(d, new object[] { text });
            }
            else
            {
                infoLabel.Text = text;
            }
        }

        delegate void SetButtonTextCallback(Button btn, string text);
        private void SetButtonText(Button btn, string text)
        {
            if (btn.InvokeRequired)
            {
                SetButtonTextCallback d = new(SetButtonText);
                Invoke(d, new object[] { btn, text });
            }
            else
            {
                btn.Text = text;
            }
        }

        delegate void SetButtonColorCallback(Button btn, Color color);
        private void SetButtonColor(Button btn, Color color)
        {
            if (btn.InvokeRequired)
            {
                SetButtonColorCallback d = new(SetButtonColor);
                Invoke(d, new object[] { btn, color });
            }
            else
            {
                btn.BackColor = color;
            }
        }

        delegate void SetTLPEnabledCallback(TableLayoutPanel tlp, bool enabled);
        private void SetTLPEnabled(TableLayoutPanel tlp, bool enabled)
        {
            if (tlp.InvokeRequired)
            {
                SetTLPEnabledCallback d = new(SetTLPEnabled);
                Invoke(d, new object[] { tlp, enabled });
            }
            else
            {
                tlp.Enabled = enabled;
            }
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Start new game?", "New Game", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (game is ChessOnline)
                {
                    _ = ((ChessOnline)game).Disconnect();
                }

                game = new Chess(PromotePiece);
                rotateTable = bool.Parse(ConfigurationManager.AppSettings.Get("Rotate")!);
                UpdateButtons(game);

                SetTLPEnabled(table, true);
                SetInfoText("Offline game");
            }
        }

        private async void onlineGameButton_Click(object sender, EventArgs e)
        {
            OnlineGameDialog dialog = new();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SetTLPEnabled(table, false);

                CColor? color = dialog.Color == 2 ? null : (CColor)(dialog.Color);
                ChessOnline onlineGame = new ChessOnline(PromotePiece, color, dialog.Username, dialog.OpponUsername);

                onlineGame.UpdateEvent += (object? o, Chess game) => UpdateButtons(game);
                onlineGame.EndGameEvent += (object? o, string reason) => End(reason);

                SetInfoText("Connecting...");
                string IP = ConfigurationManager.AppSettings.Get("IP")!;
                int Port = int.Parse(ConfigurationManager.AppSettings.Get("Port")!);


                IPAddress iPAddress = IPAddress.Parse(IP);
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, Port);

                Socket socket = new Socket(iPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                
                try
                {
                    await socket.ConnectAsync(iPEndPoint);
                    onlineGame.Connect(new SocketConnection(socket));
                    onlineGame.ServerResponseEvent += 
                        (object? o, string serverResponse) =>
                        {
                            HandleResponse(onlineGame, serverResponse);
                        };
                    SetInfoText("Connected. Waiting for response...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connecting was not succesfull because: {ex.Message}", "OOPS!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SetTLPEnabled(table, true);
                    SetInfoText("Offline game");
                }
            }
        }

        private void End(string reason)
        {
            MessageBox.Show($"Game ended because {reason}");

            game = new Chess(PromotePiece);
            rotateTable = bool.Parse(ConfigurationManager.AppSettings.Get("Rotate")!);
            UpdateButtons(game);

            SetInfoText("Offline game");
        }

        private void HandleResponse(ChessOnline onlineGame, string serverResponse)
        {
            if(!done)
            { 
                switch (serverResponse)
                {
                    case "welcome":

                        SetInfoText($"Opponent: {onlineGame.opponUsername}");
                        rotateTable = onlineGame.myColor == CColor.Black;

                        if (game is ChessOnline)
                        {
                            _ = ((ChessOnline)game).Disconnect();
                        }
                        game = onlineGame;
                        UpdateButtons(game);
                        done = true;

                        break;

                    case "waiting":

                        SetInfoText("Waiting for opponent...");

                        break;

                    case "username":

                        SetInfoText("Offline game");
                        MessageBox.Show("Username already taken!", "Username", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        done = true;

                        break;
                }
            }

            SetTLPEnabled(table, true);
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsDialog dialog = new SettingsDialog();
            dialog.ShowDialog();
            rotateTable = bool.Parse(ConfigurationManager.AppSettings.Get("Rotate")!);
            UpdateButtons(game);
        }

        private void infoLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
