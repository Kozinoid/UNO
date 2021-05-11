using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class UNOOneGame : UserControl
    {
        public event EventHandler<ScrollVisiblityEventArgs> ScrollVisiblityChangeEvent = null;
        public event EventHandler<NameChangedEventArgs> NameChangedEvent = null;
        public event EventHandler<ChangeRoundValueEventArgs> RoundValueChanged = null;

        public static int RowHeaderWidth = 76;
        public static int RowHeight = 50;
        public static int ColumnHeaderHeight = 38;
        public static int BottomHeight = 12;
        public static int ColumnWidth = 50;
        public static int TailWidth = 74;
        public static Color PenColor = Color.FromArgb(27, 1, 112);
        public static StringFormat sf = new StringFormat();

        private List<OneRound> allRounds = new List<OneRound>();
        private OneGamePlayers playersHeader = null;
        private OneGameResultsCtrl resultsControl = null;
        private int playerCount = 0;
        private bool initialized = false;
        private string unoStageName = "Новый UNO-этап";
        private string unoStageDate = GameTittlesDlg.GetDateString(DateTime.Now);

        public bool ScrollVisiblity
        {
            get
            {
                return hScrollBar1.Visible;
            }
        }
        public int RoundsCount
        {
            get
            {
                return allRounds.Count;
            }
        }
        public string UNOStageName
        {
            get
            {
                return unoStageName;
            }
            set
            {
                unoStageName = value;
            }
        }
        public string UNOStageDate
        {
            get
            {
                return unoStageDate;
            }
            set
            {
                unoStageDate = value;
            }
        }

        public UNOOneGame()
        {
            InitializeComponent();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
        }

        public void CreateUNOGame(int playersNumber, string[] names)
        {
            if (playersNumber > 0)
            {
                initialized = true;
                playerCount = playersNumber;
                playersHeader = OneGamePlayers.CreateGameHeader(playersNumber, names);
                playersHeader.NameChangedEvent += new EventHandler<NameChangedEventArgs>(playersHeader_NameChangedEvent);
                
                this.Controls.Add(playersHeader);

                resultsControl = OneGameResultsCtrl.CreateGameResults(playersNumber);
                this.Controls.Add(resultsControl);

                RecalcPositions();
                this.Height = RowHeight * playerCount + ColumnHeaderHeight + BottomHeight + hScrollBar1.Height;
            }
        }

        private void playersHeader_NameChangedEvent(object sender, NameChangedEventArgs e)
        {
            if (NameChangedEvent != null) NameChangedEvent(this, e);
        }

        private void RecalcPositions()
        {
            int pos = (hScrollBar1.Visible) ? hScrollBar1.Value : 0;

            playersHeader.Location = new Point(0 - pos, 0);
            for (int i = 0; i < allRounds.Count; i++)
            {
                allRounds[i].Location = new Point(RowHeaderWidth + i * ColumnWidth - pos, 0);
            }
            resultsControl.Location = new Point(RowHeaderWidth + allRounds.Count * ColumnWidth - pos, 0);
        }

        public void AddRound(int playersNumber)
        {
            OneRound or = OneRound.CreateRound(playersNumber, allRounds.Count + 1);
            or.PlayerValueChanged += new EventHandler<ChangePlayerValueEventArgs>(or_PlayerValueChanged);
            allRounds.Add(or);
            this.Controls.Add(or);
            RecalcPositions();
            RecalcHeight();
        }

        private void or_PlayerValueChanged(object sender, ChangePlayerValueEventArgs e)
        {
            RefreshResults();

            for (int i = 0; i < allRounds.Count; i++)
            {
                if (object.ReferenceEquals(sender, allRounds[i]))
                {
                    if (RoundValueChanged != null) RoundValueChanged(this, new ChangeRoundValueEventArgs(i, e.Index, e.Value));
                }
            }
        }

        public void RefreshResults()
        {
            int[] sum = new int[playerCount];
            for (int j = 0; j < playerCount; j++)
            {
                sum[j] = 0;
            }

            for (int i = 0; i < allRounds.Count; i++)
            {
                for (int j = 0; j < playerCount; j++)
                {
                    sum[j] += allRounds[i].GetValue(j);
                }
            }

            for (int j = 0; j < playerCount; j++)
            {
                resultsControl.SetResult(j, sum[j]);
            }
        }

        public void DeleteLast()
        {
            if (allRounds.Count > 0)
            {
                this.Controls.Remove(allRounds[allRounds.Count - 1]);
                allRounds.RemoveAt(allRounds.Count - 1);
                RecalcPositions();
                RecalcHeight();
            }
        }

        public void DeleteAt(int num)
        {
            if (allRounds.Count > 0)
            {
                if ((num >= 0) && (num < allRounds.Count))
                {
                    for (int i = num; i < allRounds.Count - 1; i++)
                    {
                        for (int j = 0; j < playerCount; j++)
                        {
                            allRounds[i].SetValue(j, allRounds[i + 1].GetValue(j));
                        }
                    }
                    DeleteLast();
                }
            }
        }

        private void UNOOneGame_Resize(object sender, EventArgs e)
        {
            RecalcPositions();
            RecalcHeight();
        }

        private void RecalcHeight()
        {
            if (initialized)
            {
                int allWidth = RowHeaderWidth + allRounds.Count * ColumnWidth + TailWidth;
                hScrollBar1.Minimum = 0;
                hScrollBar1.Maximum = allWidth - this.Width;
                if (allWidth > this.Width)
                {
                    if (!hScrollBar1.Visible)
                    {
                        hScrollBar1.Visible = true;
                        this.Height = RowHeight * playerCount + ColumnHeaderHeight + BottomHeight + hScrollBar1.Height;
                        if (ScrollVisiblityChangeEvent != null) ScrollVisiblityChangeEvent(this, new ScrollVisiblityEventArgs(true));
                    }
                }
                else
                {
                    if (hScrollBar1.Visible)
                    {
                        hScrollBar1.Visible = false;
                        this.Height = RowHeight * playerCount + ColumnHeaderHeight + BottomHeight;
                        if (ScrollVisiblityChangeEvent != null) ScrollVisiblityChangeEvent(this, new ScrollVisiblityEventArgs(false));
                    }
                }
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (initialized)
            {
                RecalcPositions();
            }
        }

        public void RefreshNames(string[] names)
        {
            playersHeader.RefreshNames(names);
        }

        public int GetValue(int rdNum, int plNum)
        {
            return allRounds[rdNum].GetValue(plNum);
        }

        public void SetValue(int rdNum, int plNum, int val)
        {
            allRounds[rdNum].SetValue(plNum, val);
        }

        public bool GetWinState(int rdNum, int plNum)
        {
            return allRounds[rdNum].GetWinState(plNum);
        }

        public void SetWinState(int rdNum, int plNum, bool state)
        {
            allRounds[rdNum].SetWinState(plNum, state);
        }

        public void ShowHideResults(bool state)
        {
            resultsControl.ShowHideResults(state);
        }

        public int GetResult(int index)
        {
            return resultsControl.GetResult(index);
        }
    }
}
