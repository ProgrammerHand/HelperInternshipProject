using System.Xml.Linq;

namespace Helper.Application.Integrations
{
    public interface IPdfGenerator
    {
        public void GeneratePdf(string name);
    }
}
