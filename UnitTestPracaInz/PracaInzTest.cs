using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracaInz;

namespace UnitTestPracaInz
{
    [TestClass]
    public class PracaInzTest
    {
        private readonly PracaInz.Services.NetworkDeviceServices networkDeviceServices;
        public PracaInzTest(PracaInz.Services.NetworkDeviceServices _networkDeviceServices)
        {
            networkDeviceServices = _networkDeviceServices;
        }
        [TestMethod]
        public void TestMethod1()
        {

        }
        [TestMethod]
        public async Task DodajUrzadzenieSiecioweAsync()
        {
            string manufacturer = "Dell";
            string model = "Vostro";
            string serialNumber = "3JJ0TC2";
            string deviceDescription = "Description1";
            int userId = 1004;
            int categoryId = 1003;
            string ipAddress = "192.168.0.1";

            await networkDeviceServices.AddAsync(manufacturer,
                model,
                serialNumber,
                deviceDescription,
                userId,
                categoryId,
                ipAddress);


            
        }
    }
}
