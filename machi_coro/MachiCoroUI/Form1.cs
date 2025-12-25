using Game.Models;
using ProtocolFramework;
using ProtocolFramework.Serializator;
using Shared.Models;
using System.Windows.Forms;

namespace MachiCoroUI
{
    public partial class Form1 : Form
    {
        private static int _handshakeMagic;
        private int _myPlayerId;
     


        private readonly Dictionary<int, string> PlayerNameById = new();

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
            XPacketTypeManager.RegisterType(XPacketType.PointPlaced, 1, 4);

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

        private void button2_Click(object sender, EventArgs e)
        {
            _username = textBox5.Text;
            var p = XPacket.Create(XPacketType.PlayerConnected);
            p.SetString(1, _username);
            client.QueuePacketSend(p.ToPacket());
            button2.Visible = false;
            textBox5.Visible = false;
            label5.Visible = true;
            label1.Visible = true;
        }

        private void Client_OnPacketRecieve(byte[] raw)
        {
            var packet = XPacket.Parse(raw);
            if (packet == null) return;

            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.PlayerJoined:
                    {
                        int id = packet.GetValue<int>(1);
                        string username = packet.GetString(2);
                        PlayerNameById[id] = username;
                        BeginInvoke(new Action(UpdatePlayersList));
                        break;
                    }

            }
        }

        private void UpdatePlayersList()
        {
            listBox1.Items.Clear();
            foreach (var name in PlayerNameById.Values)
                listBox1.Items.Add(name);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            RenderMarket(new List<EnterpriseView>
            {
                new EnterpriseView { Name="Bakery", ImageName="bakery.png" },
                 new EnterpriseView { Name="Cafe", ImageName="cafe.png" }
             });
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {


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
            var pb = new PictureBox
            {
                Image = Image.FromFile($"Assets/Enterprises/{card.ImageName}"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 90,
                Height = 130,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Tag = card // ← КЛЮЧЕВО
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

            HighlightSelected(pb);
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

        private void buildButton_Click(object sender, EventArgs e)
        {
            if (_selectedMarketCard == null)
            {
                labelLastAction.Text = "Сначала выберите карту";
                return;
            }

            // UI формирует намерение, НЕ покупает
            var choice = new BuildChoice
            {
                Type = BuildChoiceType.Enterprise,
                EnterpriseName = _selectedMarketCard.Name
            };

            SendBuildChoice(choice);

            _selectedMarketCard = null;
            ClearMarketSelection();
        }

        private void SendBuildChoice(BuildChoice choice)
        {
            // ВРЕМЕННО
            labelLastAction.Text = $"Отправлен запрос на покупку: {choice.EnterpriseName!}";
        }

        private void ClearMarketSelection()
        {
            foreach (Control c in flowMarket.Controls)
            {
                if (c is PictureBox pb)
                    pb.BorderStyle = BorderStyle.None;
            }
        }


    }
}
