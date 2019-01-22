using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BiluthyrningABdel1
{
    public static class Biluthyrning
    {
        private const string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BiluthyrningAB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static Func<SqlDataReader, CarBooking> bookingFunc = (c) => (new CarBooking { Id = (int)c[0], SSN = (string)c[1], CarType = (int)c[2], CarRegistrationNumber = (string)c[3], StartTime = (DateTime)c[4], NumberOfKmStart = (int)c[5] });

        const decimal baseDayRental = 500;
        const decimal kmPrice = 20;
        static List<Car> cars = new List<Car>
            {
            new Car{CarType = 1, NumOfKm = 12000, RegNum = "LTN123"},
            new Car{CarType = 2, NumOfKm = 5500, RegNum = "VAN123"},
            new Car{CarType = 3, NumOfKm = 4000, RegNum = "MBS123"},
    };
        /// <summary>
        /// typeCar = 1 - small car, typeCar = 2 - Van, typeCar = 3 - Minibus
        /// Returns negative values for invalid types
        /// </summary>
        /// <param name="baseDayRental"></param>
        /// <param name="numberOfDays"></param>
        /// <param name="kmPrice"></param>
        /// <param name="typeCar"></param>
        /// <returns></returns>
        public static decimal CarCost(long numberOfDays, int numberOfKm, int typeCar)
        {
            return baseDayRental * numberOfDays * CarFactor(typeCar) + KmPrice(numberOfKm, typeCar);
        }

        private static decimal CarFactor(int typeCar)
        {
            switch (typeCar)
            {
                case 1:
                    return 1M;
                case 2:
                    return 1.2M;
                case 3:
                    return 1.7M;
                default:
                    return -1M;
            }
        }

        private static decimal KmPrice(int numberOfKm, int typeCar)
        {
            switch (typeCar)
            {
                case 1:
                    return 0;
                case 2:
                    return kmPrice * numberOfKm;
                case 3:
                    return kmPrice * numberOfKm * 1.5M;
                default:
                    return -1;
            }
        }

        public static void RentCar(int typeCar, string SSN)
        {
            var car = cars.Single(c => c.CarType == typeCar);
            int? carId = AddToDb<CarBooking>(new CarBooking { CarType = typeCar, NumberOfKmStart = car.NumOfKm, CarRegistrationNumber = car.RegNum, SSN = SSN, StartTime = DateTime.Now.Date });
            Console.WriteLine($"Tack för att du hyr hos oss ditt bookningsnummer är {carId}");
        }

        public static void ReturnCar(int bookingId, int newMilage)
        {
            CarBooking booking = ReadDb<CarBooking>(bookingId, bookingFunc);
            decimal totalCost = CarCost((DateTime.Now.Date.Ticks - booking.StartTime.Date.Ticks)/TimeSpan.TicksPerDay, newMilage - booking.NumberOfKmStart, booking.CarType);
            AddToDb<CarReturn>(new CarReturn { CarbookingId = bookingId, NumberOfKmReturn = newMilage, ReturnTime = DateTime.Now.Date });
            Console.WriteLine($"Den total kostnaden blir {totalCost}kr");
        }

        private static int? AddToDb<T>(T model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        var commandText = new StringBuilder($"insert into {typeof(T).Name} values (");
                        foreach (var item in model.GetType().GetProperties())
                        {
                            if (item.Name != "Id")
                            {
                                commandText.Append($"@{item.Name}, ");
                            }
                        }
                        commandText.Remove(commandText.Length - 2, 2);
                        commandText.Append($") select top 1 id from {typeof(T).Name} order by id desc");
                        command.CommandText = commandText.ToString();
                        command.CommandType = CommandType.Text;
                        command.Connection = connection;
                        foreach (var item in model.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue($"@{item.Name}", item.GetValue(model, null));
                        }
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            return reader[0] as int?;
                        }
                    }
                    connection.Close();
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        private static T ReadDb<T>(int id, Func<SqlDataReader, T> func)
        {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.CommandText = $"select * from {typeof(T).Name} where id=@id";
                            command.CommandType = CommandType.Text;
                            command.Connection = connection;

                            var idParam = command.CreateParameter();
                            idParam.ParameterName = "@id";
                            idParam.Value = id;
                            command.Parameters.Add(idParam);

                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                var outId = reader[0];
                                if (outId != null)
                                {
                                    return func(reader);
                                }
                            }
                            else
                                connection.Close();
                            return default;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return default;
                }
            }
    }
}
