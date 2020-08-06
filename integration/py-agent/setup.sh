echo 'Initializing environment'
echo $DB_SERVER
echo $SA_PASSWORD

pip install -r requirements.txt
#pip freeze > requirements.txt

#curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | tee /etc/apt/sources.list.d/msprod.list
apt update
apt install mssql-tools

#https://github.com/twright-msft/mssql-node-docker-demo-app/blob/master/import-data.sh
#run the setup script to create the DB and the schema in the DB
#do this in a loop because the timing for when the SQL instance is ready is indeterminate

echo 'Creating  database with empty initial state'

for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S $DB_SERVER -U sa -P $SA_PASSWORD -d master -i create-database.sql
    if [ $? -eq 0 ]
    then
        echo "Database schema creation is completed"
        break
    else
        echo "Creating initial schema..."
        sleep 15
    fi
done


#2 Countries
#2 Categories
#2 Products
#2 Years
tail -f /dev/null


