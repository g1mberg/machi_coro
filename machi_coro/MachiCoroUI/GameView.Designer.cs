namespace MachiCoroUI
{
    partial class GameView
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>

        #endregion
        private TableLayoutPanel mainTable;
        private TableLayoutPanel topTable;
        private TableLayoutPanel centerTopTable;
        private TableLayoutPanel bottomTable;

        private Panel leftOppPanel;
        private Panel topOppPanel;
        private Panel rightOppPanel;

        private Label leftOppName;
        private Label leftOppMoneyCaption;
        private Label leftOppMoney;
        private FlowLayoutPanel leftOppEnterprises;
        private FlowLayoutPanel leftOppSites;

        private Label topOppName;
        private Label topOppMoneyCaption;
        private Label topOppMoney;
        private FlowLayoutPanel topOppEnterprises;
        private FlowLayoutPanel topOppSites;

        private Label rightOppName;
        private Label rightOppMoneyCaption;
        private Label rightOppMoney;
        private FlowLayoutPanel rightOppEnterprises;
        private FlowLayoutPanel rightOppSites;

        private FlowLayoutPanel marketPanel;

        private Panel playerInfoPanel;
        private Label playerName;
        private Label playerMoneyCaption;
        private Label playerMoney;
        private Button rollDiceButton;
        private Button buildButton;
        private Button rerollButton;

        private TableLayoutPanel playerCardsTable;
        private FlowLayoutPanel playerEnterprises;
        private FlowLayoutPanel playerSites;

        private Panel statusPanel;
        private FlowLayoutPanel statusFlow;
        private Label labelCurrentPlayer;
        private Label labelPhase;
        private Label labelDice;
        private Label labelLastAction;
        private Label howdice;
        private Button roll1;
        private Button roll2;
        private Button changeButton;
        private Button skipChangeButton;
        private Button stealButton;
        private Button skipButton;

        // Helper panels for opponent layout (name+money strip)
        private Panel leftOppHeader;
        private Panel topOppHeader;
        private Panel rightOppHeader;

        private void InitializeComponent()
        {
            mainTable = new TableLayoutPanel();
            topTable = new TableLayoutPanel();
            leftOppPanel = new Panel();
            leftOppHeader = new Panel();
            leftOppSites = new FlowLayoutPanel();
            leftOppEnterprises = new FlowLayoutPanel();
            leftOppMoney = new Label();
            leftOppMoneyCaption = new Label();
            leftOppName = new Label();
            centerTopTable = new TableLayoutPanel();
            topOppPanel = new Panel();
            topOppHeader = new Panel();
            topOppSites = new FlowLayoutPanel();
            topOppEnterprises = new FlowLayoutPanel();
            topOppMoney = new Label();
            topOppMoneyCaption = new Label();
            topOppName = new Label();
            marketPanel = new FlowLayoutPanel();
            rightOppPanel = new Panel();
            rightOppHeader = new Panel();
            rightOppSites = new FlowLayoutPanel();
            rightOppEnterprises = new FlowLayoutPanel();
            rightOppMoney = new Label();
            rightOppMoneyCaption = new Label();
            rightOppName = new Label();
            bottomTable = new TableLayoutPanel();
            playerInfoPanel = new Panel();
            playerName = new Label();
            playerMoneyCaption = new Label();
            playerMoney = new Label();
            rollDiceButton = new Button();
            buildButton = new Button();
            rerollButton = new Button();
            playerCardsTable = new TableLayoutPanel();
            playerEnterprises = new FlowLayoutPanel();
            playerSites = new FlowLayoutPanel();
            statusPanel = new Panel();
            statusFlow = new FlowLayoutPanel();
            labelCurrentPlayer = new Label();
            labelPhase = new Label();
            labelDice = new Label();
            labelLastAction = new Label();
            howdice = new Label();
            roll1 = new Button();
            roll2 = new Button();
            changeButton = new Button();
            skipChangeButton = new Button();
            stealButton = new Button();
            skipButton = new Button();
            mainTable.SuspendLayout();
            topTable.SuspendLayout();
            leftOppPanel.SuspendLayout();
            leftOppHeader.SuspendLayout();
            centerTopTable.SuspendLayout();
            topOppPanel.SuspendLayout();
            topOppHeader.SuspendLayout();
            rightOppPanel.SuspendLayout();
            rightOppHeader.SuspendLayout();
            bottomTable.SuspendLayout();
            playerInfoPanel.SuspendLayout();
            playerCardsTable.SuspendLayout();
            statusPanel.SuspendLayout();
            statusFlow.SuspendLayout();
            SuspendLayout();
            //
            // mainTable — two rows: 60% top (opponents+market), 40% bottom (player)
            //
            mainTable.ColumnCount = 1;
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainTable.Controls.Add(topTable, 0, 0);
            mainTable.Controls.Add(bottomTable, 0, 1);
            mainTable.Dock = DockStyle.Fill;
            mainTable.Location = new Point(0, 0);
            mainTable.Margin = new Padding(0);
            mainTable.Name = "mainTable";
            mainTable.RowCount = 2;
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            mainTable.Size = new Size(1789, 864);
            mainTable.TabIndex = 0;
            //
            // topTable — 3 columns: left opp 25%, center 50%, right opp 25%
            //
            topTable.ColumnCount = 3;
            topTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            topTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            topTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            topTable.Controls.Add(leftOppPanel, 0, 0);
            topTable.Controls.Add(centerTopTable, 1, 0);
            topTable.Controls.Add(rightOppPanel, 2, 0);
            topTable.Dock = DockStyle.Fill;
            topTable.Margin = new Padding(0);
            topTable.Name = "topTable";
            topTable.RowCount = 1;
            topTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            topTable.Size = new Size(1783, 514);
            topTable.TabIndex = 0;
            //
            // ===== LEFT OPPONENT =====
            //
            // leftOppPanel — uses Dock layout: header Top, sites Bottom, enterprises Fill
            //
            leftOppPanel.BorderStyle = BorderStyle.FixedSingle;
            leftOppPanel.Controls.Add(leftOppEnterprises);
            leftOppPanel.Controls.Add(leftOppSites);
            leftOppPanel.Controls.Add(leftOppHeader);
            leftOppPanel.Dock = DockStyle.Fill;
            leftOppPanel.Name = "leftOppPanel";
            leftOppPanel.Padding = new Padding(4);
            leftOppPanel.Size = new Size(439, 508);
            leftOppPanel.TabIndex = 0;
            //
            // leftOppHeader — top strip with name + money
            //
            leftOppHeader.Controls.Add(leftOppName);
            leftOppHeader.Controls.Add(leftOppMoneyCaption);
            leftOppHeader.Controls.Add(leftOppMoney);
            leftOppHeader.Dock = DockStyle.Top;
            leftOppHeader.Height = 30;
            leftOppHeader.Name = "leftOppHeader";
            //
            // leftOppName
            //
            leftOppName.AutoSize = true;
            leftOppName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            leftOppName.Location = new Point(2, 4);
            leftOppName.Name = "leftOppName";
            leftOppName.Text = "Игрок L";
            leftOppName.TabIndex = 0;
            //
            // leftOppMoneyCaption
            //
            leftOppMoneyCaption.AutoSize = true;
            leftOppMoneyCaption.Location = new Point(120, 4);
            leftOppMoneyCaption.Name = "leftOppMoneyCaption";
            leftOppMoneyCaption.Text = "Монет:";
            leftOppMoneyCaption.TabIndex = 1;
            //
            // leftOppMoney
            //
            leftOppMoney.AutoSize = true;
            leftOppMoney.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            leftOppMoney.Location = new Point(175, 4);
            leftOppMoney.Name = "leftOppMoney";
            leftOppMoney.Text = "0";
            leftOppMoney.TabIndex = 2;
            //
            // leftOppEnterprises — fills remaining space
            //
            leftOppEnterprises.AutoScroll = true;
            leftOppEnterprises.Dock = DockStyle.Fill;
            leftOppEnterprises.Name = "leftOppEnterprises";
            leftOppEnterprises.TabIndex = 3;
            //
            // leftOppSites — bottom strip
            //
            leftOppSites.AutoScroll = true;
            leftOppSites.Dock = DockStyle.Bottom;
            leftOppSites.Height = 90;
            leftOppSites.Name = "leftOppSites";
            leftOppSites.TabIndex = 4;
            //
            // ===== CENTER (top opponent + market) =====
            //
            // centerTopTable
            //
            centerTopTable.ColumnCount = 1;
            centerTopTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            centerTopTable.Controls.Add(topOppPanel, 0, 0);
            centerTopTable.Controls.Add(marketPanel, 0, 1);
            centerTopTable.Dock = DockStyle.Fill;
            centerTopTable.Margin = new Padding(0);
            centerTopTable.Name = "centerTopTable";
            centerTopTable.RowCount = 2;
            centerTopTable.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            centerTopTable.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            centerTopTable.Size = new Size(885, 508);
            centerTopTable.TabIndex = 1;
            //
            // topOppPanel — uses Dock layout: header Top, sites Bottom, enterprises Fill
            //
            topOppPanel.BorderStyle = BorderStyle.FixedSingle;
            topOppPanel.Controls.Add(topOppEnterprises);
            topOppPanel.Controls.Add(topOppSites);
            topOppPanel.Controls.Add(topOppHeader);
            topOppPanel.Dock = DockStyle.Fill;
            topOppPanel.Name = "topOppPanel";
            topOppPanel.Padding = new Padding(4);
            topOppPanel.Size = new Size(879, 146);
            topOppPanel.TabIndex = 0;
            //
            // topOppHeader — top strip with name + money
            //
            topOppHeader.Controls.Add(topOppName);
            topOppHeader.Controls.Add(topOppMoneyCaption);
            topOppHeader.Controls.Add(topOppMoney);
            topOppHeader.Dock = DockStyle.Top;
            topOppHeader.Height = 30;
            topOppHeader.Name = "topOppHeader";
            //
            // topOppName
            //
            topOppName.AutoSize = true;
            topOppName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            topOppName.Location = new Point(2, 4);
            topOppName.Name = "topOppName";
            topOppName.Text = "Игрок T";
            topOppName.TabIndex = 0;
            //
            // topOppMoneyCaption
            //
            topOppMoneyCaption.AutoSize = true;
            topOppMoneyCaption.Location = new Point(120, 4);
            topOppMoneyCaption.Name = "topOppMoneyCaption";
            topOppMoneyCaption.Text = "Монет:";
            topOppMoneyCaption.TabIndex = 1;
            //
            // topOppMoney
            //
            topOppMoney.AutoSize = true;
            topOppMoney.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            topOppMoney.Location = new Point(175, 4);
            topOppMoney.Name = "topOppMoney";
            topOppMoney.Text = "0";
            topOppMoney.TabIndex = 2;
            //
            // topOppEnterprises — fills remaining space
            //
            topOppEnterprises.AutoScroll = true;
            topOppEnterprises.Dock = DockStyle.Fill;
            topOppEnterprises.Name = "topOppEnterprises";
            topOppEnterprises.TabIndex = 3;
            //
            // topOppSites — bottom strip
            //
            topOppSites.AutoScroll = true;
            topOppSites.Dock = DockStyle.Bottom;
            topOppSites.Height = 50;
            topOppSites.Name = "topOppSites";
            topOppSites.TabIndex = 4;
            //
            // marketPanel
            //
            marketPanel.AutoScroll = true;
            marketPanel.BorderStyle = BorderStyle.FixedSingle;
            marketPanel.Dock = DockStyle.Fill;
            marketPanel.Name = "marketPanel";
            marketPanel.Size = new Size(879, 356);
            marketPanel.TabIndex = 1;
            marketPanel.WrapContents = false;
            //
            // ===== RIGHT OPPONENT =====
            //
            // rightOppPanel — uses Dock layout: header Top, sites Bottom, enterprises Fill
            //
            rightOppPanel.BorderStyle = BorderStyle.FixedSingle;
            rightOppPanel.Controls.Add(rightOppEnterprises);
            rightOppPanel.Controls.Add(rightOppSites);
            rightOppPanel.Controls.Add(rightOppHeader);
            rightOppPanel.Dock = DockStyle.Fill;
            rightOppPanel.Name = "rightOppPanel";
            rightOppPanel.Padding = new Padding(4);
            rightOppPanel.Size = new Size(441, 508);
            rightOppPanel.TabIndex = 2;
            //
            // rightOppHeader — top strip with name + money
            //
            rightOppHeader.Controls.Add(rightOppName);
            rightOppHeader.Controls.Add(rightOppMoneyCaption);
            rightOppHeader.Controls.Add(rightOppMoney);
            rightOppHeader.Dock = DockStyle.Top;
            rightOppHeader.Height = 30;
            rightOppHeader.Name = "rightOppHeader";
            //
            // rightOppName
            //
            rightOppName.AutoSize = true;
            rightOppName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightOppName.Location = new Point(2, 4);
            rightOppName.Name = "rightOppName";
            rightOppName.Text = "Игрок R";
            rightOppName.TabIndex = 0;
            //
            // rightOppMoneyCaption
            //
            rightOppMoneyCaption.AutoSize = true;
            rightOppMoneyCaption.Location = new Point(120, 4);
            rightOppMoneyCaption.Name = "rightOppMoneyCaption";
            rightOppMoneyCaption.Text = "Монет:";
            rightOppMoneyCaption.TabIndex = 1;
            //
            // rightOppMoney
            //
            rightOppMoney.AutoSize = true;
            rightOppMoney.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightOppMoney.Location = new Point(175, 4);
            rightOppMoney.Name = "rightOppMoney";
            rightOppMoney.Text = "0";
            rightOppMoney.TabIndex = 2;
            //
            // rightOppEnterprises — fills remaining space
            //
            rightOppEnterprises.AutoScroll = true;
            rightOppEnterprises.Dock = DockStyle.Fill;
            rightOppEnterprises.Name = "rightOppEnterprises";
            rightOppEnterprises.TabIndex = 3;
            //
            // rightOppSites — bottom strip
            //
            rightOppSites.AutoScroll = true;
            rightOppSites.Dock = DockStyle.Bottom;
            rightOppSites.Height = 90;
            rightOppSites.Name = "rightOppSites";
            rightOppSites.TabIndex = 4;
            //
            // ===== BOTTOM SECTION (player) =====
            //
            // bottomTable — 3 columns: player info 20%, cards 50%, status 30%
            //
            bottomTable.ColumnCount = 3;
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            bottomTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            bottomTable.Controls.Add(playerInfoPanel, 0, 0);
            bottomTable.Controls.Add(playerCardsTable, 1, 0);
            bottomTable.Controls.Add(statusPanel, 2, 0);
            bottomTable.Dock = DockStyle.Fill;
            bottomTable.Margin = new Padding(0);
            bottomTable.Name = "bottomTable";
            bottomTable.RowCount = 1;
            bottomTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bottomTable.Size = new Size(1783, 344);
            bottomTable.TabIndex = 1;
            //
            // playerInfoPanel
            //
            playerInfoPanel.BorderStyle = BorderStyle.FixedSingle;
            playerInfoPanel.Controls.Add(playerName);
            playerInfoPanel.Controls.Add(playerMoneyCaption);
            playerInfoPanel.Controls.Add(playerMoney);
            playerInfoPanel.Controls.Add(rollDiceButton);
            playerInfoPanel.Controls.Add(buildButton);
            playerInfoPanel.Controls.Add(rerollButton);
            playerInfoPanel.Dock = DockStyle.Fill;
            playerInfoPanel.Name = "playerInfoPanel";
            playerInfoPanel.Padding = new Padding(8);
            playerInfoPanel.Size = new Size(350, 338);
            playerInfoPanel.TabIndex = 0;
            //
            // playerName
            //
            playerName.AutoSize = true;
            playerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            playerName.Location = new Point(8, 8);
            playerName.Name = "playerName";
            playerName.Text = "Вы";
            playerName.TabIndex = 0;
            //
            // playerMoneyCaption
            //
            playerMoneyCaption.AutoSize = true;
            playerMoneyCaption.Location = new Point(8, 38);
            playerMoneyCaption.Name = "playerMoneyCaption";
            playerMoneyCaption.Text = "Монет:";
            playerMoneyCaption.TabIndex = 1;
            //
            // playerMoney
            //
            playerMoney.AutoSize = true;
            playerMoney.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            playerMoney.Location = new Point(75, 38);
            playerMoney.Name = "playerMoney";
            playerMoney.Text = "0";
            playerMoney.TabIndex = 2;
            //
            // rollDiceButton
            //
            rollDiceButton.Location = new Point(8, 70);
            rollDiceButton.Name = "rollDiceButton";
            rollDiceButton.Size = new Size(140, 32);
            rollDiceButton.TabIndex = 3;
            rollDiceButton.Text = "Бросить";
            //
            // buildButton
            //
            buildButton.Location = new Point(8, 108);
            buildButton.Name = "buildButton";
            buildButton.Size = new Size(140, 32);
            buildButton.TabIndex = 4;
            buildButton.Text = "Построить";
            //
            // rerollButton
            //
            rerollButton.Location = new Point(8, 146);
            rerollButton.Name = "rerollButton";
            rerollButton.Size = new Size(140, 32);
            rerollButton.TabIndex = 5;
            rerollButton.Text = "Перебросить";
            rerollButton.Visible = false;
            //
            // playerCardsTable
            //
            playerCardsTable.ColumnCount = 1;
            playerCardsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            playerCardsTable.Controls.Add(playerEnterprises, 0, 0);
            playerCardsTable.Controls.Add(playerSites, 0, 1);
            playerCardsTable.Dock = DockStyle.Fill;
            playerCardsTable.Name = "playerCardsTable";
            playerCardsTable.RowCount = 2;
            playerCardsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            playerCardsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            playerCardsTable.Size = new Size(885, 338);
            playerCardsTable.TabIndex = 1;
            //
            // playerEnterprises
            //
            playerEnterprises.AutoScroll = true;
            playerEnterprises.BorderStyle = BorderStyle.FixedSingle;
            playerEnterprises.Dock = DockStyle.Fill;
            playerEnterprises.Name = "playerEnterprises";
            playerEnterprises.Size = new Size(879, 163);
            playerEnterprises.TabIndex = 0;
            //
            // playerSites
            //
            playerSites.AutoScroll = true;
            playerSites.BorderStyle = BorderStyle.FixedSingle;
            playerSites.Dock = DockStyle.Fill;
            playerSites.Name = "playerSites";
            playerSites.Size = new Size(879, 163);
            playerSites.TabIndex = 1;
            //
            // ===== STATUS PANEL =====
            //
            // statusPanel
            //
            statusPanel.BorderStyle = BorderStyle.FixedSingle;
            statusPanel.Controls.Add(statusFlow);
            statusPanel.Dock = DockStyle.Fill;
            statusPanel.Name = "statusPanel";
            statusPanel.Padding = new Padding(5);
            statusPanel.Size = new Size(530, 338);
            statusPanel.TabIndex = 2;
            //
            // statusFlow — vertical top-down flow
            //
            statusFlow.AutoScroll = true;
            statusFlow.Controls.Add(labelCurrentPlayer);
            statusFlow.Controls.Add(labelPhase);
            statusFlow.Controls.Add(labelDice);
            statusFlow.Controls.Add(labelLastAction);
            statusFlow.Controls.Add(howdice);
            statusFlow.Controls.Add(roll1);
            statusFlow.Controls.Add(roll2);
            statusFlow.Controls.Add(changeButton);
            statusFlow.Controls.Add(skipChangeButton);
            statusFlow.Controls.Add(stealButton);
            statusFlow.Controls.Add(skipButton);
            statusFlow.Dock = DockStyle.Fill;
            statusFlow.FlowDirection = FlowDirection.TopDown;
            statusFlow.WrapContents = false;
            statusFlow.Name = "statusFlow";
            statusFlow.Size = new Size(518, 326);
            statusFlow.TabIndex = 0;
            //
            // labelCurrentPlayer
            //
            labelCurrentPlayer.AutoSize = false;
            labelCurrentPlayer.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelCurrentPlayer.Size = new Size(500, 36);
            labelCurrentPlayer.BackColor = Color.LightYellow;
            labelCurrentPlayer.Margin = new Padding(3, 4, 3, 4);
            labelCurrentPlayer.Padding = new Padding(4, 4, 0, 0);
            labelCurrentPlayer.Name = "labelCurrentPlayer";
            labelCurrentPlayer.Text = "Ход: —";
            labelCurrentPlayer.TabIndex = 0;
            //
            // labelPhase
            //
            labelPhase.AutoSize = false;
            labelPhase.Font = new Font("Segoe UI", 10F);
            labelPhase.Size = new Size(500, 28);
            labelPhase.Margin = new Padding(3, 2, 3, 2);
            labelPhase.Name = "labelPhase";
            labelPhase.Text = "Фаза: —";
            labelPhase.TabIndex = 1;
            //
            // labelDice
            //
            labelDice.AutoSize = false;
            labelDice.Font = new Font("Segoe UI", 10F);
            labelDice.Size = new Size(500, 28);
            labelDice.Margin = new Padding(3, 2, 3, 2);
            labelDice.Name = "labelDice";
            labelDice.Text = "Кубик: —";
            labelDice.TabIndex = 2;
            //
            // labelLastAction
            //
            labelLastAction.AutoSize = false;
            labelLastAction.Font = new Font("Segoe UI", 10F);
            labelLastAction.Size = new Size(500, 28);
            labelLastAction.Margin = new Padding(3, 2, 3, 6);
            labelLastAction.Name = "labelLastAction";
            labelLastAction.Text = "Последнее: —";
            labelLastAction.TabIndex = 3;
            //
            // howdice
            //
            howdice.AutoSize = true;
            howdice.Margin = new Padding(3, 8, 3, 2);
            howdice.Name = "howdice";
            howdice.Text = "Сколько кубиков кинуть?";
            howdice.Visible = false;
            howdice.TabIndex = 4;
            //
            // roll1
            //
            roll1.Margin = new Padding(3, 2, 3, 2);
            roll1.Name = "roll1";
            roll1.Size = new Size(100, 28);
            roll1.TabIndex = 5;
            roll1.Text = "1 кубик";
            roll1.Visible = false;
            //
            // roll2
            //
            roll2.Margin = new Padding(3, 2, 3, 2);
            roll2.Name = "roll2";
            roll2.Size = new Size(100, 28);
            roll2.TabIndex = 6;
            roll2.Text = "2 кубика";
            roll2.Visible = false;
            //
            // changeButton
            //
            changeButton.Margin = new Padding(3, 8, 3, 2);
            changeButton.Name = "changeButton";
            changeButton.Size = new Size(140, 28);
            changeButton.TabIndex = 7;
            changeButton.Text = "Обмен";
            changeButton.Visible = false;
            //
            // skipChangeButton
            //
            skipChangeButton.Margin = new Padding(3, 2, 3, 2);
            skipChangeButton.Name = "skipChangeButton";
            skipChangeButton.Size = new Size(180, 28);
            skipChangeButton.TabIndex = 8;
            skipChangeButton.Text = "Пропустить обмен";
            skipChangeButton.Visible = false;
            //
            // stealButton
            //
            stealButton.Margin = new Padding(3, 2, 3, 2);
            stealButton.Name = "stealButton";
            stealButton.Size = new Size(140, 28);
            stealButton.TabIndex = 9;
            stealButton.Text = "Украсть";
            stealButton.Visible = false;
            //
            // skipButton
            //
            skipButton.Margin = new Padding(3, 2, 3, 2);
            skipButton.Name = "skipButton";
            skipButton.Size = new Size(140, 28);
            skipButton.TabIndex = 10;
            skipButton.Text = "Пропустить";
            skipButton.Visible = false;
            //
            // GameView
            //
            AutoScaleMode = AutoScaleMode.None;
            Controls.Add(mainTable);
            Name = "GameView";
            Size = new Size(1789, 864);
            mainTable.ResumeLayout(false);
            topTable.ResumeLayout(false);
            leftOppPanel.ResumeLayout(false);
            leftOppHeader.ResumeLayout(false);
            leftOppHeader.PerformLayout();
            centerTopTable.ResumeLayout(false);
            topOppPanel.ResumeLayout(false);
            topOppHeader.ResumeLayout(false);
            topOppHeader.PerformLayout();
            rightOppPanel.ResumeLayout(false);
            rightOppHeader.ResumeLayout(false);
            rightOppHeader.PerformLayout();
            bottomTable.ResumeLayout(false);
            playerInfoPanel.ResumeLayout(false);
            playerInfoPanel.PerformLayout();
            playerCardsTable.ResumeLayout(false);
            statusPanel.ResumeLayout(false);
            statusFlow.ResumeLayout(false);
            statusFlow.PerformLayout();
            ResumeLayout(false);
        }

    }
}
