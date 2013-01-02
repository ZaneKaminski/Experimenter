namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    public class ImageSlide : Slide
    {
        #region Fields

        private Image media;

        #endregion Fields

        #region Constructors

        public ImageSlide(IExperimentContainer experiment)
            : base(experiment)
        {
            this.media = new Image();
        }

        #endregion Constructors

        #region Properties

        public Image Media
        {
            get { return this.media; }
            set
            {
                this.media = value;
                //this.Media.Position = new TimeSpan(0, 0, 25);
            }
        }

        public bool Repeat
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override void PreLoad()
        {
            this.Media.Width = 500;
            this.Media.Height = 500;
            this.grid = new System.Windows.Controls.Grid();
            this.grid.Children.Add(this.Media);
            this.Content = this.grid;
        }

        #endregion Methods
    }
}