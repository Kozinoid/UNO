using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using PIC18F4550Controller;

namespace UNO
{
    public partial class Form1 : Form
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private NewUNODlg dlg = null;
        private GameTittlesDlg gtd = null;
        private StatisticsDlg sd = null;
        private int playerCount = 0;
        private string[] playerNames = new string[8];
        private int[] statWins = new int[8];
        private int[] statZeros = new int[8];
        private int[] statTotal = new int[8];
        private string currentFile = "";
        private bool saved = true;
        private int HeaderStyle = 0;
        private int MainTextStyle = 0;
        private int StageHeader = 0;
        private int OtherHeader = 0;
        private int ErrorText = 0;

        public Form1()
        {
            InitializeComponent();

            LoadPrivateFontCollection();

            dlg = new NewUNODlg();
            for (int i = 0; i < NewUNODlg.maxPlayers; i++)
            {
                playerNames[i] = "";
            }
            gtd = new GameTittlesDlg();
            sd = new StatisticsDlg();
            sd.ClearStyles();
            HeaderStyle = sd.AddNewStyle(18, Color.Red, UNOTextAlign.center);
            MainTextStyle = sd.AddNewStyle(12, UNOOneGame.PenColor, UNOTextAlign.left);
            StageHeader = sd.AddNewStyle(16, Color.Green, UNOTextAlign.left);
            OtherHeader = sd.AddNewStyle(12, Color.Brown, UNOTextAlign.left);
            ErrorText = sd.AddNewStyle(12, Color.Red, UNOTextAlign.left);
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("New Stage");
            UNOOneGame unoOneGame = new UNOOneGame();
            unoOneGame.CreateUNOGame(playerCount, playerNames);
            unoOneGame.ShowHideResults(!showResultsToolStripMenuItem.Checked);
            unoOneGame.Dock = DockStyle.Top;
            unoOneGame.ScrollVisiblityChangeEvent += new EventHandler<ScrollVisiblityEventArgs>(unoOneGame_ScrollVisiblityChangeEvent);
            unoOneGame.NameChangedEvent += new EventHandler<NameChangedEventArgs>(unoOneGame_NameChangedEvent);
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Tag = unoOneGame;
            tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(unoOneGame);

            if (!roundToolStripMenuItem.Enabled)
            {
                roundToolStripMenuItem.Enabled = true;
            }

            saved = false;
        }

