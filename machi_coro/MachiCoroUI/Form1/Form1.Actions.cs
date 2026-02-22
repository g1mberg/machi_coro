using ProtocolFramework;
using ProtocolFramework.Serializator;

namespace MachiCoroUI
{
    public partial class Form1
    {
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
                _lastDiceCount = 1;
                _rerollUsed = false;
                var packet = XPacket.Create(XPacketType.Roll);
                packet.SetValue(1, 1);
                client.QueuePacketSend(packet.ToPacket());
            }
        }

        private void roll1_Click(object sender, EventArgs e)
        {
            _lastDiceCount = 1;
            _rerollUsed = false;
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
            _rerollUsed = false;
            var packet = XPacket.Create(XPacketType.Roll);
            packet.SetValue(1, _lastDiceCount);
            client.QueuePacketSend(packet.ToPacket());
            _gameView.HowDiceLabel.Visible = false;
            _gameView.Roll1Button.Visible = false;
            _gameView.Roll2Button.Visible = false;
        }

        private void rerollButton_Click(object sender, EventArgs e)
        {
            _rerollUsed = true;
            _gameView.RerollButton.Visible = false;
            var packet = XPacket.Create(XPacketType.Reroll);
            packet.SetValue(1, _lastDiceCount);
            client.QueuePacketSend(packet.ToPacket());
        }

        private void confirmPhase_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Confirm);
            client.QueuePacketSend(packet.ToPacket());
        }

        private void skipBuild_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Build);
            packet.SetString(1, "");
            client.QueuePacketSend(packet.ToPacket());
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

        private void skip_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Steal);
            packet.SetValue(1, -1);
            packet.SetValue(2, 0);
            client.QueuePacketSend(packet.ToPacket());
        }

        private void skipChangeButton_Click(object sender, EventArgs e)
        {
            var packet = XPacket.Create(XPacketType.Change);
            packet.SetValue(1, false);
            client.QueuePacketSend(packet.ToPacket());
        }

        private void confirmChangeButton_Click(object sender, EventArgs e)
        {
            if (_lastGameState == null) return;
            ShowTradeSelectionDialog();
        }

        private void buildButton_Click(object sender, EventArgs e) => ShowBuildDialog();
    }
}
