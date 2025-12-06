using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class PhotoGear
{
    public Guid Id { get; }
    public Guid PhotographerId { get; private set; }
    public string Name { get; private set; }
    public string Type { get; private set; }
    public string Model { get; private set; }
    public string SerialNumber { get; private set; }
    public GearStatus Status { get; private set; }
    public DateTime CreatedAt { get; }

    private PhotoGear(
        Guid id,
        Guid photographerId,
        string name,
        string type,
        string model,
        string serialNumber,
        GearStatus status,
        DateTime createdAt)
    {
        Id = id;
        PhotographerId = photographerId;
        Name = name;
        Type = type;
        Model = model;
        SerialNumber = serialNumber;
        Status = status;
        CreatedAt = createdAt;
    }

    public static PhotoGear New(
        Guid id,
        Guid photographerId,
        string name,
        string type,
        string model,
        string serialNumber)
    {
        return new PhotoGear(id, photographerId, name, type, model, serialNumber, GearStatus.Operational, DateTime.UtcNow);
    }

    public void UpdateDetails(string name, string model)
    {
        Name = name;
        Model = model;
    }

    public void ChangeStatus(GearStatus newStatus)
    {
        Status = newStatus;
    }
}

public enum GearStatus
{
    Operational,
    UnderMaintenance,
    OutOfService
}

