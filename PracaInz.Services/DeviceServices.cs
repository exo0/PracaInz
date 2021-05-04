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
            //TODO: automapper możliwy ?
            var vm = new DeviceListViewModel()
            {
                Devices = _context.Device.Select(x => new DeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceDescription = x.DeviceDescription,
                    DeviceOwner = x.DeviceOwner,
                    Categories = x.Categories
                })
            };
            return vm;
        }

        public DeviceListViewModel GetAllDevicesFilterByUserId(string userName)
        {
            //TODO: automapper możliwy ?
            var vm = new DeviceListViewModel()
            {
                Devices = _context.Device.Where(x=>x.DeviceOwner.UserName == userName)
                .Select(x => new DeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceOwner = x.DeviceOwner,
                    DeviceDescription = x.DeviceDescription,
                    Categories = x.Categories
                })
            };
            return vm;
        }

        public DeviceListViewModel GetAllDevicesFilteredBySearch(string searchString)
        {
            var vm = new DeviceListViewModel
            {
                Devices = _context.Device
                .Include(e => e.Categories)
                .Where(x => x.SerialNumber.Contains(searchString))
                .Select(x => new DeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceOwner = x.DeviceOwner,
                    DeviceDescription = x.DeviceDescription,
                    Categories = x.Categories
                })
            };

            return vm;
        }

        public DeviceListViewModel GetNormalDevice()
        {
            //TODO: automapper możliwy ?
            var vm = new DeviceListViewModel()
            {
                Devices = _context.Device.OfType<Device>().Select(x => new DeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceOwner = x.DeviceOwner,
                    DeviceDescription = x.DeviceDescription,
                    Categories = x.Categories
                })
            };
            return vm;
        }

        public DeviceListViewModel GetNormalDeviceFilteredByUser(string userName)
        {
            //TODO: automapper możliwy ?
            var vm = new DeviceListViewModel()
            {
                Devices = _context.Device.OfType<Device>()
                .Where(x=>x.DeviceOwner.UserName == userName)
                .Select(x => new DeviceListItemViewModel
                {
                    Id = x.Id,
                    Manufacturer = x.Manufacturer,
                    Model = x.Model,
                    SerialNumber = x.SerialNumber,
                    DeviceOwner = x.DeviceOwner,
                    DeviceDescription = x.DeviceDescription,
                    Categories = x.Categories
                })
            };
            return vm;
        }

        public DeviceListItemViewModel GetDevice(int id)
        {
            //TODO: automapper możliwy ?
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

            //TODO: automapper możliwy ?
            var device = new Device
            {
                Manufacturer = manufacturer,
                Model = model,
                SerialNumber = serialNumber,
                DeviceDescription = deviceDescription,
                DeviceOwner = usr,
                Categories = new List<Category>()
            };

            device.Categories.Add(cat);
            _context.Device.Add(device);
            _context.SaveChanges();
        }

        public void UpdateDevice(int _ID, string _Manufacturer, string _Model, string _serialNumber, string _deviceDescription,int _categoryId, int _userId)
        {
            var cat = _context.Categories.Find(_categoryId);
            var User = _context.Users.Find(_userId);

            var DeviceInDB = _context.Device
                .Where(b => b.Id == _ID)
                .Include(b => b.Categories)
                .FirstOrDefault();

            if (DeviceInDB.Categories.Count() == 0)
            {

            }
            else
            {
                var currentCategory = DeviceInDB.Categories.First();
                if(_ID == DeviceInDB.Id)
                {
                    DeviceInDB.Manufacturer = _Manufacturer;
                    DeviceInDB.Model = _Model;
                    DeviceInDB.SerialNumber = _serialNumber;
                    DeviceInDB.DeviceDescription = _deviceDescription;
                    DeviceInDB.Categories.Remove(currentCategory);
                    DeviceInDB.Categories.Add(cat);
                    DeviceInDB.UserId = _userId;
                }
            }
            _context.Device.Update(DeviceInDB);
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
