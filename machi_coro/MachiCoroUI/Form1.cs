using Game;
using Game.Models;
using Game.Models.Dice;
using Game.Models.Enterprises;
using Game.Models.Player;
using Game.Models.Sites;
using Game.Utils;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Server.Lobby;
using Shared.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;

namespace MachiCoroUI
{
    public partial class Form1 : Form
    {
        private GameView _gameView;
        private static int _handshakeMagic;
        private int _myPlayerId;

        private Dictionary<int, string> PlayerNameById = new Dictionary<int, string>();
        private List<EnterpriseView> _allMarketCards;

        string? _myBuildingToGive;
        int? _targetPlayerId;
        string? _targetBuilding;
        int? _stealTargetPlayerId;
        int _stealAmount = 5;
        private int _lastDiceCount = 1;
        private bool _canRollTwoDice = false;
        private GameState? _lastGameState;



        XClient client = new XClient();

        private string _username = "";
        public Form1()
        {
            InitializeComponent();
            _gameView = new GameView();
            _gameView.Dock = DockStyle.Fill;
            GamePanel.Controls.Clear();
            GamePanel.Controls.Add(_gameView);

            // Wire up GameView button handlers
            _gameView.RollDiceButton.Click += rollDice_Click;
            _gameView.BuildButton.Click += buildButton_Click;
            _gameView.RerollButton.Click += rerollButton_Click;
            _gameView.Roll1Button.Click += roll1_Click;
            _gameView.Roll2Button.Click += roll2_Click;
            _gameView.ChangeButton.Click += confirmChangeButton_Click;
            _gameView.SkipChangeButton.Click += skipChangeButton_Click;
            _gameView.StealButton.Click += stealButton_Click;
            _gameView.SkipButton.Click += skip_Click;
            _gameView.ConfirmPhaseButton.Click += confirmPhase_Click;
            _gameView.SkipBuildButton.Click += skipBuild_Click;

            client.OnPacketRecieve += Client_OnPacketRecieve;
            ConnectPanel.Visible = true;
            GamePanel.Visible = false;
            LobbyPanel.Visible = false;

            ProtocolFramework.Utils.Utils.RegisterAllTypes();

            try
            {
                client.Connect("127.0.0.1", 4910);
                var rand = new Random();
                _handshakeMagic = rand.Next();

                client.QueuePacketSend(
                    XPacketConverter.Serialize(
                        XPacketType.Handshake,
                        new XPacketHandshake { MagicHandshakeNumber = _handshakeMagic }
                    ).ToPacket()
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Sign_in_button_Click(object sender, EventArgs e)
        {
            var nickname = NicknameBox.Text.Trim();
            if (string.IsNullOrEmpty(nickname))
                return;
            _username = nickname;
            var packet = XPacket.Create(XPacketType.PlayerConnected);
            packet.SetString(1, _username);
            client.QueuePacketSend(packet.ToPacket());
        }

        private void Client_OnPacketRecieve(byte[] raw)
        {
            var packet = XPacket.Parse(raw);
            if (packet == null) return;

            var type = XPacketTypeManager.GetTypeFromPacket(packet);
            Console.WriteLine("PACKET RECEIVED: " + type);


            switch (type)
            {
                case XPacketType.LobbyState:
                    {
                        var playersData = packet.GetValueRaw(0);

                        var lobby = DeserializeLobbyState(playersData);
                        Console.WriteLine($"Players count: {lobby.Players.Count}");
                        BeginInvoke(() => RenderLobby(lobby));
                        break;
                    }



                case XPacketType.Welcome:
                    {
                        BeginInvoke(() =>
                        {
                            ConnectPanel.Visible = false;
                            LobbyPanel.Visible = true;
                            LobbyPanel.BringToFront();
                        });

                        break;
                    }

                case XPacketType.GameStart:
                    {
                        BeginInvoke(() =>
                        {
                            ConnectPanel.Visible = false;
                            LobbyPanel.Visible = false;
                            GamePanel.Visible = true;
                            GamePanel.BringToFront();

                        });
                        break;
                    }


                case XPacketType.Handshake:
                    {
                        int serverMagic = packet.GetValue<int>(1);
                        if (serverMagic != _handshakeMagic - 15)
                        {
                            Console.WriteLine("Handshake failed");
                        }
                        break;
                    }

                case XPacketType.GameStateUpdate:
                    {
                        var game = DeserializeGameState(packet);
                        BeginInvoke(() => RenderGame(game));
                        break;
                    }
                case XPacketType.YouArePlayer:
                    {
                        _myPlayerId = packet.GetValue<int>(1);
                        break;
                    }

                case XPacketType.Error:
                    {
                        var msg = packet.GetString(1);
                        BeginInvoke(() => _gameView.LastActionLabel.Text = $"Ошибка: {msg}");
                        ShakeForm();
                        break;
                    }




            }
        }

        private void rerollButton_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Reroll);
            packet.SetValue(1, _lastDiceCount); // 1 или 2
            client.QueuePacketSend(packet.ToPacket());
        }
        private void ShakeForm()
        {
            var originalLocation = this.Location;
            var rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                this.Location = new Point(
                    originalLocation.X + rnd.Next(-5, 5),
                    originalLocation.Y + rnd.Next(-5, 5)
                );
                System.Threading.Thread.Sleep(20); // Маленькая пауза
            }
            this.Location = originalLocation; // Возвращаем на место
        }

