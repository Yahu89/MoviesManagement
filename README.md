## Movies Management API
The project is Movies Management Library (API) written in .NET 8.0 + simple Web Application GUI.
In the project Rate property is skiped intentionally when create (and edit). Rate should be a result from voting, not from writing by hand. That's the reason of.
## External Packages and Libraries
* Microsoft Entity Framework Core (for ORM database approach and code first)
* Swagger is also added and configured as a default
## How to Run
### Prerequisites
* .NET 8.0.
* Visual Studio 2022 recommended
* To run project open MoviesManagementApi.sln in Visual Studio and run the solution (both projects should be set as a startup)
## API Endpoints
**GET (api/Movie/list)** 
* No parameters </br>

**GET (api/Movie/{id})**
* Parameters: id </br>

**POST (api/Movie/create)**
* No parameters
* Body: JSON with properties *Title*, *Director*, *Year* </br>

**POST (api/Movie/createrange)**
* Body: JSON (list of objects) with properties *Title*, *Director*, *Year* </br>

**PUT (api/Movie/edit/{id})**
* Parameters: id
* Body: JSON with properties *Title*, *Director*, *Year* </br>

**DELETE (api/Movie/delete/{id})**
* Parameters: id

API documentation is shown in Swagger as well
