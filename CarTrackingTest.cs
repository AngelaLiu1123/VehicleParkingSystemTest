using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace VehicleParkingSystemTest
{
    [TestClass]
    public class CarTrackingTest
    {
        public VehicleTracker vt;
        int capacity = 5;
        string address = "fake street";

        [TestInitialize]
        public void CarTrackingTestInitialize()
        {
            vt = new VehicleTracker(capacity, address);
        }
        /*
         * 1.When initialized, a VehicleTracker object should have empty slots [{SlotNumber, Vehicle}] 
         * from 1 - Capacity in VehicleTracker.VehicleList (ie. { {1, null}, {2, null}, {3,null}, //etc}
         * 
         *Test GenerateSlots() to check if the this.VehicleList.count ==  this.Capacity
         */
        [TestMethod]
        public void GenerateSlotesVehicleCountEqualsCapacity()
        {
            //Arrange

            //Act
            vt.GenerateSlots();
            //Assert
            Assert.AreEqual(vt.VehicleList.Count, capacity);
        }

        /*
         * 2.If the AddVehicle method is called, it should add the vehicle to the first slot in VehicleList that is not full.
         * If there are no open slots, it should throw a generic exception with the VehicleTracker.AllSlotsFull message.
         * 
         * Test AddVehicleTest() to check if the vehicle is added to the fist null value VehicleList that is not full
         *  
         */
        [TestMethod]
        public void AddVehicleTestVehicleAddToList()
        {
            //Arrange
            Vehicle vehicle1 = new Vehicle("ABC123", true);
            //Act
            vt.AddVehicle(vehicle1);
            //Assert
            Assert.AreEqual(vt.VehicleList.ContainsValue(vehicle1), true);
        }

        /*
         * 2.If the AddVehicle method is called, it should add the vehicle to the first slot in VehicleList that is not full.
         * If there are no open slots, it should throw a generic exception with the VehicleTracker.AllSlotsFull message.
         * 
         * If there are no open slots, show error message
         *  
         */
        [TestMethod]
        public void AddVehicleTestThrowExceptionWhenSlotsFull()
        {
            //Arrange
            Vehicle vehicle3 = new Vehicle("GHI789", true);

            //Act&//Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() => vt.AddVehicle(vehicle3));
        }

        /**
         * 3. RemoveVehicle should accept either a licence or slot number, and set that slot¡¯s vehicle to ¡°null¡±.
         * 3.1 accept a slot number
         */
        [TestMethod]
        public void RemoveVehicleEitherBySlotNum()
        {
            //Arrange
            int slotNum = 1;

            //Act
            vt.RemoveVehicle(slotNum);

            //Assert
            Assert.IsTrue(vt.VehicleList[slotNum] == null);
        }

        /**
         * 3. RemoveVehicle should accept either a licence or slot number, and set that slot¡¯s vehicle to ¡°null¡±.
         * 3.1 accept a slot number
         */
        [TestMethod]
        public void RemoveVehicleEitherByLicence()
        {
            //Arrange
            Vehicle vehicle4 = new Vehicle("GHI789", true);
            

            //Act
            vt.AddVehicle(vehicle4);
            vt.RemoveVehicle("GHI789");

            //Assert
            Assert.IsFalse(vt.VehicleList.ContainsValue(vehicle4));

        }

        /*
         * RemoveVehicle should throw an exception if the licence passed to RemoveVehicle() is not found, 
         * if the slot number is invalid (greater than capacity or negative), or the slot is not filled.
         */
        [TestMethod]
        public void RemoveVehicleTestInvalidLicenceThrowException()
        {
            //Arrange
            String licence = "GHI789";
            //Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => vt.RemoveVehicle(licence));

        }

        [TestMethod]
        public void RemoveVehicleTestInvalidSlotThrowException()
        {
            //Arrange
            int slotNum = 9;
            int slotNum2 = -3;
            ////Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => vt.RemoveVehicle(slotNum));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => vt.RemoveVehicle(slotNum2));
        }

        /*
         * VehicleTracker should track the proper number of slots available at all times with VehicleTracker.SlotsAvailable.
         */
        [TestMethod]
        public void AddVehicleTestRemainingOpenSlots()
        {
            //Arrange
            Vehicle vehicle2 = new Vehicle("DEF456", true);
            //Act
            vt.AddVehicle(vehicle2);
            int ExpectedAvalibleSlots = 4;
            //Assert
            Assert.AreEqual(vt.SlotsAvailable, ExpectedAvalibleSlots);
        }

        [TestMethod]
        public void GenerateSlotsTestInitialAvalibleSlots()
        {
            //Act
            vt.GenerateSlots();
            //Assert
            Assert.AreEqual(vt.SlotsAvailable, vt.Capacity);
        }

        /*
         * The VehicleTracker.ParkedPassholders() method should return a list of all parked vehicles that have a pass.
         */
        [TestMethod]
        public void ParkedPassholdersTestReturnAllParkedVehicles()
        {
            //Arrange
            Vehicle vehicle1 = new Vehicle("ABC321", true);
            Vehicle vehicle2 = new Vehicle("DEF456", true);
            //Act
            vt.AddVehicle(vehicle1);
            vt.AddVehicle(vehicle2);
            List<Vehicle> passHolders =vt.ParkedPassholders();
            //Assert
            Assert.AreEqual(passHolders.Count, 2);
        }

        /*
         * VehicleTracker.PassholderPercentage() method should return the percentage of vehicles that are parked which have passes.
         * Note that this method uses the ParkedPassholders() method to get a count of passholders.
         */
        [TestMethod]
        public void PassholderPercentageTestReturnPercentage()
        {
            //Arrange
            Vehicle vehicle1 = new Vehicle("ABC321", true);
            Vehicle vehicle2 = new Vehicle("DEF456", true);
            //Act
            vt.AddVehicle(vehicle1);
            vt.AddVehicle(vehicle2);
            float pct = vt.PassholderPercentage();
            //Assert
            Assert.AreEqual(pct, 40);
        }
    }
}