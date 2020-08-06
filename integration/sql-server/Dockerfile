FROM mcr.microsoft.com/mssql/server

COPY ./setup.sh  /usr/src/setup/

WORKDIR /usr/src/setup/

ENTRYPOINT /bin/bash ./setup.sh