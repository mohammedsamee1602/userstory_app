using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.OleDb;

namespace User_Story
{
   

    class AttendanceSystem
    {
        private static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\AttendanceSystem\attendance_system.accdb";

        public static void InitializeDatabase()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    // Create tbl_events
                    string createEventsTable = @"
                    CREATE TABLE tbl_events (
                        EventID AUTOINCREMENT PRIMARY KEY,
                        EventName TEXT(255),
                        EventDate DATETIME,
                        TotalGuests INT
                    )";
                    using (OleDbCommand cmd = new OleDbCommand(createEventsTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("tbl_events created successfully.");
                    }

                    // Create tbl_attendance
                    string createAttendanceTable = @"
                    CREATE TABLE tbl_attendance (
                        AttendanceID AUTOINCREMENT PRIMARY KEY,
                        EventID INT,
                        UserName TEXT(255),
                        UserEmail TEXT(255),
                        AttendanceDate DATETIME,
                        FOREIGN KEY (EventID) REFERENCES tbl_events(EventID)
                    )";
                    using (OleDbCommand cmd = new OleDbCommand(createAttendanceTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("tbl_attendance created successfully.");
                    }

                    // Insert dummy data into tbl_events
                    string insertEvents = @"
                    INSERT INTO tbl_events (EventName, EventDate, TotalGuests)
                    VALUES
                        ('Tech Meetup', #2024-12-12#, 50),
                        ('Code Jam', #2024-12-15#, 30),
                        ('AI Workshop', #2024-12-20#, 40)";
                    using (OleDbCommand cmd = new OleDbCommand(insertEvents, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Dummy data inserted into tbl_events.");
                    }

                    // Insert dummy data into tbl_attendance
                    string insertAttendance = @"
                    INSERT INTO tbl_attendance (EventID, UserName, UserEmail, AttendanceDate)
                    VALUES
                        (1, 'John Doe', 'john@example.com', #2024-12-12#),
                        (1, 'Jane Smith', 'jane@example.com', #2024-12-12#),
                        (2, 'Alice Cooper', 'alice@example.com', #2024-12-15#),
                        (3, 'Bob Johnson', 'bob@example.com', #2024-12-20#)";
                    using (OleDbCommand cmd = new OleDbCommand(insertAttendance, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Dummy data inserted into tbl_attendance.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error initializing database: " + ex.Message);
            }
        }

       
    }

}
