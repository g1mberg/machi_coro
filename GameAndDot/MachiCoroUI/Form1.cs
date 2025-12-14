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
        private Bitmap _canvas;
        private Graphics _graphics;
        private readonly Dictionary<int, Brush> PlayerBrushById = new();
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
                        string colorHex = packet.GetString(3);

                        PlayerNameById[id] = username;
                        PlayerBrushById[id] = new SolidBrush(ColorTranslator.FromHtml(colorHex));

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
    }
}
