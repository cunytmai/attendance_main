using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Movements;

namespace CustomListBox
{
    public class ListBoxItemEx : Control
    {
        public static Color TitleColor = Color.Black;
        public static Color StatusColor = Color.DarkGray;
        public static Color Backround = Color.White;
        public static Color SelectedBackround = Color.FromArgb(225, 235, 255);
        public Trigger KeyArrowDownPress = new Trigger();
        public Trigger KeyArrowUpPress = new Trigger();
        private int LastMovement;
        private int MouseOffset;
        private int _lastswsecond;
        public Trigger SelectedChanged = new Trigger();
        private bool _Selected;
        private string _TextStatus = "";
        private string _TextTitle = "";
        private string _TextVanity = "";
        private bool isMouseDown;

        private Movement mov;

        public ListBoxItemEx()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            BackColor = Backround;
        }

        private int pLastMovement
        {
            get { return int.Parse(LastMovement + "0") * 4; }
        }

        public string TextTitle
        {
            get { return _TextTitle; }
            set
            {
                _TextTitle = value;
                Invalidate();
            }
        }

        public string TextStatus
        {
            get { return _TextStatus; }
            set
            {
                _TextStatus = value;
                Invalidate();
            }
        }

        public string TextVanity
        {
            get { return _TextVanity; }
            set
            {
                _TextVanity = value;
                Invalidate();
            }
        }

        public int Index { get; set; }

        public virtual int GetRank
        {
            get { return -1; }
        }

        public virtual int GetGroup
        {
            get { return 3; }
        }

