namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IExperimentContainer
    {
        #region Constructors

        public MainWindow()
        {
            InitializeComponent();

            /*this.Slides = new SlideCollection(this);

            ColorSlide color = new ColorSlide(this, Colors.LightBlue);

            color.Events.AddEvent(new RaisedEvent(
                "KeyUp", new string[] { "NextSlide" }));

            ButtonSlideObject button = new ButtonSlideObject(
                color, new Thickness(100, 100, 0, 0), "Button! :)", 100, 20);

            button.Events.AddEvent(new RaisedEvent(
                "Click", new string[] { "MessageBox(Hello)" }));

            color.SlideObjects.Add(button);

            this.Slides.Add(color);*/
            this.OutputFile = "Output.txt";
            this.Output = false;
            this.Slides = ConfigParser.ParseConfig("Config.xml", this);
        }

        #endregion Constructors

        #region Properties

        public bool Output
        {
            get; set;
        }

        public string OutputFile
        {
            get; set;
        }

        public SlideCollection Slides
        {
            get; private set;
        }

        public bool StopExit
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public void Call(string call)
        {
            string name = call.Split('(')[0];

            string[] args = new string[0];

            try
            {
                args = call.Split('(')[1].Replace(")", "").Split(',');
            }
            catch { }

            switch (name)
            {
                case "MessageBox":
                    MessageBox.Show(ScriptHandler.GetArgumentValue(
                        args[0], this.Slides[this.Slides.CurrentSlideIndex]));
                    break;
                case "NextSlide":
                    this.Slides.NextSlide();
                    break;
                case "SetOutput":
                    this.OutputFile = ScriptHandler.GetArgumentValue(
                        args[0], this.Slides[this.Slides.CurrentSlideIndex]);
                    break;
            }
        }

        public void ChangeSlide(Slide slide)
        {
            this.Content = slide;
            if (!slide.UseHeightWidth)
            {
                this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                this.VerticalContentAlignment = VerticalAlignment.Stretch;
            }
            else
            {
                this.HorizontalContentAlignment = HorizontalAlignment.Center;
                this.VerticalContentAlignment = VerticalAlignment.Center;
            }
            /*slide.Effect = new System.Windows.Media.Effects.DropShadowEffect()
            {
                BlurRadius = 25,
                Color = Colors.Red,
                Opacity = 1.0,
                ShadowDepth = 0
            };*/
        }

        public void CloseWindow()
        {
            this.StopExit = false;
            this.Close();
        }

        private void RaiseEvent(object sender, RoutedEventArgs e)
        {
            this.Slides[this.Slides.CurrentSlideIndex].Events.RaiseAll(
                e.RoutedEvent.Name, sender);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.StopExit)
                e.Cancel = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Slides.NextSlide();
        }

        #endregion Methods
    }
}