import kagglehub
import pandas as pd
from sqlalchemy import create_engine, text, Connection
from sqlalchemy.exc import SQLAlchemyError
import pyodbc
from dotenv import load_dotenv
import os


def DownloadInjectionData():
    path = kagglehub.dataset_download("saadharoon27/airlines-dataset")
    # path = r"Data Sample Injection"
    print("Path to dataset files:", path)

    # Download latest version
    path = kagglehub.dataset_download("iamsouravbanerjee/airline-dataset")

    print("Path to dataset files:", path)


def MountingScrappingServer():
    load_dotenv(dotenv_path=r"Data Sample Injection\secrets.env")
    path = os.getenv("sql_lite")
    # path = r"sqlite:///E:\Coding\SQL\Project\Data Sample Injection\Data\dataset 1\travel.sqlite"
    my_conn = create_engine(path)
    my_conn = my_conn.connect()
    return my_conn


def CreateDataFrame(Table_Name: str, con: Connection):
    df = pd.read_sql(sql=text(f"SELECT* FROM {Table_Name}"), con=con)
    return df


def MountingTargettedServer():
    load_dotenv(dotenv_path=r"Data Sample Injection\secrets.env")
    conn_str = os.getenv("my_conn_str")
    # print(f"My SQL Conn STR -> {conn_str}")
    # Connect to the database
    conn = pyodbc.connect(conn_str)
    cursor = conn.cursor()
    return cursor, conn


def MakeASqlServerQuery(SqlQuery: str, cursor: pyodbc.Cursor, conn: pyodbc.Connection):
    # cursor = cursor.execute(SqlQuery)
    # rows = cursor.fetchall()
    # print(f"Return Len ===>  {len(rows)} \n")
    # for row in rows:
    #     print(row)
    # conn.commit()
    # cursor.close()
    # conn.close()

    try:
        print(
            f"Executing query: {SqlQuery}"
        )  # Debugging: print the query to check its format
        cursor.execute(SqlQuery)  # Execute the query
        print(f"Connection: {conn}, Cursor: {cursor}")

        conn.commit()  # Commit the transaction
        print("Query executed and committed successfully.")
    except Exception as e:
        print(f"Error executing query: {e}")  # Print any errors that occur
