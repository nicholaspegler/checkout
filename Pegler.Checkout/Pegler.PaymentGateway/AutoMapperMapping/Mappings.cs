using AutoMapper;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using Pegler.PaymentGateway.ViewModels.Payment.GET;
using Pegler.PaymentGateway.ViewModels.Payment.POST;

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

            CreateMap<PaymentReqModel, PaymentReqVM>()
                .ReverseMap()
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString()));

            CreateMap<PaymentCardReqModel, PaymentCardReqVM>()
                .ReverseMap()
                .ForMember(dest => dest.CardType, opt => opt.MapFrom(src => src.CardType.ToString()))
                .ForMember(dest => dest.Issuer, opt => opt.MapFrom(src => src.Issuer.ToString()));

            CreateMap <PaymentRecipientReqModel, PaymentRecipientReqVM>()
                .ReverseMap();

        }
    }
}