        void RenderGame(GameState game)
        {
            _lastGameState = game;
            var me = _myPlayerId;
            var left = (me + 1) % 4;
            var top = (me + 2) % 4;
            var right = (me + 3) % 4;

            var currentId = game.CurrentPlayer.Id;
            bool isMyTurn = currentId == me;
            bool isChangePhase = game.Phase == Phase.Change;
            bool isStealPhase = game.Phase == Phase.Steal;

            // Track two-dice ability
            _canRollTwoDice = game.Players[me].IsTwoDices;

            // Show/hide action buttons based on phase
            bool isRollPhase = game.Phase == Phase.Roll;
            bool diceNotRolled = game.DiceValue.Sum == 0;
            _gameView.RollDiceButton.Visible = isMyTurn && isRollPhase && diceNotRolled;
            _gameView.ChangeButton.Visible = isMyTurn && isChangePhase;
            _gameView.SkipChangeButton.Visible = isMyTurn && isChangePhase;
            _gameView.StealButton.Visible = isMyTurn && isStealPhase;
            _gameView.SkipButton.Visible = isMyTurn && isStealPhase;
            _gameView.BuildButton.Visible = isMyTurn && game.Phase == Phase.Build;
            _gameView.SkipBuildButton.Visible = isMyTurn && game.Phase == Phase.Build;

            _gameView.RerollButton.Visible = game.Phase == Phase.Roll &&
                                             isMyTurn &&
                                             game.CurrentPlayer.IsReroll;

            // Show confirm button after dice have been rolled
            bool diceRolled = isRollPhase && !diceNotRolled;
            _gameView.ConfirmPhaseButton.Visible = isMyTurn && diceRolled;

            // Hide dice choice after roll
            _gameView.HowDiceLabel.Visible = false;
            _gameView.Roll1Button.Visible = false;
            _gameView.Roll2Button.Visible = false;

            // Status labels
            var turnText = isMyTurn ? ">>> ВАШ ХОД <<<" : $"Ход: {game.CurrentPlayer.Name}";
            _gameView.CurrentPlayerLabel.Text = turnText;
            _gameView.CurrentPlayerLabel.ForeColor = isMyTurn ? Color.DarkGreen : Color.Black;
            _gameView.CurrentPlayerLabel.BackColor = isMyTurn ? Color.LightGreen : Color.LightYellow;
            this.Text = "Machi Coro";

            _gameView.DiceLabel.Text = game.DiceValue.Sum == 0
                ? "Кубик: —"
                : game.DiceValue.Die2 == 0
                    ? $"Кубик: {game.DiceValue.Die1}"
                    : $"Кубики: {game.DiceValue.Die1} + {game.DiceValue.Die2} = {game.DiceValue.Sum}";
            _gameView.PhaseLabel.Text = $"Фаза: {game.Phase}";
            _gameView.LastActionLabel.Text = game.LastAction;

            // Player info
            _gameView.PlayerNameLabel.Text = isMyTurn ? $"* {_username} *" : _username;
            _gameView.PlayerNameLabel.ForeColor = isMyTurn ? Color.Green : Color.Black;
            _gameView.PlayerMoneyLabel.Text = $"Монет: {game.Players[me].Money}";
            RenderCity(_gameView.PlayerEnterprisesPanel, game.Players[me].City);
            RenderSites(_gameView.PlayerSitesPanel, game.Players[me].Sites);

            // Left opponent
            bool isLeftTurn = currentId == left;
            _gameView.LeftNameLabel.Text = isLeftTurn ? $">> {game.Players[left].Name}" : game.Players[left].Name;
            _gameView.LeftNameLabel.ForeColor = isLeftTurn ? Color.OrangeRed : Color.Black;
            _gameView.LeftMoneyLabel.Text = $"Монет: {game.Players[left].Money}";
            RenderCity(_gameView.LeftEnterprisesPanel, game.Players[left].City);
            RenderSites(_gameView.LeftSitesPanel, game.Players[left].Sites);

            // Right opponent
            bool isRightTurn = currentId == right;
            _gameView.RightNameLabel.Text = isRightTurn ? $">> {game.Players[right].Name}" : game.Players[right].Name;
            _gameView.RightNameLabel.ForeColor = isRightTurn ? Color.OrangeRed : Color.Black;
            _gameView.RightMoneyLabel.Text = $"Монет: {game.Players[right].Money}";
            RenderCity(_gameView.RightEnterprisesPanel, game.Players[right].City);
            RenderSites(_gameView.RightSitesPanel, game.Players[right].Sites);

            // Top opponent
            bool isTopTurn = currentId == top;
            _gameView.TopNameLabel.Text = isTopTurn ? $">> {game.Players[top].Name}" : game.Players[top].Name;
            _gameView.TopNameLabel.ForeColor = isTopTurn ? Color.OrangeRed : Color.Black;
            _gameView.TopMoneyLabel.Text = $"Монет: {game.Players[top].Money}";
            RenderCity(_gameView.TopEnterprisesPanel, game.Players[top].City);
            RenderSites(_gameView.TopSitesPanel, game.Players[top].Sites);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            _allMarketCards = LoadMarket();
            RenderMarket(_allMarketCards);

            ConnectPanel.Resize += (_, _) => CenterConnectControls();
            LobbyPanel.Resize += (_, _) => CenterLobbyControls();
            CenterConnectControls();
            CenterLobbyControls();
        }

