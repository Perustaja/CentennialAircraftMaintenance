# CentennialAircraftMaintenance
MVC application with a few side services

# Requirements
This project requires configuration variables or usersecrets for:
- SendGridKey & SendGridUser used in EmailSender
- FspUser & FspPass required for logging in to FSP, used by FspScraper

# Development environment
- All databases are currently SQLite and will use SQL server probably through azure
- User secrets are temporary as well for development
- Static files are currently just stored in the repo, but will be hooked up with blob storage
