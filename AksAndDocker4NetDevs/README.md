# Docker and Microsoft Azure Kubernetes Service 4 .NET Developers


<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker0.png"/>
</p>


First, install Azure Command Line Interface:
https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest


## CREATE AZURE KUBERNETES SERVICE CLUSTER

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker1.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker2.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker3.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker4.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker5.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker6.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker7.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker8.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker9.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker10.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker11.png"/>
</p>


## CREATE AZURE CONTAINER REGISTRY

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker16.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker17.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker18.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker19.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker20.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker21.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker22.png"/>
</p>


## ASP .NET CORE WEB APP CONFIGURATION – STEP BY STEP

1. Open Visual Studio 2017 and create new Web App project (ASP .NET Core):

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker23.png"/>
</p>

2. Select “Enable Docker Support” checkbox and click “OK”:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker24.png"/>
</p>

3. You should see newly created project with Docker support:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker25.png"/>
</p>

4. Set “docker-compose” as startup project:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker26.png"/>
</p>

5. Run application in the local Docker container by clicking “Docker” button in the Visual Studio:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker27.png"/>
</p>

That’s it, now you know how to run ASP .NET Core web app using Docker container. 


## AZURE CONTAINER REGISTRY (ACR) CONFIGURATION – STEP BY STEP
