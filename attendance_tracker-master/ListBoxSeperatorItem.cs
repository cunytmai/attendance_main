using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CustomListBox
{
    internal class ListBoxSeparatorItem : ListBoxItemEx
    {
        #region Status enum

        public enum Status
        {
            Online,
            Offline
        }

        #endregion

        private bool _expanded = true;

        public Status status;

        public ListBoxSeparatorItem(Status sts)
        {
            status = sts;
            Height = 20;
        }

        private string SeparatorSource
        {
            get
            {
                string bmp =
                    @"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";
                return bmp;
            }
        }

        private Image SeparatorImage
        {
            get
            {
                Image img;
                byte[] bitmapBytes = Convert.FromBase64String(SeparatorSource);
                using (var memoryStream = new MemoryStream(bitmapBytes))
                {
                    img = Image.FromStream(memoryStream);
                }
                return img;
            }
        }

        public override int GetGroup
        {
            get
            {
                switch (status)
                {
                    case Status.Offline:
                        return 2;
                    case Status.Online:
                        return 0;
                }

                return 0;
            }
        }

        public bool Expanded
        {
            get { return _expanded; }
            set { _expanded = value; }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            if (Expanded)
            {
                Expanded = false;
                var lst = Parent as ListBoxEx;
                lst.HideGroup(GetGroup + 1, true);
                lst.ReDrawItems();
            }
            else if (Expanded == false)
            {
                Expanded = true;
                var lst = Parent as ListBoxEx;
                lst.HideGroup(GetGroup + 1, false);
                lst.ReDrawItems();
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.Default;
            e.Graphics.InterpolationMode = InterpolationMode.Default;
            e.Graphics.SmoothingMode = SmoothingMode.Default;
            base.OnPaint(e);
            e.Graphics.DrawImage(SeparatorImage, ClientRectangle);
            e.Graphics.DrawLine(new Pen(new SolidBrush(SelectedBackround)), new Point(0, Height - 1), new Point(Width - 1, Height - 1));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Backround)), new Point(0, Height - 2), new Point(Width, Height - 2));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Backround)), new Point(0, 0), new Point(Width, 0));
            var strFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString(TextTitle, new Font("Tahoma", 7f, FontStyle.Regular), Brushes.White, ClientRectangle, strFormat);
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (_expanded)
                e.Graphics.FillPolygon(new SolidBrush(SelectedBackround),
                                       new[]
                                           {
                                               new Point(((Width/2) + 15), 6), new Point(((Width/2) + 25), 6),
                                               new Point(((Width/2) + 20), Height - 6)
                                           });
            else
                e.Graphics.FillPolygon(new SolidBrush(SelectedBackround),
                                       new[]
                                           {
                                               new Point(((Width/2) + 15), Height - 7),
                                               new Point(((Width/2) + 25), Height - 7), new Point(((Width/2) + 20), 5)
                                           });
        }
    }
}