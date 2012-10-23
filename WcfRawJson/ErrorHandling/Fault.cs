namespace WcfRawJson.ErrorHandling
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    /// <summary>
    /// Describes serializable information about an exception.
    /// </summary>
    [DataContract]
    public class Fault
    {
        #region Properties
        
        /// <summary>
        /// Gets a collection of key/value pairs that provide additional user-defined information about the exception.
        /// </summary>
        [DataMember]
        public IDictionary Data
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Fault which triggered this one.
        /// </summary>
        [DataMember]
        public Fault Detail
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the name of the Type of exception from which this Fault was derived.
        /// </summary>
        [DataMember]
        public string FaultType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a link to the help file associated with this Fault.
        /// </summary>
        [DataMember]
        public string HelpLink
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a message that describes the current Fault.
        /// </summary>
        [DataMember]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the application or the object that causes the error.
        /// </summary>
        [DataMember]
        public string Source
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a string representation of the immediate frames on the call stack.
        /// </summary>
        [DataMember]
        public FaultStackTrace StackTrace
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the method that throws the current Fault.
        /// </summary>
        [DataMember]
        public string TargetSite
        {
            get;
            set;
        }

        #endregion Properties

        /// <summary>
        /// Returns a Fault generated from a particular Exception.
        /// </summary>
        /// <param name="error">The exception to be converted into a Fault.</param>
        /// <returns>The new Fault.</returns>
        public static Fault GetFault(Exception error)
        {
            // If error is null, return null to avoid a NullReferenceException.
            if (error == null)
            {
                return null;
            }

            // Return a new Fault that copies the properties of the Exception.
            return new Fault()
            {
                Message = error.Message,
                FaultType = error.GetType().Name,
                HelpLink = error.HelpLink,

                // These properties may be sensitive!
                // Consider removing them in production builds.
                Detail = Fault.GetFault(error.InnerException),
                Data = error.Data,
                Source = error.Source,
                StackTrace = new FaultStackTrace(error),
                TargetSite = error.TargetSite.ToString()
            };
        }
    }
}