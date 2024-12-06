from methods import *
import json
import random
import datetime
import math
import ast

# DownloadInjectionData()


def insert_air_craft_data():
    cursor, conn = MountingTargettedServer()
    df = CreateDataFrame("aircrafts_data", MountingScrappingServer())
    print(df.head())
    df["aircraft_code"] = df["aircraft_code"].astype(str)
    df["model"] = df["model"].apply(json.loads)
    df["range"] = df["range"].astype(float)
    print(df.info())
    for i in range(df.shape[0]):
        aid = df["aircraft_code"][i]
        model = df["model"][i]["en"]
        speed = random.randint(299, 651)
        manufacture = df["model"][i]["en"].split()[0]
        capacity = random.randint(99, 251)
        # Generate a random datetime
        random_year = random.randint(2000, 2024)  # Random year between 2000 and 2024
        random_month = random.randint(1, 12)  # Random month between 1 and 12
        random_day = random.randint(
            1, 28
        )  # Random day between 1 and 28 (for simplicity, assuming no leap years)

        random_date = datetime.date(random_year, random_month, random_day)
        print(
            f"Inserting record: {aid}, {model}, {speed}, {manufacture}, {capacity}, {random_date}"
        )
        sql_query = f"""
        INSERT INTO AIRCRAFT (aid, model, speed, manufacture, capacity, join_date)
        VALUES ('{aid}', '{model}', {speed}, '{manufacture}', {capacity}, '{random_date}')
        """
        MakeASqlServerQuery(
            SqlQuery=sql_query,
            cursor=cursor,
            conn=conn,
        )

    cursor.close()  # Ensure the cursor is closed
    conn.close()


def insert_airports_data():
    cursor, conn = MountingTargettedServer()
    df = CreateDataFrame("airports_data", MountingScrappingServer())
    print(df.head())
    df["airport_code"] = df["airport_code"].astype(str)
    df["airport_name"] = df["airport_name"].apply(json.loads)
    df["coordinates"] = df["coordinates"].astype(str)
    print(df.info())
    for i in range(len(df)):
        aic = df["airport_code"][i]
        name = df["airport_name"][i]["en"]
        location = df["coordinates"][i]

        sql_query = f"""
        INSERT INTO AIRPORT (airport_id, name_, location_)
        VALUES  ('{aic}', '{name}','{location}')
        """
        MakeASqlServerQuery(
            SqlQuery=sql_query,
            cursor=cursor,
            conn=conn,
        )
        print(f"counter {i}")

    cursor.close()  # Ensure the cursor is closed
    conn.close()


def building_routes_table():
    cursor, conn = MountingTargettedServer()
    df = CreateDataFrame("airports_data", MountingScrappingServer())
    # df["coordinates"] = df["coordinates"].apply(lambda x: tuple(map(float, x.split(','))))
    print(df["coordinates"][0])
    df["airport_code"] = df["airport_code"].astype(str)
    for i in range(df.shape[0]):
        for j in range(len(df)):
            lon1, lat1 = ast.literal_eval(df["coordinates"][i])
            lon2, lat2 = ast.literal_eval(df["coordinates"][j])

            lat1 = math.radians(float(lat1))
            lon1 = math.radians(float(lon1))
            lat2 = math.radians(float(lat2))
            lon2 = math.radians(float(lon2))
            # Skip if airports do not exist in the AIRPORT table

            delta_lat = lat2 - lat1
            delta_lon = lon2 - lon1

            # Haversine formula
            a = (
                math.sin(delta_lat / 2) ** 2
                + math.cos(lat1) * math.cos(lat2) * math.sin(delta_lon / 2) ** 2
            )
            c = 2 * math.atan2(math.sqrt(a), math.sqrt(1 - a))

            # Earth's radius in kilometers
            radius = 6371.0  # use 3958.8 for miles

            # in kilometers
            distance = radius * c

            id = df["airport_code"][i] + "_" + df["airport_code"][j]

            # fixed_overhead = Time for takeoff, landing, and waiting
            # duration = fixed_overhead + (distance / speed)

            duration = 1 + (distance / 650)
            base_price = duration * math.ceil(distance)

            sql_query = f"""
            INSERT INTO ROUTE (ro_id,start_airport,end_airport, distance, duration_in_hours, base_price)
            VALUES  ('{id}','{df["airport_code"][i]}', '{df["airport_code"][j]}',{round(distance)},{math.ceil(duration)} ,{round(base_price)})
            """
            MakeASqlServerQuery(
                SqlQuery=sql_query,
                cursor=cursor,
                conn=conn,
            )
    cursor.close()  # Ensure the cursor is closed
    conn.close()


def building_flights_table():
    cursor, conn = MountingTargettedServer()
    df = CreateDataFrame("flights", MountingScrappingServer())
    df["fid"] = df["flight_no"] + df["flight_id"].astype(str)
    df["scheduled_departure"] = pd.to_datetime(df["scheduled_departure"])
    df["scheduled_arrival"] = pd.to_datetime(df["scheduled_arrival"])
    df["route_id"] = df["departure_airport"] + "_" + df["arrival_airport"]
    df["duration"] = df["scheduled_arrival"] - df["scheduled_departure"]
    df["duration_hours"] = df["duration"].apply(
        lambda x: math.ceil(x.total_seconds() / 3600)
    )
    for i in range(df.shape[0]):
        # df.loc[i, "scheduled_departure"] = pd.to_datetime(df["scheduled_departure"][i])
        # print(f"""{df["flight_id"][i]} ,
        #     {df["flight_no"][i]} ,
        #     {df["scheduled_departure"][i]} ,
        #     {df["departure_airport"][i]}
        #     """)
        sql_query = f"""
            INSERT INTO FLIGHT (fid, depart_time , arrival_time , status_ , route_id , duration , aircraft_id)
            VALUES  ('{df["fid"][i]}',
            '{df["scheduled_departure"][i]}', 
            '{df["scheduled_arrival"][i]}',
            '{df["status"][i]}',
            '{df["route_id"][i]}' 
            ,{df["duration_hours"][i]} 
            ,'{df["aircraft_code"][i]}')
            """

        MakeASqlServerQuery(
            SqlQuery=sql_query,
            cursor=cursor,
            conn=conn,
        )
    cursor.close()  # Ensure the cursor is closed
    conn.close()



def building_seats_table():
    cursor , conn = MountingTargettedServer()
    df = CreateDataFrame("seats", MountingScrappingServer())
    df["si"] = df["aircraft_code"] +'_'+ df["seat_no"]
    for i in range(df.shape[0]):
        sql_query = f"""
            INSERT INTO SEAT (aircraft_id, seat_id , class)
            VALUES  ('{df["aircraft_code"][i]}',
            '{df["si"][i]}', 
            '{df["fare_conditions"][i]}'
            )
            """
        MakeASqlServerQuery(
            SqlQuery=sql_query,
            cursor=cursor,
            conn=conn,
        )
    cursor.close()  # Ensure the cursor is closed
    conn.close()



# def building_tickets_table():
#     cursor , conn = MountingTargettedServer()
#     df = CreateDataFrame("tickets", MountingScrappingServer())