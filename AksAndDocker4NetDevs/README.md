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

1. Login with Azure Command Line Interface:
 az login --tenant [tenant name]
Open browser and paste the code displayed on the console.

2. Login to the ACR with below command:
 az acr login --name [acr name]

3. Tag Docker image with below command:
docker tag [image id] [acr name]. azurecr.io/[image name]:[image tag]

Example:
docker tag 3d233184a5d7 devislandacr.azurecr.io/samples/aspondockersampleweb

4. Push Docker image to the ACR with below command:
docker push [ACR NAME]. azurecr.io/samples/samplewebapi:dev

Example:
docker push devislandacr.azurecr.io/samples/aspondockersampleweb:latest

You can find more information in the official Microsoft documentation:
https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-azure-cli


5. You can verify whether Docker image was pushed to the ACR in the Azure portal:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker28.png"/>
</p>


## AZURE KUBERNETES SERVICE (AKS) CONFIGURATION – STEP BY STEP
