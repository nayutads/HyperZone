using HyperZone.Entities;
using System;

namespace HyperZone.Repositories
{
    public interface ITemperatureData
    {
        TemperatureTableEntity GetRecord(int id);

        void SetRecord(DateTime time,float temperature);
    }
}
