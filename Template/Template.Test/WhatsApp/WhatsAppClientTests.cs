using Microsoft.VisualStudio.TestTools.UnitTesting;
using Template.WhatsAppApi.WhatsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.WhatsAppApi.Contract;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Template.WhatsAppApi.Contract.Header;
using Template.WhatsAppApi.Contract.Buttons;
using Template.Contract;
using Template.WhatsAppApi.Contract.Body;
using System.Collections;
using System.Text.Json.Nodes;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;

namespace Template.WhatsAppApi.WhatsApp.Tests
{
    [TestClass()]
    public class WhatsAppClientTests
    {

        /// <summary>
        /// Output from the Header => {"example":{"header_text":["Summer Sale"]},"type":"Header","format":"Text","text":"Out {{1}} is on!"}
        /// </summary>
        [TestMethod]
        public void CheckTextTemplateHeaderCreation()
        {
            var text = new HeaderText()
            {
                _text = new[] { "Summer Sale" }
            };

            var textTemplate = new TextTemplateHeader()
            {
                Type = ComponentTypes.Header.ToString(),
                Format = ComponentFormat.Text.ToString(),
                Text = "Out {{1}} is on!",

                Example = text
            };

            Console.WriteLine(textTemplate);

            var serializedTemplate = JsonConvert.SerializeObject(textTemplate);
            Assert.IsNotNull(serializedTemplate);
        }


        /// <summary>
        /// Output from the Header => {"example":{"header_handle":["4::aW..."]},"type":"Header","format":"Image"}
        /// </summary>
        [TestMethod]
        public void CheckMediaTemplateHeaderCreation()
        {
            var text = new HeaderHandle()
            {
                _media = new[] { "4::aW..." }
            };

            var textTemplate = new MediaTemplateHeader()
            {
                Type = ComponentTypes.Header.ToString(),
                Format = ComponentFormat.Image.ToString(),

                Example = text
            };

            Console.WriteLine(textTemplate);

            var serializedTemplate = JsonConvert.SerializeObject(textTemplate);

        }

        /// <summary>
        /// Output from the Header => {"type":"Header","format":"Location"}
        /// </summary>
        [TestMethod]
        public void CheckLocationTemplateHeaderCreation()
        {
            var textTemplate = new LocationTemplateHeader()
            {
                Type = ComponentTypes.Header.ToString(),
                Format = ComponentFormat.Location.ToString(),
            };

            Console.WriteLine(textTemplate);

            var serializedTemplate = JsonConvert.SerializeObject(textTemplate);
        }


        [TestMethod]
        public void CheckButtonTemplateHeaderCreation()
        {
            var buttonTemplate = new ButtonTemplate()
            {
                Type = ComponentTypes.BUTTONS.ToString(),
                Buttons = new Collection<Object>()
                {
                    new PhoneNumberButtonsTemplate()
                    {
                        Type = "PHONE_NUMBER",
                        Text ="Call",
                        PhoneNumber = "15550051310"
                    },

                    new URLButtonsTemplate()
                    {
                        Type = "URL",
                        Text = "Shop Now",
                        Url = "https://www.luckyshrub.com/shop/"
                    }


                }
            };

            Console.WriteLine(buttonTemplate);

            var serializedTemplate = JsonConvert.SerializeObject(buttonTemplate);
        }


        [TestMethod()]
        public void PostTemplateNameSpaceTest()
        {
            IWhatsAppClient whatsAppClient = new WhatsAppClient();

            var text = new HeaderText()
            {
                _text = new[] { "Summer Sale" }
            };

            var textTemplate = new TextTemplateHeader()
            {
                Type = ComponentTypes.Header.ToString(),
                Format = ComponentFormat.Text.ToString(),
                Text = "Out {{1}} is on!",

                Example = text
            };

            var templateExample = new BodyText()
            {
                Body_Texts = new[] { "10OFF" }
            };

            var tempateBody = new TemplateBody()
            {
                Type = ComponentTypes.Body.ToString(),
                Text = "Looks like you left some items in your cart! Use code {{1}} and you can get 10% off of all of them!",
                Example = templateExample
            };

            var x = new []
            {
                 JsonConvert.SerializeObject(tempateBody),
                JsonConvert.SerializeObject(textTemplate)
            };


            ////var obj = new List<JObject>();
            ////obj.Add(new JObject(tempateBody));
            ////obj.Add(new JObject(textTemplate));

            var obje = new[]
            {
                JObject.FromObject(tempateBody),
                JObject.FromObject(textTemplate)
            };

            ////var componets = new ArrayList()
            ////{
            ////    tempateBody,
            ////    textTemplate
            ////};

            ////var str = componets.ToString();

            ////var data = from object o in componets
            ////select o.ToString();

            ////var theString = string.Join(" ", data.ToArray());

            ////var value = JsonConvert.DeserializeObject(componets.ToString());

            var request = new TemplateRequests()
            {
                Name = "name",
                Language = "en_US",
                Category = TemplateRequest.MarketingCategory,
                TemplateMessageComponents = obje
            };




            var serializedTemplate = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(serializedTemplate);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            whatsAppClient.PostTemplate(byteContent, "12345678");
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTemplateNameSpaceTest()
        {
            ////EAAJPe33QUZC4BO8BNi5ypbl873CvPjn6q2nl2iyMmBc0Ok4ruZCrPlrNzScBZCSVg6cOSKG3M1CBpRLLa5m3GBh4PIkYWAjyZC7qYvFEhBfxufOeJZB6VuFxavJgXyBYXEIgF6XOAaFle3FKOaiqURUoaZCFxXTHzn2MZAUPMiEsqCHpZBcPlMlBbMrGGzME5H4wQq26mrsAmyxEkIHqPKcZD
            IWhatsAppClient whatsAppClient = new WhatsAppClient();
            whatsAppClient.GetAllTemplateTest();
        }
    }
}