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

            CreateMap<PaymentCardRespVM, PaymentCardRespModel>()
                .ReverseMap()
                .ForMember(dest => dest.Cvv, opt => opt.MapFrom(src => "***"))
                .ForMember(dest => dest.CardnumberLast4, opt => opt.MapFrom(src => src.Cardnumber.Substring(src.Cardnumber.Length - 4, 4)));
        }
    }
}
