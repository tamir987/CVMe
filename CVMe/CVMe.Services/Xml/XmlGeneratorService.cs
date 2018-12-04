using CVMe.DataObjects.Requests;
using CVMe.DataObjects.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
