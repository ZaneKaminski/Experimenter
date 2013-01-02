namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Xml;

    public static class ConfigParser
    {
        #region Methods

        public static double GetDouble(string val)
        {
            double setTo;

            if (val.EndsWith("W"))
            {
                setTo = System.Windows.SystemParameters.PrimaryScreenWidth /
                    Double.Parse(val.Substring(0, val.Length - 1));
            }
            else if (val.EndsWith("H"))
            {
                setTo = System.Windows.SystemParameters.PrimaryScreenHeight /
                    Double.Parse(val.Substring(0, val.Length - 1));
            }
            else
            {
                setTo = Double.Parse(val);
            }

            return setTo;
        }

        public static SlideCollection ParseConfig(string file, IExperimentContainer experiment)
        {
            SlideCollection retVal = new SlideCollection(experiment);

            XmlDocument xml = new XmlDocument();
            xml.Load(file);

            XmlElement document = xml.DocumentElement;

            try
            {
                retVal.Experiment.Output =
                    Boolean.Parse(document.Attributes["Output"].Value);
            }
            catch { }

            try
            {
                retVal.Experiment.OutputFile =
                    document.Attributes["OutputFile"].Value;
            }
            catch { }

            try
            {
                retVal.Experiment.StopExit =
                    Boolean.Parse(document.Attributes["StopExit"].Value);
            }
            catch { }

            XmlNode[] slides = document["Slides"].
                ChildNodes.Cast<XmlNode>().ToArray<XmlNode>();

            foreach (XmlNode slideNode in slides)
            {
                Slide slide = ConfigParser.GetSlideByType(
                    slideNode.Name, experiment);

                foreach (XmlAttribute attribute in slideNode.Attributes)
                {
                    ConfigParser.SetToValue(
                        slide, attribute.Name, attribute.Value, experiment);
                }

                try
                {
                    foreach (XmlNode node in slideNode["Events"])
                    {
                        slide.Events.AddEvent(
                            ConfigParser.GetRaisedEvent(node));
                    }
                }
                catch { }

                try
                {
                    foreach (XmlNode node in slideNode["SlideObjects"])
                    {
                        SlideObject slideObject = ConfigParser.GetSlideObject(
                            node.Name, slide);

                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            ConfigParser.SetToValue(slideObject,
                                attribute.Name, attribute.Value, experiment);
                        }

                        try
                        {
                            foreach (XmlNode eventNode in
                                node["Events"].ChildNodes)
                            {
                                slideObject.Events.AddEvent(
                                    ConfigParser.GetRaisedEvent(eventNode));
                            }
                        }
                        catch { }

                        slide.SlideObjects.Add(slideObject);
                    }
                }
                catch { }

                retVal.Add(slide);
            }

            try
            {
                retVal.SetSlideIndex(
                    Int32.Parse(document.Attributes["StartIndex"].Value));
            }
            catch { }

            return retVal;
        }

        private static RaisedEvent GetRaisedEvent(XmlNode node)
        {
            string[] calls = new string[node.ChildNodes.Count];

            for(int i = 0; i < node.ChildNodes.Count; i++)
            {
                calls[i] = node.ChildNodes[i].InnerText;
            }

            return new RaisedEvent(node.Attributes["Name"].Value, calls);
        }

        private static Slide GetSlideByType(string type, IExperimentContainer exp)
        {
            Slide retVal = null;

            ConstructorInfo ctor = Type.GetType(
                "Experimenter." + type, true, false).GetConstructor(
                    new Type[] { typeof(IExperimentContainer) });

            retVal = (Slide)ctor.Invoke(new object[] { exp });

            return retVal;
        }

        private static SlideObject GetSlideObject(string name, Slide parent)
        {
            SlideObject retVal;

            ConstructorInfo ctor = Type.GetType(
                "Experimenter." + name, true, false).GetConstructor(
                    new Type[] { typeof(Slide) });

            retVal = (SlideObject)ctor.Invoke(new object[] { parent });

            return retVal;
        }

        private static void SetToValue(object target, string name, string value, 
            IExperimentContainer experiment)
        {
            PropertyInfo property = target.GetType().GetProperty(name,
                BindingFlags.Instance |
                BindingFlags.Public);

            object setTo = null;

            switch (property.PropertyType.FullName)
            {
                case "System.Windows.Media.Color":
                    setTo = ColorConverter.ConvertFromString(value);
                    break;
                case "System.Windows.Media.Brush":
                    setTo = new BrushConverter().ConvertFrom(value);
                    break;
                case"System.Boolean":
                    setTo = Boolean.Parse(value);
                    break;
                case "System.Int16":
                    if (value.EndsWith("W"))
                    {
                        setTo = System.Windows.SystemParameters.PrimaryScreenWidth /
                            Int16.Parse(value.Substring(0, value.Length - 1));
                    }
                    else if (value.EndsWith("H"))
                    {
                        setTo = System.Windows.SystemParameters.PrimaryScreenHeight /
                            Int16.Parse(value.Substring(0, value.Length - 1));
                    }
                    else
                    {
                        setTo = Int16.Parse(value);
                    }
                    break;
                case "System.Int32":
                    if (value.EndsWith("W"))
                    {
                        setTo = System.Windows.SystemParameters.PrimaryScreenWidth /
                            Int32.Parse(value.Substring(0, value.Length - 1));
                    }
                    else if (value.EndsWith("H"))
                    {
                        setTo = System.Windows.SystemParameters.PrimaryScreenHeight /
                            Int32.Parse(value.Substring(0, value.Length - 1));
                    }
                    else
                    {
                        setTo = Int32.Parse(value);
                    }
                    break;
                case "System.Int64":
                    if (value.EndsWith("W"))
                    {
                        setTo = System.Windows.SystemParameters.PrimaryScreenWidth /
                            Int64.Parse(value.Substring(0, value.Length - 1));
                    }
                    else if (value.EndsWith("H"))
                    {
                        setTo = System.Windows.SystemParameters.PrimaryScreenHeight /
                            Int64.Parse(value.Substring(0, value.Length - 1));
                    }
                    else
                    {
                        setTo = Int64.Parse(value);
                    }
                    break;
                case "System.Float":
                    if (value.EndsWith("W"))
                    {
                        setTo = System.Windows.SystemParameters.PrimaryScreenWidth /
                            Single.Parse(value.Substring(0, value.Length - 1));
                    }
                    else if (value.EndsWith("H"))
                    {
                        setTo = System.Windows.SystemParameters.PrimaryScreenHeight /
                            Single.Parse(value.Substring(0, value.Length - 1));
                    }
                    else
                    {
                        setTo = Single.Parse(value);
                    }
                    break;
                case "System.Double":
                    setTo = ConfigParser.GetDouble(value);
                    break;
                case "System.String":
                    setTo = value;
                    break;
                case "System.Windows.Thickness":
                    if (value.StartsWith("~"))
                    {
                        string[] parts = value.Substring(1).Split(',');

                        setTo = new Thickness(
                            ConfigParser.GetDouble(parts[0]),
                            ConfigParser.GetDouble(parts[1]),
                            ConfigParser.GetDouble(parts[2]),
                            ConfigParser.GetDouble(parts[3]));
                    }
                    else
                    {
                        setTo = new ThicknessConverter().ConvertFromString(
                            value);
                    }
                    break;
                case "System.Windows.HorizontalAlignment":
                    setTo = Enum.Parse(typeof(HorizontalAlignment), value);
                    break;
                case "System.Windows.VerticalAlignment":
                    setTo = Enum.Parse(typeof(VerticalAlignment), value);
                    break;
                case "System.Windows.Controls.MediaElement":
                    setTo = new MediaElement()
                    {
                        Source = new Uri(value, UriKind.RelativeOrAbsolute)
                    };
                    break;
                case "System.Windows.Controls.Image":
                    BitmapImage image = new BitmapImage();
                    image.UriSource = 
                        new Uri(value, UriKind.RelativeOrAbsolute);
                    setTo = new Image() { Source = image };
                    break;
                default:
                    throw new ArgumentException("Nonsupported Type!");
            }

            property.SetValue(target, setTo, null);
        }

        #endregion Methods
    }
}