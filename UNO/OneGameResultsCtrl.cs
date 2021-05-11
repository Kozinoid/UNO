using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class OneGameResultsCtrl : UserControl
    {
        private UNOMenuItem headerText = new UNOMenuItem("Итог", new Rectangle(0, 0, UNOOneGame.TailWidth, UNOOneGame.ColumnHeaderHeight), UNOOneGame.PenColor);

        private List<ResultCellCtrl> allResults = new List<ResultCellCtrl>();

        private OneGameResultsCtrl()
        {
            InitializeComponent();
        }

        public static OneGameResultsCtrl CreateGameResults(int playersCount)
        {
            OneGameResultsCtrl oneGameResults = null;
            if (playersCount > 0)
            {
                oneGameResults = new OneGameResultsCtrl();
                oneGameResults.Height = UNOOneGame.RowHeight * playersCount + UNOOneGame.ColumnWidth;
                oneGameResults.Width = UNOOneGame.TailWidth;
                for (int i = 0; i < playersCount; i++)
                {
                    ResultCellCtrl rcc = new ResultCellCtrl();
                    oneGameResults.allResults.Add(rcc);
                    rcc.Location = new Point(0, i * UNOOneGame.RowHeight + UNOOneGame.ColumnHeaderHeight);
                    rcc.Size = new Size(UNOOneGame.TailWidth, UNOOneGame.RowHeight);
                    oneGameResults.Controls.Add(rcc);
                }
            }
            return oneGameResults;
        }

        public void ShowHideResults(bool state)
        {
            for (int i = 0; i < allResults.Count; i++)
            {
                allResults[i].HideResult = state;
            }
        }

        public void SetResult(int index, int val)
        {
            allResults[index].Result = val;
        }

        public int GetResult(int index)
        {
            return allResults[index].Result;
        }

        private void OneGameResultsCtrl_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            gr.DrawString(headerText.NameField, UNOFonts.WriteFont12, new SolidBrush(headerText.NameColor), headerText.AllRectArea, UNOOneGame.sf);
        }
    }
}
