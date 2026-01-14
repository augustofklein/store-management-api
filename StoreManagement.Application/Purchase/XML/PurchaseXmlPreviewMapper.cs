using StoreManagement.Application.Purchase.Model;
using System.Globalization;
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
                DocumentNumber = xml
                    .Root?
                    .Element(ns + "NFe")?
                    .Element(ns + "infNFe")?
                    .Element(ns + "ide")?
                    .Element(ns + "nNF")?
                    .Value
                    ?? string.Empty,

                DocumentSerie = xml
                    .Root?
                    .Element(ns + "NFe")?
                    .Element(ns + "infNFe")?
                    .Element(ns + "ide")?
                    .Element(ns + "serie")?
                    .Value
                    ?? string.Empty,

                DocumentMod = xml
                    .Root?
                    .Element(ns + "NFe")?
                    .Element(ns + "infNFe")?
                    .Element(ns + "ide")?
                    .Element(ns + "mod")?
                    .Value
                    ?? string.Empty,

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
                    ?? "0", out var documentStatus) ? documentStatus: 0,

                DocumentDate = DateTimeOffset.TryParse(
                    xml.Root?
                        .Element(ns + "NFe")?
                        .Element(ns + "infNFe")?
                        .Element(ns + "ide")?
                        .Element(ns + "dhEmi")?
                        .Value,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var parsedDate
                )
                ? parsedDate.UtcDateTime
                : DateTime.UtcNow
            };

            var supplierData = new PurchasePreviewDto.SupplierPreviewDto
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

            var storeDocumentNumber = xml
                    .Root?
                    .Element(ns + "NFe")?
                    .Element(ns + "infNFe")?
                    .Element(ns + "dest")?
                    .Element(ns + "CNPJ")?
                    .Value
                    ?? string.Empty;

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
                    Price = decimal.TryParse(prod.Element(ns + "vUnCom")?.Value, out var price) ? price : 0,
                    Package = int.TryParse(prod.Element(ns + "qTrib")?.Value,
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out var pkg) ? pkg : 0,
                    Quantity = int.TryParse(prod.Element(ns + "qCom")?.Value, out var qty) ? qty : 0,
                    ShippingCost = decimal.TryParse(prod.Element(ns + "vFrete")?.Value, out var shippingCost) ? shippingCost : 0
                })
                .ToList() ?? [];

            return new PurchasePreviewDto
            {
                StoreDocumentNumber = storeDocumentNumber,
                SupplierInformation = supplierData,
                FiscalDocument = fiscalDocument,
                Products = products,
                TotalAmount = products.Sum(p => p.Total)
            };
        }
    }
}
