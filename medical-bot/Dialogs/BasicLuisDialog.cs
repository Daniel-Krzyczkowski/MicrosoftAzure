using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("Greeting")]
        public async Task GreetingIntent(IDialogContext context, LuisResult result)
        {
            var reply = context.MakeMessage();
            reply.Speak = "Hello! How can I help you?";
            reply.Text = "Hello! How can I help you?";

            await context.PostAsync(reply);
            context.Wait(MessageReceived);
        }

        [LuisIntent("SoarThroat")]
        public async Task SoarThroatIntent(IDialogContext context, LuisResult result)
        {
            var reply = context.MakeMessage();
            reply.Speak = "If it is soar throat I think that you should drink hot tea.";
            reply.Text = "If it is soar throat I think that you should drink hot tea.";
            reply.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://assets.tetrapak.com/static/publishingimages/find-by-food/rollup/juice-drinks-tea.jpg",
                ContentType = "image/jpg",
                Name = "Tea.png"
            });

            await context.PostAsync(reply);
            context.Wait(MessageReceived);
        }

        [LuisIntent("HeadAke")]
        public async Task HeadAkeIntent(IDialogContext context, LuisResult result)
        {
            var reply = context.MakeMessage();
            reply.Speak = "If it is head ake I think that you should get aspirine.";
            reply.Text = "If it is head ake I think that you should get aspirine.";
            reply.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://atlas-content-cdn.pixelsquid.com/assets_v2/11/1158052823821195061/jpeg-600/G02.jpg",
                ContentType = "image/jpg",
                Name = "Aspirin.png"
            });

            await context.PostAsync(reply);
            context.Wait(MessageReceived);
        }

        [LuisIntent("TwistedAnkle")]
        public async Task TwistedAnkleIntent(IDialogContext context, LuisResult result)
        {
            var reply = context.MakeMessage();
            reply.Speak = "If it is twisted ankle I think should call your doctor and go to the hospital.";
            reply.Text = "If it is twisted ankle I think should call your doctor and go to the hospital.";
            reply.Attachments.Add(new Attachment()
            {
                ContentUrl = "https://injuryhealthblog.com/wp-content/uploads/2017/11/shutterstock_227120620-min.jpg",
                ContentType = "image/jpg",
                Name = "Ankle.png"
            });

            await context.PostAsync(reply);
            context.Wait(MessageReceived);
        }
    }
}