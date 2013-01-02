namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RaisedEvent
    {
        #region Constructors

        public RaisedEvent(string eventName, string[] calls)
        {
            this.Event = eventName;
            this.Calls = calls;
        }

        #endregion Constructors

        #region Properties

        public string[] Calls
        {
            get; private set;
        }

        public string Event
        {
            get; private set;
        }

        public RaisedEventCollection EventCollection
        {
            get; internal set;
        }

        public IExperimentContainer Experiment
        {
            get
            {
                return
                    this.EventCollection.ParentSlide.Experiment;
            }
        }

        public object Sender
        {
            get
            {
                return this.EventCollection.Sender;
            }
        }

        #endregion Properties

        #region Methods

        public void Execute(string eventName, object sender)
        {
            if (this.Event == eventName && this.Sender == sender)
            {
                foreach (string call in this.Calls)
                {
                    this.Experiment.Call(call);
                }
            }
        }

        #endregion Methods
    }
}