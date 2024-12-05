CREATE DATABASE AIRLINE;

-- DROP DATABASE AIRLINE

USE AIRLINE


CREATE TABLE AIRPORT(
    airport_id VARCHAR(50) NOT NULL ,
    name_  VARCHAR(25) NOT NULL ,  
    location_ VARCHAR(MAX) NOT NULL ,
    PRIMARY KEY (airport_id)
);



CREATE TABLE AIRCRAFT (
    aid VARCHAR(50) NOT NULL,
    model VARCHAR(25) NOT NULL,
    manufacture VARCHAR(25) NOT NULL,
    capacity INT NOT NULL,
    join_date DATE NOT NULL,
    speed DECIMAL(10,2) NOT NULL, 
    PRIMARY KEY (aid)
);



create table Cargo(
        Cargo_ID VARCHAR(50) NOT NULL,
        Aircraft_ID VARCHAR(50) NOT NULL,
        capacity INT NOT NULL,
        PRIMARY KEY (Cargo_ID),
        -- PRIMARY KEY (Aircraft_ID , capacity), This Solution is not working , So I'm  making Cargo_ID that will be a combinationof air craft id and capacity to refrence it foreignly in baggage entitiy
        FOREIGN KEY (Aircraft_ID) REFERENCES AIRCRAFT(aid)
            ON DELETE  CASCADE
            ON UPDATE CASCADE
);






CREATE TABLE ROUTE (
    ro_id VARCHAR(50) UNIQUE NOT NULL,
    start_airport VARCHAR(50) NOT NULL,  -- First airport
    end_airport VARCHAR(50) NOT NULL,  -- Second airport
    distance BIGINT NOT NULL, 
    duration_in_hours INT NOT NULL,
    base_price BIGINT NOT NULL,
    PRIMARY KEY (ro_id), 
    FOREIGN KEY (start_airport) REFERENCES AIRPORT(airport_id)
            ON DELETE  NO ACTION
            ON UPDATE NO ACTION
    ,  
    FOREIGN KEY (end_airport) REFERENCES AIRPORT(airport_id)
            ON DELETE  NO ACTION
            ON UPDATE NO ACTION
    ,
    -- ADD CONSTRAINT UC_Route UNIQUE (start_airport, end_airport)

);




CREATE TABLE FLIGHT (
    fid   VARCHAR(50) NOT NULL UNIQUE,
    aircraft_id VARCHAR(50) NOT NULL,
    depart_time DATETIME NOT NULL,
    status_ VARCHAR(20) NOT NULL,
    route_id VARCHAR(50) NOT NULL,
    arrival_time DATETIME NOT NULL,
    duration INT NOT NULL,
    aid VARCHAR(50) NOT NULL
    PRIMARY KEY (fid),
    
    FOREIGN KEY (aircraft_id) REFERENCES AIRCRAFT(aid)
        ON DELETE  CASCADE
        ON UPDATE CASCADE
    ,
    
    FOREIGN KEY (route_id) REFERENCES ROUTE(ro_id)
        ON DELETE  CASCADE
        ON UPDATE CASCADE
);




create table Boarding(
    Board_ID VARCHAR(50) NOT NULL UNIQUE,
    Seat_Number INT NOT NULL,
    GATE VARCHAR(50) NOT NULL,
    primary key(Board_ID)
);




create table PASSENGER(
    Passport_No VARCHAR(50) NOT NULL UNIQUE,
    Name VARCHAR(25) NOT NULL , 
    gender CHAR(1) NOT NULL,
    birth_date DATE NOT NULL,
    contact_info VARCHAR(50) NOT NULL,
    nationality VARCHAR(25) NOT NULL , 
    primary key (Passport_No)
);






create table TICKET(
    Ticket_ID VARCHAR(50) UNIQUE NOT NULL,
    Passport_No VARCHAR(50) NOT NULL,
    Class VARCHAR(50) NOT NULL,
    Pay_Status VARCHAR(10) NOT NULL,
    Flight_id VARCHAR(50) NOT NULL UNIQUE,
    Passenger VARCHAR(50) NOT NULL,
    Payment_method VARCHAR(30) NOT NULL,
    date_ DATE NOT NULL,
    discount VARCHAR(30) NOT NULL,
    Board_ID VARCHAR(50) NOT NULL UNIQUE,
    PRIMARY KEY(Ticket_ID),
    FOREIGN KEY (Flight_id) REFERENCES FLIGHT(fid)
            ON DELETE  CASCADE
            ON UPDATE CASCADE,
    FOREIGN KEY (Board_ID) REFERENCES Boarding(Board_ID)
            ON DELETE  CASCADE
            ON UPDATE CASCADE
    ,
    FOREIGN KEY (Passport_No) REFERENCES Passenger(Passport_No)
            ON DELETE  CASCADE
            ON UPDATE CASCADE,

);





