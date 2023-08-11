#WINDOWS SERVICE TO START A DEV SERVER FOR SYMFONY APPLICATIONS

Register this service to always autostart:

WHAT this service simply does is run   ```symfony server:start -d```

###Requirements:

1. Symfony CLI
2. PHP 
3. Ensure to give required permission to run the service:


###WHY?
-What this means is that your dev server will start on windows startup. You can change this behavoir by going to services and changing the startup mode
- Bytheway you can always type ```symfony server:start -d``` 
- You only need this service when you need your dev server to be running in most cases.

####Configs

Change cofigs in appsettings.json

