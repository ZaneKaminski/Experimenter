namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public abstract class Slide : UserControl
    {
        #region Fields

        protected Grid grid = null;

        #endregion Fields

        #region Constructors

        public Slide(IExperimentContainer experiment)
        {
            this.SlideObjects = new List<SlideObject>();
            this.Experiment = experiment;
            this.Events = new RaisedEventCollection(this);
            this.Events.Sender = this.Experiment;
            this.Loaded += new RoutedEventHandler(Slide_Loaded);
            this.UseHeightWidth = false;
        }

        #endregion Constructors

        #region Events

        public event EventHandler NextSlide;

        #endregion Events

        #region Properties

        public RaisedEventCollection Events
        {
            get; private set;
        }

        public IExperimentContainer Experiment
        {
            get; private set;
        }

        public DateTime Shown
        {
            get; private set;
        }

        public List<SlideObject> SlideObjects
        {
            get; private set;
        }

        public bool UseHeightWidth
        {
            get; set;
        }

        public bool Active
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public void OnNextSlide(object sender, EventArgs e)
        {
            this.NextSlide(sender, e);
        }

        public virtual void PostLoad()
        {
        }

        public virtual void PreLoad()
        {
        }

        public void RaiseEvent(object sender, RoutedEventArgs e)
        {
            this.Events.RaiseAll(e.RoutedEvent.Name, sender);
            foreach (SlideObject slideObject in this.SlideObjects)
            {
                slideObject.Events.RaiseAll(e.RoutedEvent.Name, sender);
            }
        }

        void Slide_Loaded(object sender, RoutedEventArgs e)
        {
            this.PreLoad();

            if (this.grid == null)
                this.grid = new Grid();

            this.grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.grid.VerticalAlignment = VerticalAlignment.Stretch;

            this.Content = this.grid;

            UIElement focus = null;

            foreach (SlideObject obj in this.SlideObjects)
            {
                this.grid.Children.Add(obj.Control);
                if (focus == null && obj.Control is TextBoxBase)
                {
                    focus = obj.Control;
                }
            }

            this.KeyDown += new System.Windows.Input.KeyEventHandler(RaiseEvent);
            this.KeyUp += new System.Windows.Input.KeyEventHandler(RaiseEvent);

            this.Shown = DateTime.Now;

            if (this.SlideObjects.Count > 0 && focus == null)
                this.SlideObjects[0].Control.Focus();
            else
                this.Focus();

            try
            {
                KeyEventArgs args = new KeyEventArgs(Keyboard.PrimaryDevice,
                    Keyboard.PrimaryDevice.ActiveSource, 0, Key.Tab);
                args.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(args);
                this.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            }
            catch { }

            this.PostLoad();
        }

        #endregion Methods
    }
}