namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Timers;
    using System.Threading;
    using System.IO;

    public class WebSlide : Slide
    {
        #region Fields

        private WebBrowser web;
        private System.Timers.Timer runTimer;
        private string url = null;

        #endregion Fields

        #region Constructors

        public WebSlide(IExperimentContainer experiment)
            : base(experiment)
        {
            this.RunTime = -1;
        }

        #endregion Constructors

        #region Properties

        public WebBrowser Web
        {
            get { return this.web; }
            set
            {
                this.web = value;
            }
        }

        public bool Repeat
        {
            get;
            set;
        }

        public double RunTime
        {
            get;
            set;
        }

        public string Url
        {
            get { return this.url; }
            set
            {
                int index = value.IndexOf("%C;");

                if (index != -1)
                {
                    this.url = Path.Combine(Path.Combine(
                        value.Substring(0, index), Environment.CurrentDirectory),
                        value.Substring(8 + 3));
                }
                else
                    this.url = value;
            }
        }

        #endregion Properties

        #region Methods

        public override void PostLoad()
        {
            try
            {
                this.Web.Navigate(new Uri(this.Url));

                if (this.RunTime != -1)
                {
                    this.runTimer = new System.Timers.Timer(this.RunTime);
                    this.runTimer.Elapsed += 
                        new ElapsedEventHandler(runTimer_Elapsed);
                    this.runTimer.Start();
                }
            }
            catch { }
        }

        void runTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new ThreadStart(delegate
                {
                    this.runTimer.Stop();
                    this.grid.Children.Clear();
                    this.Web.Dispose();

                    if (this.Active)
                        this.Experiment.Slides.NextSlide();
                }));
        }

        public override void PreLoad()
        {
            this.Web = new WebBrowser();

            this.Web.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.Web.VerticalAlignment = VerticalAlignment.Stretch;

            this.grid = new System.Windows.Controls.Grid();

            this.grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.grid.VerticalAlignment = VerticalAlignment.Stretch;

            this.grid.Children.Add(this.Web);
            this.Content = this.grid;
        }

        #endregion Methods
    }
}