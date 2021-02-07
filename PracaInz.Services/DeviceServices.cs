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
                Devices = _context.Devices.Select(x => new DeviceListItemViewModel
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
            var Device = _context.Devices
                .Where(b => b.Id == id)
                .Include(b => b.Categories)
                .FirstOrDefault();

            var vm = new DeviceListItemViewModel
            {
                Id = Device.Id,
                Manufacturer = Device.Manufacturer,
                Model = Device.Model,
                SerialNumber = Device.SerialNumber,
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


            var device = new Device
            {
                Manufacturer = manufacturer,
                Model = model,
                SerialNumber = serialNumber,
                DeviceDescription = deviceDescription,
                DeviceOwner = await _userManager.FindByIdAsync(userId.ToString()),
                Categories = new List<Category>()
            };

            device.Categories.Add(cat);
            _context.Devices.Add(device);
            _context.SaveChanges();
        }


        // Funkcja która jest odpowiedzialna za usuwanie urządzenia z bazy danych 
        // wyszukując go w bazie po "id"
        public void DeleteDevice(int id)
        {
            var Device = _context.Devices.Find(id);
            _context.Devices.Remove(Device);
            _context.SaveChanges();
        }


    }

    
}
