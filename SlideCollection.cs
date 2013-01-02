namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows;

    public class SlideCollection : List<Slide>
    {
        #region Constructors

        public SlideCollection(IExperimentContainer container)
        {
            this.Experiment = container;
            this.CurrentSlideIndex = -1;
        }

        #endregion Constructors

        #region Properties

        public int CurrentSlideIndex
        {
            get; private set;
        }

        public IExperimentContainer Experiment
        {
            get; private set;
        }

        public string Output
        {
            get; private set;
        }

        #endregion Properties

        #region Methods

        public void NextSlide()
        {
            if (this.CurrentSlideIndex == -1)
            {
                foreach (Slide slide in this)
                {
                    slide.NextSlide += new EventHandler(slide_NextSlide);
                    slide.Active = true;
                }
            }

            //this.Output += "Slide " + (this.CurrentSlideIndex + 1).ToString()
             //   + Environment.NewLine;

            if (this.CurrentSlideIndex < this.Count &&
                this.CurrentSlideIndex >= 0)
            {
                this.Output +=
                    //"Time: " +
                    (DateTime.Now - this[this.CurrentSlideIndex].Shown).
                        TotalMilliseconds + ",";// +

                foreach (SlideObject slideObject in
                    this[this.CurrentSlideIndex].SlideObjects)
                {
                    if (slideObject.Output)
                        this.Output += slideObject.MediaSlide;
                }

                this.Output += Environment.NewLine;
                try
                {
                    this[this.CurrentSlideIndex - 1].Active = false;
                    this[this.CurrentSlideIndex].Active = true;
                }
                catch { }
            }

            this.Output += Environment.NewLine;

            this.CurrentSlideIndex++;

            if (this.CurrentSlideIndex >= this.Count)
            {
                if (this.Experiment.Output)
                {

                    StreamWriter writer = null;
                    try
                    {
                        writer = new StreamWriter(this.Experiment.OutputFile);

                        writer.WriteLine(this.Output);
                    }
                    catch
                    {
                        MessageBox.Show("Result output not successful.");
                    }
                    finally
                    {
                        writer.Close();
                        writer.Dispose();
                    }
                }
                this.Experiment.CloseWindow();
            }
            else
            {
                this.Experiment.ChangeSlide(this[this.CurrentSlideIndex]);
            }
        }

        public void SetSlideIndex(int index)
        {
            if (index < 0)
                return;

            this.CurrentSlideIndex = index - 2;
            this.NextSlide();
        }

        void slide_NextSlide(object sender, EventArgs e)
        {
            this.NextSlide();
        }

        #endregion Methods
    }
}