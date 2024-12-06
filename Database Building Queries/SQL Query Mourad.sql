-- Schedule Flights page
SELECT start_airport, end_airport, CAST(depart_time AS TIME), duration
FROM (FLIGHT JOIN [ROUTE] ON route_id = ro_id)
WHERE CAST(depart_time AS DATE) = "6-12-2024";

INSERT INTO FLIGHT (route_id, depart_time) VALUES ("JFKLHR", "2-4-2024 12:31:43 -02:00");

UPDATE FLIGHT
SET depart_time = "6-12-2024 04:12:44 -02:00"
WHERE fid like "JFKLHR612241231%";

INSERT INTO WORK (Flight_ID, Employee_ID) VALUES ("JFKLHR61224123100", "214341");

-- Amenities
SELECT *
FROM supplier;

SELECT fid, CAST(depart_time AS TIME), Supplier_ID, content
FROM (Supply JOIN FLIGHT ON Flight_ID= fid)
WHERE CAST(depart_time AS DATE) = "6-12-2024";

-- TrackCargo
SELECT Cargo_ID, Cargo.capacity, model, AIRCRAFT.capacity 
FROM (Cargo JOIN AIRCRAFT ON Aircraft_ID = aid);

SELECT count(*)
FROM Cargo;

-- Monitor Incident
SELECT Flight_ID, Incident_location,date_,No_of_Casualties ,No_of_Survivors ,Cause_of_Incident, route_id
FROM (Incident JOIN FLIGHT ON Flight_ID= fid);

SELECT COUNT(*)
FROM Incident

SELECT SUM(No_of_Casualties), SUM(No_of_Survivors)
FROM Incident
