#Basic py script for automatic generation and insertion of price forecasts to SQL Server
#This script can be used for emulating an external system which periodically pushes data to DB
#Invocation: python ./insert-forecast.py --server localhost,1433 --database PriceForecastDb --uid sa --pwd Aa123456!

import pyodbc
import argparse
import os
from datetime import datetime  
from querygen import generate_price_forecast
import csv


def get_connection(server, database, uid, pwd):

    connstr = '''
    Driver={{ODBC Driver 17 for SQL Server}};
    Server={server};
    Database={database};
    UID={uid};
    PWD={pwd};'''.format(server=server, database=database, uid=uid, pwd=pwd)
    
    return pyodbc.connect(connstr)

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--input", help="Input file", required = True)
    
    args = parser.parse_args()
    forecast_input = args.input

    server = os.environ.get('DB_SERVER')
    database = os.environ.get('DB_NAME')
    uid = os.environ.get('DB_USER')
    pwd = os.environ.get('SA_PASSWORD')

    forecast_array = []
    with open(forecast_input, newline='') as f:
        reader = csv.reader(f)
        forecast_array = list(reader)
    
    #Make below conditional...
    day_offset = 0
    hour_offset = 0
    key = 'c'
    
    while key != 'q':
        key = 'c'
        
        if(hour_offset == 24):
            hour_offset = 0
            day_offset = day_offset + 1
        print(f"Inserting forecasts for day_offset: {day_offset} hour_offset: {hour_offset}")
        
        for forecast in forecast_array:
            with get_connection(server, database, uid, pwd) as conn:
                cursor = conn.cursor()

                #Generate Insert Query
                query = generate_price_forecast(forecast, hour_offset, day_offset)

                #Insert Forecasts
                cursor.execute(query)
                conn.commit()
        hour_offset = hour_offset + 1
        print("Press any key to generate forecast for next hour or press q to quit")
        key = input()
    

if __name__ == "__main__":
    main()