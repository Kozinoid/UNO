using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class OneRound : UserControl
    {
        public event EventHandler<ChangePlayerValueEventArgs> PlayerValueChanged = null;

        private List<RoundResultCellCtrl> resultCells = new List<RoundResultCellCtrl>();
        private RoundNumberCtrl rnc = new RoundNumberCtrl();

        private OneRound(int playersCount, int num)
        {
            InitializeComponent();

            this.Height = UNOOneGame.RowHeight * playersCount + UNOOneGame.ColumnWidth;
            this.Width = UNOOneGame.ColumnWidth;
            this.rnc.Location = new Point(0, 0);
            this.rnc.Size = new Size(UNOOneGame.ColumnWidth, UNOOneGame.ColumnHeaderHeight);
            this.rnc.Number = num;
            this.Controls.Add(this.rnc);
            for (int i = 0; i < playersCount; i++)
            {
                RoundResultCellCtrl rrcc = new RoundResultCellCtrl();
                rrcc.ResultValueChanged += new EventHandler(rrcc_ResultValueChanged);
                rrcc.WinStateChanged += new EventHandler(rrcc_WinStateChanged);
                this.resultCells.Add(rrcc);
                rrcc.Location = new Point(0, i * UNOOneGame.RowHeight + UNOOneGame.ColumnHeaderHeight);
                rrcc.Size = new Size(UNOOneGame.ColumnWidth, UNOOneGame.RowHeight);
                this.Controls.Add(rrcc);
            }
        }

        void rrcc_WinStateChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < resultCells.Count; i++)
            {
                if (!object.ReferenceEquals(sender, resultCells[i]))
                {
                    resultCells[i].Winner = false;
                }
            }
        }

        private void rrcc_ResultValueChanged(object sender, EventArgs e)
        {
            int winCount = 0;
            int winInd = -1;
            for (int i = 0; i < resultCells.Count; i++)
            {
                if (resultCells[i].Result != 0)
                {
                    resultCells[i].Winner = false;
                }
                else
                {
                    winCount++;
                    winInd = i;
                }
                if (object.ReferenceEquals(sender, resultCells[i]))
                {
                    if (PlayerValueChanged != null) PlayerValueChanged(this, new ChangePlayerValueEventArgs(i, resultCells[i].Result));
                }
            }
            if (winCount == 1)
            {
                resultCells[winInd].Winner = true;
            }
        }

        public static OneRound CreateRound(int playersCount, int num)
        {
            OneRound round = null;
            if (playersCount > 0)
            {
                round = new OneRound(playersCount, num);
            }
            return round;
        }

        public int GetValue(int index)
        {
            return resultCells[index].Result;
        }

        public void SetValue(int index, int val)
        {
            resultCells[index].Result = val;
        }

        public bool GetWinState(int index)
        {
            return resultCells[index].Winner;
        }

        public void SetWinState(int index, bool val)
        {
            resultCells[index].Winner = val;
        }
    }
}
