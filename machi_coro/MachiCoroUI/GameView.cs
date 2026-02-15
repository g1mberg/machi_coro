using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MachiCoroUI
{
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
        }

        // Маркет
        public FlowLayoutPanel MarketPanel => marketPanel;

        // Игрок
        public Label PlayerNameLabel => playerName;
        public Label PlayerMoneyLabel => playerMoney;
        public FlowLayoutPanel PlayerEnterprisesPanel => playerEnterprises;
        public FlowLayoutPanel PlayerSitesPanel => playerSites;
        public Button RollDiceButton => rollDiceButton;
        public Button BuildButton => buildButton;
        public Button RerollButton => rerollButton;

        // Оппоненты
        public Label LeftNameLabel => leftOppName;
        public Label LeftMoneyLabel => leftOppMoney;
        public FlowLayoutPanel LeftEnterprisesPanel => leftOppEnterprises;
        public FlowLayoutPanel LeftSitesPanel => leftOppSites;

        public Label RightNameLabel => rightOppName;
        public Label RightMoneyLabel => rightOppMoney;
        public FlowLayoutPanel RightEnterprisesPanel => rightOppEnterprises;
        public FlowLayoutPanel RightSitesPanel => rightOppSites;

        public Label TopNameLabel => topOppName;
        public Label TopMoneyLabel => topOppMoney;
        public FlowLayoutPanel TopEnterprisesPanel => topOppEnterprises;
        public FlowLayoutPanel TopSitesPanel => topOppSites;

        // Статус
        public Label CurrentPlayerLabel => labelCurrentPlayer;
        public Label PhaseLabel => labelPhase;
        public Label DiceLabel => labelDice;
        public Label LastActionLabel => labelLastAction;

        public Label HowDiceLabel => howdice;
        public Button Roll1Button => roll1;
        public Button Roll2Button => roll2;
        public Button ChangeButton => changeButton;
        public Button SkipChangeButton => skipChangeButton;
        public Button StealButton => stealButton;
        public Button SkipButton => skipButton;
    }

}
