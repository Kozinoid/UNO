using System;
using System.Collections.Generic;
using System.Text;

namespace UNO
{
    public class NameChangedEventArgs : EventArgs
    {
        public int Index = -1;
        public string NewName = "";

        public NameChangedEventArgs(string s, int n)
        {
            Index = n;
            NewName = s;
        }
    }

    public class ChangePlayerValueEventArgs : EventArgs
    {
        public int Index = -1;
        public int Value = 0;

        public ChangePlayerValueEventArgs(int plNum, int val)
        {
            Index = plNum;
            Value = val;
        }
    }

    public class ScrollVisiblityEventArgs : EventArgs
    {
        public bool visiblity = false;
        public ScrollVisiblityEventArgs(bool state)
        {
            visiblity = state;
        }
    }

    public class ChangeRoundValueEventArgs : EventArgs
    {
        public int rdIndex = -1;
        public int plIndex = -1;
        public int Value = 0;

        public ChangeRoundValueEventArgs(int rdNum, int plNum, int val)
        {
            rdIndex = rdNum;
            plIndex = plNum;
            Value = val;
        }
    }
}
