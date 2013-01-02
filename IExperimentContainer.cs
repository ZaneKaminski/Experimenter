namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IExperimentContainer
    {
        #region Properties

        double Height
        {
            get; set;
        }

        bool Output
        {
            get; set;
        }

        string OutputFile
        {
            get; set;
        }

        SlideCollection Slides
        {
            get;
        }

        bool StopExit
        {
            get; set;
        }

        double Width
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        void Call(string call);

        void ChangeSlide(Slide slide);

        void CloseWindow();

        #endregion Methods
    }
}