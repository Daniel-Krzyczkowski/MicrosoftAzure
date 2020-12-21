# Azure API Management - jump start


<p align="center">
  <img src="/azure-api-management/Assets/IntroImage.PNG"/>
</p>
&nbsp;

#### You can find my introduction to the Azure API Management in this repository:
[Azure API Management - jump start](https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/azure-api-management/AzureApiManagementJumpStart.pdf)

#### Additional information:

###### First, create Azure API Management service:
https://docs.microsoft.com/en-us/azure/api-management/get-started-create-service-instance

## Overview of APIM in the Azure portal 

Once Azure API Management service is ready you should receive e-mail notification.

You can find it in the Azure Portal:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM0.PNG"/>
</p>

Please note that there are two URL addresses on the right side:

1. Developer portal URL - portal where developers can subscribe to the APIs managed by Azure API Management
2. Gateway URL - this is just a main address and entry point to all APIs handled by Azure API Management

We will start from the import of sample API.


## Import Swagger Petstore sample API

We will use sample API called Swagget Petsotre available here: https://petstore.swagger.io/ together with swagger definition: https://petstore.swagger.io/v2/swagger.json

1. Click APIs tab:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM25.PNG"/>
</p>

2. Select "OpenAPI":

<p align="center">
  <img src="/azure-api-management/Assets/ApiM13.PNG"/>
</p>

<p align="center">
  <img src="/azure-api-management/Assets/ApiM14.PNG"/>
</p>

3. Provide below details including OpenAPI specification:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM14.PNG"/>
</p>


## Create new API Management Product

*APIM product - a product contains one or more APIs as well as a usage quota and the terms of use. You can include a number of APIs and offer them to developers through the Developer portal. For more information, see Create and publish a product.*

1. Click "Products" section and click "Add" button:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM8.PNG"/>
</p>

2. Provide details for the new Product. In this case we will create Product called "Trial". Under this Product our Petstore API will be available so include it in the "APIs" section:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM9.PNG"/>
</p>

Done! Now our "Trial" Product is published and we can access Petstore API - let's see how to do it below.

## Add Petstore API to the Trial Product

Now we can add Petstore API to the Trial Product so it is available for the developers:

1. Select "Swagger Petstore" API:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM15.PNG"/>
</p>

2. Go to the "Settings" tab and make sure that the configuration looks like below:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM16.PNG"/>
</p>

That's it. Now Petstore API is available under Trial subscription. There is one more step - we have to enable access for Developers:

1. Open Trial Product and select "Access control" tab:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM27.PNG"/>
</p>

2. Klick "Add group":

<p align="center">
  <img src="/azure-api-management/Assets/ApiM28.PNG"/>
</p>

3. Select "Developers" group:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM29.PNG"/>
</p>

Now you should be able to see Trial product in the Developer portal once registered.


## Subscribe to the "Trial" product in the developer portal

At the beginning I mentioned about "Developer portal URL". We will use it now to register as developer and get subscription key to acccess Petstore API.

1. First of you have to sign up using e-mail (it is good to open Developer Portal in the separate tab to make sure you are not signed in as Administrator). Once its verified you should be able to access Developer portal:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM1.PNG"/>
</p>

<p align="center">
  <img src="/azure-api-management/Assets/ApiM12.PNG"/>
</p>

2. After successful registration got to the "Products" tab and select "Trial":

<p align="center">
  <img src="/azure-api-management/Assets/ApiM2.PNG"/>
</p>

3. Click "Add subscription" to get subscription keys:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM3.PNG"/>
</p>

4. You should receive e-mail confirmation. Now you can access Petstore API using one of the assigned subscription keys:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM26.PNG"/>
</p>

5. Open "Swagger Petstore" API and click "Try it":

<p align="center">
  <img src="/azure-api-management/Assets/ApiM4.PNG"/>
</p>

6. Please note that subscription key is used when sending the new request:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM5.PNG"/>
</p>

<p align="center">
  <img src="/azure-api-management/Assets/ApiM6.PNG"/>
</p>

