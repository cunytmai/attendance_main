using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CustomListBox
{
    public class NormalListBoxItem : ListBoxItemEx
    {
        #region Rank enum

        public enum Rank
        {
            Owner,
            Admin,
            Mod,
            Staff,
            Vanity,
            Student,
            Instructor,
            Positive,
            Negative
        }

        #endregion

        #region Status enum

        public enum Status
        {
            Online,
            Offline
        }

        #endregion

        public Rank[] TextRank = new Rank[] { };

        public Status status = Status.Offline;

        public NormalListBoxItem()
        {
            Height = 35;
        }

        public override int GetRank
        {
            get
            {
                try
                {
                    foreach (Rank r in TextRank)
                    {
                        switch (r)
                        {
                            case Rank.Owner:
                                return 0;
                            case Rank.Admin:
                                return 1;
                            case Rank.Instructor:
                                return 2;
                            case Rank.Student:
                                return 4;
                        }
                    }
                }
                catch { }
                return 3;
            }
        }

        public override int GetGroup
        {
            get
            {
                switch (status)
                {
                    case Status.Offline:
                        return 3;
                    case Status.Online:
                        return 1;
                }

                return 3;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            e.Graphics.DrawLine(new Pen(new SolidBrush(SelectedBackround)), new Point(0, Height - 1), new Point(Width - 1, Height - 1));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Backround)), new Point(0, Height - 2), new Point(Width, Height - 2));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Backround)), new Point(0, 0), new Point(Width, 0));
            e.Graphics.DrawString(TextTitle, new Font("Tahoma", 8f, FontStyle.Bold), new SolidBrush(TitleColor), 4f, 4f);
            e.Graphics.DrawString(TextStatus, new Font("Tahoma", 7f, FontStyle.Regular), new SolidBrush(StatusColor), 5f, 18f);
            //
            if (TextRank != null)
            {
                DrawRanks(e);
            }
        }

        private void DrawRanks(PaintEventArgs pe)
        {
            var currentMeasure = 3;
            foreach (var rank in TextRank)
            {
                switch (rank)
                {
                    case Rank.Admin:
                        var stsize = pe.Graphics.MeasureString("Admin", new Font("Verdana", 7f, FontStyle.Regular));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f);
                            currentMeasure += 4;
                        }
                        pe.Graphics.DrawString("Admin", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Red), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    case Rank.Mod:
                        stsize = pe.Graphics.MeasureString("Mod", new Font("Verdana", 7f, FontStyle.Regular));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f);
                            currentMeasure += 4;
                        }
                        pe.Graphics.DrawString("Mod", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.DarkGreen), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    case Rank.Staff:
                        stsize = pe.Graphics.MeasureString("Staff", new Font("Verdana", 7f, FontStyle.Regular));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f); currentMeasure += 4;
                        }
                        pe.Graphics.DrawString("Staff", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    case Rank.Instructor:
                        stsize = pe.Graphics.MeasureString("Instructor", new Font("Verdana", 7f, FontStyle.Regular));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f);
                            currentMeasure += 4;
                        }
                        pe.Graphics.DrawString("Instructor", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Red), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    case Rank.Vanity:
                        stsize = pe.Graphics.MeasureString(TextVanity, new Font("Verdana", 7f, FontStyle.Regular));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f);
                            currentMeasure += 4;
                        }
                        pe.Graphics.DrawString(TextVanity, new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Salmon), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    case Rank.Positive:
                        stsize = pe.Graphics.MeasureString("Positive", new Font("Verdana", 7f, FontStyle.Bold));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f);
                            currentMeasure += 4;
                        }
                        pe.Graphics.DrawString("Positive", new Font("Verdana", 7f, FontStyle.Bold), new SolidBrush(Color.DarkGreen), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    case Rank.Owner:
                        stsize = pe.Graphics.MeasureString("Owner", new Font("Verdana", 7f, FontStyle.Regular));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f);
                            currentMeasure += 4;
                        }
                        pe.Graphics.DrawString("Owner", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Purple), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    case Rank.Student:
                        stsize = pe.Graphics.MeasureString("Student", new Font("Verdana", 7f, FontStyle.Regular));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f);
                            currentMeasure += 4;
                        }
                        pe.Graphics.DrawString("Student", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Black), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    case Rank.Negative:
                        stsize = pe.Graphics.MeasureString("Negative", new Font("Verdana", 7f, FontStyle.Regular));
                        if (currentMeasure != 3)
                        {
                            pe.Graphics.DrawString("|", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.Gray), Width - currentMeasure - 6, 5f);
                            currentMeasure += 4; 
                        }
                        pe.Graphics.DrawString("Negative", new Font("Verdana", 7f, FontStyle.Regular), new SolidBrush(Color.DarkRed), Width - stsize.Width - currentMeasure, 5f);
                        currentMeasure += (int)stsize.Width;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}