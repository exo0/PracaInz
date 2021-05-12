using AutoMapper;
using PracaInz.BLL;
using PracaInz.ViewModels.NetworkDevicesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracaInz.Services.MapProfiles
{
    public class NetworkDeviceMappingProfile : Profile
    {

        public NetworkDeviceMappingProfile()
        {
            CreateMap<NetworkDevice, NetworkDeviceListItemViewModel>();
        }
    }
}