        public virtual bool Selected
        {
            set
            {
                if (value) BackColor = SelectedBackround;
                else BackColor = Backround;
                _Selected = value;
                Invalidate();
                SelectedChanged.Execute(this, value);
            }
            get { return _Selected; }
        }
        Stopwatch sw = new Stopwatch();
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isMouseDown && sw != null)
            {
                int CurrentMousePoint = MouseUtilities.CorrectGetPosition().Y;
                if (CurrentMousePoint > MouseOffset)
                {
                    //down
                    var parent = (ListBoxEx)Parent;
                    int DownMove = (CurrentMousePoint - MouseOffset);
                    parent.ScrollTo(parent.ScrollCurrent - DownMove);
                    MouseOffset = MouseUtilities.CorrectGetPosition().Y;
                    LastMovement = DownMove / -1;
                }
                else if (CurrentMousePoint < MouseOffset)
                {
                    //up
                    var parent = (ListBoxEx)Parent;
                    int UpMove = (MouseOffset - CurrentMousePoint);
                    parent.ScrollTo(parent.ScrollCurrent + UpMove);
                    MouseOffset = MouseUtilities.CorrectGetPosition().Y;
                    LastMovement = UpMove;
                }
                _lastswsecond = Convert.ToInt32(sw.ElapsedMilliseconds);
            }
            base.OnMouseMove(e);
        }

        public void stop_test()
        {
            if (mov != null) mov.Stop();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseOffset = MouseUtilities.CorrectGetPosition().Y;
            isMouseDown = true;
            Selected = true;
            _lastswsecond = 0;
            sw = new Stopwatch();
            sw.Start();
            Worker.Start(stop_test);
            //TODO: Debug Auto-Scroll stopping method on MouseDown.
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {

            if (sw != null) sw.Stop();
            sw = null;
            isMouseDown = false;
            try
            {
                //TODO: make this work? 
                /*int time = 1;
                if (sw != null)
                {
                    time = (int)(sw.ElapsedMilliseconds) - lastswsecond;
                }
                ListBoxEx parent = (ListBoxEx)this.Parent;
                int velocity = LastMovement / time;
                Console.WriteLine(velocity);
                if (velocity > 1)
                {
                    mov = new Movements.Movement(Movements.Movement.MovementType.Deceleration, time*(100));
                    double dis = (velocity * time) + ((1 / 2) * (9.81 * (time ^ 2)));
                    mov.AddMovement(parent, "ScrollCurrent", Convert.ToInt32(parent.ScrollCurrent + dis * 100));
                }
                else if (velocity < -1)
                {
                    mov = new Movements.Movement(Movements.Movement.MovementType.Deceleration, time * (100));
                    double dis = (velocity * time) + ((1 / 2) * (-9.81 * (time ^ 2)));
                    mov.AddMovement(parent, "ScrollCurrent", Convert.ToInt32(parent.ScrollCurrent - (dis*100 / -1)));
                }
                //d=vi*t+1/2(at2)
                mov.Run();*/
            }
            catch
            {
            }
            base.OnMouseUp(e);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down)
            {
                return true;
            }
            else return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyData == Keys.Down)
            {
                KeyArrowDownPress.Execute(this, null);
            }
            else if (e.KeyData == Keys.Up)
            {
                KeyArrowUpPress.Execute(this, null);
            }
        }
    }

    public sealed class Trigger
    {
        #region Delegates

        public delegate void Delegate(object sender, object contents);

        #endregion

        private readonly List<object> SubscribingEvents = new List<object>();
        private event Delegate _Event;

        public event Delegate Event
        {
            add
            {
                _Event += value;
                SubscribingEvents.Add(value);
            }
            remove
            {
                _Event -= value;
                SubscribingEvents.Remove(value);
            }
        }

        public void Execute(object sender, object contents)
        {
            Delegate statusHandler = _Event;
            if (statusHandler != null)
            {
                statusHandler(sender, contents);
            }
        }

        public void UnsubscribeAllDelegates()
        {
            foreach (object evnt in SubscribingEvents)
            {
                _Event -= (Delegate)evnt;
            }
            SubscribingEvents.Clear();
        }
    }

    public static class MouseUtilities
    {
        public static Point CorrectGetPosition()
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        #region Nested type: Win32Point

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        #endregion
    }

    public class Worker
    {
        #region Delegates

        public delegate void Construct();

        public delegate void GeneralEventDelegate();

        #endregion

        [field: NonSerializedAttribute] private Construct asd;
        [field: NonSerializedAttribute] private Form frm;
        private bool isWorking;
        [field: NonSerializedAttribute] private Thread thr;

        private Worker()
        {
        }

        public bool Working
        {
            get { return isWorking; }
        }

        private Thread tStart(ThreadStart method)
        {
            var t = new Thread(method);
            t.Start();
            return t;
        }

        private void iInvoke(Form form, Action<Construct> action)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new ActionInvokingDelegate(Invoke), form, action);
                return;
            }
            action(null);
        }

        public static void Invoke(Form form, Action<Construct> action)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new ActionInvokingDelegate(Invoke), form, action);
                return;
            }
            action(null);
        }

        private void doMethod()
        {
            isWorking = true;
            GeneralEventDelegate statusHandler = WorkStarted;
            if (statusHandler != null)
            {
                statusHandler();
            }
            asd();
            isWorking = false;
            statusHandler = WorkFinnished;
            if (statusHandler != null)
            {
                statusHandler();
            }
        }

        private void threadstart()
        {
            doMethod();
        }

        public static Worker Start(Construct svoid, Form form, bool destroyThreadOnFormClose)
        {
            var w = new Worker();
            w.asd = svoid;
            w.frm = form;
            w.thr = w.tStart(w.threadstart);
            if (destroyThreadOnFormClose)
            {
                form.FormClosing += w.form_FormClosing;
            }
            return w;
        }

        public static Worker Start(Construct svoid, Form form)
        {
            var w = new Worker();
            w.asd = svoid;
            w.frm = form;
            w.thr = w.tStart(w.threadstart);
            return w;
        }

        public static Worker Start(Construct svoid)
        {
            var w = new Worker();
            w.asd = svoid;
            w.thr = w.tStart(w.threadstart);
            return w;
        }

        public event GeneralEventDelegate WorkStarted;
        public event GeneralEventDelegate WorkFinnished;

        public void Stop()
        {
            try
            {
                thr.Abort();
                isWorking = false;
            }
            catch
            {
            }
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        #region Nested type: ActionInvokingDelegate

        private delegate void ActionInvokingDelegate(Form frm, Action<Construct> action);

        #endregion
    }
}