        private void CenterConnectControls()
        {
            int cx = ConnectPanel.ClientSize.Width / 2;
            int cy = ConnectPanel.ClientSize.Height / 2;

            label4.Location = new Point(cx - label4.Width - 10, cy - 15);
            NicknameBox.Location = new Point(cx - label4.Width - 10 + label4.Width + 20, cy - 15);
            button2.Location = new Point(cx - button2.Width / 2, cy + 30);
        }

        private void CenterLobbyControls()
        {
            int cx = LobbyPanel.ClientSize.Width / 2;
            int cy = LobbyPanel.ClientSize.Height / 2;

            label1.Location = new Point(cx - label1.Width / 2, cy - 120);
            playerList.Location = new Point(cx - playerList.Width / 2, cy - 80);
            readyButton.Location = new Point(cx - readyButton.Width / 2, cy + 90);
            Confirm.Location = new Point(cx - Confirm.Width / 2, cy + 90);
        }

        private void RenderLobby(LobbyState lobby)
        {
            if (lobby.Players == null) return;

            playerList.Items.Clear();

            foreach (var p in lobby.Players)
                playerList.Items.Add($"{p.Name} {(p.IsReady ? "✔" : "❌")}");
        }
        private void RenderSites(FlowLayoutPanel panel, Dictionary<string, Site> da)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