CREATE TABLE EMPLOYEE(
    emp_id VARCHAR(50) NOT NULL UNIQUE, 
    name_ VARCHAR(25) NOT NULL,
    role_ VARCHAR(25) NOT NULL,
    join_date DATE NOT NULL,
    contact_info VARCHAR(50) NOT NULL,
    license_number INT NOT NULL UNIQUE,
    birth_date DATE NOT NULL,
    gender CHAR(1) NOT NULL,
    nationality VARCHAR(25) NOT NULL,
    salary INT NOT NULL,
    PRIMARY KEY (emp_id)
);




CREATE TABLE WORK(
  Employee_ID VARCHAR(50) NOT NULL UNIQUE,
  Flight_ID VARCHAR(50) NOT NULL UNIQUE,
  Primary key (Employee_ID , Flight_ID),
  FOREIGN KEY (Employee_ID) REFERENCES EMPLOYEE(emp_id)
     	ON DELETE  CASCADE
        ON UPDATE CASCADE,
  FOREIGN KEY (Flight_ID) REFERENCES FLIGHT(fid)
        ON DELETE  CASCADE
        ON UPDATE CASCADE
);





Create table Baggage(
    TAG VARCHAR(50) NOT NULL UNIQUE,
    Cargo_ID VARCHAR(50) NOT NULL,
    Weight Decimal(5 , 3) NOT NULL,
    Type VARCHAR(30) NOT NULL,
    Board_No VARCHAR(50) NOT NULL UNIQUE,
    Passport_No VARCHAR(50) NOT NULL,
    primary key (TAG),
    FOREIGN KEY (Cargo_ID) REFERENCES Cargo(Cargo_ID) --Refrecing our composite primarykey
            ON DELETE  CASCADE
            ON UPDATE CASCADE,
    FOREIGN KEY (Board_No) REFERENCES Boarding(Board_ID)
            ON DELETE  CASCADE
            ON UPDATE CASCADE,
    FOREIGN KEY (Passport_No) REFERENCES PASSENGER(Passport_No)
            ON DELETE  CASCADE
            ON UPDATE CASCADE
  
);
  





create table Incident(
    Flight_ID VARCHAR(50) NOT NULL UNIQUE,
    Incident_location VARCHAR(50) NOT NULL,
    date_ DATETIME NOT NULL,
    No_of_Casualties INT NOT NULL,
    No_of_Survivors INT NOT NULL,
    Cause_of_Incident VARCHAR(50) NOT NULL,
    Penalties VARCHAR(50) NOT NULL,
    FOREIGN KEY (Flight_ID) REFERENCES FLIGHT(fid)
            ON DELETE CASCADE
            ON UPDATE CASCADE
);





create table supplier(
    Supplier_ID VARCHAR(50) UNIQUE NOT NULL,
    Content VARCHAR(50) NOT NULL,
    primary key(Supplier_ID)
);





create table Supply(
    Supplier_ID VARCHAR(50) NOT NULL UNIQUE,
    Flight_ID VARCHAR(50) NOT NULL UNIQUE,
    Primary key (Supplier_ID , Flight_ID),
    FOREIGN KEY (Supplier_ID) REFERENCES supplier(Supplier_ID)
            ON DELETE  CASCADE
            ON UPDATE CASCADE,
    FOREIGN KEY (Flight_ID) REFERENCES FLIGHT(fid)
            ON DELETE  CASCADE
            ON UPDATE CASCADE
);






create table Refund(
    Ticket_ID VARCHAR(50) UNIQUE NOT NULL,
    Amount INT NOT NULL,
    date_ DATE NOT NULL,
    Status VARCHAR(20) NOT NULL,
    -- primary key(Ticket_ID),
    FOREIGN KEY (Ticket_ID) REFERENCES TICKET(Ticket_ID)
                ON DELETE CASCADE
                ON UPDATE CASCADE
);



create table Lost_and_Found(
    LostFound_ID VARCHAR(50) UNIQUE NOT NULL,
    passenger_id  VARCHAR(50) NOT NULL,
    Baggage_TAG  VARCHAR(50) NOT NULL,
    Lost_Item VARCHAR(30) NOT NULL,
    Status VARCHAR(20) NOT NULL,
    lost_date DATE NOT NULL,
    found_date DATE NOT NULL,
    primary key(passenger_id, Baggage_TAG), --Composite Primary Key
    FOREIGN KEY (passenger_id) REFERENCES  PASSENGER(Passport_No)
        ON DELETE  NO ACTION
        ON UPDATE NO ACTION
    ,
    FOREIGN KEY (Baggage_TAG) REFERENCES Baggage(TAG)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
