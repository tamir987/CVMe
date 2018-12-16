using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using CVMe.Services.CV;
using CVMe.Services.Templates;
using CVMe.Services.Xml;
using System;
using System.Web.Http;

namespace CVMe.Controllers
{
    public class CVController : ApiController
    {
        private readonly ICVGeneratorService _cvGeneratorService;

        public CVController(ICVGeneratorService cvGeneratorService )
        {
            _cvGeneratorService = cvGeneratorService;
        }

        public CVResponse GenerateCV ([FromUri]CVRequest request)
        {
            try
            {
                //ToDo : validate request
                return new CVResponse { IsSuccess = false };

            }
            catch(Exception ex)
            {
                return new CVResponse { IsSuccess = false };
            }
        }
    }
}
