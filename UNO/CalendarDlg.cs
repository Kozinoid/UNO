using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class CalendarDlg : Form
    {
        public DateTime PickedDate
        {
            get
            {
                return monthCalendar1.SelectionStart;
            }
        }

        public CalendarDlg()
        {
            InitializeComponent();
        }
    }
}