            foreach (var kvp in da)
            {
                var view = new EnterpriseView { Name = kvp.Key };
                var pb = (PictureBox)CreateCard(view); // Приводим к PictureBox

                if (kvp.Value.IsActivated)
                {
                    // АКТИВИРОВАНА: делаем яркую рамку и обычный цвет
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.BackColor = Color.Gold;
                    pb.Padding = new Padding(3);
                }
                else
                {
                    // НЕ АКТИВИРОВАНА: делаем полупрозрачной/серой
                    pb.Image = CreateGrayscaleImage(pb.Image);
                    pb.Enabled = true; // Оставляем включенной, чтобы можно было кликнуть и купить
                    pb.BackColor = Color.Gray;
                }

                panel.Controls.Add(pb);
            }

            panel.ResumeLayout();
        }

        // Вспомогательный метод для создания "серой" картинки (проще всего)
        private Image CreateGrayscaleImage(Image original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                // Матрица для перевода в ч/б и прозрачность 50%
                System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
                    new float[][]
                    {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 0.5f, 0}, // 0.5f — это прозрачность
                new float[] {0, 0, 0, 0, 1}
                    });

                using (var attributes = new System.Drawing.Imaging.ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                        0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }

        private void stealButton_Click(object sender, EventArgs e)
        {
            if (_lastGameState == null) return;

            var dialog = new Form
            {
                Text = "Выбери цель для кражи",
                Width = 300,
                Height = 80,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true
            };
            dialog.Controls.Add(flow);

            foreach (var player in _lastGameState.Players)
            {
                if (player.Id == _myPlayerId) continue;

                var btn = new Button
                {
                    Text = $"{player.Name}  ({player.Money} монет)",
                    Width = 260,
                    Height = 36,
                    Tag = player.Id
                };
                btn.Click += (_, _) =>
                {
                    _stealTargetPlayerId = (int)btn.Tag;
                    dialog.DialogResult = DialogResult.OK;
                    dialog.Close();
                };
                flow.Controls.Add(btn);
                dialog.Height += 46;
            }

            if (dialog.ShowDialog(this) != DialogResult.OK || _stealTargetPlayerId == null)
                return;

            var packet = XPacket.Create(XPacketType.Steal);
            packet.SetValue(1, _stealTargetPlayerId.Value);
            packet.SetValue(2, _stealAmount);
            client.QueuePacketSend(packet.ToPacket());
            _stealTargetPlayerId = null;
        }

        private void skipBuild_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Build);
            packet.SetString(1, "");
            client.QueuePacketSend(packet.ToPacket());
        }

        private void confirmPhase_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Confirm);
            client.QueuePacketSend(packet.ToPacket());
        }

        private void skip_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Steal);
            packet.SetValue(1, -1); // нет цели
            packet.SetValue(2, 0);  // нет суммы
            client.QueuePacketSend(packet.ToPacket());
        }



        private List<EnterpriseView> LoadMarket()
        {
            var json = File.ReadAllText("market_cards.json");
            var allCards = JsonSerializer.Deserialize<List<EnterpriseView>>(json)!;


            var excludedNames = new HashSet<string>
            {
                "Пшеничное поле",
                "Пекарня",
               "Terminal",
               "Mall",
                "Park",
                "TvTower"
            };

            return allCards.Where(c => !excludedNames.Contains(c.Name)).ToList();
        }

        void OnOpponentClicked(int playerId, GameState game)
        {
            _stealTargetPlayerId = playerId;
            _gameView.LastActionLabel.Text = $"Цель выбрана: {game.Players[playerId].Name}";
        }

        private LobbyState DeserializeLobbyState(byte[] data)
        {
            using var ms = new MemoryStream(data);
            using var br = new BinaryReader(ms);

            var playerCount = br.ReadByte();  // количество игроков
            var players = new List<LobbyPlayerState>();

            for (int i = 0; i < playerCount; i++)
            {
                var nameLength = br.ReadByte();
                var nameBytes = br.ReadBytes(nameLength);
                var name = Encoding.UTF8.GetString(nameBytes);
                var isReady = br.ReadBoolean();

                players.Add(new LobbyPlayerState { Name = name, IsReady = isReady });
            }

            return new LobbyState { Players = players };
        }

        private void RenderMarket(List<EnterpriseView> marketCards)
        {
            var panel = _gameView.MarketPanel;
            panel.SuspendLayout();
            panel.Controls.Clear();

            foreach (var card in marketCards)
                panel.Controls.Add(CreateMarketCard(card));

            panel.ResumeLayout();
        }

        private EnterpriseView MapToView(Enterprise e)
        {
            return new EnterpriseView
            {
                Name = e.Name,
            };
        }

        private void RenderCity(FlowLayoutPanel panel, List<Enterprise> city)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

            foreach (var enterprise in city)
            {
                var view = MapToView(enterprise);
                panel.Controls.Add(CreateCard(view));
            }
            panel.ResumeLayout();
        }

        Control CreateCard(EnterpriseView card)
        {
            // Try Sites first, then Enterprises
            var path = $"Assets/Sites/{card.ImageName}";
            if (!File.Exists(path))
                path = $"Assets/Enterprises/{card.ImageName}";

            if (File.Exists(path))
            {
                var pb = new PictureBox
                {
                    Image = Image.FromFile(path),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 90,
                    Height = 130,
                    Margin = new Padding(5),
                    Cursor = Cursors.Hand,
                    Tag = card
                };
                pb.Click += MarketCard_Click;
                return pb;
            }

            // Fallback: text label when image not found
            var lbl = new Label
            {
                Text = card.Name,
                Width = 90,
                Height = 130,
                Margin = new Padding(5),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray,
                Font = new Font("Segoe UI", 7F),
                Tag = card
            };
            lbl.Click += MarketCard_Click;
            return lbl;
        }

        PictureBox CreateMarketCard(EnterpriseView card)
        {
            var path = $"Assets/Enterprises/{card.ImageName}";
            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            var pb = new PictureBox
            {
                Image = Image.FromFile(path),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 90,
                Height = 130,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Tag = card
            };

            pb.Click += MarketCard_Click;
            return pb;
        }


        private EnterpriseView? _selectedMarketCard;

        private void MarketCard_Click(object? sender, EventArgs e)
        {
            if (sender is not PictureBox pb) return;


            foreach (Control c in _gameView.MarketPanel.Controls)
            {
                c.Size = new Size(90, 130);
                c.BackColor = Color.Transparent;
            }


            _selectedMarketCard = (EnterpriseView)pb.Tag;
            pb.Size = new Size(100, 145);
            pb.BackColor = Color.Gold;
        }


        void HighlightSelected(PictureBox selected)
        {
            foreach (Control c in _gameView.MarketPanel.Controls)
            {
                if (c is PictureBox pb)
                    pb.BorderStyle = BorderStyle.None;
            }

            selected.BorderStyle = BorderStyle.Fixed3D;
        }

        private void ClearMarketSelection()
        {
            foreach (Control c in _gameView.MarketPanel.Controls)
            {
                if (c is PictureBox pb)
                    pb.BorderStyle = BorderStyle.None;
            }
        }

        private void readyButton_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.PlayerReady);
            client.QueuePacketSend(packet.ToPacket());

            readyButton.Enabled = false;
            readyButton.Visible = false;
            Confirm.Visible = true;
            Confirm.Enabled = true;
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.GameStart);
            client.QueuePacketSend(packet.ToPacket());
        }

        private void rollDice_Click(object sender, EventArgs e)
        {
            if (_canRollTwoDice)
            {
                _gameView.HowDiceLabel.Visible = true;
                _gameView.Roll1Button.Visible = true;
                _gameView.Roll2Button.Visible = true;
            }
            else
            {
                // Без Вокзала — сразу кидаем 1 кубик
                _lastDiceCount = 1;
                var packet = XPacket.Create(XPacketType.Roll);
                packet.SetValue(1, 1);
                client.QueuePacketSend(packet.ToPacket());
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {


        }

        private void roll1_Click(object sender, EventArgs e)
        {
            _lastDiceCount = 1;

            var packet = XPacket.Create(XPacketType.Roll);
            packet.SetValue(1, _lastDiceCount);
            client.QueuePacketSend(packet.ToPacket());
            _gameView.HowDiceLabel.Visible = false;
            _gameView.Roll1Button.Visible = false;
            _gameView.Roll2Button.Visible = false;
        }

        private void roll2_Click(object sender, EventArgs e)
        {
            _lastDiceCount = 2;
            var packet = XPacket.Create(XPacketType.Roll);
            packet.SetValue(1, _lastDiceCount);
            client.QueuePacketSend(packet.ToPacket());
            _gameView.HowDiceLabel.Visible = false;
            _gameView.Roll1Button.Visible = false;
            _gameView.Roll2Button.Visible = false;
        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            if (_selectedMarketCard == null)
            {
                _gameView.LastActionLabel.Text = "Сначала выбери карту";
                return;
            }

            var packet = XPacket.Create(XPacketType.Build);
            packet.SetString(1, _selectedMarketCard.Name);

            client.QueuePacketSend(packet.ToPacket());

            _selectedMarketCard = null;
            ClearMarketSelection();
        }

        private void skipChangeButton_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Change);
            packet.SetValue(1, false); // wants = false
            client.QueuePacketSend(packet.ToPacket());
        }

        private void confirmChangeButton_Click(object sender, EventArgs e)
        {
            if (_myBuildingToGive == null ||
                _targetPlayerId == null ||
                _targetBuilding == null)
            {
                _gameView.LastActionLabel.Text = "Выбери здания для обмена";
                return;
            }

            var packet = XPacket.Create(XPacketType.Change);
            packet.SetValue(1, true); // wants
            packet.SetString(2, _myBuildingToGive);
            packet.SetValue(3, _targetPlayerId.Value);
            packet.SetString(4, _targetBuilding);

            client.QueuePacketSend(packet.ToPacket());

            // очистка UI
            _myBuildingToGive = null;
            _targetPlayerId = null;
            _targetBuilding = null;
        }

        private GameState DeserializeGameState(XPacket packet)
        {
            var currentPlayerId = packet.GetValue<int>(1);
            var phase = (Phase)packet.GetValue<int>(2);

            var diceField = packet.GetField(3);
            var diceValue = (diceField?.Contents != null && diceField.FieldSize >= 5)
                ? DiceResult.DeserializeDice(diceField.Contents)
                : new DiceResult(0, false);

            var lastAction = packet.GetString(4);

            var players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                var money = packet.GetValue<int>((byte)(10 + i));
                var name = packet.GetString((byte)(20 + i));
                var cityStr = packet.GetString((byte)(30 + i));
                var sitesStr = packet.GetString((byte)(40 + i));

                var player = new Player(i, name);

                // Set money: player starts with 3, adjust to match server value
                player.AddMoney(money - Player.StartMoney);

                // Rebuild city from comma-separated enterprise names
                player.City.Clear();
                if (!string.IsNullOrEmpty(cityStr))
                {
                    foreach (var eName in cityStr.Split(','))
                    {
                        if (!string.IsNullOrEmpty(eName))
                            player.City.Add(new Enterprise { Name = eName });
                    }
                }

                // Rebuild sites activation state
                if (!string.IsNullOrEmpty(sitesStr))
                {
                    foreach (var pair in sitesStr.Split(','))
                    {
                        var parts = pair.Split(':');
                        if (parts.Length == 2 && parts[1] == "1")
                        {
                            if (player.Sites.ContainsKey(parts[0]))
                                player.Sites[parts[0]].Activate();
                        }
                    }
                }

                players[i] = player;
            }

            return new GameState
            {
                CurrentPlayer = players[currentPlayerId],
                Phase = phase,
                DiceValue = diceValue,
                LastAction = lastAction,
                Players = players
            };
        }

        private void skip_Click_1(object sender, EventArgs e)
        {

        }

        private void stealButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}
