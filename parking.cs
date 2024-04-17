using System;
using System.Collections.Generic;
using System.Linq;

public class ParkingLot
{
    private List<Slot> slots;
    private Dictionary<string, Vehicle> vehicles;

    public ParkingLot(int totalSlots)
    {
        slots = new List<Slot>();
        vehicles = new Dictionary<string, Vehicle>();

        for (int i = 0; i < totalSlots; i++)
        {
            slots.Add(new Slot(i + 1));
        }
    }

    public void Park(Vehicle vehicle)
    {
        Slot availableSlot = slots.FirstOrDefault(slot => slot.IsAvailable);

        if (availableSlot!= null)
        {
            availableSlot.IsAvailable = false;
            availableSlot.Vehicle = vehicle;
            vehicles[vehicle.RegistrationNumber] = vehicle;
            Console.WriteLine($"Allocated slot number: {availableSlot.Number}");
        }
        else
        {
            Console.WriteLine("Sorry, parking lot is full");
        }
    }

    public void Leave(int slotNumber)
    {
        Slot slot = slots.FirstOrDefault(s => s.Number == slotNumber);

        if (slot!= null &&!slot.IsAvailable)
        {
            slot.IsAvailable = true;
            slot.Vehicle = null;
            vehicles.Remove(slot.Vehicle.RegistrationNumber);
            Console.WriteLine($"Slot number {slotNumber} is free");
        }
    }

    public void Status()
    {
        Console.WriteLine("Slot \tNo. \tType\tRegistration No Colour");
        foreach (Slot slot in slots)
        {
            if (!slot.IsAvailable)
            {
                Console.WriteLine($"{slot.Number} \t{slot.Vehicle.RegistrationNumber} \t{slot.Vehicle.Type}\t{slot.Vehicle.Color}");
            }
        }
    }

    public int TypeOfVehicles(string vehicleType)
    {
        return vehicles.Values.Count(v => v.Type == vehicleType);
    }

    public string[] RegistrationNumbersForVehiclesWithOddPlate()
    {
        return vehicles.Values.Where(v => v.RegistrationNumber.EndsWith("1", "3", "5", "7", "9")).Select(v => v.RegistrationNumber).ToArray();
    }

    public string[] RegistrationNumbersForVehiclesWithEvenPlate()
    {
        return vehicles.Values.Where(v => v.RegistrationNumber.EndsWith("0", "2", "4", "6", "8")).Select(v => v.RegistrationNumber).ToArray();
    }

    public string[] RegistrationNumbersForVehiclesWithColour(string color)
    {
        return vehicles.Values.Where(v => v.Color == color).Select(v => v.RegistrationNumber).ToArray();
    }

    public int[] SlotNumbersForVehiclesWithColour(string color)
    {
        return vehicles.Values.Where(v => v.Color == color).Select(v => slots.FindIndex(s => s.Vehicle == v) + 1).ToArray();
    }

    public int SlotNumberForRegistrationNumber(string registrationNumber)
    {
        Slot slot = slots.FirstOrDefault(s => s.Vehicle!= null && s.Vehicle.RegistrationNumber == registrationNumber);

        if (slot!= null)
        {
            return slot.Number;
        }

        return -1;
    }
}

public class Slot
{
    public int Number { get; set; }
    public bool IsAvailable { get; set; }
    public Vehicle Vehicle { get; set; }

    public Slot(int number)
    {
        Number = number;
        IsAvailable = true;
        Vehicle = null;
    }
}

public class Vehicle
{
    public string RegistrationNumber { get; set; }
    public string Type { get; set; }
    public string Color { get; set; }

    public Vehicle(string registrationNumber, string type, string color)
    {
        RegistrationNumber = registrationNumber;
        Type = type;
        Color = color;
    }
}