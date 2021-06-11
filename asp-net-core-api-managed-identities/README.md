# Project setup

1. Download the source code from this folder
2. Open `appsettings.json` file
3. Update `VaultUri` to match the name of the Key Vault instance created in the Azure portal
4. Right-click on the project in the Visual Studio and select *Manage User Secrets*
5. Add sample secret value like:

```json
{
  "APP-SECRET": "Local value of the secret"
}
```

This code sample was explained in the article [here the link to the Auth0 blog...]