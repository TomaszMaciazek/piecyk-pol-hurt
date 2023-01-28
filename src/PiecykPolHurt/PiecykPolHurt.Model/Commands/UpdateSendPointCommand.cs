﻿namespace PiecykPolHurt.Model.Commands
{
    public class UpdateSendPointCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public double Logitude { get; set; }
        public double Latitude { get; set; }
        public bool IsActive { get; set; }
    }
}
