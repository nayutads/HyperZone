using HyperZone.Entities;
using HyperZone.Repositories;
using System;
using SQLite;
using System.Collections.ObjectModel;

namespace HyperZone.Infrastructure.SQLiteSolver
{
    /// <summary>
    /// SQLiteへの問い合わせを行うクラス
    /// </summary>
    public class TemperatureSQLite : ITemperatureData
    {
        /// <summary>
        /// 未実装
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TemperatureTableEntity GetRecord(int id)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// temperatureテーブルの中身をすべて取得する
        /// </summary>
        /// <returns>レコードの中身</returns>
        public ObservableCollection<TemperatureTableEntity> GetAllRecords()
        {
            var results = new ObservableCollection<TemperatureTableEntity>();
            using (var db = new SQLiteConnection(SQLiteHelper.DbPath))
            {
                var table = db.Table<TemperatureTableEntity>();
                
                foreach (var t in table)
                {
                    results.Add(t);
                }
            }
            return results;
        }


        public ObservableCollection<TemperatureTableEntity> GetAllRecordsOrderByDatetime()
        {
            var results = new ObservableCollection<TemperatureTableEntity>();
            using (var db = new SQLiteConnection(SQLiteHelper.DbPath))
            {
                var table = db.Table<TemperatureTableEntity>().OrderBy(x=>x.Datetime);

                foreach (var t in table)
                {
                    results.Add(t);
                }
            }
            return results;
        }


        /// <summary>
        /// Temperatureテーブルに日付、温度値をinsertする
        /// idは自動連番
        /// </summary>
        /// <param name="time">計測日時</param>
        /// <param name="temperature">体温値</param>
        public void SetRecord(DateTime time, float temperature)
        {
            using (var connection = new SQLiteConnection(SQLiteHelper.DbPath))
            {
                try
                {
                    connection.CreateTable<TemperatureTableEntity>();
                    connection.Insert(new TemperatureTableEntity() { Datetime = time, Temperature = temperature });

                }
                catch(System.Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }


        /// <summary>
        /// temperatureテーブルから全レコードを削除する
        /// </summary>
        public void DeleteAllRecords()
        {
            using(var db=new SQLiteConnection(SQLiteHelper.DbPath))
            {
                var table = db.Table<TemperatureTableEntity>();
                db.DeleteAll<TemperatureTableEntity>();
            }
        }


        /// <summary>
        /// temperatureテーブルからレコードを削除する
        /// </summary>
        /// <param name="record">削除するレコードのモデル</param>
        /// <returns>削除後のテーブルの全レコード</returns>
        public ObservableCollection<TemperatureTableEntity> DeleteRecord(TemperatureTableEntity record)
        {
            var results = new ObservableCollection<TemperatureTableEntity>();
            using(var db=new SQLiteConnection(SQLiteHelper.DbPath))
            {
                db.Delete<TemperatureTableEntity>(record.Id);
            }
            results=GetAllRecordsOrderByDatetime();
            return results;
        }

    }
}
