# Azure API Management - jump start


<p align="center">
  <img src="/AzureApiManagament/Assets/IntroImage.PNG"/>
</p>
&nbsp;

#### You can find my introduction to the Azure API Management in this repository:
[Azure API Management - jump start](https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AzureApiManagament/AzureApiManagementJumpStart.pdf)

#### Additional information:

###### First, create Azure API Management service:
https://docs.microsoft.com/en-us/azure/api-management/get-started-create-service-instance

## Overview of APIM in the Azure portal 

Once Azure API Management service is ready you should receive e-mail notification.

You can find it in the Azure Portal:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM0.PNG"/>
</p>

Please note that there are two URL addresses on the right side:

1. Developer portal URL - portal where developers can subscribe to the APIs managed by Azure API Management
2. Gateway URL - this is just a main address and entry point to all APIs handled by Azure API Management

We will start from the import of sample API.


## Import Swagger Petstore sample API

We will use sample API called Swagget Petsotre available here: https://petstore.swagger.io/ 

1. Click APIs tab:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM25.PNG"/>
</p>

2. Select "OpenAPI":

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM13.PNG"/>
</p>

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM14.PNG"/>
</p>

3. Provide below details including OpenAPI specification:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM14.PNG"/>
</p>


## Create new API Management Product

*APIM product - a product contains one or more APIs as well as a usage quota and the terms of use. You can include a number of APIs and offer them to developers through the Developer portal. For more information, see Create and publish a product.*

1. Click "Products" section and click "Add" button:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM8.PNG"/>
</p>

2. Provide details for the new Product. In this case we will create Product called "Trial":

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM9.PNG"/>
</p>



