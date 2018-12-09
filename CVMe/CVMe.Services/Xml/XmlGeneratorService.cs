using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;

namespace CVMe.Services.Xml
{
    public interface IXmlGeneratorService
    {
        XmlGeneratorResponse GenerateXml(XmlGeneratorRequest request);
    }

    public class XmlGeneratorService : IXmlGeneratorService
    {
        public XmlGeneratorResponse GenerateXml(XmlGeneratorRequest request)
        {
            return new XmlGeneratorResponse { IsSuccess = false };
        }
    }
}
