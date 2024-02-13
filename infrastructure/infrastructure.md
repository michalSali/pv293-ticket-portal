- Azure Standard Load Balancer
	- 2 instances
	- load balancer between browser and frontend
	- https://learn.microsoft.com/en-us/azure/load-balancer/load-balancer-overview
	- https://azure.microsoft.com/en-us/pricing/details/load-balancer/
	- could consider Azure Front Door (CDN) - can be integrated with Azure Gateway Load Balancer


- Database:
	- https://azure.microsoft.com/en-us/pricing/details/postgresql/flexible-server/
		- flexible server
		- for less users:
			- B2s	2 vCores	4 GiB RAM	$58.108/month
		- for more users:
			- 
		- 
	- can add read-only replicas
		- For each read replica you create, you are billed for the provisioned compute in vCores and provisioned storage in GiB/month.
	- also use Azure Blob Storage for blob data

_ Backend:
	- ASP.NET Core Web API
	- Azure Container App

- Cache:
	- Azure Cache for Redis
	- is possible to set read-only replicas

- Frontend
	- node.js app
	- necessary to use StrongPool PM2
	- Azure Container App

- use ElasticSearch ?