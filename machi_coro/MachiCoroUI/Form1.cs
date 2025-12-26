using Game;
using Game.Models;
using Game.Models.Enterprises;
using Game.Models.Sites;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Server.Lobby;
using Shared.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Text.Json;
using System.Windows.Forms;

namespace MachiCoroUI
{
    public partial class Form1 : Form
    {
        private static int _handshakeMagic;
        private int _myPlayerId;

        private Dictionary<int, string> PlayerNameById = new Dictionary<int, string>();
        private List<EnterpriseView> _allMarketCards;

        string? _myBuildingToGive;
        int? _targetPlayerId;
        string? _targetBuilding;
        int? _stealTargetPlayerId;
        int _stealAmount = 1;
        private int _lastDiceCount = 1;



        XClient client = new XClient();

        private string _username = "";
        public Form1()
        {
            InitializeComponent();
            client.OnPacketRecieve += Client_OnPacketRecieve;
            XPacketTypeManager.RegisterType(XPacketType.Handshake, 1, 0);
            XPacketTypeManager.RegisterType(XPacketType.PlayerConnected, 1, 1);
            XPacketTypeManager.RegisterType(XPacketType.Welcome, 1, 2);
            XPacketTypeManager.RegisterType(XPacketType.PlayerJoined, 1, 3);
            XPacketTypeManager.RegisterType(XPacketType.PlayerReady, 1, 5);
            XPacketTypeManager.RegisterType(XPacketType.LobbyState, 1, 6);
            XPacketTypeManager.RegisterType(XPacketType.GameStart, 1, 7);
            XPacketTypeManager.RegisterType(XPacketType.YouArePlayer, 1, 8);
            ConnectPanel.Visible = true;
            GamePanel.Visible = false;
            LobbyPanel.Visible = false;



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
            Console.WriteLine("PACKET RECEIVED");
            Console.WriteLine(XPacketTypeManager.GetTypeFromPacket(packet).ToString());


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
                        var game = XPacketConverter.Deserialize<GameState>(packet);
                        BeginInvoke(() => RenderGame(game));
                        break;
                    }
                case XPacketType.YouArePlayer:
                    {
                        _myPlayerId = packet.GetValue<int>(1);
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


        void RenderGame(GameState game)
        {
            var me = _myPlayerId;

            var left = (me + 1) % 4;
            var top = (me + 2) % 4;
            var right = (me + 3) % 4;
            bool isMyTurn = game.CurrentPlayer.Name ==_username;
            bool isChangePhase = game.Phase == Phase.Change;

            changeButton.Visible = isMyTurn && isChangePhase;
            skipChangeButton.Visible = isMyTurn && isChangePhase;
            bool isStealPhase = game.Phase == Phase.Steal;

            stealButton.Visible = isMyTurn && isStealPhase;
            skip.Visible = isMyTurn && isStealPhase;

            rerollButton.Visible = game.Phase == Phase.Roll && isMyTurn && game.CurrentPlayer.IsReroll;



            labelCurrentPlayer.Text = game.CurrentPlayer.Name;
            labelDice.Text = game.DiceValue.Sum.ToString();
            labelPhase.Text = game.Phase.ToString();
            labelLastAction.Text = game.LastAction.ToString();
            PlayerName.Text = _username;
            playerMoney.Text = game.Players[me].Money.ToString();
            RenderCity(playerEnterprices, game.Players[me].City);
            RenderSites(playerSites, game.Players[me].Sites);
            leftOppName.Text = game.Players[left].Name;
            leftOppMoney.Text = game.Players[left].Money.ToString();
            RenderCity(leftOppEnterprices, game.Players[left].City);
            RenderSites(leftOppSites, game.Players[left].Sites);
            rightOppName.Text = game.Players[right].Name;
            rightOppMoney.Text = game.Players[right].Money.ToString();
            RenderCity(rightOppEnterprices, game.Players[right].City);
            RenderSites(rightOppSites, game.Players[right].Sites);
            topOppName.Text = game.Players[top].Name;
            topOppMoney.Text = game.Players[top].Money.ToString();
            RenderCity(topOppEnterprices, game.Players[top].City);
            RenderSites(topOppSites, game.Players[top].Sites);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            _allMarketCards = LoadMarket();
            RenderMarket(_allMarketCards);
        }

        private void RenderLobby(LobbyState lobby)
        {
            if (lobby.Players == null) return;

            playerList.Items.Clear();

            foreach (var p in lobby.Players)
                playerList.Items.Add($"{p.Name} {(p.IsReady ? "✔" : "❌")}");
        }
        private EnterpriseView MapLandmarkToView(Site l)
        {
            return new EnterpriseView
            {
                Name = l.Name,
            };
        }
        private void RenderSites(FlowLayoutPanel panel, Dictionary<string, Site> da)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

            foreach (var landmark in da.Values)
            {
                var view = MapLandmarkToView(landmark);
                var pb = CreateCard(view);

                // 🔒 нельзя кликать, если уже построено
                if (landmark.IsActivated)
                    pb.Enabled = false;

                panel.Controls.Add(pb);
            }

            panel.ResumeLayout();
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
            var json = File.ReadAllText("enterprises.json");

            return JsonSerializer.Deserialize<List<EnterpriseView>>(json)!;
        }

        void OnOpponentClicked(int playerId,GameState game)
        {
            _stealTargetPlayerId = playerId;
            labelLastAction.Text = $"Цель выбрана: {game.Players[playerId].Name}";
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
            flowMarket.SuspendLayout();
            flowMarket.Controls.Clear();

            foreach (var card in marketCards)
            {
                flowMarket.Controls.Add(CreateMarketCard(card));
            }

            flowMarket.ResumeLayout();
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


        PictureBox CreateCard(EnterpriseView card)
        {
            var path = $"Assets/Sites/{card.ImageName}";
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
            if (sender is not PictureBox pb)
                return;

            _selectedMarketCard = (EnterpriseView)pb.Tag;

            foreach (PictureBox c in flowMarket.Controls)
                c.BorderStyle = BorderStyle.None;

            pb.BorderStyle = BorderStyle.Fixed3D;
        }


        void HighlightSelected(PictureBox selected)
        {
            foreach (Control c in flowMarket.Controls)
            {
                if (c is PictureBox pb)
                    pb.BorderStyle = BorderStyle.None;
            }

            selected.BorderStyle = BorderStyle.Fixed3D;
        }

     




        private void ClearMarketSelection()
        {
            foreach (Control c in flowMarket.Controls)
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
            howdice.Visible = true;
            roll1.Visible = true;
            roll2.Visible = true;
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
            howdice.Visible = false;
            roll1.Visible = false;
            roll2.Visible = false;
        }

        private void roll2_Click(object sender, EventArgs e)
        {
            var dice = 2;
            var packet = XPacket.Create(XPacketType.Roll);
            packet.SetValue(1, _lastDiceCount);
            client.QueuePacketSend(packet.ToPacket());
            howdice.Visible = false;
            roll1.Visible = false;
            roll2.Visible = false;

        }

        private void buildButton_Click(object sender, EventArgs e)
        {
            if (_selectedMarketCard == null)
            {
                labelLastAction.Text = "Сначала выбери карту";
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
                labelLastAction.Text = "Выбери здания для обмена";
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



    }
}
