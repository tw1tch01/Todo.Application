Endpoints for a simple Todo application using ASP.NET Core. This project is merely a presentation layer implementation of the `Todo.Core` system.

## Description

This API is documented in OpenAPI format and is an implementation of the `Todo.Core` system found here.

## Data Types

### Date Times

All dates that are sent to the API must be in IS0 8601  format (YYYY-MM-DDTHH:MM:SS+-hh:mm). e.g. 2020-08-03T14:15:44-05:00. Dates and times that the API replies with will use this format.

`Todo.Core` systems operate on Coordinated Universal Time (UTC) therefore all dates are converted to this timezone.

*Important*: Dates and times that are sent without timezone information (i.e. without 'Z' or '±hh:mm') are assumed to be in Coordinated Universal Time (UTC).

For brevity, it is recommended that dates and times are sent without timezone information so that the `Todo.Core` systems automatically treats them in the preferred local time.

### Identifiers

All records in the `Todo.Core` use GUIDS (uuid) as primary keys.

### Enumerations

All enumerations are sent/received as string representations of its corresponding value.

## Cross-Origin Resource Sharing

This API features Cross-Origin Resource Sharing (CORS) implemented in compliance with W3C spec. And that allows cross-domain communication from the browser. All responses have a wildcard same-origin which makes them completely public and accessible to everyone, including any code on any site.

## Authentication

There is no authentication necessary for this implementation of the `Todo.Core` system.
