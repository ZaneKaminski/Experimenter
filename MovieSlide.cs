namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    public class MediaSlide : Slide
    {
        #region Fields

        private MediaElement media;

        #endregion Fields

        #region Constructors

        public MediaSlide(IExperimentContainer experiment)
            : base(experiment)
        {
            this.media = new MediaElement();
            this.Media.MediaEnded += new RoutedEventHandler(Media_MediaEnded);
        }

        #endregion Constructors

        #region Properties

        public MediaElement Media
        {
            get { return this.media; }
            set
            {
                this.media = value;
                this.Media.MediaEnded +=
                    new RoutedEventHandler(Media_MediaEnded);
                this.Media.LoadedBehavior = MediaState.Manual;
                this.Media.UnloadedBehavior = MediaState.Manual;
                //this.Media.Position = new TimeSpan(0, 0, 25);
            }
        }

        public bool Repeat
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override void PostLoad()
        {
            try
            {
                this.Media.Play();
            }
            catch { }
        }

        public override void PreLoad()
        {
            this.grid = new System.Windows.Controls.Grid();
            this.grid.Children.Add(this.Media);
            this.Content = this.grid;
        }

        void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            //this.RaiseEvent(this.Experiment, e);
            if (this.Repeat)
            {
                this.Media.Position = TimeSpan.Zero;
                this.Media.Play();
            }
        }

        #endregion Methods
    }
}