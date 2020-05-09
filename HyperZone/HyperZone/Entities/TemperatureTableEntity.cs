using System;
using SQLite;

namespace HyperZone.Entities
{
    public class TemperatureTableEntity
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public DateTime Datetime { get; set; }
        public float Temperature { get; set; }
    }
}