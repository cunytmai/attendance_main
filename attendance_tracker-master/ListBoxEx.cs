using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CustomListBox
{
    public class ListBoxEx : UserControl
    {
        public List<ListBoxItemEx> Items { get { return _Items; } }

        private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int SB_THUMBPOSITION = 4;
        private readonly Dictionary<int, bool> _HiddenGroups = new Dictionary<int, bool>();
        private List<ListBoxItemEx> _Items = new List<ListBoxItemEx>();
        private ListBoxItemEx _SelectedItem;
        private int _Top = 1;

        public ListBoxEx()
        {
            ControlAdded += CustomListBox_ControlAdded;
            ControlRemoved += CustomListBox_ControlRemoved;
            Layout += CustomListBox_Layout;
            InitializeComponent();
        }

        public int ScrollCurrent
        {
            get { return GetScrollPos((int)Handle, 0x1); }
            set
            {
                SetScrollPos(Handle, 0x1, value, true);
                PostMessageA(Handle, 0x115, 4 + 0x10000 * value, 0);
            }
        }

        public ListBoxItemEx this[int index]
        {
            get { return Controls[index] as ListBoxItemEx; }
        }

        public ListBoxItemEx this[string title]
        {
            get
            {
                foreach (Control kid in Controls)
                {
                    var child = kid as ListBoxItemEx;
                    if (child != null)
                    {
                        if (child.TextTitle.ToLower() == title.ToLower())
                        {
                            return child;
                        }
                    }
                }
                return null;
            }
        }

        public int this[ListBoxItemEx item]
        {
            get { return item.Index; }
        }

        public int Count
        {
            get { return Controls.Count; }
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            SuspendLayout();
            BackColor = ListBoxItemEx.Backround;
            Name = "CustomContainerControl";
            Size = new Size(632, 308);
            Load += CustomListBox_Load;
            AutoScroll = false;
            BackColor = ListBoxItemEx.Backround;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            ResumeLayout(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawLine(new Pen(new SolidBrush(ListBoxItemEx.SelectedBackround)), new Point(0, 0),
                                new Point(Width, 0));
        }

        [DllImport("user32.dll")]
        public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollPos(int hWnd, int nBar);

        [DllImport("user32.dll")]
        private static extern bool PostMessageA(IntPtr hWnd, int nBar, int wParam, int lParam);

        protected override void AdjustFormScrollbars(bool displayScrollbars)
        {
            base.AdjustFormScrollbars(displayScrollbars);
            ShowScrollBar(Handle, 0, false);
        }

        public void ScrollTo(int Position)
        {
            int percent = Position;
            SetScrollPos(Handle, 0x1, percent, true);
            PostMessageA(Handle, 0x115, 4 + 0x10000 * percent, 0);
        }

        public void SortList()
        {
            List<ListBoxItemEx> list =
                _Items.OrderBy(item => item.GetGroup).ThenBy(item => item.GetRank).ThenBy(item => item.TextTitle).ToList
                    ();
            _Items = list;
            ReDrawItems();
        }

        private void CustomListBox_Layout(object sender, LayoutEventArgs e)
        {
            SuspendLayout();
            Rectangle rect = RectangleToScreen(ClientRectangle);
            int num2 = Controls.Count - 1;
            for (int i = 0; i <= num2; i++)
            {
                if (VScroll)
                {
                    Controls[i].Width = Width - 17;
                }
                else
                {
                    Controls[i].Width = Width;
                }
            }
            ResumeLayout();
        }

        private void CustomListBox_ControlRemoved(object sender, ControlEventArgs e)
        {
        }

        private void CustomListBox_ControlAdded(object sender, ControlEventArgs e)
        {
        }

        private void ChildSelected(object sender, object contents)
        {
            if ((Boolean)contents)
            {
                if (_SelectedItem != null && _SelectedItem != sender as ListBoxItemEx)
                {
                    _SelectedItem.Selected = false;
                }
                _SelectedItem = sender as ListBoxItemEx;
                ScrollControlIntoView(_SelectedItem);
            }
        }

        private void CustomListBox_Load(object sender, EventArgs e)
        {
        }

        public void Add(ListBoxItemEx item)
        {
            item.Size = new Size(Width, item.Size.Height);
            item.Location = new Point(0, _Top);
            if (_Items.Contains(item))
            {
            }
            else
            {
                _Items.Add(item);
            }
            int index = Controls.Count;
            item.Index = index;
            Controls.Add(item);
            _Top += item.Height;
            item.SelectedChanged.UnsubscribeAllDelegates();
            item.SelectedChanged.Event += ChildSelected;
            item.KeyArrowDownPress.UnsubscribeAllDelegates();
            item.KeyArrowDownPress.Event += (a, b) =>
            {
                try
                {
                    Select(this[_SelectedItem.Index + 1]);
                }
                catch
                {
                }
            };
            item.KeyArrowUpPress.UnsubscribeAllDelegates();
            item.KeyArrowUpPress.Event += (a, b) =>
            {
                try
                {
                    Select(this[_SelectedItem.Index - 1]);
                }
                catch
                {
                }
            };
            SortList();
        }

        public void Add(ListBoxItemEx item, bool sort)
        {
            if (_HiddenGroups.ContainsKey(item.GetGroup))
            {
                if (_HiddenGroups[item.GetGroup])
                {
                    return;
                }
            }
            item.Size = new Size(Width, item.Size.Height);
            item.Location = new Point(0, _Top);
            if (_Items.Contains(item))
            {
            }
            else
            {
                _Items.Add(item);
            }
            int index = Controls.Count;
            item.Index = index;
            Controls.Add(item);
            _Top += item.Height;
            item.SelectedChanged.UnsubscribeAllDelegates();
            item.SelectedChanged.Event += ChildSelected;
            item.KeyArrowDownPress.UnsubscribeAllDelegates();
            item.KeyArrowDownPress.Event += (a, b) =>
            {
                try
                {
                    Select(this[_SelectedItem.Index + 1]);
                }
                catch
                {
                }
            };
            item.KeyArrowUpPress.UnsubscribeAllDelegates();
            item.KeyArrowUpPress.Event += (a, b) =>
            {
                try
                {
                    Select(this[_SelectedItem.Index - 1]);
                }
                catch
                {
                }
            };
            if (sort) SortList();
        }

        public void Clear()
        {
            SuspendLayout();
            _Top = 1;
            Controls.Clear();
            _Items.Clear();
            ResumeLayout();
        }

        public void Remove(ListBoxItemEx item)
        {
            _Items.Remove(item);
            ReDrawItems();
        }

        public void Select(ListBoxItemEx item)
        {
            item.Selected = true;
        }

        public void HideGroup(int group, bool hidden)
        {
            if (_HiddenGroups.ContainsKey(group))
            {
                _HiddenGroups[group] = hidden;
            }
            else
            {
                _HiddenGroups.Add(group, hidden);
            }
        }

        public void ReDrawItems()
        {
            SuspendLayout();
            _Top = 1;
            Controls.Clear();
            foreach (ListBoxItemEx item in _Items)
            {
                Add(item, false);
            }
            ResumeLayout();
        }

        private void ReCalculateItemsHeight()
        {
            int calc = 1;
            foreach (Control cnt in Controls)
            {
                calc += cnt.Height;
            }
            _Top = calc;
        }
    }
}