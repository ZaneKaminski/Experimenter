namespace Experimenter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RaisedEventCollection : List<RaisedEvent>
    {
        #region Constructors

        public RaisedEventCollection(object sender, Slide slide)
        {
            this.Sender = sender;
            this.ParentSlide = slide;
        }

        public RaisedEventCollection(Slide slide)
            : this(slide.Parent, slide)
        {
        }

        #endregion Constructors

        #region Properties

        public Slide ParentSlide
        {
            get; set;
        }

        public object Sender
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public void AddEvent(RaisedEvent raisedEvent)
        {
            raisedEvent.EventCollection = this;
            this.Add(raisedEvent);
        }

        public void RaiseAll(string eventName, object sender)
        {
            foreach (RaisedEvent raisedEvent in this)
            {
                raisedEvent.Execute(eventName, sender);
            }
        }

        #endregion Methods
    }
}