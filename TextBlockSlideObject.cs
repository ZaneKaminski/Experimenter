namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class TextBlockSlideObject : SlideObject
    {
        #region Fields

        private TextBlock control;

        #endregion Fields

        #region Constructors

        public TextBlockSlideObject(Slide slide, Thickness margins)
            : this(slide, margins, "", 100, 20)
        {
        }

        public TextBlockSlideObject(Slide slide, Thickness margins, string text,
            double width, double height)
            : this(slide, margins, text, HorizontalAlignment.Left,
            VerticalAlignment.Top, width, height)
        {
        }

        public TextBlockSlideObject(Slide slide, Thickness margins, string text,
            HorizontalAlignment horAlign, VerticalAlignment verAlign,
            double width, double height)
            : this(slide)
        {
            this.control.Text = text;
            this.control.Height = height;
            this.control.Width = width;
            this.control.Margin = margins;
            this.control.HorizontalAlignment = horAlign;
            this.control.VerticalAlignment = verAlign;
        }

        public TextBlockSlideObject(Slide parent)
            : base(parent)
        {
            this.control = new TextBlock();
            this.Events.Sender = this.control;
        }

        #endregion Constructors

        #region Properties

        public override UIElement Control
        {
            get
            {
                return this.control;
            }
        }

        public double FontSize
        {
            get { return this.control.FontSize; }
            set { this.control.FontSize = value; }
        }

        public Brush Foreground
        {
            get { return this.control.Foreground; }
            set { this.control.Foreground = value; }
        }

        public double Height
        {
            get { return this.control.Height; }
            set { this.control.Height = value; }
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get { return this.control.HorizontalAlignment; }
            set { this.control.HorizontalAlignment = value; }
        }

        public Thickness Margins
        {
            get { return this.control.Margin; }
            set { this.control.Margin = value; }
        }

        public string Text
        {
            get { return this.control.Text; }
            set { this.control.Text = value; }
        }

        public override string Value
        {
            get { return this.control.Text; }
        }

        public VerticalAlignment VerticalAlignment
        {
            get { return this.control.VerticalAlignment; }
            set { this.control.VerticalAlignment = value; }
        }

        public double Width
        {
            get { return this.control.Width; }
            set { this.control.Width = value; }
        }

        #endregion Properties

        #region Methods

        public override void Loaded()
        {
        }

        #endregion Methods
    }
}