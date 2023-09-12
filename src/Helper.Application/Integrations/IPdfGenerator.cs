using System.Xml.Linq;

namespace Helper.Application.Integrations
{
    public interface IPdfGenerator
    {
        public byte[] GeneratePdf(string name);
    }
}
