namespace MachiCoroUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button2 = new Button();
            playerList = new ListBox();
            label1 = new Label();
            ConnectPanel = new Panel();
            label4 = new Label();
            NicknameBox = new TextBox();
            LobbyPanel = new Panel();
            Confirm = new Button();
            readyButton = new Button();
            GamePanel = new Panel();
            splitContainer1 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            marketPanel = new Panel();
            flowMarket = new FlowLayoutPanel();
            panel1 = new Panel();
            topOppSites = new FlowLayoutPanel();
            topOppEnterprises = new FlowLayoutPanel();
            topOppMoney = new Label();
            label10 = new Label();
            topOppName = new Label();
            panel3 = new Panel();
            rightOppSites = new FlowLayoutPanel();
            rightOppEnterprises = new FlowLayoutPanel();
            rightOppMoney = new Label();
            label7 = new Label();
            rightOppName = new Label();
            panel2 = new Panel();
            leftOppSites = new FlowLayoutPanel();
            leftOppEnterprises = new FlowLayoutPanel();
            leftOppMoney = new Label();
            label3 = new Label();
            leftOppName = new Label();
            player = new Panel();
            buildButton = new Button();
            label2 = new Label();
            Status = new Panel();
            flowLayoutPanel2 = new FlowLayoutPanel();
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
            skip = new Button();
            rollDice = new Button();
            playerSites = new FlowLayoutPanel();
            playerEnterprises = new FlowLayoutPanel();
            playerMoney = new Label();
            PlayerName = new Label();
            rerollButton = new Button();
            ConnectPanel.SuspendLayout();
            LobbyPanel.SuspendLayout();
            GamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            marketPanel.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            player.SuspendLayout();
            Status.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(669, 137);
            button2.Margin = new Padding(2, 1, 2, 1);
            button2.Name = "button2";
            button2.Size = new Size(390, 75);
            button2.TabIndex = 1;
            button2.Text = "Подключиться";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Sign_in_button_Click;
            // 
            // playerList
            // 
            playerList.FormattingEnabled = true;
            playerList.ItemHeight = 15;
            playerList.Location = new Point(538, 98);
            playerList.Margin = new Padding(2, 1, 2, 1);
            playerList.Name = "playerList";
            playerList.Size = new Size(184, 154);
            playerList.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(488, 40);
            label1.Name = "label1";
            label1.Size = new Size(289, 28);
            label1.TabIndex = 13;
            label1.Text = "\"Ожидание других игроков…\"";
            // 
            // ConnectPanel
            // 
            ConnectPanel.Controls.Add(label4);
            ConnectPanel.Controls.Add(NicknameBox);
            ConnectPanel.Controls.Add(button2);
            ConnectPanel.Location = new Point(138, 54);
            ConnectPanel.Name = "ConnectPanel";
            ConnectPanel.Size = new Size(1344, 596);
            ConnectPanel.TabIndex = 14;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(47, 166);
            label4.Name = "label4";
            label4.Size = new Size(128, 15);
            label4.TabIndex = 3;
            label4.Text = "Введите ваш никнейм";
            // 
            // NicknameBox
            // 
            NicknameBox.Location = new Point(203, 163);
            NicknameBox.Name = "NicknameBox";
            NicknameBox.Size = new Size(339, 23);
            NicknameBox.TabIndex = 2;
            // 
            // LobbyPanel
            // 
            LobbyPanel.Controls.Add(Confirm);
            LobbyPanel.Controls.Add(readyButton);
            LobbyPanel.Controls.Add(playerList);
            LobbyPanel.Controls.Add(label1);
            LobbyPanel.Location = new Point(30, 57);
            LobbyPanel.Name = "LobbyPanel";
            LobbyPanel.Size = new Size(1316, 566);
            LobbyPanel.TabIndex = 15;
            // 
            // Confirm
            // 
            Confirm.Enabled = false;
            Confirm.Location = new Point(538, 331);
            Confirm.Name = "Confirm";
            Confirm.Size = new Size(186, 60);
            Confirm.TabIndex = 15;
            Confirm.Text = "Начать";
            Confirm.UseVisualStyleBackColor = false;
            Confirm.Visible = false;
            Confirm.Click += Confirm_Click;
            // 
            // readyButton
            // 
            readyButton.Location = new Point(538, 331);
            readyButton.Name = "readyButton";
            readyButton.Size = new Size(186, 60);
            readyButton.TabIndex = 14;
            readyButton.Text = "Готов";
            readyButton.UseVisualStyleBackColor = true;
            readyButton.Click += readyButton_Click;
            // 
            // GamePanel
            // 
            GamePanel.Controls.Add(splitContainer1);
            GamePanel.Location = new Point(61, 26);
            GamePanel.Name = "GamePanel";
            GamePanel.Size = new Size(1353, 611);
            GamePanel.TabIndex = 16;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(player);
            splitContainer1.Size = new Size(1353, 611);
            splitContainer1.SplitterDistance = 451;
            splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(marketPanel, 1, 1);
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Controls.Add(panel3, 2, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Location = new Point(6, 15);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1339, 434);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // marketPanel
            // 
            marketPanel.Controls.Add(flowMarket);
            marketPanel.Location = new Point(337, 133);
            marketPanel.Name = "marketPanel";
            marketPanel.Size = new Size(663, 298);
            marketPanel.TabIndex = 2;
            // 
            // flowMarket
            // 
            flowMarket.AutoScroll = true;
            flowMarket.Dock = DockStyle.Fill;
            flowMarket.Location = new Point(0, 0);
            flowMarket.Name = "flowMarket";
            flowMarket.Size = new Size(663, 298);
            flowMarket.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(topOppSites);
            panel1.Controls.Add(topOppEnterprises);
            panel1.Controls.Add(topOppMoney);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(topOppName);
            panel1.Location = new Point(337, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(663, 124);
            panel1.TabIndex = 4;
            // 
            // topOppSites
            // 
            topOppSites.Location = new Point(379, 0);
            topOppSites.Name = "topOppSites";
            topOppSites.Size = new Size(269, 121);
            topOppSites.TabIndex = 7;
            // 
            // topOppEnterprises
            // 
            topOppEnterprises.Location = new Point(94, 0);
            topOppEnterprises.Name = "topOppEnterprises";
            topOppEnterprises.Size = new Size(279, 118);
            topOppEnterprises.TabIndex = 6;
            // 
            // topOppMoney
            // 
            topOppMoney.AutoSize = true;
            topOppMoney.Location = new Point(50, 42);
            topOppMoney.Name = "topOppMoney";
            topOppMoney.Size = new Size(38, 15);
            topOppMoney.TabIndex = 5;
            topOppMoney.Text = "label2";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(2, 42);
            label10.Name = "label10";
            label10.Size = new Size(42, 15);
            label10.TabIndex = 4;
            label10.Text = "Денег:";
            // 
            // topOppName
            // 
            topOppName.AutoSize = true;
            topOppName.Location = new Point(3, 15);
            topOppName.Name = "topOppName";
            topOppName.Size = new Size(38, 15);
            topOppName.TabIndex = 3;
            topOppName.Text = "label2";
            // 
            // panel3
            // 
            panel3.Controls.Add(rightOppSites);
            panel3.Controls.Add(rightOppEnterprises);
            panel3.Controls.Add(rightOppMoney);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(rightOppName);
            panel3.Location = new Point(1006, 3);
            panel3.Name = "panel3";
            tableLayoutPanel1.SetRowSpan(panel3, 2);
            panel3.Size = new Size(330, 428);
            panel3.TabIndex = 6;
            // 
            // rightOppSites
            // 
            rightOppSites.Location = new Point(2, 251);
            rightOppSites.Name = "rightOppSites";
            rightOppSites.Size = new Size(325, 174);
            rightOppSites.TabIndex = 8;
            // 
            // rightOppEnterprises
            // 
            rightOppEnterprises.Location = new Point(3, 74);
            rightOppEnterprises.Name = "rightOppEnterprises";
            rightOppEnterprises.Size = new Size(325, 171);
            rightOppEnterprises.TabIndex = 7;
            // 
            // rightOppMoney
            // 
            rightOppMoney.AutoSize = true;
            rightOppMoney.Location = new Point(50, 42);
            rightOppMoney.Name = "rightOppMoney";
            rightOppMoney.Size = new Size(38, 15);
            rightOppMoney.TabIndex = 5;
            rightOppMoney.Text = "label2";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(2, 42);
            label7.Name = "label7";
            label7.Size = new Size(42, 15);
            label7.TabIndex = 4;
            label7.Text = "Денег:";
            // 
            // rightOppName
            // 
            rightOppName.AutoSize = true;
            rightOppName.Location = new Point(3, 15);
            rightOppName.Name = "rightOppName";
            rightOppName.Size = new Size(38, 15);
            rightOppName.TabIndex = 3;
            rightOppName.Text = "label2";
            // 
            // panel2
            // 
            panel2.Controls.Add(leftOppSites);
            panel2.Controls.Add(leftOppEnterprises);
            panel2.Controls.Add(leftOppMoney);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(leftOppName);
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            tableLayoutPanel1.SetRowSpan(panel2, 2);
            panel2.Size = new Size(328, 428);
            panel2.TabIndex = 5;
            // 
            // leftOppSites
            // 
            leftOppSites.Location = new Point(10, 251);
            leftOppSites.Name = "leftOppSites";
            leftOppSites.Size = new Size(325, 175);
            leftOppSites.TabIndex = 7;
            // 
            // leftOppEnterprises
            // 
            leftOppEnterprises.Location = new Point(9, 74);
            leftOppEnterprises.Name = "leftOppEnterprises";
            leftOppEnterprises.Size = new Size(325, 171);
            leftOppEnterprises.TabIndex = 6;
            // 
            // leftOppMoney
            // 
            leftOppMoney.AutoSize = true;
            leftOppMoney.Location = new Point(57, 42);
            leftOppMoney.Name = "leftOppMoney";
            leftOppMoney.Size = new Size(38, 15);
            leftOppMoney.TabIndex = 5;
            leftOppMoney.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 42);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 4;
            label3.Text = "Денег:";
            // 
            // leftOppName
            // 
            leftOppName.AutoSize = true;
            leftOppName.Location = new Point(10, 15);
            leftOppName.Name = "leftOppName";
            leftOppName.Size = new Size(38, 15);
            leftOppName.TabIndex = 3;
            leftOppName.Text = "label2";
            // 
            // player
            // 
            player.Controls.Add(rerollButton);
            player.Controls.Add(buildButton);
            player.Controls.Add(label2);
            player.Controls.Add(Status);
            player.Controls.Add(rollDice);
            player.Controls.Add(playerSites);
            player.Controls.Add(playerEnterprises);
            player.Controls.Add(playerMoney);
            player.Controls.Add(PlayerName);
            player.Dock = DockStyle.Fill;
            player.Location = new Point(0, 0);
            player.Name = "player";
            player.Size = new Size(1353, 156);
            player.TabIndex = 0;
            // 
            // buildButton
            // 
            buildButton.Location = new Point(112, 57);
            buildButton.Name = "buildButton";
            buildButton.Size = new Size(82, 29);
            buildButton.TabIndex = 8;
            buildButton.Text = "Построить";
            buildButton.UseVisualStyleBackColor = true;
            buildButton.Click += buildButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 36);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 7;
            label2.Text = "Денег:";
            // 
            // Status
            // 
            Status.Controls.Add(flowLayoutPanel2);
            Status.Location = new Point(1016, -2);
            Status.Name = "Status";
            Status.Size = new Size(327, 155);
            Status.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(labelCurrentPlayer);
            flowLayoutPanel2.Controls.Add(labelPhase);
            flowLayoutPanel2.Controls.Add(labelDice);
            flowLayoutPanel2.Controls.Add(labelLastAction);
            flowLayoutPanel2.Controls.Add(howdice);
            flowLayoutPanel2.Controls.Add(roll1);
            flowLayoutPanel2.Controls.Add(roll2);
            flowLayoutPanel2.Controls.Add(changeButton);
            flowLayoutPanel2.Controls.Add(skipChangeButton);
            flowLayoutPanel2.Controls.Add(stealButton);
            flowLayoutPanel2.Controls.Add(skip);
            flowLayoutPanel2.Location = new Point(0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(328, 158);
            flowLayoutPanel2.TabIndex = 0;
            // 
            // labelCurrentPlayer
            // 
            labelCurrentPlayer.AutoSize = true;
            labelCurrentPlayer.Location = new Point(3, 0);
            labelCurrentPlayer.Name = "labelCurrentPlayer";
            labelCurrentPlayer.Size = new Size(55, 15);
            labelCurrentPlayer.TabIndex = 0;
            labelCurrentPlayer.Text = "\"Ход: —\"";
            // 
            // labelPhase
            // 
            labelPhase.AutoSize = true;
            labelPhase.Location = new Point(64, 0);
            labelPhase.Name = "labelPhase";
            labelPhase.Size = new Size(61, 15);
            labelPhase.TabIndex = 1;
            labelPhase.Text = "\"Фаза: —\"";
            // 
            // labelDice
            // 
            labelDice.AutoSize = true;
            labelDice.Location = new Point(131, 0);
            labelDice.Name = "labelDice";
            labelDice.Size = new Size(63, 15);
            labelDice.TabIndex = 2;
            labelDice.Text = "Кубик: —\"";
            // 
            // labelLastAction
            // 
            labelLastAction.AutoSize = true;
            labelLastAction.Location = new Point(217, 0);
            labelLastAction.Margin = new Padding(20, 0, 3, 0);
            labelLastAction.Name = "labelLastAction";
            labelLastAction.Size = new Size(95, 15);
            labelLastAction.TabIndex = 3;
            labelLastAction.Text = "\"Последнее: —\"";
            // 
            // howdice
            // 
            howdice.AutoSize = true;
            howdice.Location = new Point(3, 25);
            howdice.Margin = new Padding(3, 10, 20, 0);
            howdice.Name = "howdice";
            howdice.Size = new Size(204, 15);
            howdice.TabIndex = 4;
            howdice.Text = "Сколько кубиков вы хотите кинуть?";
            howdice.Visible = false;
            howdice.Click += label5_Click;
            // 
            // roll1
            // 
            roll1.Location = new Point(230, 35);
            roll1.Margin = new Padding(3, 20, 3, 3);
            roll1.Name = "roll1";
            roll1.Size = new Size(75, 23);
            roll1.TabIndex = 5;
            roll1.Text = "1";
            roll1.UseVisualStyleBackColor = true;
            roll1.Visible = false;
            roll1.Click += roll1_Click;
            // 
            // roll2
            // 
            roll2.Location = new Point(230, 64);
            roll2.Margin = new Padding(230, 3, 3, 3);
            roll2.Name = "roll2";
            roll2.Size = new Size(75, 23);
            roll2.TabIndex = 6;
            roll2.Text = "2";
            roll2.UseVisualStyleBackColor = true;
            roll2.Click += roll2_Click;
            // 
            // changeButton
            // 
            changeButton.Location = new Point(3, 93);
            changeButton.Name = "changeButton";
            changeButton.Size = new Size(81, 33);
            changeButton.TabIndex = 7;
            changeButton.Text = "поменяться";
            changeButton.UseVisualStyleBackColor = true;
            changeButton.Visible = false;
            // 
            // skipChangeButton
            // 
            skipChangeButton.Location = new Point(90, 93);
            skipChangeButton.Name = "skipChangeButton";
            skipChangeButton.Size = new Size(83, 33);
            skipChangeButton.TabIndex = 8;
            skipChangeButton.Text = "пропустить";
            skipChangeButton.UseVisualStyleBackColor = true;
            skipChangeButton.Visible = false;
            // 
            // stealButton
            // 
            stealButton.Location = new Point(179, 93);
            stealButton.Name = "stealButton";
            stealButton.Size = new Size(57, 38);
            stealButton.TabIndex = 9;
            stealButton.Text = "украсть";
            stealButton.UseVisualStyleBackColor = true;
            stealButton.Visible = false;
            // 
            // skip
            // 
            skip.Location = new Point(242, 93);
            skip.Name = "skip";
            skip.Size = new Size(80, 36);
            skip.TabIndex = 10;
            skip.Text = "пропустить";
            skip.UseVisualStyleBackColor = true;
            skip.Visible = false;
            // 
            // rollDice
            // 
            rollDice.Location = new Point(6, 57);
            rollDice.Name = "rollDice";
            rollDice.Size = new Size(100, 29);
            rollDice.TabIndex = 5;
            rollDice.Text = "Бросить кубик";
            rollDice.UseVisualStyleBackColor = true;
            rollDice.Click += rollDice_Click;
            // 
            // playerSites
            // 
            playerSites.Location = new Point(622, 6);
            playerSites.Name = "playerSites";
            playerSites.Size = new Size(322, 147);
            playerSites.TabIndex = 4;
            // 
            // playerEnterprises
            // 
            playerEnterprises.Location = new Point(248, 6);
            playerEnterprises.Name = "playerEnterprises";
            playerEnterprises.Size = new Size(322, 147);
            playerEnterprises.TabIndex = 3;
            // 
            // playerMoney
            // 
            playerMoney.AutoSize = true;
            playerMoney.Location = new Point(57, 36);
            playerMoney.Name = "playerMoney";
            playerMoney.Size = new Size(38, 15);
            playerMoney.TabIndex = 2;
            playerMoney.Text = "label2";
            // 
            // PlayerName
            // 
            PlayerName.AutoSize = true;
            PlayerName.Location = new Point(10, 9);
            PlayerName.Name = "PlayerName";
            PlayerName.Size = new Size(38, 15);
            PlayerName.TabIndex = 0;
            PlayerName.Text = "label2";
            // 
            // rerollButton
            // 
            rerollButton.Location = new Point(10, 99);
            rerollButton.Name = "rerollButton";
            rerollButton.Size = new Size(87, 28);
            rerollButton.TabIndex = 9;
            rerollButton.Text = "перебросить";
            rerollButton.UseVisualStyleBackColor = true;
            rerollButton.Visible = false;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1485, 673);
            Controls.Add(GamePanel);
            Controls.Add(ConnectPanel);
            Controls.Add(LobbyPanel);
            Margin = new Padding(2, 1, 2, 1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ConnectPanel.ResumeLayout(false);
            ConnectPanel.PerformLayout();
            LobbyPanel.ResumeLayout(false);
            LobbyPanel.PerformLayout();
            GamePanel.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            marketPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            player.ResumeLayout(false);
            player.PerformLayout();
            Status.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button button2;
        private ListBox playerList;
        private Label label1;
        private Panel ConnectPanel;
        private Panel LobbyPanel;
        private Panel GamePanel;
        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel marketPanel;
        private Panel player;
        private Label PlayerName;
        private Label playerMoney;
        private Panel panel1;
        private Label topOppMoney;
        private Label label10;
        private Label topOppName;
        private Panel panel2;
        private Label leftOppMoney;
        private Label label3;
        private Label leftOppName;
        private Panel panel3;
        private Label rightOppMoney;
        private Label label7;
        private Label rightOppName;
        private FlowLayoutPanel leftOppSites;
        private FlowLayoutPanel leftOppEnterprices;
        private FlowLayoutPanel rightOppSites;
        private FlowLayoutPanel rightOppEnterprices;
        private FlowLayoutPanel topOppSites;
        private FlowLayoutPanel topOppEnterprices;
        private FlowLayoutPanel playerSites;
        private FlowLayoutPanel playerEnterprices;
        private FlowLayoutPanel flowMarket;
        private Button rollDice;
        private Panel Status;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label labelCurrentPlayer;
        private Label labelPhase;
        private Label labelDice;
        private Label labelLastAction;
        private Label label2;
        private FlowLayoutPanel topOppEnterprises;
        private FlowLayoutPanel leftOppEnterprises;
        private FlowLayoutPanel rightOppEnterprises;
        private FlowLayoutPanel playerEnterprises;
        private Button buildButton;
        private Button readyButton;
        private Label label4;
        private TextBox textBox5;
        private Label howdice;
        private Button roll1;
        private Button roll2;
        private Button changeButton;
        private Button skipChangeButton;
        private TextBox NicknameBox;
        private Button Confirm;
        private Button stealButton;
        private Button skip;
        private Button rerollButton;
    }
}
