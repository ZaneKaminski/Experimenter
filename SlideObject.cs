namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class SlideObject
    {
        #region Constructors

        public SlideObject(Slide parent)
        {
            this.Parent = parent;
            this.Events = new RaisedEventCollection(this.Control, this.Parent);
            this.Parent.Loaded += new RoutedEventHandler(Parent_Loaded);
        }

        #endregion Constructors

        #region Properties

        public virtual UIElement Control
        {
            get; private set;
        }

        public RaisedEventCollection Events
        {
            get; private set;
        }

        public virtual string MediaSlide
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                //sb.Append(this.Name);
                //sb.Append(": ");
                sb.Append(this.Value);
                sb.Append(',');

                return sb.ToString();
            }
        }

        public string Name
        {
            get; set;
        }

        public bool Output
        {
            get; set;
        }

        public Slide Parent
        {
            get; private set;
        }

        public virtual string Value
        {
            get
            {
                return "null";
            }
        }

        #endregion Properties

        #region Methods

        public abstract void Loaded();

        protected void RaiseEvent(object sender, RoutedEventArgs e)
        {
            this.Events.RaiseAll(e.RoutedEvent.Name, sender);
        }

        void Parent_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded();
        }

        #endregion Methods
    }
}