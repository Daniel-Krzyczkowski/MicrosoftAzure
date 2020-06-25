using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotificationsWebApi.Web.NotificationHubs
{
    public sealed class HubResponse<TResult> : HubResponse where TResult : class
    {
        public TResult Result { get; set; }

        public HubResponse()
        {
        }

        public new HubResponse<TResult> SetAsFailureResponse()
        {
            base.SetAsFailureResponse();
            return this;
        }

        public new HubResponse<TResult> AddErrorMessage(string errorMessage)
        {
            base.AddErrorMessage(errorMessage);
            return this;
        }
    }

    public class HubResponse
    {
        private bool _forcedFailedResponse;

        public bool CompletedWithSuccess => !ErrorMessages.Any() && !_forcedFailedResponse;
        public IList<string> ErrorMessages { get; private set; }

        public HubResponse()
        {
            ErrorMessages = new List<string>();
        }

        public HubResponse SetAsFailureResponse()
        {
            _forcedFailedResponse = true;
            return this;
        }

        public HubResponse AddErrorMessage(string errorMessage)
        {
            ErrorMessages.Add(errorMessage);
            return this;
        }

        public string FormattedErrorMessages => ErrorMessages.Any()
            ? ErrorMessages.Aggregate((prev, current) => prev + Environment.NewLine + current)
            : string.Empty;
    }
}
