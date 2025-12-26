using Game.Models;
using Game.Models.Enterprises;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Server.Lobby;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                            return;
                        }
                        break;
                    }


            }
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



        private List<EnterpriseView> LoadMarket()
        {
            var json = File.ReadAllText("enterprises.json");

            return JsonSerializer.Deserialize<List<EnterpriseView>>(json)!;
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


        //private void buyButton_Click(object sender, EventArgs e)
        //{
        //    if (_selectedMarketCard == null)
        //        return;

        //    _controller.ChooseBuild(new BuildChoice
        //    {
        //        Type = BuildChoiceType.Enterprise,
        //        Enterprise = _selectedMarketCard
        //    });
        //}

        void HighlightSelected(PictureBox selected)
        {
            foreach (Control c in flowMarket.Controls)
            {
                if (c is PictureBox pb)
                    pb.BorderStyle = BorderStyle.None;
            }

            selected.BorderStyle = BorderStyle.Fixed3D;
        }

        //private void buildButton_Click(object sender, EventArgs e)
        //{
        //    if (_selectedMarketCard == null)
        //    {
        //        labelLastAction.Text = "Сначала выберите карту";
        //        return;
        //    }


        //    var choice = new BuildChoice
        //    {
        //        Type = BuildChoiceType.Enterprise,
        //        EnterpriseName = _selectedMarketCard.Name
        //    };

        //    SendBuildChoice(choice);

        //    _selectedMarketCard = null;
        //    ClearMarketSelection();
        //}

        //private void SendBuildChoice(BuildChoice choice)
        //{
        //    var packet = XPacket.Create(XPacketType.BuyEnterprise);
        //    packet.SetString(1, "BuyEnterprise");
        //    packet.SetString(2, choice.EnterpriseName!);
        //    client.QueuePacketSend(packet.ToPacket());
        //}




        private void ClearMarketSelection()
        {
            foreach (Control c in flowMarket.Controls)
            {
                if (c is PictureBox pb)
                    pb.BorderStyle = BorderStyle.None;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
    }
}
