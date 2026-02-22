
using Game.Models;
using Game.Models.Enterprises;
using Game.Models.Player;
using Game.Models.Sites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MachiCoroUI
{
    public partial class Form1
    {
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

            _canRollTwoDice = game.Players[me].HasEffect(TurnEffect.TwoDice);

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
                                             game.CurrentPlayer.HasEffect(TurnEffect.Reroll) &&
                                             !diceNotRolled &&
                                             !_rerollUsed;

            bool diceRolled = isRollPhase && !diceNotRolled;
            _gameView.ConfirmPhaseButton.Visible = isMyTurn && diceRolled;

            _gameView.HowDiceLabel.Visible = false;
            _gameView.Roll1Button.Visible = false;
            _gameView.Roll2Button.Visible = false;

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

            _gameView.PlayerNameLabel.Text = isMyTurn ? $"* {_username} *" : _username;
            _gameView.PlayerNameLabel.ForeColor = isMyTurn ? Color.Green : Color.Black;
            _gameView.PlayerMoneyLabel.Text = $"Монет: {game.Players[me].Money}";
            RenderCity(_gameView.PlayerEnterprisesPanel, game.Players[me].City);
            RenderSites(_gameView.PlayerSitesPanel, game.Players[me].Sites);

            bool isLeftTurn = currentId == left;
            _gameView.LeftNameLabel.Text = isLeftTurn ? $">> {game.Players[left].Name}" : game.Players[left].Name;
            _gameView.LeftNameLabel.ForeColor = isLeftTurn ? Color.OrangeRed : Color.Black;
            _gameView.LeftMoneyLabel.Text = $"Монет: {game.Players[left].Money}";
            RenderCity(_gameView.LeftEnterprisesPanel, game.Players[left].City);
            RenderSites(_gameView.LeftSitesPanel, game.Players[left].Sites);

            bool isRightTurn = currentId == right;
            _gameView.RightNameLabel.Text = isRightTurn ? $">> {game.Players[right].Name}" : game.Players[right].Name;
            _gameView.RightNameLabel.ForeColor = isRightTurn ? Color.OrangeRed : Color.Black;
            _gameView.RightMoneyLabel.Text = $"Монет: {game.Players[right].Money}";
            RenderCity(_gameView.RightEnterprisesPanel, game.Players[right].City);
            RenderSites(_gameView.RightSitesPanel, game.Players[right].Sites);

            bool isTopTurn = currentId == top;
            _gameView.TopNameLabel.Text = isTopTurn ? $">> {game.Players[top].Name}" : game.Players[top].Name;
            _gameView.TopNameLabel.ForeColor = isTopTurn ? Color.OrangeRed : Color.Black;
            _gameView.TopMoneyLabel.Text = $"Монет: {game.Players[top].Money}";
            RenderCity(_gameView.TopEnterprisesPanel, game.Players[top].City);
            RenderSites(_gameView.TopSitesPanel, game.Players[top].Sites);
        }

        private void RenderCity(FlowLayoutPanel panel, List<Enterprise> city)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

            const int perRow = 4;
            const int margin = 4;
            int panelW = panel.ClientSize.Width > 0 ? panel.ClientSize.Width : panel.Width;
            int panelH = panel.ClientSize.Height > 0 ? panel.ClientSize.Height : panel.Height;

            int cardW = Math.Max(40, (panelW - perRow * margin * 2) / perRow);
            int cardH = cardW * 130 / 90;

            if (panelH > margin * 2 && cardH > panelH - margin * 2)
            {
                cardH = panelH - margin * 2;
                cardW = cardH * 90 / 130;
            }

            foreach (var enterprise in city)
                panel.Controls.Add(CreateCard(MapToView(enterprise), cardW, cardH));

            panel.ResumeLayout();
        }

        private void RenderSites(FlowLayoutPanel panel, Dictionary<string, Site> da)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

            const int count = 4;
            const int margin = 4;
            int panelW = panel.ClientSize.Width > 0 ? panel.ClientSize.Width : panel.Width;
            int panelH = panel.ClientSize.Height > 0 ? panel.ClientSize.Height : panel.Height;

            int cardW = Math.Max(20, (panelW - count * margin * 2) / count);
            int cardH = cardW * 130 / 90;

            if (cardH > panelH - margin * 2)
            {
                cardH = Math.Max(20, panelH - margin * 2);
                cardW = cardH * 90 / 130;
            }

            foreach (var kvp in da)
            {
                var view = new EnterpriseView { Name = kvp.Key };
                var ctrl = CreateCard(view, cardW, cardH);

                if (ctrl is PictureBox pb)
                {
                    if (kvp.Value.IsActivated)
                    {
                        pb.BackColor = Color.Gold;
                        pb.Padding = new Padding(2);
                    }
                    else
                    {
                        pb.Image = CreateGrayscaleImage(pb.Image);
                        pb.BackColor = Color.Gray;
                    }
                }

                panel.Controls.Add(ctrl);
            }

            panel.ResumeLayout();
        }

        private Control CreateCard(EnterpriseView card, int w = 90, int h = 130)
        {
            var path = $"Assets/Sites/{card.ImageName}";
            if (!File.Exists(path))
                path = $"Assets/Enterprises/{card.ImageName}";

            if (File.Exists(path))
            {
                return new PictureBox
                {
                    Image = Image.FromFile(path),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = w,
                    Height = h,
                    Margin = new Padding(4),
                    Tag = card
                };
            }

            return new Label
            {
                Text = card.Name,
                Width = w,
                Height = h,
                Margin = new Padding(4),
                TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray,
                Font = new Font("Segoe UI", 7F),
                Tag = card
            };
        }

        private EnterpriseView MapToView(Enterprise e) => new EnterpriseView { Name = e.Name };

        void OnOpponentClicked(int playerId, GameState game)
        {
            _stealTargetPlayerId = playerId;
            _gameView.LastActionLabel.Text = $"Цель выбрана: {game.Players[playerId].Name}";
        }

        private Image CreateGrayscaleImage(Image original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(newBitmap))
            {
                var colorMatrix = new System.Drawing.Imaging.ColorMatrix(new float[][]
                {
                    new float[] {.3f,  .3f,  .3f,  0,    0},
                    new float[] {.59f, .59f, .59f, 0,    0},
                    new float[] {.11f, .11f, .11f, 0,    0},
                    new float[] {0,    0,    0,    0.5f, 0},
                    new float[] {0,    0,    0,    0,    1}
                });

                using var attributes = new System.Drawing.Imaging.ImageAttributes();
                attributes.SetColorMatrix(colorMatrix);
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            }
            return newBitmap;
        }
    }
}
