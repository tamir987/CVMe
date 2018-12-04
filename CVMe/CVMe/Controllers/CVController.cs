using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using CVMe.Services.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CVMe.Controllers
{
    public class CVController : ApiController
    {
        private readonly IXmlGeneratorService _xmlGeneratorService;

        public CVController(IXmlGeneratorService xmlGeneratorService)
        {
            _xmlGeneratorService = xmlGeneratorService;
        }

        public CVResponse GenerateCV ([FromUri]CVRequest request)
        {
            return new CVResponse { IsSuccess = false };
        }
    }
}
