namespace Airline.Models;

using System.Data;
using System.Data.SqlClient;


    public class SeatSelectionModel
    {
        public List<SeatInfo> GetAvailableSeats(string flightID)
        {
            SqlQueryHelper sqh = new SqlQueryHelper();
            var seats = new List<SeatInfo>();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@FlightID", SqlDbType.VarChar, 50) { Value = flightID }
            };

            string query = @"
                SELECT s.seat_id, s.class, s.is_available, s.aircraft_id
                FROM SEAT s
                INNER JOIN FLIGHT f ON f.aircraft_id = s.aircraft_id
                WHERE f.fid = @FlightID
                ORDER BY s.seat_id;";

            var result = sqh.MakeCommandWithReturn(
                query,
                sqh.GetConnectionObject(),
                parameters,
                "",
                4
            );

            foreach (List<object> row in result)
            {
                seats.Add(new SeatInfo
                {
                    SeatID = row[0].ToString(),
                    Class = row[1].ToString(),
                    IsAvailable = Convert.ToBoolean(row[2]),
                    AircraftID = row[3].ToString()
                });
            }

            return seats;
        }

        public bool UpdateSeatSelection(string boardingID, string seatID)
        {
            SqlQueryHelper sqh = new SqlQueryHelper();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@BoardingID", SqlDbType.VarChar, 50) { Value = boardingID },
                new SqlParameter("@SeatID", SqlDbType.VarChar, 50) { Value = seatID }
            };

            string query1 = @"
            UPDATE Boarding 
            SET Seat_Number = @SeatID 
            WHERE Board_ID = @BoardingID;";

            string query2 = @"
            UPDATE SEAT 
            SET is_available = 0 
            WHERE seat_id = @SeatID;";

            var affectedRows1 = sqh.MakeCommandWithoutReturn(
                query1,
                parameters,
                sqh.GetConnectionObject()
            );
            var affectedRows2 = sqh.MakeCommandWithoutReturn(
                query2,
                parameters,
                sqh.GetConnectionObject()
            );
            return affectedRows1+affectedRows2 > 0;
        }

        public bool ValidateBoardingPass(string boardingID, string flightID)
        {
            SqlQueryHelper sqh = new SqlQueryHelper();

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@BoardingID", SqlDbType.VarChar, 50) { Value = boardingID },
                new SqlParameter("@FlightID", SqlDbType.VarChar, 50) { Value = flightID }
            };

            string query = @"
                SELECT COUNT(*)
                FROM Boarding b
                INNER JOIN TICKET t ON b.ticket_id = t.Ticket_ID
                WHERE b.Board_ID = @BoardingID 
                AND t.Flight_id = @FlightID;";

            var result = sqh.MakeCommandWithReturn(
                query,
                sqh.GetConnectionObject(),
                parameters,
                "",
                1
            );

            return result.Count > 0 && Convert.ToInt32(result[0][0]) > 0;
        }
    }

