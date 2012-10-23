using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using WcfRawJson.ErrorHandling;
using System.ServiceModel.Dispatcher;

namespace WcfRawJson.ClientCommunication
{
    /// <summary>
    /// A WebHttpBehavior which facilitates REST-ful data transfer in raw JSON.
    /// </summary>
    public class RawJsonWebHttpBehavior : WebHttpBehavior
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the RawJsonWebHttpBehavior class.
        /// </summary>
        public RawJsonWebHttpBehavior() : this(WebMessageBodyStyle.WrappedRequest) { }

        /// <summary>
        /// Creates a new instance of the RawJsonWebHttpBehavior 
        /// class with a specificied default BodyStyle.
        /// </summary>
        /// <param name="messageBodyStyle">The WebMessageBodyStyle to use by default.</param>
        public RawJsonWebHttpBehavior(WebMessageBodyStyle messageBodyStyle)
        {
            this.DefaultBodyStyle = messageBodyStyle;
            this.DefaultOutgoingRequestFormat = WebMessageFormat.Json;
            this.DefaultOutgoingResponseFormat = WebMessageFormat.Json;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clears the ChannelDispatcher's ErrorHandlers 
        /// collection and adds the JsonErrorHandler class.
        /// </summary>
        /// <param name="endpoint">The specified ServiceEndpoint.</param>
        /// <param name="dispatcher">The specified EndpointDispatcher.</param>
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, 
            EndpointDispatcher dispatcher)
        {
            // Remove whatever error handlers used to be there.
            dispatcher.ChannelDispatcher.ErrorHandlers.Clear();

            // Add a new instance of the JsonErrorHandler.
            dispatcher.ChannelDispatcher.ErrorHandlers.Add(new JsonErrorHandler());
        }

        #endregion Methods
    }
}