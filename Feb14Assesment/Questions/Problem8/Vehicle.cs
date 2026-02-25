using System;

namespace Problem8.TrafficViolation
{
    public abstract class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string Model { get; set; }
        public string OwnerName { get; set; }

        protected Vehicle(string regNo, string model, string owner)
        {
            RegistrationNumber = regNo;
            Model = model;
            OwnerName = owner;
        }

        public abstract string GetVehicleType();
    }

    public class Car : Vehicle
    {
        public Car(string regNo, string model, string owner)
            : base(regNo, model, owner) { }

        public override string GetVehicleType() => "Car";
    }

    public class Truck : Vehicle
    {
        public Truck(string regNo, string model, string owner)
            : base(regNo, model, owner) { }

        public override string GetVehicleType() => "Truck";
    }

    public class Bike : Vehicle
    {
        public Bike(string regNo, string model, string owner)
            : base(regNo, model, owner) { }

        public override string GetVehicleType() => "Bike";
    }
}
