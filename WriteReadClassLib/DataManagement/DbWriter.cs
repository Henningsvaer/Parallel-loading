using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteReadClassLib.DataManagement
{
    public sealed class DbWriter
    {
        public string ConnectionString { get; private set; }

        public DbWriter()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void WriteToDbRow(Person person, ref string log)
        {
            // Создание подключения
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                // Открываем подключение
                connection.Open();

                // Запись в бд.
                SqlCommand command = new SqlCommand();
                command.CommandText = $"INSERT INTO People (Fio, City, Email, PhoneNumber, UpdDateTime) " +
                    $"VALUES ('{person.Fio}', '{person.City}', '{person.Email}', " +
                    $"'{person.PhoneNumber}', '{person.UpdDateTime}')";
                command.Connection = connection;
                command.ExecuteNonQuery();

                // Логирование.
                log = $"В базу записан {person.Id} {person.Fio} " +
                    $"{person.City} {person.Email} {person.PhoneNumber} {person.UpdDateTime}";

            }
            catch (SqlException ex)
            {
                //log = ex.Message;
                Console.WriteLine(ex.ErrorCode);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
                Console.WriteLine("Подключение закрыто...");
            }
        }
    }
}
