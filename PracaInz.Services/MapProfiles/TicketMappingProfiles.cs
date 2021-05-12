using AutoMapper;
using PracaInz.BLL;
using PracaInz.ViewModels.TicketViewModels;


namespace PracaInz.Services.MapProfiles
{
    public class TicketMappingProfiles : Profile
    {
        public TicketMappingProfiles()
        {
            CreateMap<Ticket, TicketListItemViewModel>();
        }
    }
}
