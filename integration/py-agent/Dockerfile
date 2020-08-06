
# parent image
#lbosqmsft/mssql-python-pyodb
FROM laudio/pyodbc

# COPY ./setup.sh /usr/src/scripts/
# COPY ./requirements.txt /usr/src/scripts/
# COPY ./main.py  /usr/src/scripts/
COPY ./ /usr/src/scripts/

WORKDIR /usr/src/scripts/
ENTRYPOINT /bin/bash ./setup.sh