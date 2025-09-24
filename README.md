# FTLSupporter
A teljesítendő szállítási feladatok beosztásának felhasználói támogatása. Ehhez a program minden egyes átadott szállítási feladathoz a megkapott járműadatok alapján egy-egy  teljesítés-listát ad vissza. A teljesítés-listákban a költség alapján egy sorrend van meghatározva, evvel is segítve a megfelelő választást a kliens oldalon.

## Project board
https://github.com/orgs/SkillTech-Organization/projects/8

## Repositories
https://github.com/SkillTech-Organization/FTLSupporter.git

## Documentation
https://drive.google.com/drive/folders/18RYAFEOxiDgCHope-qj1OKLJQdwY715f?usp=sharing

## Branching strategy
- branch for development: *develop*
- branch for customer test: *stage*
- branch for live environment: *main*

## Environments and access
- development: *https://ftlsupporterapi.azurewebsites.net/*
- customer test:
- live: *https://prvcloudwebapitest.azurewebsites.net/*

--- 

# From the Knowledge base

## Definitions Per Environment

### Develop
| Item              | Description                                             |
| ----------------- | ------------------------------------------------------- |
| Account           | bbxsoftware@gmail.com                                   |
| Management Group  | e608d7e2-ce31-4d3b-9686-ba94873e8e3e                    |
| Subscription      | 1ef900f3-4e6b-4c87-b78f-67245f6130b4                    |
| Resource Group    | ftlsupporterapi_group                                   |
| Tenant ID         | e608d7e2-ce31-4d3b-9686-ba94873e8e3e                    |



### Prod

| Item              | Description                                             |
| ----------------- | ------------------------------------------------------- |
| Account           | agyorgyi01@gmail.com, szdezso@gmail.com                 |
| Management Group  | 8875ae16-2b24-4357-95a4-62e9df84fe06                    |
| Subscription      | 702fab27-7b08-4bcd-a29e-4c15e902dca2                    |
| Resource Group    | FTLSupporter                                            |
| Tenant ID         | 8875ae16-2b24-4357-95a4-62e9df84fe06                    |

### Resource Items

*BEFEJEZETLEN*

| Resource Type       | Resource Name               | Description                    | Tags                     | note      |
| ------------------- | --------------------------- | ------------------------------ | ------------------------ | --------- |
| App Service         | ftlsupporterapi             | Application service            | -                        |           |
| App Service Plan    | ASP-DefaultResourceGroupDEWC-8494| Application service plan  | -                        |           |
| Application Insights| ftlsupporterapi             | trace data                     | -                        |nincs bekötve|
| Application Insights| ftlsupporter_insights       | trace data                     | -                        |nincs bekötve|
| Smart detector alert rule| Failure Anomalies - ftlsupporterapi| alerter            | -                        |ftlsupporterapi app insight-hez van kötve|
| Smart detector alert rule| Failure Anomalies - ftlsupporter_insights| alerter      | -                        |ftlsupporter_insights app insight-hez van kötve|



#### Prod
ideiglenes megoldás


| Resource Type       | Resource Name               | Description                    | Tags                     | note      |
| ------------------- | --------------------------- | ------------------------------ | ------------------------ | --------- |
| App Service         | ftlsupporterwebapidev       | Application service            | -                        |           |
| App Service Plan    | ftlsupporterwebapidev       | Application service plan       | -                        |           |
| Application Insights| ftlsupporterwebapidev       | trace data                     | -                        |           |
| Runbook             | ftlsupporterwebapidev_start_automation_job|App service start | -                        |           |
| Runbook             | ftlsupporterwebapidev_stop_automation_job | App service stop | -                        |           |
| Smart detector alert rule| Failure Anomalies - ftlsupporterwebapidev| alerter      | -                        |           |
| Template spec       | ftlsupporterwebapi           | ftlsupporter web api creator  | -                        |           |
| Storage account     | mapstrg.map                 | térkép json-ok                 | -                        |           |
| Storage account     | ftlsupporterdev.$logs       | App ingsight logok             | -                        |           |



---

## planned resource and components
