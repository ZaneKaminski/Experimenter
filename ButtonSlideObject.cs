namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ButtonSlideObject : SlideObject
    {
        #region Fields

        private Button control;

        #endregion Fields

        #region Constructors

        public ButtonSlideObject(Slide parent, Thickness margins,
            string text)
            : this(parent, margins, text, 20, 100)
        {
        }

        public ButtonSlideObject(Slide parent, Thickness margins,
            string text, double width, double height)
            : this(parent, margins, text,
            width, height, HorizontalAlignment.Left, VerticalAlignment.Top)
        {
        }

        public ButtonSlideObject(Slide parent, Thickness margins, 
            string text, double width, double height,
            HorizontalAlignment horAlign, VerticalAlignment verAlign)
            : this(parent)
        {
            this.control.Content = text;
            this.control.Height = height;
            this.control.Width = width;
            this.control.Margin = margins;
            this.control.HorizontalAlignment = horAlign;
            this.control.VerticalAlignment = verAlign;
        }

        public ButtonSlideObject(Slide parent)
            : base(parent)
        {
            this.control = new Button();
            this.Events.Sender = this.control;
        }

        #endregion Constructors

        #region Properties

        public int ClickCount
        {
            get; private set;
        }

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
            get { return (string)this.control.Content; }
            set { this.control.Content = value; }
        }

        public override string Value
        {
            get
            {
                return this.ClickCount.ToString();
            }
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
            this.control.Click += new RoutedEventHandler(control_Click);
        }

        void control_Click(object sender, RoutedEventArgs e)
        {
            this.ClickCount++;
            this.RaiseEvent(sender, e);
        }

        #endregion Methods
    }
}