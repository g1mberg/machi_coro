using Game.Models;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private Dictionary<string, int> _cardCosts = new();

        int? _stealTargetPlayerId;
        int _stealAmount = 5;
        private int _lastDiceCount = 1;
        private bool _canRollTwoDice = false;
        private bool _rerollUsed = false;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            _allMarketCards = LoadMarket();
            LoadCardCosts();

            ConnectPanel.Resize += (_, _) => CenterConnectControls();
            LobbyPanel.Resize += (_, _) => CenterLobbyControls();
            CenterConnectControls();
            CenterLobbyControls();
        }

        private void LoadCardCosts()
        {
            if (File.Exists("enterprises.json"))
            {
                using var doc = JsonDocument.Parse(File.ReadAllText("enterprises.json"));
                foreach (var prop in doc.RootElement.EnumerateObject())
                {
                    var name = prop.Value.GetProperty("Name").GetString() ?? "";
                    var cost = prop.Value.GetProperty("Cost").GetInt32();
                    _cardCosts[name] = cost;
                }
            }
            if (File.Exists("sites.json"))
            {
                using var doc = JsonDocument.Parse(File.ReadAllText("sites.json"));
                foreach (var prop in doc.RootElement.EnumerateObject())
                {
                    var cost = prop.Value.GetProperty("Cost").GetInt32();
                    _cardCosts[prop.Name] = cost;
                }
            }
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
                System.Threading.Thread.Sleep(20);
            }
            this.Location = originalLocation;
        }

        private void label5_Click(object sender, EventArgs e) { }
        private void skip_Click_1(object sender, EventArgs e) { }
        private void stealButton_Click_1(object sender, EventArgs e) { }
    }
}
