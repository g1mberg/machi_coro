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
        private Panel playerHeader;
        private FlowLayoutPanel buttonFlow;
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
        private Button confirmPhaseButton;
        private Button skipBuildButton;

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
            playerHeader = new Panel();
            buttonFlow = new FlowLayoutPanel();
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
            confirmPhaseButton = new Button();
            skipBuildButton = new Button();
            mainTable.SuspendLayout();
            topTable.SuspendLayout();
            leftOppPanel.SuspendLayout();
            leftOppHeader.SuspendLayout();
            topOppPanel.SuspendLayout();
            topOppHeader.SuspendLayout();
            rightOppPanel.SuspendLayout();
            rightOppHeader.SuspendLayout();
            bottomTable.SuspendLayout();
            playerInfoPanel.SuspendLayout();
            buttonFlow.SuspendLayout();
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
            // topTable — 3 columns equal width
            //
            topTable.ColumnCount = 3;
            topTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            topTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            topTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            topTable.Controls.Add(leftOppPanel, 0, 0);
            topTable.Controls.Add(topOppPanel, 1, 0);
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
            leftOppHeader.Controls.Add(leftOppMoney);
            leftOppHeader.Dock = DockStyle.Top;
            leftOppHeader.Height = 36;
            leftOppHeader.Name = "leftOppHeader";
            //
            // leftOppName
            //
            leftOppName.AutoSize = true;
            leftOppName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            leftOppName.Location = new Point(4, 4);
            leftOppName.Name = "leftOppName";
            leftOppName.Text = "Игрок L";
            leftOppName.TabIndex = 0;
            //
            // leftOppMoney
            //
            leftOppMoney.AutoSize = true;
            leftOppMoney.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            leftOppMoney.Location = new Point(4, 16);
            leftOppMoney.Name = "leftOppMoney";
            leftOppMoney.Text = "Монет: 0";
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
            leftOppSites.Height = 140;
            leftOppSites.Name = "leftOppSites";
            leftOppSites.TabIndex = 4;
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
            topOppHeader.Controls.Add(topOppMoney);
            topOppHeader.Dock = DockStyle.Top;
            topOppHeader.Height = 36;
            topOppHeader.Name = "topOppHeader";
            //
            // topOppName
            //
            topOppName.AutoSize = true;
            topOppName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            topOppName.Location = new Point(4, 4);
            topOppName.Name = "topOppName";
            topOppName.Text = "Игрок T";
            topOppName.TabIndex = 0;
            //
            // topOppMoney
            //
            topOppMoney.AutoSize = true;
            topOppMoney.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            topOppMoney.Location = new Point(4, 16);
            topOppMoney.Name = "topOppMoney";
            topOppMoney.Text = "Монет: 0";
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
            topOppSites.Height = 140;
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
            rightOppHeader.Controls.Add(rightOppMoney);
            rightOppHeader.Dock = DockStyle.Top;
            rightOppHeader.Height = 36;
            rightOppHeader.Name = "rightOppHeader";
            //
            // rightOppName
            //
            rightOppName.AutoSize = true;
            rightOppName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightOppName.Location = new Point(4, 4);
            rightOppName.Name = "rightOppName";
            rightOppName.Text = "Игрок R";
            rightOppName.TabIndex = 0;
            //
            // rightOppMoney
            //
            rightOppMoney.AutoSize = true;
            rightOppMoney.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            rightOppMoney.Location = new Point(4, 16);
            rightOppMoney.Name = "rightOppMoney";
            rightOppMoney.Text = "Монет: 0";
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
            rightOppSites.Height = 140;
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
            playerInfoPanel.Controls.Add(buttonFlow);   // Fill — добавлен первым (обрабатывается последним)
            playerInfoPanel.Controls.Add(playerHeader); // Top — добавлен последним (обрабатывается первым)
            playerInfoPanel.Dock = DockStyle.Fill;
            playerInfoPanel.Name = "playerInfoPanel";
            playerInfoPanel.Padding = new Padding(0);
            playerInfoPanel.Size = new Size(350, 338);
            playerInfoPanel.TabIndex = 0;
            //
            // playerHeader — шапка с именем и монетами
            //
            playerHeader.Controls.Add(playerName);
            playerHeader.Controls.Add(playerMoney);
            playerHeader.Dock = DockStyle.Top;
            playerHeader.Height = 52;
            playerHeader.Name = "playerHeader";
            playerHeader.Padding = new Padding(8, 6, 8, 4);
            //
            // playerName
            //
            playerName.AutoSize = true;
            playerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            playerName.Location = new Point(8, 6);
            playerName.Name = "playerName";
            playerName.Text = "Вы";
            playerName.TabIndex = 0;
            //
            // playerMoneyCaption (не используется, скрыт)
            //
            playerMoneyCaption.AutoSize = true;
            playerMoneyCaption.Location = new Point(8, 38);
            playerMoneyCaption.Name = "playerMoneyCaption";
            playerMoneyCaption.Text = "";
            playerMoneyCaption.Visible = false;
            playerMoneyCaption.TabIndex = 1;
            //
            // playerMoney
            //
            playerMoney.AutoSize = true;
            playerMoney.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            playerMoney.Location = new Point(8, 28);
            playerMoney.Name = "playerMoney";
            playerMoney.Text = "Монет: 0";
            playerMoney.TabIndex = 2;
            //
            // buttonFlow — все кнопки действий
            //
            buttonFlow.AutoScroll = false;
            buttonFlow.Dock = DockStyle.Fill;
            buttonFlow.FlowDirection = FlowDirection.TopDown;
            buttonFlow.WrapContents = false;
            buttonFlow.Padding = new Padding(6, 4, 6, 4);
            buttonFlow.Name = "buttonFlow";
            buttonFlow.Controls.Add(rollDiceButton);
            buttonFlow.Controls.Add(howdice);
            buttonFlow.Controls.Add(roll1);
            buttonFlow.Controls.Add(roll2);
            buttonFlow.Controls.Add(confirmPhaseButton);
            buttonFlow.Controls.Add(buildButton);
            buttonFlow.Controls.Add(skipBuildButton);
            buttonFlow.Controls.Add(rerollButton);
            buttonFlow.Controls.Add(stealButton);
            buttonFlow.Controls.Add(skipButton);
            buttonFlow.Controls.Add(changeButton);
            buttonFlow.Controls.Add(skipChangeButton);
            //
            // rollDiceButton
            //
            rollDiceButton.Margin = new Padding(0, 4, 0, 2);
            rollDiceButton.Name = "rollDiceButton";
            rollDiceButton.Size = new Size(200, 34);
            rollDiceButton.TabIndex = 3;
            rollDiceButton.Text = "Бросить кубик";
            //
            // buildButton
            //
            buildButton.Margin = new Padding(0, 2, 0, 2);
            buildButton.Name = "buildButton";
            buildButton.Size = new Size(200, 34);
            buildButton.TabIndex = 4;
            buildButton.Text = "Построить";
            //
            // rerollButton
            //
            rerollButton.Margin = new Padding(0, 2, 0, 2);
            rerollButton.Name = "rerollButton";
            rerollButton.Size = new Size(200, 34);
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
            statusFlow.AutoScroll = false;
            statusFlow.Controls.Add(labelCurrentPlayer);
            statusFlow.Controls.Add(labelPhase);
            statusFlow.Controls.Add(labelDice);
            statusFlow.Controls.Add(labelLastAction);
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
            howdice.AutoSize = false;
            howdice.Size = new Size(200, 22);
            howdice.Margin = new Padding(0, 6, 0, 2);
            howdice.Name = "howdice";
            howdice.Text = "Сколько кубиков?";
            howdice.Visible = false;
            howdice.TabIndex = 4;
            //
            // roll1
            //
            roll1.Margin = new Padding(0, 2, 0, 2);
            roll1.Name = "roll1";
            roll1.Size = new Size(200, 34);
            roll1.TabIndex = 5;
            roll1.Text = "1 кубик";
            roll1.Visible = false;
            //
            // roll2
            //
            roll2.Margin = new Padding(0, 2, 0, 2);
            roll2.Name = "roll2";
            roll2.Size = new Size(200, 34);
            roll2.TabIndex = 6;
            roll2.Text = "2 кубика";
            roll2.Visible = false;
            //
            // confirmPhaseButton
            //
            confirmPhaseButton.Margin = new Padding(0, 2, 0, 2);
            confirmPhaseButton.Name = "confirmPhaseButton";
            confirmPhaseButton.Size = new Size(200, 34);
            confirmPhaseButton.TabIndex = 11;
            confirmPhaseButton.Text = "Подтвердить бросок";
            confirmPhaseButton.Visible = false;
            //
            // skipBuildButton
            //
            skipBuildButton.Margin = new Padding(0, 2, 0, 2);
            skipBuildButton.Name = "skipBuildButton";
            skipBuildButton.Size = new Size(200, 34);
            skipBuildButton.TabIndex = 12;
            skipBuildButton.Text = "Пропустить постройку";
            skipBuildButton.Visible = false;
            //
            // stealButton
            //
            stealButton.Margin = new Padding(0, 2, 0, 2);
            stealButton.Name = "stealButton";
            stealButton.Size = new Size(200, 34);
            stealButton.TabIndex = 9;
            stealButton.Text = "Украсть";
            stealButton.Visible = false;
            //
            // skipButton
            //
            skipButton.Margin = new Padding(0, 2, 0, 2);
            skipButton.Name = "skipButton";
            skipButton.Size = new Size(200, 34);
            skipButton.TabIndex = 10;
            skipButton.Text = "Пропустить кражу";
            skipButton.Visible = false;
            //
            // changeButton
            //
            changeButton.Margin = new Padding(0, 2, 0, 2);
            changeButton.Name = "changeButton";
            changeButton.Size = new Size(200, 34);
            changeButton.TabIndex = 7;
            changeButton.Text = "Обмен зданиями";
            changeButton.Visible = false;
            //
            // skipChangeButton
            //
            skipChangeButton.Margin = new Padding(0, 2, 0, 2);
            skipChangeButton.Name = "skipChangeButton";
            skipChangeButton.Size = new Size(200, 34);
            skipChangeButton.TabIndex = 8;
            skipChangeButton.Text = "Пропустить обмен";
            skipChangeButton.Visible = false;
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
            topOppPanel.ResumeLayout(false);
            topOppHeader.ResumeLayout(false);
            topOppHeader.PerformLayout();
            rightOppPanel.ResumeLayout(false);
            rightOppHeader.ResumeLayout(false);
            rightOppHeader.PerformLayout();
            bottomTable.ResumeLayout(false);
            playerInfoPanel.ResumeLayout(false);
            buttonFlow.ResumeLayout(false);
            playerCardsTable.ResumeLayout(false);
            statusPanel.ResumeLayout(false);
            statusFlow.ResumeLayout(false);
            statusFlow.PerformLayout();
            ResumeLayout(false);
        }

    }
}
