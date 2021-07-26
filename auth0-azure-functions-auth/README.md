
# Microsoft Azure Function and Xamarin Forms - secured with Auth0

This repository contains sample to help developers implement Auth0 authentication in Microsoft Azure Function and Xamarin Forms application.

<p align="center">
<img src="/auth0-azure-functions-auth/images/XF3.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
<br/>
<img src="/auth0-azure-functions-auth/images/AF3.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>


In this Proof of Concept (PoC) I secured access to Microsoft Azure Function access with Auth0. In Xamarin Forms application user has to authenticate first, then access Azure Function with access token retrieved from Auth0 API application.
Please follow below instructions to configure solution:

## Auth0 API application configuration

API application and client (mobile) application has to be created in [Auth0 Portal](https://auth0.com) to enable Auth0 authentication. Please follow below instructions:

### Create API application:

<p align="center">
<img src="/auth0-azure-functions-auth/images/Auth1.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/Auth2.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/Auth3.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

### Create mobile application client (Xamarin):

<p align="center">
<img src="/auth0-azure-functions-auth/images/Auth4.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/Auth5.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/Auth6.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/Auth7.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>




## Microsoft Azure Function configuration:

Sign in to [Microsoft Azure Portal](https://portal.azure.com) and follow below instructions:


<p align="center">
<img src="/auth0-azure-functions-auth/images/AF1.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF2.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF3.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF3.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF4.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF5.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>


<p align="center">
<img src="/auth0-azure-functions-auth/images/AF6.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>


<p align="center">
<img src="/auth0-azure-functions-auth/images/AF7.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF8.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF9.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF10.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>


<p align="center">
<img src="/auth0-azure-functions-auth/images/AF11.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF12.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>


Replace below initial code for function with source code [run.csx file](/auth0-azure-functions-auth/run.csx)

Now upload [project.json file](/auth0-azure-functions-auth/project.json). This file is required to retrieve Microsoft.IdentityModel.Protocols.OpenIdConnect NuGet package:

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF13.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF14.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

Now click Save and verify if compilation succeeded:

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF15.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/AF16.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>



## Xamarin Forms application configuration:

Download Xamarin Forms application [source code](https://github.com/Daniel-Krzyczkowski/Xamarin/tree/master/XamarinForms/Auth0XamarinForms)

[Auth0 OIDC Client for .NET](https://github.com/auth0/auth0-oidc-client-net) is used to secure application with Auth0.

Update two [configuration files](https://github.com/Daniel-Krzyczkowski/Xamarin/tree/master/XamarinForms/Auth0XamarinForms/Auth0XamarinForms/Auth0XamarinForms/Config)
in the project:

* AuthenticationConfig
* Azure Config

Build project and launch the application (UWP, Android or iOS). Once you create account (or sign in), greetings from Azure Function should appear:


<p align="center">
<img src="/auth0-azure-functions-auth/images/XF1.png" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/XF2.png" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/XF3.PNG" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>

<p align="center">
<img src="/auth0-azure-functions-auth/images/XF4.png" alt="Auth0 secured Microsoft Azure Function with Xamarin Forms"/>
</p>