        private void unoOneGame_NameChangedEvent(object sender, NameChangedEventArgs e)
        {
            playerNames[e.Index] = e.NewName;
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                (tabControl1.TabPages[i].Tag as UNOOneGame).RefreshNames(playerNames);
            }
            saved = false;
        }

        private void unoOneGame_ScrollVisiblityChangeEvent(object sender, ScrollVisiblityEventArgs e)
        {
            ChangeScrollVisiblity(e.visiblity);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                if (MessageBox.Show("Удалить этап?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                    if (tabControl1.TabPages.Count == 0)
                    {
                        roundToolStripMenuItem.Enabled = false;
                    }
                    saved = false;
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saved = false;
            (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).AddRound(playerCount);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить последний раунд?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                saved = false;
                (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).DeleteLast();
            }
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if ((tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).RoundsCount > 0)
            {
                RoundNumberDlg rnd = new RoundNumberDlg();
                rnd.Value = 0;
                rnd.Maximum = (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).RoundsCount - 1;
                if (rnd.ShowDialog() == DialogResult.OK)
                {
                    if (rnd.Value >= 0)
                    {
                        (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).DeleteAt(rnd.Value);
                    }
                }
                saved = false;
            }
        }

        private void newUNOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                if (MessageBox.Show("Сохранить проект?", "Проект не сохранен", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    saveProjectToolStripMenuItem_Click_1(sender, e);
                }
            }
            if (saved)
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    currentFile = "";
                    if (tabControl1.TabPages.Count > 0)
                    {
                        tabControl1.TabPages.Clear();
                    }
                    tabControl1.Visible = true;
                    gameToolStripMenuItem.Enabled = true;

                    playerCount = dlg.PlayerCount;
                    tabControl1.Height = UNOOneGame.ColumnHeaderHeight + playerCount * UNOOneGame.RowHeight + UNOOneGame.BottomHeight +
                        tabControl1.ItemSize.Height;
                    for (int i = 0; i < NewUNODlg.maxPlayers; i++)
                    {
                        playerNames[i] = dlg.PlayersNames[i];
                    }
                }
            }
        }

        private void LoadPrivateFontCollection()
        {
            // Create the byte array and get its length

            byte[] fontArray = Properties.Resources.segoescb;
            int dataLength = Properties.Resources.segoescb.Length;


            // ASSIGN MEMORY AND COPY  BYTE[] ON THAT MEMORY ADDRESS
            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontArray, 0, ptrData, dataLength);


            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();
            //PASS THE FONT TO THE  PRIVATEFONTCOLLECTION OBJECT
            pfc.AddMemoryFont(ptrData, dataLength);

            //FREE THE  "UNSAFE" MEMORY
            Marshal.FreeCoTaskMem(ptrData);

            UNOFonts.Family = pfc.Families[0];
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex >= 0)
            {
                ChangeScrollVisiblity((tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).ScrollVisiblity);
            }
        }

        private void ChangeScrollVisiblity(bool state)
        {
            tabControl1.Height = (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).Height +
                     tabControl1.ItemSize.Height + 8;
        }

        private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            gtd.StageName = (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).UNOStageName;
            gtd.StageDate = (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).UNOStageDate;
            if (gtd.ShowDialog() == DialogResult.OK)
            {
                tabControl1.TabPages[tabControl1.SelectedIndex].Text = gtd.StageName;
                (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).UNOStageName = gtd.StageName;
                (tabControl1.TabPages[tabControl1.SelectedIndex].Tag as UNOOneGame).UNOStageDate = gtd.StageDate;
            }
        }

        private void showResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    (tabControl1.TabPages[i].Tag as UNOOneGame).ShowHideResults(!showResultsToolStripMenuItem.Checked);
                }
            }
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUNO();
            saved = true;
        }

        private void LoadUNO()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.uno | *.uno";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFile = ofd.FileName;
                ProjFileStream pfs = new ProjFileStream(currentFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                if (tabControl1.TabPages.Count > 0)
                {
                    tabControl1.TabPages.Clear();
                }
                tabControl1.Visible = true;
                gameToolStripMenuItem.Enabled = true;

                showResultsToolStripMenuItem.Checked = pfs.ReadBool();

                playerCount = pfs.ReadInt();
                tabControl1.Height = UNOOneGame.ColumnHeaderHeight + playerCount * UNOOneGame.RowHeight + UNOOneGame.BottomHeight +
                    tabControl1.ItemSize.Height;
                for (int i = 0; i < playerCount; i++)
                {
                    playerNames[i] = pfs.ReadString();
                }

                int stages = pfs.ReadInt();
                for (int i = 0; i < stages; i++)
                {
                    string name = pfs.ReadString();
                    string date = pfs.ReadString();
                    tabControl1.TabPages.Add(name);

                    UNOOneGame unoOneGame = new UNOOneGame();
                    unoOneGame.UNOStageName = name;
                    unoOneGame.UNOStageDate = date;
                    unoOneGame.CreateUNOGame(playerCount, playerNames);
                    unoOneGame.ShowHideResults(!showResultsToolStripMenuItem.Checked);
                    unoOneGame.Dock = DockStyle.Top;
                    unoOneGame.ScrollVisiblityChangeEvent += new EventHandler<ScrollVisiblityEventArgs>(unoOneGame_ScrollVisiblityChangeEvent);
                    unoOneGame.NameChangedEvent += new EventHandler<NameChangedEventArgs>(unoOneGame_NameChangedEvent);
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Tag = unoOneGame;
                    tabControl1.TabPages[tabControl1.TabPages.Count - 1].Controls.Add(unoOneGame);

                    if (!roundToolStripMenuItem.Enabled)
                    {
                        roundToolStripMenuItem.Enabled = true;
                    }

                    int roundCount = pfs.ReadInt();
                    for (int j = 0; j < roundCount; j++)
                    {
                        (tabControl1.TabPages[i].Tag as UNOOneGame).AddRound(playerCount);
                        for (int k = 0; k < playerCount; k++)
                        {
                            (tabControl1.TabPages[i].Tag as UNOOneGame).SetValue(j, k, pfs.ReadInt());
                            (tabControl1.TabPages[i].Tag as UNOOneGame).SetWinState(j, k, pfs.ReadBool());
                        }
                    }
                    unoOneGame.RefreshResults();
                }

                pfs.Close();
            }
        }

        private void saveProjectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (currentFile == "")
            {
                currentFile = GetSaveFileName();
            }
            if (currentFile != "") SaveUNO(currentFile);
        }
        
        private void saveUNOAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = GetSaveFileName();
            if (str != "")
            {
                currentFile = str;
                SaveUNO(currentFile);
            }
        }

        private void SaveUNO(string name)
        {
            ProjFileStream pfs = new ProjFileStream(name, System.IO.FileMode.Create, System.IO.FileAccess.Write);

            pfs.WriteBool(showResultsToolStripMenuItem.Checked);

            pfs.WriteInt(playerCount);
            for (int i = 0; i < playerCount; i++)
            {
                pfs.WriteString(playerNames[i]);
            }

            pfs.WriteInt(tabControl1.TabPages.Count);
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                pfs.WriteString((tabControl1.TabPages[i].Tag as UNOOneGame).UNOStageName);
                pfs.WriteString((tabControl1.TabPages[i].Tag as UNOOneGame).UNOStageDate);
                pfs.WriteInt((tabControl1.TabPages[i].Tag as UNOOneGame).RoundsCount);

                for (int j = 0; j < (tabControl1.TabPages[i].Tag as UNOOneGame).RoundsCount; j++)
                {
                    for (int k = 0; k < playerCount; k++)
                    {
                        pfs.WriteInt((tabControl1.TabPages[i].Tag as UNOOneGame).GetValue(j, k));
                        pfs.WriteBool((tabControl1.TabPages[i].Tag as UNOOneGame).GetWinState(j, k));
                    }
                }
            }

            saved = true;

            pfs.Close();
        }

        private string GetSaveFileName()
        {
            string res = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "*.uno | *.uno";
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                res = sfd.FileName;
            }
            return res;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Oemplus:
                    if (e.Control)
                    {
                        if (gameToolStripMenuItem.Enabled)
                        {
                            addToolStripMenuItem1_Click(this, null);
                        }
                    }
                    else
                    {
                        if (roundToolStripMenuItem.Enabled)
                        {
                            addToolStripMenuItem_Click(this, null);
                        }
                    }
                    break;
                case Keys.Delete:
                    if (e.Control)
                    {
                        if (gameToolStripMenuItem.Enabled)
                        {
                            deleteToolStripMenuItem_Click(this, null);
                        }
                    }
                    else
                    {
                        if (roundToolStripMenuItem.Enabled)
                        {
                            deleteToolStripMenuItem1_Click(this, null);
                        }
                    }
                    break;
                case Keys.S:
                    if (e.Control)
                    {
                        if (currentFile == "")
                        {
                            currentFile = GetSaveFileName();
                        }
                        if (currentFile != "") SaveUNO(currentFile);
                    }
                    break;
                case Keys.O:
                    if (e.Control)
                    {
                        LoadUNO();
                    }
                    break;
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count > 0)
            {
                sd.ClearParagraphs();

                sd.AddParagraph("Участники:", HeaderStyle);
                string str = playerNames[0];
                for (int k = 1; k < playerCount; k++)
                {
                    str += ", " + playerNames[k];
                }
                sd.AddParagraph(str, MainTextStyle);
                sd.AddParagraph("Всего этапов: " + tabControl1.TabPages.Count.ToString(), HeaderStyle);

                int RoundsTotal = 0;
                for (int k = 0; k < playerCount; k++)
                {
                    statWins[k] = 0;
                    statZeros[k] = 0;
                    statTotal[k] = 0;
                }

                int[] stageWins = new int[8];
                int[] stageZeros = new int[8];

                int maxValue = 0;
                int maxVStage = 0;
                int maxVRound = 0;
                int maxVPlayer = 0;

                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    str = "Этап №" + (i + 1).ToString() + ": ";
                    str += "\"" + (tabControl1.TabPages[i].Tag as UNOOneGame).UNOStageName + "\" (" + (tabControl1.TabPages[i].Tag as UNOOneGame).UNOStageDate + ")";
                    sd.AddParagraph(str, StageHeader);

                    for (int k = 0; k < playerCount; k++)
                    {
                        stageWins[k] = 0;
                        stageZeros[k] = 0;
                    }

                    RoundsTotal += (tabControl1.TabPages[i].Tag as UNOOneGame).RoundsCount;
                    
                    for (int j = 0; j < (tabControl1.TabPages[i].Tag as UNOOneGame).RoundsCount; j++)
                    {
                        bool winnersInRound = false;
                        for (int k = 0; k < playerCount; k++)
                        {
                            int val = (tabControl1.TabPages[i].Tag as UNOOneGame).GetValue(j, k);
                            if (val > maxValue)
                            {
                                maxValue = val;
                                maxVPlayer = k;
                                maxVRound = j;
                                maxVStage = i;
                            }
                            bool win = (tabControl1.TabPages[i].Tag as UNOOneGame).GetWinState(j, k);
                            if (win)
                            {
                                stageWins[k]++;
                                winnersInRound = true;
                            }
                            else
                            {
                                if (val == 0)
                                {
                                    stageZeros[k]++;
                                }
                            }
                        }
                        if (!winnersInRound)
                        {
                            str = "Раунд №" + (j + 1).ToString() + " - Нет победителя!";
                            sd.AddParagraph(str, ErrorText);
                        }
                    }

                    for (int k = 0; k < playerCount; k++)
                    {
                        str = playerNames[k] + " - " + (tabControl1.TabPages[i].Tag as UNOOneGame).GetResult(k);
                        str += "   Побед - " + stageWins[k].ToString() + "   Закончил с \"0\" - " + stageZeros[k].ToString();
                        sd.AddParagraph(str, MainTextStyle);

                        statWins[k] += stageWins[k];
                        statZeros[k] += stageZeros[k];
                        statTotal[k] += (tabControl1.TabPages[i].Tag as UNOOneGame).GetResult(k);
                    }
                }

                sd.AddParagraph("Общий результат:", HeaderStyle);
                sd.AddParagraph("Всего раундов - " + RoundsTotal.ToString(), StageHeader);
                sd.AddParagraph("Максимум очков: " + maxValue.ToString() + "  Игрок: " + playerNames[maxVPlayer], OtherHeader);
                sd.AddParagraph("Этап: " + (tabControl1.TabPages[maxVStage].Tag as UNOOneGame).UNOStageName + "  Раунд: " + (maxVRound + 1).ToString(), OtherHeader);

                for (int k = 0; k < playerCount; k++)
                {
                    str = playerNames[k] + " - " + statTotal[k];
                    str += "   Побед - " + statWins[k].ToString() + "   Закончил с \"0\" - " + statZeros[k].ToString();
                    sd.AddParagraph(str, MainTextStyle);
                }
                if (!sd.Visible)
                {
                    sd.ShowDialog();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                if (MessageBox.Show("Сохранить проект?", "Проект не сохранен", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    saveProjectToolStripMenuItem_Click_1(sender, e);
                }
            }
        }
    }
}