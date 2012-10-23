using System;
using System.ServiceModel.Configuration;

namespace WcfRawJson.ClientCommunication
{
    /// <summary>
    /// Used in web.config files to represent a RawJsonWebHttpBehavior.
    /// </summary>
    public class RawJsonWebHttpBehaviorElement : BehaviorExtensionElement
    {
        #region Properties

        /// <summary>
        /// Gets the type of the EndpointBehavior which is added to the endpoint's behaviors.
        /// Always returns typeof(RawJsonWebHttpBehavior).
        /// </summary>
        public override Type BehaviorType
        {
            get
            {
                return typeof(RawJsonWebHttpBehavior);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new instance of the RawJsonWebHttpBehavior.
        /// </summary>
        /// <returns>The new instance of the RawJsonWebHttpBehavior.</returns>
        protected override object CreateBehavior()
        {
            return new RawJsonWebHttpBehavior();
        }

        #endregion Methods
    }
}