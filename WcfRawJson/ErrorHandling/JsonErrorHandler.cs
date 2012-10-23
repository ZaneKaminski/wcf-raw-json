using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using WcfRawJson.ErrorHandling;

namespace WcfRawJson.ErrorHandling
{
    /// <summary>
    /// An <see cref="System.ServiceModel.Dispatcher.IErrorHandler"/> that delivers
    /// JSON-encoded <see cref="WcfRawJson.ErrorHandling.Fault"/> objects to the client.
    /// </summary>
    public class JsonErrorHandler : IErrorHandler
    {
        #region Methods

        /// <summary>
        /// Used to determine whether this 
        /// <see cref="System.ServiceModel.Dispatcher.IErrorHandler"/> 
        /// is to be used to handle a given <see cref="System.Exception"/>.
        /// </summary>
        /// <param name="error">The <see cref="System.Exception"/> in question</param>
        /// <returns>
        /// True, indicating that the error is to be handled by this IErrorHandler.
        /// </returns>
        public bool HandleError(Exception error)
        {
            return true;
        }

        /// <summary>
        /// Enables the creation of a custom <see cref="System.ServiceModel.FaultException"/>
        /// that is returned from an exception in the course of a service method.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="version"></param>
        /// <param name="faultMessage"></param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message faultMessage)
        {
            // Gets a serializable Fault object fromn error.
            Fault fault = Fault.GetFault(error);

            // Now we make the message by serializing the Fault to JSON.
            faultMessage = Message.CreateMessage(version, null, fault,
              new DataContractJsonSerializer(fault.GetType()));

            // Gotta set HTTP status codes.
            HttpResponseMessageProperty prop = new HttpResponseMessageProperty()
            {
                StatusCode = HttpStatusCode.InternalServerError, // 500
                StatusDescription = "An internal server error occurred." // Could use elaboration.
            };

            // Make sure to set the content type. Important for avoiding 
            // certain kinds of encoding-specific XSS attacks.
            prop.Headers[HttpResponseHeader.ContentType] = "application/json; charset=utf-8";

            // Set a few other properties of the Message.
            faultMessage.Properties.Add(HttpResponseMessageProperty.Name, prop);
            faultMessage.Properties.Add(WebBodyFormatMessageProperty.Name, 
                new WebBodyFormatMessageProperty(WebContentFormat.Json));
        }

        #endregion Methods
    }
}