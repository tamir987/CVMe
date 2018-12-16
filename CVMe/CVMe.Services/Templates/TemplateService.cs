using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;

namespace CVMe.Services.Templates
{
    public interface ITemplateService
    {
        TemplateResponse GetTemplateById(TemplateRequest request);
    }
    public class TemplateService : ITemplateService
    {
        public TemplateResponse GetTemplateById(TemplateRequest request)
        {
            return new TemplateResponse { IsSuccess = false };
        }
    }
}
