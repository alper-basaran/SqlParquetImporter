from datetime import datetime  
from datetime import timedelta 
import numpy as np 


def generate_price_forecast(forecast, hour, day_offset):
    '''
    A function for generating a SQL statement ready for execution
    Generates random prices based with normal distribution with mean:base_price and std:price_deviation
    '''

    forecast_base_date = datetime.strptime(forecast[0], '%d-%m-%y')  + timedelta(days=day_offset)
    hour_of_day = hour
    forecast_model = forecast[2]
    market = forecast[3]
    product = forecast[4]
    country_code = forecast[5]
    category = forecast[6]
    base_price = float(forecast[7])
    price_deviation = float(forecast[8])
 
    insert_template = '''
    INSERT INTO [dbo].[PriceForecasts]
            ([ForecastDateTime]
            ,[ForecastModel]
            ,[Market]
            ,[Product]
            ,[CountryCode]
            ,[ForecastedDate]
            ,[Category]
            ,[Price])
        VALUES'''

    value_template = '''
            ('{forecast_date}'
            ,'{forecast_model}'
            ,'{market}'
            ,'{product}'
            ,'{country_code}'
            ,'{forecasted_date}'
            ,'{category}'
            , {price})
    '''
    price_samples = np.random.normal(base_price, price_deviation, 30)
    value_array = []
    seperator = ","
    for i in range(30):
        now = datetime.now()
        forecasted_date = forecast_base_date + timedelta(days=i, hours=hour_of_day)
        price = round(price_samples[i], 2)
        forecast = value_template.format(forecast_date=now,
            forecast_model=forecast_model,
            market=market,
            product=product,
            country_code = country_code,
            forecasted_date = forecasted_date,
            category=category,
            price = price)
        value_array.append(forecast)
    
    return insert_template + seperator.join(value_array)
        