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

2. Provide details for the new Product. In this case we will create Product called "Trial". Under this Product our Petstore API will be available so include it in the "APIs" section:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM9.PNG"/>
</p>

Done! Now our "Trial" Product is published and we can access Petstore API - let's see how to do it below.

## Add Petstore API to the Trial Product

Now we can add Petstore API to the Trial Product so it is available for the developers:

1. Select "Swagger Petstore" API:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM15.PNG"/>
</p>

2. Go to the "Settings" tab and make sure that the configuration looks like below:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM16.PNG"/>
</p>

That's it. Now Petstore API is available under Trial subscription.


## Subscribe to the "Trial" product in the developer portal

At the beginning I mentioned about "Developer portal URL". We will use it now to register as developer and get subscription key to acccess Petstore API.

1. First of you have to sign up using e-mail. Once its verified you should be able to access Developer portal:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM1.PNG"/>
</p>

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM12.PNG"/>
</p>

2. After successful registration got to the "Products" tab and select "Trial":

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM2.PNG"/>
</p>

3. Click "Add subscription" to get subscription keys:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM3.PNG"/>
</p>

4. You should receive e-mail confirmation. Now you can access Petstore API using one of the assigned subscription keys:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM26.PNG"/>
</p>

5. Open "Swagger Petstore" API and click "Try it":

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM4.PNG"/>
</p>

6. Please note that subscription key is used when sending the new request:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM5.PNG"/>
</p>

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM6.PNG"/>
</p>

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM7.PNG"/>
</p>

## Apply API Gatway Policies

*In Azure API Management (APIM), policies are a powerful capability of the system that allow the publisher to change the behavior of the API through configuration. Policies are a collection of Statements that are executed sequentially on the request or response of an API.*

[It is worth to read more about policies and scopes](https://docs.microsoft.com/en-us/azure/api-management/set-edit-policies#configure-scope)

#### Policy for the single endpoint

First policy will be applied to the single endpoint of our API:
https://servless-meetup-apim.azure-api.net/pets/pet/findByStatus?status=available

We will enable caching responses for 20 seconds:

1. Select "Find pets by status" endpoint in the "Design" tab and select "Inbound processing":

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM19.PNG"/>
</p>

2. Modify source code of the policy to look like below (source code of this policy is also included in the "Policies" folder in this repo):

```
<policies>
    <inbound>
        <base />
        <cache-lookup vary-by-developer="false" vary-by-developer-groups="false" downstream-caching-type="none">
            <vary-by-header>Accept</vary-by-header>
            <vary-by-header>Accept-Charset</vary-by-header>
            <vary-by-header>Authorization</vary-by-header>
        </cache-lookup>
    </inbound>
    <backend>
        <base />
    </backend>
    <outbound>
        <base />
        <cache-store duration="20" />
    </outbound>
    <on-error>
        <base />
    </on-error>
</policies>
```
3. Now you should see applied caching policy:

<p align="center">
  <img src="/AzureApiManagament/Assets/ApiM20.PNG"/>
</p>

You can test this endpoint using Postman (you can modify the collection I provided in this repo in the "Postman" folder).


#### Policy for the API

Now we will apply policy for the whole Petstore API. This one is related with JWT token validation from the Azure AD B2C. I created Azure AD B2C test tenant so you can use it during the workshop:

[Sign in/Sign up with AD B2C](https://servlessmeetup.b2clogin.com/servlessmeetup.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1_SignUpOrSignIn&client_id=b8b32cdf-bdd9-4902-bf49-47672fc7b0aa&nonce=defaultNonce&redirect_uri=http%3A%2F%2Flocalhost&scope=openid&response_type=id_token&prompt=login)

#### Policy for the Product

Aaa
