using ProtocolFramework;
using ProtocolFramework.Serializator;

namespace MachiCoroUI
{
    public partial class Form1
    {
        private void ShowGameOver(string winnerName, int winnerId)
        {
            bool isMe = winnerId == _myPlayerId;

            var dialog = new Form
            {
                Text = "Игра окончена!",
                Width = 420,
                Height = 280,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = isMe ? Color.Gold : Color.FromArgb(30, 30, 60)
            };

            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1,
                Padding = new Padding(20)
            };
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panel.BackColor = Color.Transparent;
            dialog.Controls.Add(panel);

            var trophy = new Label
            {
                Text = isMe ? "🏆" : "🥈",
                Font = new Font("Segoe UI Emoji", 48F),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };

            var mainLabel = new Label
            {
                Text = isMe ? "ВЫ ПОБЕДИЛИ!" : $"Победил {winnerName}",
                Font = new Font("Segoe UI", isMe ? 22F : 16F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = isMe ? Color.DarkRed : Color.White
            };

            var closeBtn = new Button { Text = "Закрыть", Width = 120, Height = 36, Anchor = AnchorStyles.None };
            closeBtn.Click += (_, _) => { dialog.Close(); Application.Exit(); };

            panel.Controls.Add(trophy, 0, 0);
            panel.Controls.Add(mainLabel, 0, 1);
            panel.Controls.Add(closeBtn, 0, 2);

            dialog.ShowDialog(this);
        }

        private void ShowBuildDialog()
        {
            if (_lastGameState == null) return;

            var me = _lastGameState.Players[_myPlayerId];
            var myMoney = me.Money;
            var purpleNames = new HashSet<string> { "Стадион", "Телецентр", "Бизнес-центр" };
            var myPurples = me.City
                .Where(e => purpleNames.Contains(e.Name))
                .Select(e => e.Name)
                .ToHashSet();

            var dialog = new Form
            {
                Text = $"Построить здание  (у вас {myMoney} монет)",
                Width = 1100,
                Height = 620,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                Padding = new Padding(8)
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            dialog.Controls.Add(mainLayout);

            var cardsFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                BackColor = Color.White
            };
            mainLayout.Controls.Add(cardsFlow, 0, 0);

            string? selectedName = null;
            Panel? selectedPanel = null;

            void SelectCard(Panel p, string name)
            {
                if (selectedPanel != null) selectedPanel.BackColor = Color.Transparent;
                selectedPanel = p;
                selectedName = name;
                p.BackColor = Color.Gold;
            }

            Panel MakeCardPanel(string name, bool disabled)
            {
                var view = new EnterpriseView { Name = name };
                var path = $"Assets/Sites/{view.ImageName}";
                if (!File.Exists(path)) path = $"Assets/Enterprises/{view.ImageName}";

                Image? img = File.Exists(path) ? Image.FromFile(path) : null;
                if (disabled && img != null)
                    img = CreateGrayscaleImage(img);

                var panel = new Panel
                {
                    Width = 155,
                    Height = 220,
                    Margin = new Padding(6),
                    Cursor = disabled ? Cursors.Default : Cursors.Hand,
                    BackColor = Color.Transparent
                };

                var pb = new PictureBox
                {
                    Image = img,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 155,
                    Height = 220,
                    Location = new Point(0, 0),
                    BackColor = Color.Transparent
                };
                panel.Controls.Add(pb);

                if (!disabled)
                {
                    EventHandler onClick = (_, _) => SelectCard(panel, name);
                    panel.Click += onClick;
                    pb.Click += onClick;
                }

                return panel;
            }

            foreach (var card in _allMarketCards)
            {
                var cost = _cardCosts.GetValueOrDefault(card.Name, 0);
                bool alreadyHavePurple = purpleNames.Contains(card.Name) && myPurples.Contains(card.Name);
                bool cantAfford = myMoney < cost;
                cardsFlow.Controls.Add(MakeCardPanel(card.Name, cantAfford || alreadyHavePurple));
            }

            foreach (var kvp in me.Sites)
            {
                var cost = _cardCosts.GetValueOrDefault(kvp.Key, 0);
                bool disabled = kvp.Value.IsActivated || myMoney < cost;
                cardsFlow.Controls.Add(MakeCardPanel(kvp.Key, disabled));
            }

            var btnFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(4)
            };
            var btnCancel = new Button { Text = "Отмена", Width = 110, Height = 36 };
            var btnBuild = new Button { Text = "Построить", Width = 120, Height = 36, BackColor = Color.LightGreen };

