using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using PushNotificationsWebApi.Web.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PushNotificationsWebApi.Web.NotificationHubs
{
    public class NotificationHubProxy
    {
        private NotificationHubConfiguration _configuration;
        private NotificationHubClient _hubClient;

        public NotificationHubProxy(NotificationHubConfiguration configuration)
        {
            _configuration = configuration;
            _hubClient = NotificationHubClient.CreateClientFromConnectionString(_configuration.ConnectionString, _configuration.HubName);
        }

        /// <summary>
        /// Get registration ID from Azure Notification Hub
        /// </summary>

        public async Task<string> CreateRegistrationId()
        {
            return await _hubClient.CreateRegistrationIdAsync();
        }

        /// <summary>
        /// Delete registration ID from Azure Notification Hub
        /// </summary>
        /// <param name="registrationId"></param>
        public async Task DeleteRegistration(string registrationId)
        {
            await _hubClient.DeleteRegistrationAsync(registrationId);
        }

        /// <summary>
        /// Register device to receive push notifications. 
        /// Registration ID ontained from Azure Notification Hub has to be provided
        /// Then basing on platform (Android, iOS or Windows) specific
        /// handle (token) obtained from Push Notification Service has to be provided
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deviceUpdate"></param>
        /// <returns></returns>
        public async Task<HubResponse> RegisterForPushNotifications(string id, DeviceRegistration deviceUpdate)
        {
            RegistrationDescription registrationDescription = null;

            switch (deviceUpdate.Platform)
            {
                case MobilePlatform.wns:
                    registrationDescription = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case MobilePlatform.apns:
                    registrationDescription = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case MobilePlatform.gcm:
                    registrationDescription = new GcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    return new HubResponse().AddErrorMessage("Please provide correct platform notification service name.");
            }

            registrationDescription.RegistrationId = id;
            if (deviceUpdate.Tags != null)
                registrationDescription.Tags = new HashSet<string>(deviceUpdate.Tags);

            try
            {
                await _hubClient.CreateOrUpdateRegistrationAsync(registrationDescription);
                return new HubResponse();
            }
            catch (MessagingException)
            {
                return new HubResponse().AddErrorMessage("Registration failed because of HttpStatusCode.Gone. PLease register once again.");
            }
        }

        /// <summary>
        /// Send push notification to specific platform (Android, iOS or Windows)
        /// </summary>
        /// <param name="newNotification"></param>
        /// <returns></returns>
        public async Task<HubResponse<NotificationOutcome>> SendNotification(Notification newNotification)
        {
            try
            {
                NotificationOutcome outcome = null;

                switch (newNotification.Platform)
                {
                    case MobilePlatform.wns:
                        if (newNotification.Tags == null)
                            outcome = await _hubClient.SendWindowsNativeNotificationAsync(newNotification.Content);
                        else
                            outcome = await _hubClient.SendWindowsNativeNotificationAsync(newNotification.Content, newNotification.Tags);
                        break;
                    case MobilePlatform.apns:
                        if (newNotification.Tags == null)
                            outcome = await _hubClient.SendAppleNativeNotificationAsync(newNotification.Content);
                        else
                            outcome = await _hubClient.SendAppleNativeNotificationAsync(newNotification.Content, newNotification.Tags);
                        break;
                    case MobilePlatform.gcm:
                        if (newNotification.Tags == null)
                            outcome = await _hubClient.SendGcmNativeNotificationAsync(newNotification.Content);
                        else
                            outcome = await _hubClient.SendGcmNativeNotificationAsync(newNotification.Content, newNotification.Tags);
                        break;
                }

                if (outcome != null)
                {
                    if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
                        (outcome.State == NotificationOutcomeState.Unknown)))
                        return new HubResponse<NotificationOutcome>();
                }

                return new HubResponse<NotificationOutcome>().SetAsFailureResponse().AddErrorMessage("Notification was not sent due to issue. Please send again.");
            }

            catch (MessagingException ex)
            {
                return new HubResponse<NotificationOutcome>().SetAsFailureResponse().AddErrorMessage(ex.Message);
            }

            catch (ArgumentException ex)
            {
                return new HubResponse<NotificationOutcome>().SetAsFailureResponse().AddErrorMessage(ex.Message);
            }
        }
    }
}
