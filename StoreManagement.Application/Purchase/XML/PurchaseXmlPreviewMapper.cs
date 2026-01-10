using StoreManagement.Application.Purchase.Model;
using System.Xml.Linq;

namespace StoreManagement.Application.Purchase.XML
{
    public static class PurchaseXmlPreviewMapper
    {
        public static PurchasePreviewDto Map(XDocument xml)
        {
            XNamespace ns = xml.Root!.Name.Namespace;

            var fiscalDocument = new PurchasePreviewDto.DocumentPreviewDto
            {
                DocumentKey = xml
                .Root?
                .Element(ns + "protNFe")?
                .Element(ns + "infProt")?
                .Element(ns + "chNFe")?
                .Value
                ?? string.Empty,

                DocumentStatus = int.TryParse(xml
                    .Root?
                    .Element(ns + "protNFe")?
                    .Element(ns + "infProt")?
                    .Element(ns + "cStat")?
                    .Value
                    ?? "0", out var documentStatus) ? documentStatus: 0
            };

            var store = new PurchasePreviewDto.StorePreviewDto
            {
                DocumentNumber = xml
                    .Root?
                    .Element(ns + "NFe")?
                    .Element(ns + "infNFe")?
                    .Element(ns + "dest")?
                    .Element(ns + "CNPJ")?
                    .Value
                    ?? string.Empty
            };

            var supplier = new PurchasePreviewDto.SupplierPreviewDto
            {
                DocumentNumber = xml
                    .Root?
                    .Element(ns + "NFe")?
                    .Element(ns + "infNFe")?
                    .Element(ns + "emit")?
                    .Element(ns + "CNPJ")?
                    .Value
                    ?? string.Empty
            };

            var products = xml.Root?
                .Element(ns + "NFe")?
                .Element(ns + "infNFe")?
                .Elements(ns + "det")?
                .Select(det => det.Element(ns + "prod"))
                .Where(prod => prod != null)
                .Select(prod => new PurchasePreviewDto.ProductPreviewDto
                {
                    Barcode = prod!.Element(ns + "cEAN")?.Value ?? string.Empty,
                    Description = prod.Element(ns + "xProd")?.Value ?? string.Empty,
                    Quantity = decimal.TryParse(prod.Element(ns + "qCom")?.Value, out var qty) ? qty : 0,
                    Price = decimal.TryParse(prod.Element(ns + "vUnCom")?.Value, out var price) ? price : 0
                })
                .ToList() ?? [];

            return new PurchasePreviewDto
            {
                FiscalDocument = fiscalDocument,
                Store = store,
                Supplier = supplier,
                Products = products,
                TotalAmount = products.Sum(p => p.Total)
            };
        }
    }
}
