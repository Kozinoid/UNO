using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class OneGamePlayers : UserControl
    {
        private UNOMenuItem headerText = new UNOMenuItem("Игрок", new Rectangle(0, 0, UNOOneGame.TailWidth, UNOOneGame.ColumnHeaderHeight), UNOOneGame.PenColor);

        public event EventHandler<NameChangedEventArgs> NameChangedEvent = null;

        private List<PlayerNameHeaderCtrl> allPlayers = new List<PlayerNameHeaderCtrl>();

        private OneGamePlayers(int playersCount, string[] names)
        {
            InitializeComponent();

            this.Height = UNOOneGame.RowHeight * playersCount + UNOOneGame.ColumnWidth;
            this.Width = UNOOneGame.RowHeaderWidth;
            for (int i = 0; i < playersCount; i++)
            {
                PlayerNameHeaderCtrl pnhc = new PlayerNameHeaderCtrl();
                pnhc.PlayerName = names[i];
                this.allPlayers.Add(pnhc);
                pnhc.Location = new Point(0, i * UNOOneGame.RowHeight + UNOOneGame.ColumnHeaderHeight);
                pnhc.Size = new Size(UNOOneGame.RowHeaderWidth, UNOOneGame.RowHeight);
                this.Controls.Add(pnhc);
                pnhc.PlayerNameChanged += new EventHandler(pnhc_PlayerNameChanged);
            }
        }

        private void pnhc_PlayerNameChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < allPlayers.Count; i++)
            {
                if (object.ReferenceEquals(sender, allPlayers[i]))
                {
                    if (NameChangedEvent != null) NameChangedEvent(this, new NameChangedEventArgs(allPlayers[i].PlayerName, i));
                }
            }
        }

        public static OneGamePlayers CreateGameHeader(int playersCount, string[] names)
        {
            OneGamePlayers oneGamePlayers = null;
            if (playersCount > 0)
            {
                oneGamePlayers = new OneGamePlayers(playersCount, names);
            }
            return oneGamePlayers;
        }

        public void RefreshNames(string[] names)
        {
            for (int i = 0; i < allPlayers.Count; i++)
            {
                allPlayers[i].PlayerName = names[i];
            }
        }

        private void OneGamePlayers_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            gr.DrawString(headerText.NameField, UNOFonts.WriteFont12, new SolidBrush(headerText.NameColor), headerText.AllRectArea, UNOOneGame.sf);
        }
    }
}
