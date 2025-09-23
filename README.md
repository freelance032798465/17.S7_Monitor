# PLC S7-1200 DataBlock Monitor

A **Windows Forms application in C#** for monitoring and logging data from **Siemens PLC S7-1200**.  
The program connects to a **MySQL database** for data storage, filtering, exporting, and real-time monitoring.  

---

## Features
- Read and write DataBlocks from PLC S7-1200  
- Store PLC data into **MySQL database**  
- Filter data from the database based on user-defined conditions  
- Export data to **CSV files**  
- Display on-screen alerts when PLC connection issues occur  
- Execute automatic tasks based on predefined conditions  

---

## System Requirements
- Windows 10/11  
- .NET Framework 4.7.2 or higher  
- MySQL Server 5.7 / 8.0  
- Siemens PLC S7-1200  

---

## Installation
1. Install .NET Framework (if not already installed).  
2. Install MySQL Server and create a database for logging PLC data.  
3. Download and extract the program files.  
4. Import database "TestMySQL/db_s7_monitor_table_log.sql"
