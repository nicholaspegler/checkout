using AutoMapper;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.ViewModels.Payment.GET;

namespace Pegler.PaymentGateway.AutoMapperMapping
{
    public class Mappings : Profile
    {
        public Mappings()
        {

            CreateMap<PaymentRespVM, PaymentRespModel>()
                .ReverseMap();

        }
    }
}
