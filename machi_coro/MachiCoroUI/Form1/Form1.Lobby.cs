using ProtocolFramework;
using ProtocolFramework.Serializator;
using Server.Lobby;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MachiCoroUI
{
    public partial class Form1
    {
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

        private void RenderLobby(LobbyState lobby)
        {
            if (lobby.Players == null) return;

            playerList.Items.Clear();
            foreach (var p in lobby.Players)
                playerList.Items.Add($"{p.Name} {(p.IsReady ? "✔" : "❌")}");
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
    }
}
