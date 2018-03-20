using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemaTre.Site.Resume.Api.Feedback.Dto;

namespace TemaTre.Site.Resume.Api
{
    [Produces("application/json")]
    [Route("api/Feedback")]
    public class FeedbackController : Controller
    {

        [HttpPost]
        [ActionName("SaveFeedback")]
        public HttpResponseMessage SaveFeedbackPost(FeedbackRequestDto request)
        {
            if (ModelState.IsValid && request != null)
            {
                // Create a 201 response.
                var response = new HttpResponseMessage(HttpStatusCode.Accepted);

                return response;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}