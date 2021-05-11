using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class StatisticsDlg : Form
    {
        List<UNOStatStyle> stylesCollection = new List<UNOStatStyle>();
        List<UNOStatParagraph> paragraphsCollection = new List<UNOStatParagraph>();

        public StatisticsDlg()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.MouseWheel += new MouseEventHandler(StatisticsDlg_MouseWheel);
        }

        void StatisticsDlg_MouseWheel(object sender, MouseEventArgs e)
        {
            if (vScrollBar1.Visible)
            {
                int delta = (e.Delta > 0) ? 10 : -10;
                int newVal = vScrollBar1.Value - delta;

                if (newVal < vScrollBar1.Minimum) newVal = vScrollBar1.Minimum;
                if (newVal > vScrollBar1.Maximum) newVal = vScrollBar1.Maximum;
                vScrollBar1.Value = newVal;
                ArrangeParagraphs();
                Refresh();
            }
        }

        public void ClearStyles()
        {
            stylesCollection.Clear();
        }

        public int AddNewStyle(float fontSize, Color color, UNOTextAlign align)
        {
            UNOStatStyle style = new UNOStatStyle(fontSize, color, align);
            stylesCollection.Add(style);
            //return stylesCollection.Count - 1;
            return stylesCollection.IndexOf(style);
        }

        public void ClearParagraphs()
        {
            paragraphsCollection.Clear();
            vScrollBar1.Visible = false;
            vScrollBar1.Value = 0;
        }

        public void AddParagraph(string text, int index)
        {
            UNOStatParagraph usp = new UNOStatParagraph(text, stylesCollection[index], this.ClientRectangle.Width - 100, Graphics.FromHwnd(this.Handle));
            paragraphsCollection.Add(usp);
            int height = 0;
            for (int i = 0; i < paragraphsCollection.Count; i++)
            {
                height += paragraphsCollection[i].AreaRectangle.Height;
            }
            if (height > this.ClientRectangle.Height)
            {
                vScrollBar1.Minimum = 0;
                vScrollBar1.Maximum = height;
                vScrollBar1.Visible = true;
            }
            ArrangeParagraphs();
            Refresh();
        }

        private void ArrangeParagraphs()
        {
            int y = -vScrollBar1.Value;
            for (int i = 0; i < paragraphsCollection.Count; i++)
            {
                paragraphsCollection[i].AreaRectangle.X = 50;
                paragraphsCollection[i].AreaRectangle.Y = y;
                y += paragraphsCollection[i].AreaRectangle.Height;
            }
        }

        private void StatisticsDlg_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            for (int i = 0; i < paragraphsCollection.Count; i++)
            {
                gr.DrawString(paragraphsCollection[i].Text, paragraphsCollection[i].Style.TextFont, new SolidBrush(paragraphsCollection[i].Style.TextColor),
                    paragraphsCollection[i].AreaRectangle, paragraphsCollection[i].Style.SFormat);
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            ArrangeParagraphs();
            Refresh();
        }
    }

    public enum UNOTextAlign { left, center, right };

    public class UNOStatStyle
    {
        public Font TextFont = null;
        public Color TextColor;
        public StringFormat SFormat = new StringFormat();

        public UNOStatStyle(float fontSize, Color color, UNOTextAlign align)
        {
            TextFont = UNOFonts.GetUNOFont(fontSize);
            TextColor = color;
            SFormat.LineAlignment = StringAlignment.Center;

            switch (align)
            {
                case UNOTextAlign.left:
                    SFormat.Alignment = StringAlignment.Near;
                    break;
                case UNOTextAlign.center:
                    SFormat.Alignment = StringAlignment.Center;
                    break;
                case UNOTextAlign.right:
                    SFormat.Alignment = StringAlignment.Far;
                    break;
            }
        }
    }

    public class UNOStatParagraph
    {
        public string Text = "";
        public UNOStatStyle Style = null;
        public Rectangle AreaRectangle;

        public UNOStatParagraph(string text, UNOStatStyle style, int width, Graphics gr)
        {
            Text = text;
            Style = style;
            SizeF sz = gr.MeasureString(text, style.TextFont, width, style.SFormat);
            AreaRectangle = new Rectangle(0, 0, width, (int)sz.Height);
        }
    }
}