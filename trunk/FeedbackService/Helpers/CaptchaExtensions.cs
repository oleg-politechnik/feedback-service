using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;
using FeedbackService;

namespace FeedbackService.Helpers
{
    public static class CaptchaExtensions
    {
        public static string GenerateCaptcha(this HtmlHelper helper)
        {
            var captchaControl = new Recaptcha.RecaptchaControl
            {
                ID = "recaptcha",
                Theme = "white",
                PublicKey = Helper.CaptchaPublicKey,
                PrivateKey = Helper.CaptchaPrivateKey
            };
            var htmlWriter = new HtmlTextWriter(new StringWriter());
            captchaControl.RenderControl(htmlWriter);
            return htmlWriter.InnerWriter.ToString();
        }
    }

    public class CaptchaValidatorAttribute : ActionFilterAttribute
    {
        private const string CHALLENGE_FIELD_KEY = "recaptcha_challenge_field";
        private const string RESPONSE_FIELD_KEY = "recaptcha_response_field";
        private const string CAPTCHA_MODEL_KEY = "Captcha";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var captchaChallengeValue = filterContext.HttpContext.Request.Form[CHALLENGE_FIELD_KEY];
            var captchaResponseValue = filterContext.HttpContext.Request.Form[RESPONSE_FIELD_KEY];
            var captchaValidtor = new Recaptcha.RecaptchaValidator
            {
                PrivateKey = Helper.CaptchaPrivateKey,
                RemoteIP = filterContext.HttpContext.Request.UserHostAddress,
                Challenge = captchaChallengeValue,
                Response = captchaResponseValue
            };

            var recaptchaResponse = captchaValidtor.Validate();

            if (!recaptchaResponse.IsValid)
            {
                filterContext.Controller
                    .ViewData.ModelState
                    .AddModelError(
                        CAPTCHA_MODEL_KEY,
                        "Ошибка капчи");
            }

            base.OnActionExecuting(filterContext);
        }
    }

}
