namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class ColorSlide : Slide
    {
        #region Constructors

        public ColorSlide(IExperimentContainer experiment, Color color)
            : base(experiment)
        {
            this.Color = color;
        }

        public ColorSlide(IExperimentContainer experiment)
            : base(experiment)
        {
        }

        #endregion Constructors

        #region Properties

        public Color Color
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public override void PreLoad()
        {
            this.Background = new SolidColorBrush(this.Color);
        }

        #endregion Methods
    }
}