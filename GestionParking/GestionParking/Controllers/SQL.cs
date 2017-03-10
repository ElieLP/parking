using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking;

namespace Parking.Controllers
{
    class SQL
    {
        private static void OpenSqlConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

                conn.Open();
                Console.WriteLine("State: {0}", conn.State);
                Console.WriteLine("ConnectionString: {0}",
                    conn.ConnectionString);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void CloseSqlConnection(SqlConnection conn)
        {
            try
            {
                conn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void GetSlots(SqlConnection conn)//Acquérir tout les slots
        {
            string sqlCmd = "SELECT * FROM places";
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            int i = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    //TODO
                    //Faire ce qu'il y a à faire
                    //Exemple : Console.WriteLine(reader.GetInt32(0) + " " + reader.GetString(1));


                }
            }
        }

        private static void SetSlot(SqlConnection conn)
        {
            string sqlCmd = "SET ";
        }

        private static void GetSlot(SqlConnection conn, Slot p_slot) //Info d'un slot
        {
            string sqlCmd = "SELECT * FROM Slots WHERE Id=" + p_slot.ID + ";";
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                /*while (reader.Read())
                {
                    p_slot.location = new Coord(reader.GetInt32(1), reader.GetInt32(2));
                    p_slot.lastTimeReserved = reader.GetDateTime(3);
                    p_slot.lastTimeTaken = reader.GetDateTime(4);
                    
                    if(reader.GetString(4) == "Empty")
                    {
                        p_slot.state = SlotState.Empty;
                    }
                    else if(reader.GetString(4) == "Used")
                    {
                        p_slot.state = SlotState.Used;
                    }
                    else
                    {
                        p_slot.state = SlotState.Reserved;
                    }

                }*/
            }
        }

        private static void GetExit(SqlConnection conn, Exit p_exit)
        {
            string sqlCmd = "SELECT * FROM Exit WHERE Id=" + p_exit.ID + ";";
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    p_exit.location = new Coord(reader.GetInt32(1), reader.GetInt32(2));
                    if(reader.GetBoolean(3) == true)
                    {
                        p_exit.Opened = true;
                    }
                    else
                    {
                        p_exit.Opened = false;
                    }
                }
            }
        }

        private static void GetFloor(SqlConnection conn, Floor p_floor)
        {
            string sqlCmd = "SELECT * FROM Floor WHERE Id=" + p_floor.ID + ";";
            SqlCommand cmd = new SqlCommand(sqlCmd, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                string sqlCmdSlot = "SELECT * FROM Slots WHERE IdFloor=" + p_floor.ID + ";";
                SqlCommand cmdSlot = new SqlCommand(sqlCmdSlot, conn);



                using (SqlDataReader readerSlot = cmdSlot.ExecuteReader())
                {
                    Slot temp_slot = new Slot(readerSlot.GetInt32(0), readerSlot.GetInt32(1), readerSlot.GetInt32(2), readerSlot.GetDateTime(3), readerSlot.GetDateTime(4), readerSlot.GetString(5));
                    
                }
            }
        }

    }

}