<p align="center">
  <img src="/azure-api-management/Assets/ApiM7.PNG"/>
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
  <img src="/azure-api-management/Assets/ApiM19.PNG"/>
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
  <img src="/azure-api-management/Assets/ApiM20.PNG"/>
</p>

You can test this endpoint using Postman (you can modify the collection I provided in this repo in the "Postman" folder).


#### Policy for the API

Now we will apply policy for the whole Petstore API. This one is related with JWT token validation from the Azure AD B2C. I created Azure AD B2C test tenant so you can use it during the workshop:

[Sign in/Sign up with AD B2C](https://servlessmeetup.b2clogin.com/servlessmeetup.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1_SignUpOrSignIn&client_id=b8b32cdf-bdd9-4902-bf49-47672fc7b0aa&nonce=defaultNonce&redirect_uri=http%3A%2F%2Flocalhost&scope=openid&response_type=id_token&prompt=login)

Once you register you should see the JWT token in the broswer - copy it because we will use it later.

1. Select "Swagger Petstore" API and then select "Inbound processing":

<p align="center">
  <img src="/azure-api-management/Assets/ApiM15.PNG"/>
</p>

<p align="center">
  <img src="/azure-api-management/Assets/ApiM18.png"/>
</p>

2. To configure Azure AD B2C JWT token validation policy it is required to provide two details:
a. openid-config url
b. audience
c. required-claims

This is the source code of the policy to apply:

```
<policies>
    <inbound>
        <base />
        <validate-jwt header-name="Authorization" failed-validation-httpcode="401" failed-validation-error-message="Unauthorized. Access token is missing or invalid.">
            <openid-config url="https://servlessmeetup.b2clogin.com/servlessmeetup.onmicrosoft.com/v2.0/.well-known/openid-configuration?p=B2C_1_SignUpOrSignIn" />
            <audiences>
                <audience>b8b32cdf-bdd9-4902-bf49-47672fc7b0aa</audience>
            </audiences>
            <required-claims>
                <claim name="aud" match="all">
                    <value>b8b32cdf-bdd9-4902-bf49-47672fc7b0aa</value>
                </claim>
            </required-claims>
        </validate-jwt>
    </inbound>
    <backend>
        <base />
    </backend>
    <outbound>
        <base />
    </outbound>
    <on-error>
        <base />
    </on-error>
</policies>
```

3. Now in the Postman try to call below endpoint without providing JWT token obtained from the AD B2C:

https://servless-meetup-apim.azure-api.net/pets/store/inventory

You should see unauthorized reponse. Then try to call with the JTW token again.


#### Policy for the Product

Last policy will be applied to the whole "Trial" product. This one will reduce the number of possible requests to the Petstore API in the specific time period - "Quota Limit Policy".

1. To apply this policy go to the "Products" tab, select "Trial" and then "Policies":

<p align="center">
  <img src="/azure-api-management/Assets/ApiM10.PNG"/>
</p>

2. Paste below policy code:

```
<policies>
    <inbound>
        <base />
 <quota calls="5" bandwidth="40000" renewal-period="3600" />
    </inbound>
    <backend>
        <base />
    </backend>
    <outbound>
        <base />
    </outbound>
    <on-error>
        <base />
    </on-error>
</policies>
```

Now up to 5 requests are available in the 60 minutes. If you try to call 6th time you will be notified that quota limit was exceeded. You will be also notified by e-mail:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM11.PNG"/>
</p>


## Analytics

One of the great features of the Azure API Management is analytics related with usage of specific API - including information about failed requests or number of users. You can review this data using "Analytics" tab:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM21.PNG"/>
</p>

Swith between different tabs:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM22.PNG"/>
</p>

## Notification templates

It is also possible to customize e-mail templates sent from the Azure API Management - for instance "welcome" messages using "Notification templates" tab:

<p align="center">
  <img src="/azure-api-management/Assets/ApiM23.PNG"/>
</p>

<p align="center">
  <img src="/azure-api-management/Assets/ApiM24.PNG"/>
</p>

#### Summary

I tried to show you some nice and interesting features of Azure API Management service. Of course there are more of them. I encourage you to check [official documentation](https://docs.microsoft.com/en-us/azure/api-management/) to learn more.
