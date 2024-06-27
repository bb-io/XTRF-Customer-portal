# Blackbird.io XTRF Customer Portal

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet, and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

The Client Portal allows your clients to submit a job or quote request 24/7, rather than waiting for your office hours. After all, when it comes to quoting, speed is the name of the game. You can choose to respond with an automated quote or keep the quoting process manual.

## Before Setting Up

Before you can connect, you need to make sure that:

- You have an XTRF account.
- You have an XTRF client, based on which you can successfully log in to the XTRF Customer Portal.
- You have a `Username` and `Password` for the XTRF client.
- You have the `Host` for the XTRF Customer Portal. E.g., `organization.xtrf.eu`, this is the domain you use to log in to the XTRF Customer Portal.

Typically, the URL for the XTRF Customer Portal is `https://[your-xtrf-host.com]/customers`. Note that for the XTRF Customer Portal you are using the same host as for the common XTRF system; the only difference is the path.

## Setting Up the XTRF Client for the XTRF Customer Portal

1. Create a new client in XTRF or use an existing one.
2. Go to the 'Main Data' tab and select the 'System Accounts' tab.
3. Modify 'Access Rights' to set it to `Active`.
4. Mark the 'Client Portal Access Allowed' checkbox.
5. Set the password for the client by clicking on the 'Send Welcome E-mail' or 'Send Password Reset E-mail' button. This will send an email to the client with steps to set the password.
6. You can also use 'Sign in as this Partner' to log in and validate the client's access to the XTRF Customer Portal.

![setup](/image/README/setup.png)

## Connecting

1. Navigate to Apps, and identify the **XTRF Customer Portal** app. You can use search to find it.
2. Click _Add Connection_.
3. Name your connection for future reference, e.g., 'My Organization Connection'.
4. Fill in the `Host`, `Username`, and `Password` fields.
5. Click _Connect_.
6. Make sure that the connection is successfully established.

![connection](/image/README/connection.png)

## Actions

### Invoice

- **Search invoices** - Search invoices based on the provided criteria.
- **Get invoice** - Get a specific invoice by ID.
- **Download invoice** - Download a specific invoice as a PDF.

### Project

- **Search projects** - Search projects based on search criteria.
- **Get project** - Get a project based on project ID.
- **Create project** - Create a new project.
- **Download project files** - Download project translation files.

### Quote

- **Search quotes** - Search quotes based on the provided criteria.
- **Get quote** - Get a specific quote by Quote ID.
- **Create quote** - Create a new quote based on the provided data.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
