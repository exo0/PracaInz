using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracaInz.BLL;
using PracaInz.DAL.EF;
using PracaInz.ViewModels.DevicesViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Services
{
    public class DeviceServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public DeviceServices(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public DeviceListViewModel GetAllDevices()
        {
            var vm = new DeviceListViewModel()
            {
                Devices = _context.Device.Select(x => new DeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceDescription = x.DeviceDescription,
                    Categories = x.Categories
                })
            };
            return vm;
        }

        public DeviceListItemViewModel GetDevice(int id)
        {
            var Device = _context.Device
                .Where(b => b.Id == id)
                .Include(b => b.Categories)
                .FirstOrDefault();

            var vm = new DeviceListItemViewModel
            {
                Id = Device.Id,
                Manufacturer = Device.Manufacturer,
                Model = Device.Model,
                SerialNumber = Device.SerialNumber,
                DeviceOwner = Device.DeviceOwner,
                DeviceDescription = Device.DeviceDescription,
                Categories = Device.Categories
            };

            return vm;
        }

        public async Task AddAsync(string manufacturer,
            string model,
            string serialNumber,
            string deviceDescription,
            int userId,
            int categoryId)
        {
            Category cat = _context.Categories.Find(categoryId);
            User usr = _context.Users.Find(userId);


            var device = new Device
            {
                Manufacturer = manufacturer,
                Model = model,
                SerialNumber = serialNumber,
                DeviceDescription = deviceDescription,
                //DeviceOwner = await _userManager.FindByIdAsync(userId.ToString()),
                DeviceOwner = usr,
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


    }

    
}
