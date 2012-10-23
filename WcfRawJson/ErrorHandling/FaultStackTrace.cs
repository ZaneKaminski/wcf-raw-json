using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace WcfRawJson.ErrorHandling
{
    /// <summary>
    /// Represents a stack trace, which is an ordered collection of 
    /// one or more <see cref="WcfRawJson.ErrorHandling.FaultStackFrame"/>s.
    /// </summary>
    [DataContract]
    public class FaultStackTrace : IEnumerable, IEnumerable<FaultStackFrame>
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the 
        /// <see cref="WcfRawJson.ErrorHandling.FaultStackTrace"/> class.
        /// </summary>
        /// <param name="callStack">
        /// The <see cref="System.Diagnostics.StackTrace"/> object from which to derive this
        /// <see cref="WcfRawJson.ErrorHandling.FaultStackTrace"/>.
        /// </param>
        public FaultStackTrace(StackTrace callStack)
        {
            // Set the StackFrames array's length to how much we need.
            this.StackFrames = new FaultStackFrame[callStack.FrameCount];

            // Instantiate new FaultStackFrames from callStack's frames.
            for (int i = 0; i < this.StackFrames.Length; i++)
            {
                this.StackFrames[i] = new FaultStackFrame(callStack.GetFrame(i));
            }
        }

        /// <summary>
        /// Creates a new instance of the 
        /// <see cref="WcfRawJson.ErrorHandling.FaultStackTrace"/> class.
        /// </summary>
        /// <param name="error">
        /// The <see cref="System.Exception"/> object from which to derive this
        /// <see cref="WcfRawJson.ErrorHandling.FaultStackTrace"/>.
        /// </param>
        public FaultStackTrace(Exception error) : this(new StackTrace(error)) { }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The <see cref="WcfRawJson.ErrorHandling.FaultStackFrames"/> belonging to 
        /// the current <see cref="WcfRawJson.ErrorHandling.FaultStackTrace"/> object.
        /// </summary>
        [DataMember]
        public FaultStackFrame[] StackFrames
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns an enumerator that iterates through the collection
        /// </summary>
        /// <returns>
        /// A System.Collections.Generic.IEnumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<FaultStackFrame> GetEnumerator()
        {
            return ((IEnumerable<FaultStackFrame>)this.StackFrames).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection
        /// </summary>
        /// <returns>
        /// A System.Collections.Generic.IEnumerator that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion Methods
    }
}