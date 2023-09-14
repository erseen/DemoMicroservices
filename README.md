<h2>Microservices with Multiple Databases and Ocelot API Gateway</h2>

This repository contains a set of microservices designed to serve different purposes, each using a different database technology. The microservices in this project include:

<h4> ProductApi:</h4> This microservice utilizes MySQL as its database.

<h4>CustomerApi:</h4> CustomerApi is powered by Microsoft SQL Server (MSSQL).

<h4>OrderApi:</h4> OrderApi makes use of MongoDB as its database.

<h2>API Gateway with Ocelot</h2>
To manage the routing and communication between these microservices, we have implemented Ocelot API Gateway. Ocelot allows us to efficiently route requests to the appropriate microservice, abstracting the complexity of handling multiple services behind a single entry point.

<h2>Authentication with JWT</h2>
For securing our microservices, we employ JSON Web Tokens (JWT) for authentication. This ensures that only authorized users or applications can access our services, enhancing the security of our system.

<h2>Docker Containerization</h2>
Our microservices are containerized using Docker, making it easy to deploy and manage them in various environments. Docker containers provide isolation and portability, allowing us to scale and maintain our services effortlessly.

<h2>CRUD Operations</h2>
Each of our microservices supports CRUD (Create, Read, Update, Delete) operations, enabling efficient data management. You can interact with these services to perform various data operations according to your needs.

Feel free to explore each microservice's individual documentation for more details on how to use and interact with them.
