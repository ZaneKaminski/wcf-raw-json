using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfRawJson.ErrorHandling
{
    /// <summary>
    /// Represents a single frame of a stack trace.
    /// </summary>
    [DataContract]
    public class FaultStackFrame
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="WcfRawJson.ErrorHandling.FaultStackFrame"/> 
        /// class from an existing <see cref="System.Diagnostics.StackFrame"/>.
        /// </summary>
        /// <param name="frame">
        /// The <see cref="System.Diagnostics.StackFrame"/> object from which to derive this
        /// <see cref="WcfRawJson.ErrorHandling.StackFrame"/>
        /// </param>
        public FaultStackFrame(StackFrame frame)
        {
            this.FileColumnNumber = frame.GetFileColumnNumber();
            this.FileLineNumber = frame.GetFileLineNumber();
            this.FileName = frame.GetFileName();
            this.ILOffset = frame.GetILOffset();
            this.Method = frame.GetMethod().ToString();
            this.NativeOffset = frame.GetNativeOffset();
            this.Description = frame.ToString();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// A readable representation of the stack frame.
        /// </summary>
        [DataMember]
        public string Description
        {
            get;
            private set;
        }
        
        /// <summary>
        /// The column number in the file that contains the code that is executing. 
        /// This information is typically extracted from the debugging symbols for the executable.
        /// </summary>
        [DataMember]
        public int FileColumnNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// The line number in the file that contains the code that is executing. 
        /// This information is typically extracted from the debugging symbols for the executable.
        /// </summary>
        [DataMember]
        public int FileLineNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// The file name of the file that contains the code that is executing. 
        /// This information is typically extracted from the debugging symbols for the executable.
        /// </summary>
        [DataMember]
        public string FileName
        {
            get;
            private set;
        }

        /// <summary>
        /// The offset from the start of the Microsoft intermediate language (MSIL) 
        /// code for the method that is executing. This offset might be an approximation 
        /// depending on whether or not the just-in-time (JIT) compiler is generating 
        /// debugging code. The generation of this debugging information is controlled 
        /// by the <see cref="System.Diagnostics.DebuggableAttribute" /> class.
        /// </summary>
        [DataMember]
        public int ILOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// The method in which the frame is executing.
        /// </summary>
        [DataMember]
        public string Method
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the offset from the start of the native just-in-time (JIT)-compiled
        /// code for the method that is being executed. The generation of this debugging
        /// information is controlled by the <see cref="System.Diagnostics.DebuggableAttribute" />
        /// class.
        /// </summary>
        [DataMember]
        public int NativeOffset
        {
            get;
            private set;
        }

        #endregion Properties
    }
}