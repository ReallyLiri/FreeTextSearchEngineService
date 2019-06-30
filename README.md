# FreeTextSearchEngineService
An example project written for interview exercise.

## The service
The service is implementing a simple free text search engine service.

The engine is supplying the following capabilities:
* Index – an ability to add a new document/s that later can be surfaced during search
* search(query) – an ability to provide a query and receive as output a ranked list of documents based on their similarity to the query

## Running
Compile the C# sln using Visual Studio, and run.

The service is running on port 5000 (configurable via `App.config` in main project).
