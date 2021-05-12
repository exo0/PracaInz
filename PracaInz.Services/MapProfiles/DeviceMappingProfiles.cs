using AutoMapper;
using PracaInz.BLL;
using PracaInz.ViewModels.DevicesViewModels;


namespace PracaInz.Services.MapProfiles
{
    public class DeviceMappingProfiles : Profile
    {
        public DeviceMappingProfiles()
        {
            CreateMap<Device, DeviceListItemViewModel>();
        }
    }
}
