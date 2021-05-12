using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracaInz.BLL;
using PracaInz.DAL.EF;
using PracaInz.ViewModels.DevicesViewModels;
using PracaInz.ViewModels.NetworkDevicesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using AutoMapper;

namespace PracaInz.Services
{
    public class NetworkDeviceServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public NetworkDeviceServices(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public NetworkDeviceListViewModel GetAllDevices()
        {
            //TODO: automapper możliwy ?
            var vm = new NetworkDeviceListViewModel()
            {
                NetworkDevices = _context.Device.OfType<NetworkDevice>().Select(x => new NetworkDeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceDescription = x.DeviceDescription,
                    DeviceOwner = x.DeviceOwner,
                    Categories = x.Categories,
                    isAlive = x.isAlive,
                    IPAddress = x.IPAddress
                    
                    
                })
            };
            return vm;
        }

        public NetworkDeviceListViewModel GetAllDevicesFilteredByUser(string userName)
        {
            //TODO: automapper możliwy ?
            var vm = new NetworkDeviceListViewModel()
            {
                NetworkDevices = _context.Device.OfType<NetworkDevice>()
                .Where(x=>x.DeviceOwner.UserName == userName)
                .Select(x => new NetworkDeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceDescription = x.DeviceDescription,
                    DeviceOwner = x.DeviceOwner,
                    Categories = x.Categories,
                    isAlive = x.isAlive,
                    IPAddress = x.IPAddress
                })
            };
            return vm;
        }

        public NetworkDeviceListItemViewModel GetDevice(int id)
        {
            var Device = _context.Device.OfType<NetworkDevice>()
                .Where(b => b.Id == id)
                .Include(b => b.Categories)
                .FirstOrDefault();
            var vm1 = _mapper.Map<NetworkDeviceListItemViewModel>(Device);
            return vm1;
        }
        /// <summary>
        /// Function which allow us to edit existing networkDevice in database 
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_Manufacturer"></param>
        /// <param name="_Model"></param>
        /// <param name="_serialNumber"></param>
        /// <param name="_deviceDescription"></param>
        /// <param name="_userId"></param>
        /// <param name="_categoryId"></param>
        /// <returns></returns>
        public async Task EditAsync(int _ID, string _Manufacturer,
            string _Model,
            string _serialNumber,
            string _deviceDescription,
            int _userId,
            int _categoryId,
            string _IPAddress)
        {
            var cat = _context.Categories.Find(_categoryId);
            var User = _context.Users.Find(_userId);

            var DeviceInDB = _context.Device
                .OfType<NetworkDevice>()
                .Where(b => b.Id == _ID)
                .Include(b => b.Categories)
                .FirstOrDefault();

            if(DeviceInDB.Categories.Count() == 0)
            {

            }
            else
            {
                var currentCategory = DeviceInDB.Categories.FirstOrDefault();

                if(_ID == DeviceInDB.Id)
                {
                    DeviceInDB.Manufacturer = _Manufacturer;
                    DeviceInDB.Model = _Model;
                    DeviceInDB.SerialNumber = _serialNumber;
                    DeviceInDB.DeviceDescription = _deviceDescription;
                    DeviceInDB.IPAddress = _IPAddress;
                    DeviceInDB.Categories.Remove(currentCategory);
                    DeviceInDB.Categories.Add(cat);
                    DeviceInDB.UserId = _userId;
                }
            }
            _context.Device.Update(DeviceInDB);
            _context.SaveChanges();
        }


        public async Task AddAsync (string manufacturer,
            string model,
            string serialNumber,
            string deviceDescription,
            int userId,
            int categoryId,
            string IPAddress)
        {
            //TODO: automapper możliwy ?
            Category cat = _context.Categories.Find(categoryId);
            User usr = _context.Users.Find(userId);

            var device = new NetworkDevice
            {
                Manufacturer = manufacturer,
                Model = model,
                SerialNumber = serialNumber,
                DeviceDescription = deviceDescription,
                DeviceOwner = usr,
                IPAddress = IPAddress,
                isAlive = false,
                Categories = new List<Category>()
            };

            device.Categories.Add(cat);
            _context.Device.Add(device);
            _context.SaveChanges();
        }

        public void DeleteDevice(int id)
        {
            var Device = _context.Device.Find(id);
            _context.Device.Remove(Device);
            _context.SaveChanges();
        }

        public void DevicePingStatus(int id)
        {
            var Device = _context.Device.OfType<NetworkDevice>().Where(x=>x.Id == id).FirstOrDefault();
            
            Ping pingService = new Ping();
            PingOptions options = new PingOptions(64, true);
            if(Device.IPAddress != null)
            {
                PingReply reply = pingService.Send(Device.IPAddress);
                if (reply.Status == IPStatus.Success)
                {
                    Device.isAlive = true;
                }
                else
                {
                    Device.isAlive = false;
                }
                _context.Device.Update(Device);

                _context.SaveChanges();
            }

            
        }

        

    }
}
