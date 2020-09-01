# SQLParquetImporter

SQLParquetImporter is a test project that is being developed for demonstrating the use of Software Engineering best practices and architectural principles.

> :warning: Please note that the code and this documentation is not a finished project and the primary purpose of this repository is for demonstating the software design of the .NET core solution located under /src folder.

![Container Diagram](/blob/c4.png?raw=true "Container Diagram")

### TODO

##### Dotnet Project

As the codebase is still in progress, there is a big room for improvement. Following list is an overview of what needs to be implemented and what can be improved
  - PriceForecastRepository need to be extended with ADO.NET queries for efficient fetch and import opertaion as currently the grouping logic is embedded into the  ParquetWriterService
  
- ParquetWriter library is far from being efficient and has only been developed for demonstrating the successful write operations. For better scalability, both time and space complexity of the method WriteFile() needs to be analyzed for different values of pageSize and improved

##### Mock/Integration Environment
Since the primary purpose of the mock setup was having an environment to simulate actual use case, this suite of scripts is currently not in production grade, however, it would be possible improve the quality of this part of the repository and use these utilities as an integration testing infrastructure
- Environment variables are currently hard-coded in the docker-compose.yml file, these values need to be refactored into an .env file and linked for CI/CD usability
- The data import script is currently not invoked in the entrypoint of the py_agent container, an additional script can be introduced into the environment for orchestrating the integration tests
- The main project needs to be dockerized as well, for CI testing

- Additionally, a standalone Hadoop container can be included into the integration setup for more end-to-end testing, by writing the parquet output into HDFS.

### Tech
This repository can be splitted into two different codebases. Under the /integration folder, there's a dockerized mock environment for:
- Creating a database instance
- Setting up a python scripting environment for creating random price forecasts based on normal distribution and populating the database with this mock data

Under the /src folder, there is a Visual Studio solution which consists of 4 different .NET Core projects as listed below:

- SqlParquetImporter.Api
- SqlParquetImporter.Domain
- SqlParquetImporter.Data
- SqlParquetImporter.Infra

The import operation has been modelled as an event source and its state is managed in the centralized database server. The main advantage of this design choice is its ability to horizontally scale the process into multiple instances and run the bulk import operations in parallel. 

### Installation
For setting up the mock environment, please run following command in the /integration folder

```sh
$ docker-compose up
$ docker attach py_agent
$ python main.py --input forecast_input.csv
```
The running script will later guide you for iterative generation and insertion of mock data. As stated above, this process can later be improved and evolved into an integration testing setup. 

For setting up the Import tool

```sh
$ dotnet publish -c release -r ubuntu.16.04-x64 --self-contained
$ dotnet run
```
Once the dotnet application is up and running, you can trigger the import operation by making a POST request to the following endpoint: 
```
/Forecast?pageSize=x
```

