# Docker and Microsoft Azure Kubernetes Service 4 .NET Developers


<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker0.png"/>
</p>
&nbsp;

###### First, install Azure Command Line Interface:
https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest

&nbsp;

###### Files for Kubernetes configuration are located in the K8S folder in this repository.

&nbsp;

## CREATE AZURE KUBERNETES SERVICE CLUSTER
&nbsp;

1. Sign in to [Microsoft Azure Portal](https://portal.azure.com) and create new resource group dedicated for AKS:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker1.png"/>
</p>

2. Inside newly created resource group click "Add" button:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker2.png"/>
</p>

3. Search for "AKS":

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker3.png"/>
</p>

4. Click "Create" button:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker4.png"/>
</p>

5. In the first tab called "Basics" you have to provide information about Azure subscription, resource group (created above), Kubernetes cluster name, region, Kubernetes version, DNS prefix, node size and node count like presented below. Then click "Next..." button:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker5.png"/>
</p>

6. In the "Authentication" tab leave "default service principal" selection and set RBAC to "NO". Click "Next..." button: 

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker6.png"/>
</p>

7. In the "Networking" tab set "HTTP application routing" to "NO" and set "Network configuration" to "Basic". Click "Next..." button:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker30.png"/>
</p>

8. In the "Monitoring" tab enable containers monitoring. Click "Next..." button:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker7.png"/>
</p>

9. Leave "Tags" tab with no tags:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker8.png"/>
</p>

10. Last step is validation and acceptance of the configuration. Once ASK cluster is created you should see it in the Azure portal:

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

&nbsp;
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

&nbsp;
## AZURE CONTAINER REGISTRY (ACR) CONFIGURATION – STEP BY STEP

1. Login with Azure Command Line Interface:
```
az login --tenant [tenant name]
```
Open browser and paste the code displayed on the console.

2. Login to the ACR with below command:
```
az acr login --name [acr name]
```
3. Tag Docker image with below command:
```
docker tag [image id] [acr name]. azurecr.io/[image name]:[image tag]
```
Example:
```
docker tag 3d233184a5d7 devislandacr.azurecr.io/samples/aspondockersampleweb
```
4. Push Docker image to the ACR with below command:
```
docker push [ACR NAME]. azurecr.io/samples/samplewebapi:dev
```
Example:
```
docker push devislandacr.azurecr.io/samples/aspondockersampleweb:latest
```
You can find more information in the official Microsoft documentation:

https://docs.microsoft.com/en-us/azure/container-registry/container-registry-get-started-azure-cli


5. You can verify whether Docker image was pushed to the ACR in the Azure portal:

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker28.png"/>
</p>

&nbsp;
## AZURE KUBERNETES SERVICE (AKS) CONFIGURATION – STEP BY STEP

1. Login with Azure Command Line Interface:
```
az login --tenant [tenant name]
```
Open browser and paste the code displayed on the console.

2. Create public static IP address:
```
az network public-ip create --resource-group [MC…] --name [name of created address] --allocation-method static
```
See the result in the console and in the Azure portal:
```
168.63.5.219
```
<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker12.png"/>
</p>

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker13.png"/>
</p>

3. Create new namespace: dev-island using command:
```
kubectl apply -f aks-namespace.yaml
```
4. Download Helm and install NGINX ingress with it:

https://github.com/helm/helm/releases

Download Helm from the Binary Releases. 

5. Login to Azure subscription through Azure Command Line:
```
az login --tenant [tenant name]
```
Get the credentials for the AKS cluster by running the following command:
az aks get-credentials --resource-group [aks resource group] --name [aks cluster name]

Install Tiller inside the AKS cluster using below command:
```
helm init
```
<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker14.png"/>
</p>

Install nginx ingress controller using helm chart:
```
helm install stable/nginx-ingress --namespace kube-system --name [nginx name] --set rbac.create=false,controller.service.loadBalancerIP=" [generated static IP address] ",controller.service.externalTrafficPolicy="Local"
```
Verify installation using command:
```
helm ls --all dev-island-nginx
```
<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker15.png"/>
</p>

Apply ingress configuration:
```
kubectl apply -f ingress.yaml  (namespace already included in the file)
```

6. Open ACR blade in the Microsoft Azure portal. In the Access keys section you will find registry name, username and password.

Login to ACR using below command in the Azure CLI:
```
az acr login --name [registry name]
```
Create secret using below command:
```
kubectl create secret docker-registry [acr connection name] --docker-server=[acr_name].azurecr.io --docker-username=[from the azure portal] --docker-password=[from the azure portal] --docker-email=docker-email=<<e-mail address>> --namespace [namespace name]
```
Registry name, username and password can be found in Azure portal in ACR blade and Access key section.

Once secret is created service account file should be updated.

Get file using below command:
```
kubectl get serviceaccounts default --namespace [namespace name] -o yaml > serviceaccount.yaml
```
Open file and add imagePullSecrets section at the bottom of the file with the name of connection:
```
imagePullSecrets:
- name: [acr connection name]
```

Apply changes with below command:
```
kubectl replace serviceaccount default -f serviceaccount.yaml --namespace [namespace name]
```
6. Apply Ingress configuration in the specific namespace (dev-island in this case):
```
kubectl apply -f ingress.yaml (namespace already included in the file)
```
7. Apply sample-webapi-service:
```
kubectl apply -f sample-webapi-service.yaml  (namespace already included in the file)
```
8. Apply sample-webapi-deployment:
```
kubectl apply -f sample-webapi-deployment.yaml  (namespace already included in the file)
```

&nbsp;
## TEST

Open browser and type the IP address together with the path to the selected microservice. In this case:

http://168.63.5.219/testpath/api/values

<p align="center">
  <img src="https://github.com/Daniel-Krzyczkowski/MicrosoftAzure/blob/master/AksAndDocker4NetDevs/images/aks_docker29.png"/>
</p>

You can find more information in the official Microsoft documentation:

https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-portal

Code sample of ASP .NET Core app is available in this repository.