            btnCancel.Click += (_, _) => { dialog.DialogResult = DialogResult.Cancel; dialog.Close(); };
            btnBuild.Click += (_, _) =>
            {
                if (selectedName == null) return;
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            };

            btnFlow.Controls.Add(btnCancel);
            btnFlow.Controls.Add(btnBuild);
            mainLayout.Controls.Add(btnFlow, 0, 1);

            if (dialog.ShowDialog(this) == DialogResult.OK && selectedName != null)
            {
                var packet = XPacket.Create(XPacketType.Build);
                packet.SetString(1, selectedName);
                client.QueuePacketSend(packet.ToPacket());
            }
        }

        private void ShowTradeSelectionDialog()
        {
            var opponents = _lastGameState!.Players
                .Where(p => p.Id != _myPlayerId)
                .ToList();

            var dialog = new Form
            {
                Text = "Обмен зданиями",
                Width = 660,
                Height = 500,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(10)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            dialog.Controls.Add(layout);

            var combo = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F)
            };
            foreach (var p in opponents)
                combo.Items.Add(p.Name);
            combo.SelectedIndex = 0;
            layout.Controls.Add(combo, 0, 0);
            layout.SetColumnSpan(combo, 2);

            layout.Controls.Add(new Label { Text = "Ваши здания", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9, FontStyle.Bold) }, 0, 1);
            layout.Controls.Add(new Label { Text = "Здания игрока", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9, FontStyle.Bold) }, 1, 1);

            var myPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true };
            var theirPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true };
            layout.Controls.Add(myPanel, 0, 2);
            layout.Controls.Add(theirPanel, 1, 2);

            string? selectedMyCard = null;
            string? selectedTheirCard = null;
            var purpleNames = new HashSet<string> { "Стадион", "Телецентр", "Бизнес-центр" };

            Control MakeCardCtrl(string name)
            {
                var view = new EnterpriseView { Name = name };
                var path = $"Assets/Sites/{view.ImageName}";
                if (!File.Exists(path)) path = $"Assets/Enterprises/{view.ImageName}";
                if (File.Exists(path))
                    return new PictureBox { Image = Image.FromFile(path), SizeMode = PictureBoxSizeMode.Zoom, Width = 82, Height = 118, Margin = new Padding(4), Cursor = Cursors.Hand, Tag = name };
                return new Label { Text = name, Width = 82, Height = 118, Margin = new Padding(4), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.LightGray, Font = new Font("Segoe UI", 7F), Tag = name };
            }

            foreach (var e in _lastGameState!.Players[_myPlayerId].City.Where(e => !purpleNames.Contains(e.Name)))
            {
                var name = e.Name;
                var ctrl = MakeCardCtrl(name);
                ctrl.Click += (_, _) =>
                {
                    foreach (Control c in myPanel.Controls) c.BackColor = Color.Transparent;
                    ctrl.BackColor = Color.LightBlue;
                    selectedMyCard = name;
                };
                myPanel.Controls.Add(ctrl);
            }

            void PopulateTheir(int playerId)
            {
                theirPanel.SuspendLayout();
                theirPanel.Controls.Clear();
                selectedTheirCard = null;
                foreach (var e in _lastGameState!.Players[playerId].City.Where(e => !purpleNames.Contains(e.Name)))
                {
                    var name = e.Name;
                    var ctrl = MakeCardCtrl(name);
                    ctrl.Click += (_, _) =>
                    {
                        foreach (Control c in theirPanel.Controls) c.BackColor = Color.Transparent;
                        ctrl.BackColor = Color.LightBlue;
                        selectedTheirCard = name;
                    };
                    theirPanel.Controls.Add(ctrl);
                }
                theirPanel.ResumeLayout();
            }

            PopulateTheir(opponents[0].Id);
            combo.SelectedIndexChanged += (_, _) => PopulateTheir(opponents[combo.SelectedIndex].Id);

            var btnPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(5) };
            var proposeBtn = new Button { Text = "Предложить обмен", Width = 160, Height = 36 };
            var skipBtn = new Button { Text = "Пропустить", Width = 120, Height = 36 };
            btnPanel.Controls.Add(proposeBtn);
            btnPanel.Controls.Add(skipBtn);
            layout.Controls.Add(btnPanel, 0, 3);
            layout.SetColumnSpan(btnPanel, 2);

            proposeBtn.Click += (_, _) =>
            {
                if (selectedMyCard == null || selectedTheirCard == null) return;
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            };
            skipBtn.Click += (_, _) => { dialog.DialogResult = DialogResult.Cancel; dialog.Close(); };

            if (dialog.ShowDialog(this) == DialogResult.OK && selectedMyCard != null && selectedTheirCard != null)
            {
                int targetId = opponents[combo.SelectedIndex].Id;
                var pkt = XPacket.Create(XPacketType.Change);
                pkt.SetValue(1, true);
                pkt.SetString(2, selectedMyCard);
                pkt.SetValue(3, targetId);
                pkt.SetString(4, selectedTheirCard);
                client.QueuePacketSend(pkt.ToPacket());
            }
            else
            {
                var pkt = XPacket.Create(XPacketType.Change);
                pkt.SetValue(1, false);
                client.QueuePacketSend(pkt.ToPacket());
            }
        }

        private void ShowTradeProposalDialog(string mustGive, string willReceive)
        {
            var dialog = new Form
            {
                Text = "Вам предложен обмен",
                Width = 500,
                Height = 340,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(10)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            dialog.Controls.Add(layout);

            layout.Controls.Add(new Label { Text = "Вы отдаёте", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9, FontStyle.Bold) }, 0, 0);
            layout.Controls.Add(new Label { Text = "Вы получаете", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 9, FontStyle.Bold) }, 1, 0);

            Control MakeCardCtrl(string name)
            {
                var view = new EnterpriseView { Name = name };
                var path = $"Assets/Sites/{view.ImageName}";
                if (!File.Exists(path)) path = $"Assets/Enterprises/{view.ImageName}";
                if (File.Exists(path))
                    return new PictureBox { Image = Image.FromFile(path), SizeMode = PictureBoxSizeMode.Zoom, Width = 90, Height = 130, Margin = new Padding(4) };
                return new Label { Text = name, Width = 90, Height = 130, Margin = new Padding(4), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.LightGray, Font = new Font("Segoe UI", 7F) };
            }

            var givePanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown };
            givePanel.Controls.Add(MakeCardCtrl(mustGive));
            layout.Controls.Add(givePanel, 0, 1);

            var receivePanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown };
            receivePanel.Controls.Add(MakeCardCtrl(willReceive));
            layout.Controls.Add(receivePanel, 1, 1);

            var btnPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(5) };
            var acceptBtn = new Button { Text = "Принять", Width = 120, Height = 36, BackColor = Color.LightGreen };
            var declineBtn = new Button { Text = "Отклонить", Width = 120, Height = 36, BackColor = Color.LightCoral };
            btnPanel.Controls.Add(acceptBtn);
            btnPanel.Controls.Add(declineBtn);
            layout.Controls.Add(btnPanel, 0, 2);
            layout.SetColumnSpan(btnPanel, 2);

            acceptBtn.Click += (_, _) => { dialog.DialogResult = DialogResult.OK; dialog.Close(); };
            declineBtn.Click += (_, _) => { dialog.DialogResult = DialogResult.Cancel; dialog.Close(); };

            var accepted = dialog.ShowDialog(this) == DialogResult.OK;
            var pkt = XPacket.Create(XPacketType.TradeResponse);
            pkt.SetValue(1, accepted);
            client.QueuePacketSend(pkt.ToPacket());
        }
    }
}
