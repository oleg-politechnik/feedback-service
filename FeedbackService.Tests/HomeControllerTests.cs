using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FeedbackService.Controllers;

namespace FeedbackService.Tests
{
    [TestFixture]
    public class HomeControllerIndexTests
    {
        //[Test]
        public void Puts_Message_In_ViewBag()
        {
            var controller = new HomeController();
            var result = controller.Index();
            //Assert.IsNotNull(result.ViewBag.Message);
        }
    }
}
