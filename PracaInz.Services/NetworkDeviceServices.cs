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

namespace PracaInz.Services
{
    public class NetworkDeviceServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public NetworkDeviceServices(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public NetworkDeviceListViewModel GetAllDevices()
        {
            var vm = new NetworkDeviceListViewModel()
            {
                NetworkDevices = _context.Device.OfType<NetworkDevice>().Select(x => new NetworkDeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceDescription = x.DeviceDescription,
                    Categories = x.Categories,
                    isAlive = x.isAlive,
                    IPAddress = x.IPAddress
                    
                    
                })
            };
            return vm;
        }

        public NetworkDeviceListViewModel GetAllDevicesFilteredByUser(string userName)
        {
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

            var vm = new NetworkDeviceListItemViewModel
            {
                Id = Device.Id,
                Manufacturer = Device.Manufacturer,
                Model = Device.Model,
                SerialNumber = Device.SerialNumber,
                DeviceOwner = Device.DeviceOwner,
                DeviceDescription = Device.DeviceDescription,
                Categories = Device.Categories,
                isAlive = Device.isAlive,
                IPAddress = Device.IPAddress
            };
            return vm;
        }


        public async Task AddAsync (string manufacturer,
            string model,
            string serialNumber,
            string deviceDescription,
            int userId,
            int categoryId,
            string IPAddress)
        {
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
