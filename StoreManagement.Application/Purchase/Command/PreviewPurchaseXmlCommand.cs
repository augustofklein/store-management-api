using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using StoreManagement.Application.Purchase.Model;

namespace StoreManagement.Application.Purchase.Command
{
    public record PreviewPurchaseXmlCommand(IFormFile File) : IRequest<Result<PurchasePreviewDto>>;
}
