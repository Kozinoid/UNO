using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace UNO
{
    public class UNOMenuItem
    {
        protected string nameField;
        protected Rectangle rectArea;
        protected Color nameColor = Color.Blue;

        public string NameField
        {
            set
            {
                nameField = value;
            }
            get
            {
                return nameField;
            }
        }
        public Rectangle AllRectArea
        {
            set
            {
                rectArea = value;
            }
            get
            {
                return rectArea;
            }
        }
        public Rectangle AllShadowRectArea
        {
            get
            {
                Rectangle rect = new Rectangle(new Point(rectArea.X + 1, rectArea.Y + 1), rectArea.Size);
                return rect;
            }
        }
        public Color NameColor
        {
            set
            {
                nameColor = value;
            }
            get
            {
                return nameColor;
            }
        }

        public UNOMenuItem(string name, Rectangle rect, Color color)
        {
            nameField = name;
            rectArea = rect;
            nameColor = color;
        }
    }

    public class UNOMenu2ColumnsItem : UNOMenuItem
    {
        protected string valueField;
        protected Color valueColor = Color.Gray;

        public string ValueField
        {
            set
            {
                valueField = value;
            }
            get
            {
                return valueField;
            }
        }
        public Rectangle NameRectArea
        {
            get
            {
                Rectangle rect = new Rectangle(rectArea.Location, new Size(rectArea.Width / 2, rectArea.Height));
                return rect;
            }
        }
        public Rectangle NameShadowRectArea
        {
            get
            {
                Rectangle rect = new Rectangle(new Point(rectArea.X + 1, rectArea.Y + 1), new Size(rectArea.Width / 2, rectArea.Height));
                return rect;
            }
        }
        public Rectangle ValueRectArea
        {
            get
            {
                Rectangle rect = new Rectangle(new Point(rectArea.Width / 2, rectArea.Y), new Size(rectArea.Width / 2, rectArea.Height));
                return rect;
            }
        }
        public Rectangle ValueShadowRectArea
        {
            get
            {
                Rectangle rect = new Rectangle(new Point(rectArea.Width / 2 + 1, rectArea.Y + 1), new Size(rectArea.Width / 2, rectArea.Height));
                return rect;
            }
        }
        public Color ValueColor
        {
            set
            {
                valueColor = value;
            }
            get
            {
                return valueColor;
            }
        }

        public UNOMenu2ColumnsItem(string name, string val, Rectangle rect, Color color)
            : base(name, rect, color)
        {
            valueField = val;
        }
    }
}
