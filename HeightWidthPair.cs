namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HeightWidthPair
    {
        #region Fields

        private double? height;
        private double? width;

        #endregion Fields

        #region Constructors

        public HeightWidthPair()
        {
            this.Height = null;
        }

        public HeightWidthPair(double h, double w)
        {
            this.height = h;
            this.width = w;
        }

        #endregion Constructors

        #region Properties

        public double? Height
        {
            get { return this.height; }
            set
            {
                this.height = value;
                if (value == null)
                    this.width = null;

                if (value != null && this.width == null)
                    this.width = 0;
            }
        }

        public bool IsNull
        {
            get { return this.Width == null; }
        }

        public double? Width
        {
            get { return this.width; }
            set
            {
                this.width = value;
                if (value == null)
                    this.height = null;

                if (value != null && this.height == null)
                    this.height = 0;
            }
        }

        #endregion Properties
    }
}